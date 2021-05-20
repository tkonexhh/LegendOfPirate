/************************
	FileName:/GFrameWork/Scripts/Base/Config/ProjectDefaultConfig/TextMeshProConfig.cs
	CreateAuthor:neo.xu
	CreateTime:9/10/2020 7:41:30 PM
	Tip:9/10/2020 7:41:30 PM
************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Qarth
{
    [System.Serializable]
    public class TextMeshProConfig
    {
        public TMP_FontAsset font;
        public TextAlignmentOptions textAlignment = TextAlignmentOptions.Center;
        public Color color = Color.white;
        public float fontSize = 36;
    }

}