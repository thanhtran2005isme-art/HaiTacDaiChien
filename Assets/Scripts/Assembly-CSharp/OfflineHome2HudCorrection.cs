using UnityEngine;
using UnityEngine.UI;

// NEW_PROJECT_DESIGN: adjust only the running Home2 scene against the user
// reference screenshot. The original AssetRipper Scene YAML, callbacks and
// source Sprite assets remain untouched.
public static class OfflineHome2HudCorrection
{
    private const string ActivityName = "HTDC Offline Activity";
    private const string ActivitySpriteResource = "images/home/homebutton/home_button_events";
    private const string ActivitySpriteEditorAsset =
        "Assets/Resources/images/home/homebutton/home_button_events.asset";

    public static void Apply(Home2Scene home)
    {
        if (home == null) return;
        Transform canvas = home.transform.root.Find("Canvas");
        if (canvas == null)
        {
            UnityEngine.Debug.LogWarning("[HTDC Home2 HUD] Original Canvas not found.", home);
            return;
        }

        // Source IDs: PanelHomeTopRight/ButtonTest (GO 10) and ButtonFb
        // (GO 82) reference the SAME Facebook Sprite GUID. Preserve ButtonFb.
        Transform social = canvas.Find("PanelHomeTopRight");
        Transform extraFb = social != null ? social.Find("ButtonTest") : null;
        Transform originalFb = social != null ? social.Find("ButtonFb") : null;
        if (extraFb != null && originalFb != null)
        {
            Image extraImage = extraFb.GetComponent<Image>();
            Image originalImage = originalFb.GetComponent<Image>();
            if (extraImage != null && originalImage != null &&
                extraImage.sprite != null && extraImage.sprite == originalImage.sprite)
                extraFb.gameObject.SetActive(false);
            else
                UnityEngine.Debug.LogWarning("[HTDC Home2 HUD] FB Sprite identity changed; refusing to hide.", home);
        }

        // Keep the original Skins GameObject and its serialized callback, but
        // remove it from the running HUD as requested.
        Transform skins = canvas.Find("PanelHomeBottomLeft/ButtonSkins");
        if (skins != null) skins.gameObject.SetActive(false);

        Transform group = canvas.Find("PanelHomeBottom/GroupContent/GroupButton");
        if (group == null || group.Find(ActivityName) != null) return;
        Transform shop = group.Find("ButtonShop");
        Transform ship = group.Find("ButtonShip");
        Transform eventControl = canvas.Find("PaneHomeCenter/GroupButtons/ButtonEvents");
        Button eventButton = eventControl != null ? eventControl.GetComponent<Button>() : null;
        if (shop == null || ship == null || eventButton == null ||
            ship.GetSiblingIndex() != shop.GetSiblingIndex() + 1)
        {
            UnityEngine.Debug.LogWarning("[HTDC Home2 HUD] Expected Shop -> Ship and source Event button not found; no replacement UI guessed.", home);
            return;
        }

        Button shopButton = shop.GetComponent<Button>();
        Text shopLabel = shop.Find("TagName") != null
            ? shop.Find("TagName").GetComponentInChildren<Text>(true) : null;
        if (shopButton == null || shopLabel == null)
        {
            UnityEngine.Debug.LogWarning("[HTDC Home2 HUD] Original bottom button template incomplete.", home);
            return;
        }

        // This is the actual source Sprite bound to HomeScene/ButtonEvent;
        // not a copied screenshot, invented graphic, or icon from another game.
        Sprite originalActivitySprite = LoadActivitySprite();
        if (originalActivitySprite == null)
        {
            UnityEngine.Debug.LogWarning("[HTDC Home2 HUD] Source event Sprite unavailable. Run git lfs pull; not substituting another icon.", home);
            return;
        }

        GameObject activity = Object.Instantiate(shop.gameObject, group, false);
        activity.name = ActivityName;
        activity.transform.SetSiblingIndex(shop.GetSiblingIndex() + 1);
        Image activityImage = activity.GetComponent<Image>();
        Button activityButton = activity.GetComponent<Button>();
        Text activityLabel = activity.transform.Find("TagName").GetComponentInChildren<Text>(true);
        if (activityImage == null || activityButton == null || activityLabel == null)
        {
            Object.Destroy(activity);
            UnityEngine.Debug.LogError("[HTDC Home2 HUD] Could not initialize the source-backed activity control.", home);
            return;
        }

        activityImage.sprite = originalActivitySprite;
        activityLabel.text = "Hoạt động";
        // A cloned source ButtonShop contains the shop's old persistent
        // callback. Reset ONLY the runtime CLONE, never the source button.
        activityButton.onClick = new Button.ButtonClickedEvent();
        // The source Home2 Events button already opens the original offline
        // PopupGameEvent. Reuse that available UI; no new gameplay claim.
        activityButton.onClick.AddListener(() => eventButton.onClick.Invoke());
        activity.SetActive(true);
        UnityEngine.Debug.Log("[HTDC Home2 HUD] Extra FB and Skins hidden; Activity added after Shop using source art.", home);
    }

    private static Sprite LoadActivitySprite()
    {
#if UNITY_EDITOR
        // .asset and .png have the same Resources basename. Pin the exact
        // original Sprite object (GUID 86cbb642c2b57314eabcb83baa79b219).
        Sprite original = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(ActivitySpriteEditorAsset);
        if (original != null) return original;
#endif
        // For standalone builds only use a uniquely resolved source Sprite.
        // If duplicate Resources names remain ambiguous, do not guess.
        Sprite[] found = Resources.LoadAll<Sprite>(ActivitySpriteResource);
        return found != null && found.Length == 1 ? found[0] : null;
    }
}
