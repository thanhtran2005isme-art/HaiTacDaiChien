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
        [TextArea] public string note =
            "Serialized pointer candidates are not verified field bindings. " +
            "No skeleton, skin or animation is assigned automatically.";
    }
}
