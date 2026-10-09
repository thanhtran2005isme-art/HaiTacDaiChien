using UnityEngine;

namespace HaiTac.OfflineViewer
{
    // Root-only metadata for local reconstruction, not original game code.
    [DisallowMultipleComponent]
    public sealed class ReconstructionEvidence : MonoBehaviour
    {
        [TextArea] public string serializedSource;
        [TextArea] public string limitations;
    }
}
