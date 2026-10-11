using UnityEngine;
using UnityEngine.SceneManagement;

// NEW_PROJECT_DESIGN: keyboard-only return path for offline Unity Play Mode.
// No UI elements or original callbacks are modified.
[DisallowMultipleComponent]
public sealed class HomeSceneReturnHotkey : MonoBehaviour
{
    private string homeScenePath = "Assets/Scenes/HomeScene.unity";

    public void SetHomeScene(string path)
    {
        homeScenePath = path;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        // Close a modal popup before attempting to leave the scene.
        if (HomeSceneNavigationBridge.CloseOpenPopup())
            return;

        Scene active = SceneManager.GetActiveScene();
        if (active.path == homeScenePath)
            return;

        HomeSceneNavigationBridge.NavigateTo(homeScenePath);
    }
}
