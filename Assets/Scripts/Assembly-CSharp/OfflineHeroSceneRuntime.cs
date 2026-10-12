using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Bind original imported scene references at RUNTIME. No edits to original serialized
// Scenes, prefabs, original OnClick functions or recovered AssetRipper stub classes.
public static class OfflineHeroSceneRuntime
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Install()
    {
        SceneManager.sceneLoaded -= OnLoaded;
        SceneManager.sceneLoaded += OnLoaded;
        OnLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private static void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HeroListNewScene")
        {
            HeroListNewScene original = UnityEngine.Object.FindObjectOfType<HeroListNewScene>();
            if (original != null && original.GetComponent<OfflineHeroListPresenter>() == null)
                original.gameObject.AddComponent<OfflineHeroListPresenter>();
        }
        else if (scene.name == "Home2Scene")
        {
            Home2Scene original = UnityEngine.Object.FindObjectOfType<Home2Scene>();
            if (original != null && original.GetComponent<OfflineHeroHomePresenter>() == null)
                original.gameObject.AddComponent<OfflineHeroHomePresenter>();
        }
    }
}
