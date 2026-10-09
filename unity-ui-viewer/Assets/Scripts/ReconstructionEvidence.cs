using UnityEngine;

namespace HaiTac.OfflineViewer
{
    // Metadata annotations only. These are not original game scripts or runtime bindings.
    [DisallowMultipleComponent]
    public sealed class ReconstructionEvidence : MonoBehaviour
    {
        [TextArea] public string serializedSource;
        [TextArea] public string limitations;
    }

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
        public int availableAnimationCount;
        [TextArea] public string knownAnimationNames;
        [TextArea] public string note =
            "Serialized pointer candidates are not verified field bindings. " +
            "No skeleton, skin or animation is assigned automatically.";
    }
    // Serialized evidence of the original uGUI component. Unknown fields never
    // become working LayoutGroups/Mask components by approximation.
    [DisallowMultipleComponent]
    public sealed class UiComponentEvidence : MonoBehaviour
    {
        public string originalClass;
        public string originalComponentId;
        public string evidenceStatus;
        [TextArea] public string serializedFields;
    }
}
