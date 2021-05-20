using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using UnityEngine.UI;

namespace GameWish.Game
{
    //可以优化 缓存加载的图片 索引获取资源
    public class SpriteLoader : TSingleton<SpriteLoader> 
    {
        private ResLoader m_UILoader;

        public override void OnSingletonInit()
        {
        
        }

        public Sprite GetSpriteByName(string newSpriteName)
        {
            if (m_UILoader == null)
            {
                m_UILoader = ResLoader.Allocate("UI_UpgradePreviewLoader");
                Debug.Log("loaded init upgrade panel");
            }

            UnityEngine.Object obj = m_UILoader.LoadSync(newSpriteName);
            Texture2D text = obj as Texture2D;
            Sprite sprite = null;

            if (text != null)
            {
                sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                sprite = obj as Sprite;
            }
            //m_UILoader.Recycle2Cache();

            return sprite;
        }

        public void ResetImageSprite(string newSpriteName, Image image)
        {
            //if (m_UILoader == null)
            //{
            //    m_UILoader = ResLoader.Allocate("UI_UpgradePreviewLoader");
            //    Debug.Log("loaded init upgrade panel");
            //}
            var uiLoader = ResLoader.Allocate("SpriteLoader");

            UnityEngine.Object obj = uiLoader.LoadSync(newSpriteName);
            Texture2D text = obj as Texture2D;
            Sprite sprite = null;

            if (text != null)
            {
                sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                sprite = obj as Sprite;
            }

            image.sprite = sprite;

            //uiLoader.ReleaseRes(newSpriteName);
            //uiLoader.Recycle2Cache();
        }
        //private void OnDestroy()
        //{
        //    if (m_UILoader != null)
        //    {
        //        m_UILoader.ReleaseAllRes();
        //        m_UILoader.Recycle2Cache();
        //        m_UILoader = null;
        //    }
        //}


    }
}
