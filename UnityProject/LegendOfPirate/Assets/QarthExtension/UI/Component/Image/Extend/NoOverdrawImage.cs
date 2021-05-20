/************************
	FileName:/GFrameWork/Scripts/Base/Component/UGUI/Image/NoOverdrawImage/NoOverdrawImage.cs
	CreateAuthor:neo.xu
	CreateTime:4/29/2020 11:13:00 AM
************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GFrame
{
    //不渲染但可以相应点击
    public class NoOverdrawImage : Graphic
    {
        public override void Rebuild(CanvasUpdate update)
        {

        }


        private void Reset()
        {
            var button = gameObject.GetComponent<Button>();
            if (button != null)
            {
                if (button.targetGraphic == null)
                    button.targetGraphic = this;
            }
        }


    }

}