using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HaiTac.OfflineViewer
{
    // No commercial game assets, recovered gameplay logic, or online API calls.
    // This is a standalone Unity uGUI inspector for verified serialized metadata.
    [Serializable]
    public sealed class ViewerDatabase
    {
        public int schemaVersion;
        public string source;
        public string viewport;
        public string licenseNotice;
        public ViewerScene[] scenes;
    }

    [Serializable]
    public sealed class ViewerScene
    {
        public string id;
        public string title;
        public string source;
        public int rootTransform;
        public string confidence;
        public int expectedNodes;
        public int linkedImageEntries;
        public int unlinkedImageEntries;
        public int nodeCount;
        public ViewerNode[] nodes;
    }

    [Serializable]
    public sealed class ViewerNode
    {
        public int id;
        public int parent;
        public string name;
        public string path;
        public bool active;
        public float[] a0, a1, pivot, delta, position, scale;
        public float rotationZ;
        public int order;
        public string[] types, sprites, spine;
        public int missingImages;
    }

    public sealed class OfflineUiViewer : MonoBehaviour
    {
        private const int VirtualWidth = 1600;
        private const int VirtualHeight = 900;
        private const int MaxDrawnBoxes = 750;
        private static readonly Color Background = new Color(.065f, .09f, .15f);
        private static readonly Color Surface = new Color(.10f, .15f, .22f);
        private static readonly Color Accent = new Color(.10f, .73f, .60f);
        private static readonly Color Muted = new Color(.68f, .75f, .83f);
        private Font uiFont;

        private ViewerDatabase database;
        private int sceneIndex;
        private RectTransform previewSurface;
        private Text statusText;
        private Text inspectorText;
        private Text navHint;
        private RectTransform buttonList;
        private readonly List<GameObject> generated = new List<GameObject>();
        private int demoShipLevel = 1;
        private int demoPoints = 0;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Init()
        {
            // A reconstructed Canvas scene is not the metadata demo: do not put
            // the demo's colored wireframe/shell over the actual imported sprites.
            var active = SceneManager.GetActiveScene();
            if (active.path.Replace('\\', '/').StartsWith(
                    "Assets/LocalReconstruction/Scenes/", StringComparison.Ordinal))
                return;
            if (FindObjectOfType<ReconstructionEvidence>() != null ||
                FindObjectOfType<OfflineUiViewer>() != null)
                return;
            new GameObject("Offline UI Viewer (metadata only)").AddComponent<OfflineUiViewer>();
        }

        private void Awake()
        {
            uiFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
            if (FindObjectOfType<EventSystem>() == null)
            {
                var system = new GameObject("EventSystem", typeof(EventSystem),
                    typeof(StandaloneInputModule));
                DontDestroyOnLoad(system);
            }
            BuildShell();
            LoadData();
        }

        private void Update()
        {
            if (database == null || database.scenes == null || database.scenes.Length == 0)
                return;
            if (Input.GetKeyDown(KeyCode.RightArrow)) SwitchScene(sceneIndex + 1);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) SwitchScene(sceneIndex - 1);
        }

        private static RectTransform Panel(Transform parent, string name,
            Vector2 anchorMin, Vector2 anchorMax, Color color)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(Image));
            var rect = go.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            go.GetComponent<Image>().color = color;
            go.GetComponent<Image>().raycastTarget = false;
            return rect;
        }

        private Text Label(Transform parent, string content, int fontSize,
            Color color, Vector2 anchorMin, Vector2 anchorMax,
            TextAnchor alignment = TextAnchor.MiddleLeft)
        {
            var go = new GameObject("Label", typeof(RectTransform), typeof(Text));
            var rect = go.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = new Vector2(9, 0);
            rect.offsetMax = new Vector2(-9, 0);
            var t = go.GetComponent<Text>();
            t.font = uiFont;
            t.fontSize = fontSize;
            t.text = content;
            t.alignment = alignment;
            t.color = color;
            t.raycastTarget = false;
            t.horizontalOverflow = HorizontalWrapMode.Wrap;
            t.verticalOverflow = VerticalWrapMode.Truncate;
            return t;
        }

        private Button ActionButton(Transform parent, string title, Vector2 anchorMin,
            Vector2 anchorMax, Action callback, Color? background = null)
        {
            var rect = Panel(parent, "Action: " + title, anchorMin, anchorMax,
                background ?? new Color(.16f, .23f, .32f));
            var image = rect.GetComponent<Image>();
            image.raycastTarget = true;
            var button = rect.gameObject.AddComponent<Button>();
            button.targetGraphic = image;
            var colors = button.colors;
            colors.highlightedColor = new Color(.24f, .43f, .50f);
            colors.pressedColor = new Color(.12f, .45f, .38f);
            button.colors = colors;
            Label(rect, title, 15, Color.white, Vector2.zero, Vector2.one,
                TextAnchor.MiddleCenter);
            button.onClick.AddListener(() => callback());
            return button;
        }

        private void BuildShell()
        {
            var go = new GameObject("Viewer Canvas", typeof(RectTransform),
                typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            var canvas = go.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 200;
            var scaler = go.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(VirtualWidth, VirtualHeight);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = .5f;

            var root = Panel(go.transform, "Background", Vector2.zero, Vector2.one, Background);
            var header = Panel(root, "Header", new Vector2(0, .925f),
                new Vector2(1, 1), Surface);
            Label(header, "HẢI TẶC · OFFLINE UI VIEWER", 27, Color.white,
                new Vector2(.015f, 0), new Vector2(.7f, 1));
            Label(header, "BẢN XEM TRƯỚC METADATA  ·  KHÔNG PHẢI CLIENT GỐC", 14, Muted,
                new Vector2(.69f, 0), new Vector2(.995f, 1), TextAnchor.MiddleRight);

            var left = Panel(root, "Scene navigation",
                new Vector2(.01f, .09f), new Vector2(.185f, .912f), Surface);
            Label(left, "DANH SÁCH MÀN HÌNH", 17, Color.white,
                new Vector2(0, .91f), new Vector2(1, .985f));

            buttonList = Panel(left, "Menu entries",
                new Vector2(.03f, .26f), new Vector2(.97f, .90f),
                new Color(.09f, .13f, .20f));
            navHint = Label(left, "Đang đọc dữ liệu...", 14, Muted,
                new Vector2(.03f, .11f), new Vector2(.97f, .255f));
            ActionButton(left, "◀ Trước", new Vector2(.04f, .02f),
                new Vector2(.48f, .095f), () => SwitchScene(sceneIndex - 1));
            ActionButton(left, "Tiếp ▶", new Vector2(.52f, .02f),
                new Vector2(.96f, .095f), () => SwitchScene(sceneIndex + 1));

            var middle = Panel(root, "Scene preview", new Vector2(.195f, .09f),
                new Vector2(.805f, .912f), Surface);
            statusText = Label(middle, "Nạp scene...", 17, Color.white,
                new Vector2(.015f, .905f), new Vector2(.985f, .985f));
            var viewport = Panel(middle, "Preview viewport",
                new Vector2(.012f, .155f), new Vector2(.988f, .895f),
                new Color(.045f, .075f, .115f));
            viewport.gameObject.AddComponent<RectMask2D>();
            var demo = new GameObject("Projected 1600x900 (no real art)", typeof(RectTransform));
            previewSurface = demo.GetComponent<RectTransform>();
            previewSurface.SetParent(viewport, false);
            previewSurface.anchorMin = new Vector2(.5f, .5f);
            previewSurface.anchorMax = new Vector2(.5f, .5f);
            previewSurface.pivot = new Vector2(.5f, .5f);
            previewSurface.anchoredPosition = Vector2.zero;
            previewSurface.sizeDelta = new Vector2(VirtualWidth, VirtualHeight);
            previewSurface.localScale = Vector3.one * .54f;

            Label(middle,
                "● Xanh lá: Sprite   ● Vàng: ảnh chưa xác định   ● Xanh dương: nút   ● Tím: chữ/Spine\n" +
                "Bấm vào một ô để xem dữ liệu. Các hình chữ nhật KHÔNG phải artwork game.",
                14, Muted, new Vector2(.025f, .038f), new Vector2(.975f, .15f));

            var right = Panel(root, "Metadata inspector",
                new Vector2(.815f, .09f), new Vector2(.99f, .912f), Surface);
            Label(right, "THÔNG TIN & THIẾU ASSET", 17, Color.white,
                new Vector2(.02f, .92f), new Vector2(.98f, .987f));
            var scrollRoot = Panel(right, "Inspector scroll",
                new Vector2(.02f, .28f), new Vector2(.98f, .915f),
                new Color(.07f, .11f, .17f));
            scrollRoot.GetComponent<Image>().raycastTarget = true;
            var scroll = scrollRoot.gameObject.AddComponent<ScrollRect>();
            scroll.horizontal = false;
            scroll.vertical = true;
            scroll.movementType = ScrollRect.MovementType.Clamped;
            scrollRoot.gameObject.AddComponent<RectMask2D>();
            var content = new GameObject("Inspector content", typeof(RectTransform));
            var contentRect = content.GetComponent<RectTransform>();
            contentRect.SetParent(scrollRoot, false);
            contentRect.anchorMin = new Vector2(0, 1);
            contentRect.anchorMax = new Vector2(1, 1);
            contentRect.pivot = new Vector2(.5f, 1);
            contentRect.sizeDelta = new Vector2(0, 1900);
            contentRect.anchoredPosition = Vector2.zero;
            inspectorText = Label(contentRect, "", 15, Color.white,
                Vector2.zero, Vector2.one);
            inspectorText.alignment = TextAnchor.UpperLeft;
            inspectorText.verticalOverflow = VerticalWrapMode.Overflow;
            scroll.content = contentRect;
            scroll.viewport = scrollRoot;

            Label(right, "THAO TÁC MÔ PHỎNG OFFLINE", 13, Muted,
                new Vector2(.02f, .215f), new Vector2(.98f, .27f));
            ActionButton(right, "Demo: nâng cấp tàu +1",
                new Vector2(.025f, .14f), new Vector2(.975f, .205f),
                () =>
                {
                    demoShipLevel++;
                    demoPoints += 10;
                    ShowInspector(null);
                }, new Color(.10f, .38f, .32f));
            ActionButton(right, "Demo: đặt lại",
                new Vector2(.025f, .062f), new Vector2(.975f, .128f),
                () =>
                {
                    demoShipLevel = 1;
                    demoPoints = 0;
                    ShowInspector(null);
                });

            var footer = Panel(root, "Footer", new Vector2(0, 0),
                new Vector2(1, .065f), Surface);
            Label(footer,
                "OFFLINE · Không API · Không tài khoản · Không asset thương mại · Không logic chiến đấu gốc",
                15, Muted, new Vector2(.017f, 0), new Vector2(.99f, 1));
        }

        private void LoadData()
        {
            var dataPath = Path.Combine(Application.streamingAssetsPath, "ui-scenes.json");
            if (!File.Exists(dataPath))
            {
                statusText.text = "THIẾU ui-scenes.json";
                inspectorText.text = "Hãy chạy: python tools/build_unity_viewer_data.py " +
                    "--repo-root <thư mục Git đã clone>\n" +
                    "Hoặc kiểm tra Assets/StreamingAssets/ui-scenes.json.";
                return;
            }
            try
            {
                database = JsonUtility.FromJson<ViewerDatabase>(File.ReadAllText(dataPath));
                if (database == null || database.schemaVersion != 1 ||
                    database.scenes == null || database.scenes.Length == 0)
                    throw new InvalidDataException("Invalid scene dataset schema");
                for (int i = 0; i < database.scenes.Length; ++i)
                {
                    var index = i;
                    var min = new Vector2(.035f, .89f - i * .14f);
                    var max = new Vector2(.965f, .982f - i * .14f);
                    ActionButton(buttonList, database.scenes[i].title, min, max,
                        () => SwitchScene(index));
                }
                SwitchScene(0);
            }
            catch (Exception error)
            {
                statusText.text = "KHÔNG ĐỌC ĐƯỢC DỮ LIỆU";
                inspectorText.text = error.Message;
                Debug.LogError(error);
            }
        }

        private void SwitchScene(int next)
        {
            if (database == null || database.scenes == null || database.scenes.Length == 0)
                return;
            sceneIndex = (next % database.scenes.Length + database.scenes.Length) %
                         database.scenes.Length;
            foreach (var go in generated)
                if (go != null) Destroy(go);
            generated.Clear();
            var scene = database.scenes[sceneIndex];
            statusText.text = scene.title + "   •   " + scene.nodeCount +
                " RectTransform   •   " + scene.confidence;
            navHint.text = "Scene " + (sceneIndex + 1) + "/" + database.scenes.Length +
                "\n◀ ▶: chuyển màn hình\nClick ô: xem metadata";
            DrawScene(scene);
            ShowInspector(null);
        }

        private static Vector2 V(float[] pair, Vector2 fallback)
        {
            if (pair == null || pair.Length != 2) return fallback;
            return new Vector2(pair[0], pair[1]);
        }

        // Approximation: no LayoutGroup/CanvasScaler-runtime, no custom pivot
        // transforms from Spine, no masking or nested Canvas sorting.
        private static Rect ComputeRect(Rect parent, ViewerNode node)
        {
            var min = V(node.a0, new Vector2(.5f, .5f));
            var max = V(node.a1, new Vector2(.5f, .5f));
            var pivot = V(node.pivot, new Vector2(.5f, .5f));
            var size = V(node.delta, Vector2.zero);
            var position = V(node.position, Vector2.zero);
            float w = parent.width * (max.x - min.x) + size.x;
            float h = parent.height * (max.y - min.y) + size.y;
            float x = parent.xMin + parent.width *
                (min.x + (max.x - min.x) * pivot.x) + position.x - w * pivot.x;
            float y = parent.yMin + parent.height *
                (min.y + (max.y - min.y) * pivot.y) + position.y - h * pivot.y;
            return new Rect(x, y, w, h);
        }

        private static bool Has(ViewerNode node, string type)
        {
            if (node.types == null) return false;
            foreach (string value in node.types)
                if (value == type) return true;
            return false;
        }

        private void DrawScene(ViewerScene scene)
        {
            if (scene.nodes == null) return;
            var rects = new Dictionary<int, Rect>();
            var root = new Rect(0, 0, VirtualWidth, VirtualHeight);
            int drawn = 0;
            foreach (var node in scene.nodes)
            {
                if (node.id == scene.rootTransform)
                {
                    rects[node.id] = root;
                    continue;
                }
                if (!rects.TryGetValue(node.parent, out Rect parent))
                    continue;
                var rect = ComputeRect(parent, node);
                if (rect.width <= 0 || rect.height <= 0 ||
                    float.IsNaN(rect.x) || float.IsNaN(rect.width))
                {
                    // Some serialized containers receive runtime LayoutGroup sizes.
                    rects[node.id] = parent;
                    continue;
                }
                rects[node.id] = rect;
                bool image = node.sprites != null && node.sprites.Length > 0;
                bool spine = node.spine != null && node.spine.Length > 0;
                bool button = Has(node, "Button");
                bool text = Has(node, "Text") || Has(node, "TextMeshProUGUI");
                bool missing = node.missingImages > 0;
                if (!(image || spine || button || text || missing))
                    continue;
                if (++drawn > MaxDrawnBoxes)
                    break;
                if (rect.width < 2 || rect.height < 2 ||
                    rect.xMin > VirtualWidth || rect.yMin > VirtualHeight ||
                    rect.xMax < 0 || rect.yMax < 0)
                    continue;

                Color tint = image ? new Color(.04f, .72f, .59f, .17f) :
                    missing ? new Color(.93f, .53f, .10f, .22f) :
                    spine ? new Color(.80f, .25f, .84f, .25f) :
                    button ? new Color(.24f, .53f, .92f, .22f) :
                    new Color(.70f, .45f, .95f, .17f);
                if (!node.active) tint.a *= .35f;
                var go = new GameObject("Placeholder: " + node.name,
                    typeof(RectTransform), typeof(Image), typeof(Button));
                var ui = go.GetComponent<RectTransform>();
                ui.SetParent(previewSurface, false);
                ui.anchorMin = Vector2.zero;
                ui.anchorMax = Vector2.zero;
                ui.pivot = Vector2.zero;
                ui.anchoredPosition = new Vector2(rect.xMin, rect.yMin) -
                                      new Vector2(VirtualWidth / 2f, VirtualHeight / 2f);
                ui.sizeDelta = new Vector2(rect.width, rect.height);
                var img = go.GetComponent<Image>();
                img.color = tint;
                var outline = go.AddComponent<Outline>();
                outline.effectColor = new Color(tint.r, tint.g, tint.b,
                    node.active ? .74f : .29f);
                outline.effectDistance = new Vector2(1.5f, 1.5f);
                var selected = node;
                var click = go.GetComponent<Button>();
                click.targetGraphic = img;
                click.transition = Selectable.Transition.None;
                click.onClick.AddListener(() => ShowInspector(selected));
                generated.Add(go);

                if (rect.width > 120 && rect.height > 28 && drawn < 180)
                {
                    var label = Label(ui, node.name, 16, Color.white,
                        Vector2.zero, Vector2.one, TextAnchor.MiddleCenter);
                    label.gameObject.name = "Metadata label only";
                }
            }
        }

        private void ShowInspector(ViewerNode selected)
        {
            if (database == null) return;
            var scene = database.scenes[sceneIndex];
            var lines = new List<string> {
                "<" + scene.id + ">",
                "",
                "Nguồn: " + scene.source,
                "Cây UI: " + scene.nodeCount + " nút",
                "Ảnh liên kết (báo cáo): " + scene.linkedImageEntries,
                "Ảnh chưa xác định: " + scene.unlinkedImageEntries,
                "Trạng thái: " + scene.confidence,
                "",
                "DEMO OFFLINE",
                "Cấp tàu mô phỏng: " + demoShipLevel,
                "Điểm mô phỏng: " + demoPoints,
                "Không có server hay nghiệp vụ gốc.",
                ""
            };
            if (selected != null)
            {
                lines.Add("ĐÃ CHỌN");
                lines.Add(selected.path);
                lines.Add("GameObject: " + selected.name);
                lines.Add("Transform ID: " + selected.id);
                lines.Add("Active trong asset: " + selected.active);
                lines.Add("Loại: " + string.Join(", ", selected.types ?? new string[0]));
                lines.Add("Sprite ứng viên: " + string.Join(", ",
                    selected.sprites ?? new string[0]));
                lines.Add("Spine: " + string.Join(", ",
                    selected.spine ?? new string[0]));
                lines.Add("Image thiếu: " + selected.missingImages);
            }
            lines.Add("");
            lines.Add("THIẾU / CHƯA CÓ SPRITE TĨNH");
            int missingShown = 0;
            if (scene.nodes != null)
                foreach (var node in scene.nodes)
                {
                    if (node.missingImages <= 0) continue;
                    lines.Add("• " + node.name + "  (" + node.missingImages + ")");
                    if (++missingShown >= 28)
                    {
                        lines.Add("… và các nút khác trong báo cáo CSV");
                        break;
                    }
                }
            lines.Add("");
            lines.Add("Lưu ý: Không có texture thật, skin Spine, animation,");
            lines.Add("layout runtime hoặc backend GOSU.");
            inspectorText.text = string.Join("\n", lines);
        }
    }
}
