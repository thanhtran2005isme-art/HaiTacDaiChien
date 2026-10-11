using UnityEngine;

namespace HaiTac.OfflineViewer
{
    /// <summary>
    /// Metadata for a disposable, visual-preview-only Canvas root. No field
    /// on this component is claimed to originate in a game's editable Prefab.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class VerifiedVisualPreviewEvidence : MonoBehaviour
    {
        public string sourceSceneId;
        public string originalSourceGraphSha256;
        public string verifiedFieldPlanSha256;
        public string exactSpritePlanSha256;
        public int sourceBoundSprites;
        public int inheritedVerifiedComponents;
        public int inheritedVerifiedFields;
        public int excludedSingleBackendFields;
        public Vector3 originalRootScale;
        public Vector3 normalizedPreviewRootScale;
        public Vector2 provisionalPreviewReferenceResolution;
        public bool previewCanvasNotClaimedAsOriginal = true;
        public bool previewCameraNotClaimedAsOriginal = true;
        [TextArea] public string limitations =
            "The Camera, outer Canvas, 1600x900 resolution and root-scale " +
            "normalization are preview-only. Original GameObject/Component " +
            "PathIDs, Sprite source links and the 3C field proof are retained " +
            "in the child study Prefab. Original runtime layout, Spine, " +
            "animation, gameplay and 651 single-backend LayoutGroup values " +
            "remain unverified. This is not an original editable Prefab.";
    }
}
