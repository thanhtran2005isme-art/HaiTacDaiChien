using System;
using System.Reflection;
using UnityEngine;

namespace HaiTac.OfflineViewer
{
    // Diagnostic ONLY: measures the actual Spine AnimationState track progression.
    // Does not claim a frame was drawn or a live game's selected skin was restored.
    public sealed class SpinePreviewProbe : MonoBehaviour
    {
        public Component spineGraphic;
        public string expectedAnimation;
        private object animationState;
        private MethodInfo getCurrent;
        private PropertyInfo trackTime;
        private float startTime;
        private float firstTrackTime;
        private bool sampled;
        private bool finished;

        private void Start()
        {
            startTime = Time.realtimeSinceStartup;
            if (spineGraphic == null)
            {
                Fail("no SkeletonGraphic component");
                return;
            }

            try
            {
                Type type = spineGraphic.GetType();
                PropertyInfo stateProperty = type.GetProperty("AnimationState",
                    BindingFlags.Instance | BindingFlags.Public);
                animationState = stateProperty == null ? null :
                    stateProperty.GetValue(spineGraphic, null);
                if (animationState == null)
                {
                    Fail("Spine AnimationState has not initialized");
                    return;
                }
                getCurrent = animationState.GetType().GetMethod("GetCurrent",
                    new[] { typeof(int) });
                if (getCurrent == null)
                    Fail("Spine AnimationState.GetCurrent(0) unavailable");
            }
            catch (Exception exc)
            {
                Fail("runtime inspection: " + exc.GetBaseException().Message);
            }
        }

        private void Update()
        {
            if (finished || animationState == null || getCurrent == null) return;
            try
            {
                var entry = getCurrent.Invoke(animationState, new object[] { 0 });
                if (entry == null)
                {
                    if (Time.realtimeSinceStartup - startTime > 2.5f)
                        Fail("no active track after 2.5 seconds");
                    return;
                }
                if (trackTime == null)
                    trackTime = entry.GetType().GetProperty("TrackTime",
                        BindingFlags.Instance | BindingFlags.Public);
                if (trackTime == null)
                {
                    Fail("Spine TrackEntry.TrackTime unavailable");
                    return;
                }
                float current = Convert.ToSingle(trackTime.GetValue(entry, null));
                if (!sampled)
                {
                    sampled = true;
                    firstTrackTime = current;
                }
                if (current - firstTrackTime > .10f)
                {
                    finished = true;
                    Debug.Log("[HaiTac Spine preview] TRACK_ADVANCING: " +
                        expectedAnimation + "; elapsed track=" + (current - firstTrackTime)
                        .ToString("F2") + "s. Visually inspect Game View for actual rendering.");
                }
                else if (Time.realtimeSinceStartup - startTime > 3f)
                    Fail("Spine track time did not advance within 3 seconds");
            }
            catch (Exception exc)
            {
                Fail(exc.GetBaseException().Message);
            }
        }

        private void Fail(string reason)
        {
            if (finished) return;
            finished = true;
            Debug.LogError("[HaiTac Spine preview] BLOCKED " + expectedAnimation +
                ": " + reason + ". Check runtime version and generated SkeletonDataAsset.");
        }
    }
}
