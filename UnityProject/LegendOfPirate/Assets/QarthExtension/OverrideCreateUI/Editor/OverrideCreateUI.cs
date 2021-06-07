using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using Qarth;
namespace GFrame.Editor
{
    public class OverrideCreateUI
    {
        /// <summary>
        /// 第一次创建UI元素时，没有canvas、EventSystem所有要生成，Canvas作为父节点
        /// 之后再空的位置上建UI元素会自动添加到Canvas下
        /// 在非UI树下的GameObject上新建UI元素也会 自动添加到Canvas下（默认在UI树下）
        /// 添加到指定的UI元素下
        /// </summary>
        [MenuItem("GameObject/UI/Image")]
        static void CreatImage()
        {
            CreateUI(Image);
        }

        [MenuItem("GameObject/UI/Text")]
        static void CreatText()
        {
            CreateUI(Text);
        }

        [MenuItem("GameObject/UI/Text - TextMeshPro")]
        static void CreatTextMeshPro()
        {
            CreateUI(TextMeshProUGUI);
        }

        [MenuItem("GameObject/UI/Button")]
        static void CreatButton()
        {
            CreateUI(Button);
        }

        private static void CreateUI(System.Func<GameObject> callback)
        {
            var canvasObj = SecurityCheck();
            var gameObject = callback();
            if (!Selection.activeTransform)      // 在根目录创建的， 自动移动到 Canvas下
            {
                gameObject.transform.SetParent(canvasObj.transform);
            }
            else // (Selection.activeTransform)
            {
                if (!Selection.activeTransform.GetComponentInParent<Canvas>())    // 没有在UI树下
                {
                    gameObject.transform.SetParent(canvasObj.transform);
                }
                else
                {
                    //callback();
                }
            }

            Undo.IncrementCurrentGroup();
            Undo.RegisterCreatedObjectUndo(gameObject, "");
        }

        #region Create

        private static GameObject Image()
        {
            System.Action<GameObject> callback = (go) =>
            {
                Image image = go.GetComponent<Image>();
                HandleImage(image);
            };
            return CreateGO<GImage>("Img_", callback);
        }

        private static GameObject Text()
        {
            System.Action<GameObject> callback = (go) =>
            {
                Text text = go.GetComponent<Text>();
                HandleText(text);
            };
            return CreateGO<GText>("Txt_", callback);
        }

        private static GameObject TextMeshProUGUI()
        {
            System.Action<GameObject> callback = (go) =>
           {
               TextMeshProUGUI text = go.GetComponent<TextMeshProUGUI>();
               HandleTextMeshPro(text);
           };
            return CreateGO<GTextMeshProUGUI>("TMP_", callback);
        }

        private static GameObject Button()
        {
            System.Action<GameObject> callback = (go) =>
            {
                var image = go.AddComponent<GImage>();
                var button = go.GetComponent<Button>();
                button.targetGraphic = image;

                GameObject textGo = new GameObject("Text", typeof(Text));
                textGo.transform.SetParent(go.transform);
                textGo.transform.localScale = Vector3.one;
                Text text = textGo.GetComponent<Text>();
                HandleText(text);
                RectTransform rectText = text.GetComponent<RectTransform>();
                rectText.SetAnchor(AnchorPresets.StretchAll, 0, 0);
                rectText.SetSize(button.GetComponent<RectTransform>().sizeDelta);
            };
            return CreateGO<Button>("Btn_", callback);
        }



        #endregion

        #region HandleUI

        private static void HandleText(Text text)
        {
            text.raycastTarget = false;
            text.alignment = TextAnchor.MiddleCenter;
            text.text = "text";
            text.supportRichText = false;
            if (UIDefaultConfig.S != null)
            {
                text.rectTransform().sizeDelta = UIDefaultConfig.textConfig.defaultTextRect;
                text.font = UIDefaultConfig.textConfig.defaultTextFont;
                text.color = UIDefaultConfig.textConfig.defaultTextColor;
            }
            text.fontSize = UIDefaultConfig.textConfig.defaultTextSize;
        }

        private static void HandleTextMeshPro(TextMeshProUGUI tmp)
        {
            tmp.raycastTarget = false;
            tmp.richText = false;
            if (UIDefaultConfig.textMeshProConfig.font != null)
            {
                tmp.font = UIDefaultConfig.textMeshProConfig.font;
            }
            tmp.text = "Text";
            tmp.alignment = UIDefaultConfig.textMeshProConfig.textAlignment;
            tmp.color = UIDefaultConfig.textMeshProConfig.color;
            tmp.fontSize = UIDefaultConfig.textMeshProConfig.fontSize;
        }

        private static void HandleImage(Image image)
        {
            image.raycastTarget = false;
        }

        #endregion


        private static GameObject CreateGO<T>(string defaultName, System.Action<GameObject> callback)
        {
            GameObject go = new GameObject(defaultName, typeof(T));
            go.transform.SetParent(Selection.activeTransform);
            go.transform.SetLocalPos(Vector3.zero);
            go.transform.localScale = Vector3.one;
            Selection.activeGameObject = go;
            callback(go);
            return go;
        }

        // 如果第一次创建UI元素 可能没有 Canvas、EventSystem对象！
        private static GameObject SecurityCheck()
        {
            GameObject canvasGO;
            var cc = Object.FindObjectOfType<Canvas>();
            if (!cc)
            {
                canvasGO = new GameObject("Canvas", typeof(Canvas));
                var scaler = canvasGO.AddComponent<CanvasScaler>();
                //scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasGO.AddComponent<GraphicRaycaster>();
                Canvas canvas = canvasGO.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }
            else
            {
                canvasGO = cc.gameObject;
            }
            if (!Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>())
            {
                GameObject eventSystemGo = new GameObject("EventSystem", typeof(EventSystem));
                eventSystemGo.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystemGo.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }

            return canvasGO;
        }

    }
}
