using System;
using UnityEngine;
using UnityEngine.UI;

namespace HaiTac.OfflineViewer
{
    /// <summary>
    /// NEW_PROJECT_DESIGN visual-only retrofit, never original XAPK runtime.
    /// Uniformly scale the EXISTING original-Sprite Background subtree to
    /// cover the available Game View. Sprite images and source child layouts
    /// are otherwise unmodified. This may CROP the image at wide aspect
    /// ratios, but cannot distort its proportions or invent game assets.
    /// Applies ONLY to the separately editable P6.2 client scene.
    /// </summary>
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public sealed class Ref04ClientBackgroundCover : MonoBehaviour
    {
        public RectTransform sourceBackground;
        public Vector3 baselineBackgroundScale = Vector3.one;
        public bool newProjectDesignNotOriginalXapk = true;
        [TextArea] public string limitations =
            "NEW_PROJECT_DESIGN background cover. Original Sprite pixels, " +
            "HUD child RectTransforms and source geometry are not modified. " +
            "Viewport cover can crop art; it is NOT original runtime Canvas.";

        private int lastWidth = -1;
        private int lastHeight = -1;
        private Vector3 lastAppliedScale;

        public void Initialize(RectTransform originalBackground)
        {
            if (originalBackground == null)
                throw new ArgumentNullException(nameof(originalBackground));
            if (originalBackground == GetComponent<RectTransform>())
                throw new InvalidOperationException(
                    "Background target must not be the Canvas root.");
            sourceBackground = originalBackground;
            baselineBackgroundScale = originalBackground.localScale;
            lastAppliedScale = baselineBackgroundScale;
            lastWidth = -1;
            lastHeight = -1;
            Refresh();
        }

        private void OnEnable()
        {
            lastWidth = -1;
            lastHeight = -1;
        }

        private void LateUpdate()
        {
            if (sourceBackground == null || Screen.width <= 0 || Screen.height <= 0)
                return;
            if (lastWidth != Screen.width || lastHeight != Screen.height)
                Refresh();
        }

        public void Refresh()
        {
            if (sourceBackground == null || Screen.width <= 0 || Screen.height <= 0)
                return;
            // Guard against overriding local editor changes. Only the
            // compositor's exact previous scale, or source baseline, is valid.
            if (sourceBackground.localScale != lastAppliedScale &&
                sourceBackground.localScale != baselineBackgroundScale)
            {
                Debug.LogWarning("[P6.2] Background was edited by another " +
                    "component; cover refused to overwrite local designer data.");
                return;
            }
            sourceBackground.localScale = baselineBackgroundScale;
            Canvas.ForceUpdateCanvases();

            Image best = null;
            float bestArea = 0f;
            var corner = new Vector3[4];
            var images = sourceBackground.GetComponentsInChildren<Image>(true);
            foreach (var item in images)
            {
                if (item == null || !item.gameObject.activeInHierarchy ||
                    !item.enabled || item.sprite == null)
                    continue;
                var rect = item.rectTransform;
                if (rect == null) continue;
                rect.GetWorldCorners(corner);
                float w = Vector3.Distance(corner[0], corner[3]);
                float h = Vector3.Distance(corner[0], corner[1]);
                float area = w * h;
                if (area > bestArea)
                {
                    bestArea = area;
                    best = item;
                }
            }
            if (best == null)
            {
                Debug.LogWarning("[P6.2] Source Background has no active " +
                    "Sprite-backed Image; cannot cover without invented art.");
                sourceBackground.localScale = baselineBackgroundScale;
                return;
            }
            best.rectTransform.GetWorldCorners(corner);
            float width = Vector3.Distance(corner[0], corner[3]);
            float height = Vector3.Distance(corner[0], corner[1]);
            if (width <= .001f || height <= .001f)
            {
                sourceBackground.localScale = baselineBackgroundScale;
                return;
            }

            // Overlay Canvas world corners correspond to display pixels in
            // the existing P6.2 study. Cover (not contain) prevents black
            // sidebars while keeping artwork uniformly scaled.
            float cover = Mathf.Max((float)Screen.width / width,
                                    (float)Screen.height / height);
            if (float.IsNaN(cover) || float.IsInfinity(cover) ||
                cover <= 0f || cover > 16f)
            {
                sourceBackground.localScale = baselineBackgroundScale;
                Debug.LogWarning("[P6.2] Invalid computed Background cover; " +
                    "refused to change any source-linked transform.");
                return;
            }
            sourceBackground.localScale = baselineBackgroundScale * cover;
            lastAppliedScale = sourceBackground.localScale;
            lastWidth = Screen.width;
            lastHeight = Screen.height;
        }
    }
}
