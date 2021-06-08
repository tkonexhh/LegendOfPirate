using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Qarth;

namespace GFrame.Editor
{
    public class GButton : Button
    {
        #region 点击缩放动画
        public bool scaleAnim = false;
        public Vector3 clickDownScale = new Vector3(0.95f, 0.95f, 0.95f);
        public Vector3 normalScale = Vector3.one;
        #endregion

        #region  
        public bool sound = false;
        public string clickEffect;
        #endregion


        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (scaleAnim)
                transform.localScale = clickDownScale;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (scaleAnim)
                transform.localScale = normalScale;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (sound && !string.IsNullOrEmpty(clickEffect))
            {
                AudioMgr.S.PlaySound(clickEffect);
            }
        }
    }

}