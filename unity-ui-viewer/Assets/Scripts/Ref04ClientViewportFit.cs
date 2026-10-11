using UnityEngine;
using UnityEngine.UI;

namespace HaiTac.OfflineViewer
{
    /// <summary>
    /// NEW_PROJECT_DESIGN, NOT recovered original XAPK runtime behavior.
    /// Fit the existing 1600x900 source-study composition inside the current
    /// Game View/device without changing original Image/child RectTransforms.
    /// This component is permitted ONLY on a separately generated P6.2 scene.
    /// Missing Spine, runtime Text and Canvas/SafeArea proof remain BLOCKED.
    /// </summary>
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Canvas), typeof(CanvasScaler))]
    public sealed class Ref04ClientViewportFit : MonoBehaviour
    {
        public Vector2 designOnlyReferenceResolution = new Vector2(1600f, 900f);
        public bool newProjectDesignNotOriginalXapk = true;
        [TextArea] public string limitations =
            "NEW_PROJECT_DESIGN. P6.2 screen fit for new Unity client; " +
            "NOT original CanvasScaler/SafeArea formula or original runtime. " +
            "All child source transforms and source Sprite links are unchanged.";

        private CanvasScaler scaler;
        private int observedWidth = -1;
        private int observedHeight = -1;

        private void OnEnable()
        {
            scaler = GetComponent<CanvasScaler>();
            ApplyFit();
        }

        private void OnValidate()
        {
            observedWidth = -1;
            observedHeight = -1;
            ApplyFit();
        }

        private void OnRectTransformDimensionsChange()
        {
            ApplyFit();
        }

        private void LateUpdate()
        {
            if (observedWidth != Screen.width ||
                observedHeight != Screen.height)
                ApplyFit();
        }

        private void ApplyFit()
        {
            if (scaler == null)
                scaler = GetComponent<CanvasScaler>();
            if (scaler == null || Screen.width <= 0 || Screen.height <= 0)
                return;
            if (designOnlyReferenceResolution.x <= 0 ||
                designOnlyReferenceResolution.y <= 0)
                return;

            // The strict source study already uses this provisional resolution.
            // This formula is an explicit NEW CLIENT design choice, not original
            // game runtime behavior. Match the smaller of the two scale ratios
            // so the reference composition does not extend beyond the viewport.
            float designAspect =
                designOnlyReferenceResolution.x / designOnlyReferenceResolution.y;
            float currentAspect = (float)Screen.width / Screen.height;
            float match = currentAspect < designAspect ? 0f : 1f;

            if (scaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            if (scaler.screenMatchMode != CanvasScaler.ScreenMatchMode.MatchWidthOrHeight)
                scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            if (scaler.referenceResolution != designOnlyReferenceResolution)
                scaler.referenceResolution = designOnlyReferenceResolution;
            if (!Mathf.Approximately(scaler.matchWidthOrHeight, match))
                scaler.matchWidthOrHeight = match;

            observedWidth = Screen.width;
            observedHeight = Screen.height;
        }
    }
}
