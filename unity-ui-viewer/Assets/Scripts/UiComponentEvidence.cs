using UnityEngine;

namespace HaiTac.OfflineViewer
{
    // Multiple original UI components may be present on one GameObject.
    // Record recoverable fields without synthesizing LayoutGroup/Mask behavior.
    public sealed class UiComponentEvidence : MonoBehaviour
    {
        public string originalClass;
        public string originalComponentId;
        public string evidenceStatus;
        [TextArea] public string serializedFields;
    }
}
