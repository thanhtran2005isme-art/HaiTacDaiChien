using UnityEngine;

namespace HaiTac.OfflineViewer
{
    /// <summary>
    /// Serialized pointer evidence. This is NOT an original Unity component
    /// or proof that a player-build GameObject came from an editable Prefab.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class OriginalSerializedEvidence : MonoBehaviour
    {
        public string serializedFile;
        public int gameObjectPathId;
        public int rectTransformPathId;
        public int[] originalComponentPathIds;
        [TextArea] public string originalComponentTypes;
        public bool sourceActiveKnown;
        public bool parentWasOutsideCandidate;
        [TextArea] public string missingComponentDetails;
        [TextArea] public string limitations =
            "Built from real local serialized GameObject/RectTransform pointers. " +
            "Original editable Prefab/Scene and managed MonoBehaviour fields " +
            "are NOT proven. No Sprite/Image/Canvas, runtime behaviors or " +
            "animation were substituted with invented defaults.";
    }
}
