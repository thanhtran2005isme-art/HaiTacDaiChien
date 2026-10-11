using UnityEngine;

namespace HaiTac.OfflineViewer
{
    /// <summary>
    /// Evidence attached only to separate, locally generated study-prefab copies.
    /// This MonoBehaviour is NOT an original XAPK component or a gameplay script.
    /// It does not prove the original editable Prefab or its runtime appearance.
    /// </summary>
    public sealed class ManagedUiSourceEvidence : MonoBehaviour
    {
        public string sourceSceneId;
        public string originalClassName;
        public int sourceGameObjectPathId;
        public int sourceRectTransformPathId;
        public int sourceMonoBehaviourPathId;
        public string sourceObjectSha256;
        public string graphSha256;
        public string libil2cppSha256;
        public string metadataSha256;
        public bool exactTwoBackendFieldAgreement;
        public string[] verifiedFieldNames;
        [TextArea] public string limitations =
            "Cross-backend source-verified managed values only. " +
            "Unity-created component dependencies and any untouched fields " +
            "are NOT asserted to equal the original game; " +
            "the editable source Prefab, runtime scripts, Sprite and " +
            "visual layout still require separate verification.";
    }
}
