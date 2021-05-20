/************************
	FileName:/Scripts/Game/Gameplay/Mgr/SpriteHandler.cs
	CreateAuthor:neo.xu
	CreateTime:6/18/2020 5:01:28 PM
************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using Qarth;

namespace GameWish.Game
{
    public class SpriteHandler : TSingleton<SpriteHandler>
    {

        private Dictionary<string, SpriteAtlas> m_AtlasMap = new Dictionary<string, SpriteAtlas>();

        private ResLoader m_Loader;
        public override void OnSingletonInit()
        {
            m_Loader = ResLoader.Allocate(this.GetType().Name);
        }

        public Sprite GetSprite(string atlasName, string spriteName)
        {
            SpriteAtlas atlas;
            if (m_AtlasMap.TryGetValue(atlasName, out atlas))
            {
                return atlas.GetSprite(spriteName);
            }
            else
            {
                atlas = m_Loader.LoadSync(atlasName) as SpriteAtlas;
                if (atlas == null)
                {
                    Log.e("Not Find Atlas:" + atlasName);
                    return null;
                }
                m_AtlasMap.Add(atlasName, atlas);
                return atlas.GetSprite(spriteName);
            }
        }

        public void SetImageSprite(Image image, string atlasName, string spriteName)
        {
            if (image.sprite.name != spriteName)
            {
                image.sprite = GetSprite(atlasName, spriteName);
                image.SetNativeSize();
            }
        }



    }

}