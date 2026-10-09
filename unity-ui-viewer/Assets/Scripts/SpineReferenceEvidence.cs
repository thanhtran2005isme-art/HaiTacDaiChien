using UnityEngine;

namespace HaiTac.OfflineViewer
{
    // Evidence only. A real SkeletonGraphic needs licensed Spine-Unity and
    // verified runtime bindings, not these source asset references.
    [DisallowMultipleComponent]
    public sealed class SpineReferenceEvidence : MonoBehaviour
    {
        public string originalClass;
        public string originalComponentId;
        public string bindingStatus;
        public int possibleSkeletonDataAssets;
        public string contentEvidenceStatus;
        public string candidateSkeletonName;
        public string candidateAtlasName;
        public string candidateSpineVersion;
        public string localPackId;
        public TextAsset sourceSkeletonJson;
        public TextAsset sourceAtlasText;
        public Texture2D[] sourceAtlasTextures;
        public int availableAnimationCount;
        [TextArea] public string knownAnimationNames;
        [TextArea] public string note =
            "Typed pointer candidates are not verified C# field bindings. " +
            "No skin, character, or active animation is assigned automatically.";
    }
}
