using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class UIClipPanel : AbstractPanel
    {
        private UIClipImage m_ClipImage = null;

        protected override void OnUIInit()
        {
            base.OnUIInit();

            m_ClipImage = GetComponentInChildren<UIClipImage>();
        }

        protected override void OnPanelOpen(params object[] args)
        {

            Transform targetUI = (Transform)args[0];
            float radius = (float)args[1];
            Vector3 offset = (Vector3)args[2];

            RectTransform rt = targetUI.GetComponent<RectTransform>();
            m_ClipImage.ShowClip(rt, radius, offset);

        }

    }
}
