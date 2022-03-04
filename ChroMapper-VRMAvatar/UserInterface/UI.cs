using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using SFB;
using ChroMapper_VRMAvatar.Configuration;
using ChroMapper_VRMAvatar.Component;

namespace ChroMapper_VRMAvatar.UserInterface
{
    public class UI
    {
        public static readonly ExtensionButton _extensionBtn = new ExtensionButton();
        public static UI instance;
        public static GameObject _MainMenu;
        public static VRMAvatarController vrmAvatarController;
        public UITextInput vrmFileInputText;

        public UI()
        {
            instance = this;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ChroMapper_VRMAvatar.Resources.Icon.png");
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);

            Texture2D texture2D = new Texture2D(256, 256);
            texture2D.LoadImage(data);

            _extensionBtn.Icon = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0), 100.0f);
            _extensionBtn.Tooltip = "VRMAvatar";
            ExtensionButtons.AddButton(_extensionBtn);
        }
        public static void ExtensionButtonEnable()
        {
            _extensionBtn.Visible = true;
        }
        public static void ExtensionButtonDisable()
        {
            _extensionBtn.Visible = false;
            _MainMenu.SetActive(false);
        }

        public void AddMenu(MapEditorUI mapEditorUI)
        {
            CanvasGroup parent = mapEditorUI.MainUIGroup[5];
            _MainMenu = new GameObject("VRMAvatar Menu");
            _MainMenu.transform.parent = parent.transform;
            //Main Menu
            AttachTransform(_MainMenu, 500, 180, 1, 1, -50, -30, 1, 1);
            Image imageMain = _MainMenu.AddComponent<Image>();
            imageMain.sprite = PersistentUI.Instance.Sprites.Background;
            imageMain.type = Image.Type.Sliced;
            imageMain.color = new Color(0.24f, 0.24f, 0.24f);
            AddLabel(_MainMenu.transform, "VRMAvatar", "VRMAvatar", new Vector2(0, -15));
            var vrmFileInput = AddTextInput(_MainMenu.transform, "VRM File", "VRM File", new Vector2(0, -55), Options.Instance.vrmAvatarFileName, (value) =>
            {
                Options.Instance.vrmAvatarFileName = value;
            });
            vrmFileInputText = vrmFileInput.Item3;
            MoveTransform(vrmFileInput.Item1, 50, 16, 0, 1, 20, -40);
            MoveTransform(vrmFileInput.Item3.transform, 430, 20, 0.1f, 1, 220, -40);

            var blinkerCheck = AddCheckbox(_MainMenu.transform, "Blinker", "Blinker", new Vector2(0, -170), Options.Instance.avatarBlinker, (check) =>
            {
                Options.Instance.avatarBlinker = check;
                VRMAvatarController.AvatarTransformSet();
            });
            MoveTransform(blinkerCheck.Item3.transform, 30, 16, 0, 1, 40, -65);
            MoveTransform(blinkerCheck.Item1, 50, 16, 0, 1, 70, -65);

            var lookAtCheck = AddCheckbox(_MainMenu.transform, "LookAt", "LookAt", new Vector2(0, -170), Options.Instance.avatarLookAt, (check) =>
            {
                Options.Instance.avatarLookAt = check;
                VRMAvatarController.AvatarTransformSet();
            });
            MoveTransform(lookAtCheck.Item3.transform, 30, 16, 0, 1, 100, -65);
            MoveTransform(lookAtCheck.Item1, 50, 16, 0, 1, 130, -65);

            var animationCheck = AddCheckbox(_MainMenu.transform, "Animation", "Animation", new Vector2(0, -170), Options.Instance.avatarAnimation, (check) =>
            {
                Options.Instance.avatarAnimation = check;
                VRMAvatarController.AvatarTransformSet();
            });
            MoveTransform(animationCheck.Item3.transform, 30, 16, 0, 1, 160, -65);
            MoveTransform(animationCheck.Item1, 50, 16, 0, 1, 190, -65);

            var reloadButton = AddButton(_MainMenu.transform, "Reload", "Reload", new Vector2(0, -170), () =>
            {
                VRMAvatarController.LoadModelAsync();
            });
            MoveTransform(reloadButton.transform, 40, 20, 1, 1, -80, -65);

            var selectButton = AddButton(_MainMenu.transform, "Select", "Select", new Vector2(0, -170), () =>
            {
                var paths = StandaloneFileBrowser.OpenFilePanel("VRM File", Path.GetDirectoryName(Options.Instance.vrmAvatarFileName), "vrm", false);
                if (paths.Length > 0 && File.Exists(paths[0]))
                {
                    Options.Instance.vrmAvatarFileName = paths[0];
                    vrmFileInputText.InputField.text = paths[0];
                }
            });
            MoveTransform(selectButton.transform, 40, 20, 1, 1, -35, -65);

            var scaleInput = AddTextInput(_MainMenu.transform, "Scale", "Scale", new Vector2(0, -75), Options.Instance.avatarScale.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Instance.avatarScale = res;
                    VRMAvatarController.AvatarTransformSet();
                }
            });
            MoveTransform(scaleInput.Item1, 40, 16, 0, 1, 30, -90);
            MoveTransform(scaleInput.Item3.transform, 40, 20, 0.1f, 1, 25, -90);

            var xOffsetInput = AddTextInput(_MainMenu.transform, "X offset", "X offset", new Vector2(0, -95), Options.Instance.avatarXoffset.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Instance.avatarXoffset = res;
                    VRMAvatarController.AvatarTransformSet();
                }
            });
            MoveTransform(xOffsetInput.Item1, 40, 16, 0f, 1, 125, -90);
            MoveTransform(xOffsetInput.Item3.transform, 40, 20, 0.1f, 1, 125, -90);

            var yOffsetInput = AddTextInput(_MainMenu.transform, "Y offset", "Y offset", new Vector2(0, -95), Options.Instance.avatarYoffset.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Instance.avatarYoffset = res;
                    VRMAvatarController.AvatarTransformSet();
                }
            });
            MoveTransform(yOffsetInput.Item1, 40, 16, 0f, 1, 225, -90);
            MoveTransform(yOffsetInput.Item3.transform, 40, 20, 0.1f, 1, 225, -90);

            var zOffsetInput = AddTextInput(_MainMenu.transform, "Z offset", "Z offset", new Vector2(0, -95), Options.Instance.avatarZoffset.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Instance.avatarZoffset = res;
                    VRMAvatarController.AvatarTransformSet();
                }
            });
            MoveTransform(zOffsetInput.Item1, 40, 16, 0f, 1, 325, -90);
            MoveTransform(zOffsetInput.Item3.transform, 40, 20, 0.1f, 1, 325, -90);

            var xRotationInput = AddTextInput(_MainMenu.transform, "X rotation", "X rotation", new Vector2(0, -95), Options.Instance.avatarXrotation.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Instance.avatarXrotation = res;
                    VRMAvatarController.AvatarTransformSet();
                }
            });
            MoveTransform(xRotationInput.Item1, 40, 16, 0f, 1, 125, -115);
            MoveTransform(xRotationInput.Item3.transform, 40, 20, 0.1f, 1, 125, -115);

            var yRotationInput = AddTextInput(_MainMenu.transform, "Y rotation", "Y rotation", new Vector2(0, -95), Options.Instance.avatarYrotation.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Instance.avatarYrotation = res;
                    VRMAvatarController.AvatarTransformSet();
                }
            });
            MoveTransform(yRotationInput.Item1, 40, 16, 0f, 1, 225, -115);
            MoveTransform(yRotationInput.Item3.transform, 40, 20, 0.1f, 1, 225, -115);

            var zRotationInput = AddTextInput(_MainMenu.transform, "Z rotation", "Z rotation", new Vector2(0, -95), Options.Instance.avatarZrotation.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Instance.avatarZrotation = res;
                    VRMAvatarController.AvatarTransformSet();
                }
            });
            MoveTransform(zRotationInput.Item1, 40, 16, 0f, 1, 325, -115);
            MoveTransform(zRotationInput.Item3.transform, 40, 20, 0.1f, 1, 325, -115);

            var vrmEnableCheck = AddCheckbox(_MainMenu.transform, "VRMAvatar Enable", "VRMAvatar Enable", new Vector2(0, -170), Options.Instance.avatarEnable, (check) =>
            {
                Options.Instance.avatarEnable = check;
                VRMAvatarController.AvatarEnableChange();
            });
            MoveTransform(vrmEnableCheck.Item3.transform, 30, 16, 0, 1, 40, -150);
            MoveTransform(vrmEnableCheck.Item1, 100, 16, 0, 1, 95, -150);

            var saveButton = AddButton(_MainMenu.transform, "Setting Save", "Setting Save", new Vector2(0, -200), () =>
            {
                Options.Instance.SettingSave();
            });
            MoveTransform(saveButton.transform, 70, 25, 1, 1, -130, -150);

            var closeButton = AddButton(_MainMenu.transform, "Close", "Close", new Vector2(0, -200), () =>
            {
                _MainMenu.SetActive(false);
            });
            MoveTransform(closeButton.transform, 70, 25, 1, 1, -50, -150);

            _MainMenu.SetActive(false);
            _extensionBtn.Click = () =>
            {
                _MainMenu.SetActive(!_MainMenu.activeSelf);
            };
        }

        // i ended up copying Top_Cat's CM-JS UI helper, too useful to make my own tho
        // after askin TC if it's one of the only way, he let me use this
        private UIButton AddButton(Transform parent, string title, string text, Vector2 pos, UnityAction onClick)
        {
            var button = Object.Instantiate(PersistentUI.Instance.ButtonPrefab, parent);
            MoveTransform(button.transform, 100, 25, 0.5f, 1, pos.x, pos.y);

            button.name = title;
            button.Button.onClick.AddListener(onClick);

            button.SetText(text);
            button.Text.enableAutoSizing = false;
            button.Text.fontSize = 12;
            return button;
        }

        private (RectTransform, TextMeshProUGUI) AddLabel(Transform parent, string title, string text, Vector2 pos, Vector2? size = null)
        {
            var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
            var rectTransform = ((RectTransform)entryLabel.transform);
            rectTransform.SetParent(parent);

            MoveTransform(rectTransform, 110, 24, 0.5f, 1, pos.x, pos.y);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = title;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Center;
            textComponent.fontSize = 16;
            textComponent.text = text;
            return (rectTransform, textComponent);
        }

        private (RectTransform, TextMeshProUGUI, UITextInput) AddTextInput(Transform parent, string title, string text, Vector2 pos, string value, UnityAction<string> onChange)
        {
            var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
            var rectTransform = ((RectTransform)entryLabel.transform);
            rectTransform.SetParent(parent);

            MoveTransform(rectTransform, 50, 16, 0.5f, 1, pos.x - 47.5f, pos.y);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = title;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Right;
            textComponent.fontSize = 12;
            textComponent.text = text;

            var textInput = Object.Instantiate(PersistentUI.Instance.TextInputPrefab, parent);
            MoveTransform(textInput.transform, 75, 20, 0.5f, 1, pos.x + 27.5f, pos.y);
            textInput.GetComponent<Image>().pixelsPerUnitMultiplier = 3;
            textInput.InputField.text = value;
            textInput.InputField.onFocusSelectAll = false;
            textInput.InputField.textComponent.alignment = TextAlignmentOptions.Left;
            textInput.InputField.textComponent.fontSize = 10;

            textInput.InputField.onValueChanged.AddListener(onChange);
            return (rectTransform, textComponent, textInput);
        }

        private (RectTransform, TextMeshProUGUI, Toggle) AddCheckbox(Transform parent, string title, string text, Vector2 pos, bool value, UnityAction<bool> onClick)
        {
            var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
            var rectTransform = ((RectTransform)entryLabel.transform);
            rectTransform.SetParent(parent);
            MoveTransform(rectTransform, 80, 16, 0.5f, 1, pos.x + 10, pos.y + 5);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = title;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Left;
            textComponent.fontSize = 12;
            textComponent.text = text;

            var original = GameObject.Find("Strobe Generator").GetComponentInChildren<Toggle>(true);
            var toggleObject = Object.Instantiate(original, parent.transform);
            MoveTransform(toggleObject.transform, 100, 25, 0.5f, 1, pos.x, pos.y);

            var toggleComponent = toggleObject.GetComponent<Toggle>();
            var colorBlock = toggleComponent.colors;
            colorBlock.normalColor = Color.white;
            toggleComponent.colors = colorBlock;
            toggleComponent.isOn = value;

            toggleComponent.onValueChanged.AddListener(onClick);
            return (rectTransform, textComponent, toggleComponent);
        }

        private RectTransform AttachTransform(GameObject obj, float sizeX, float sizeY, float anchorX, float anchorY, float anchorPosX, float anchorPosY, float pivotX = 0.5f, float pivotY = 0.5f)
        {
            RectTransform rectTransform = obj.AddComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(anchorX, anchorY);
            rectTransform.anchoredPosition = new Vector3(anchorPosX, anchorPosY, 0);

            return rectTransform;
        }

        private void MoveTransform(Transform transform, float sizeX, float sizeY, float anchorX, float anchorY, float anchorPosX, float anchorPosY, float pivotX = 0.5f, float pivotY = 0.5f)
        {
            if (!(transform is RectTransform rectTransform)) return;

            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(anchorX, anchorY);
            rectTransform.anchoredPosition = new Vector3(anchorPosX, anchorPosY, 0);
        }
    }
}