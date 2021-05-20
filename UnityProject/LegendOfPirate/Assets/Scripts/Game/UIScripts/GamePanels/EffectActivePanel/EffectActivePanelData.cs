using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class EffectActivePanelData
    {
        public string bgImageName = string.Empty;
        public string titleText = string.Empty;
        public string descriptionText = string.Empty;
        public string contentImageName = string.Empty;
        public Action confirmCallback;
        public Action closeCallback;

        public EffectActivePanelData(string bgImageName, string titleText,
            string descriptionText, string contentImageName, Action confirmCallback = null,
            Action closeCallback = null)
        {
            this.bgImageName = bgImageName;
            this.titleText = titleText;
            this.descriptionText = descriptionText;
            this.contentImageName = contentImageName;
            this.confirmCallback = confirmCallback;
            this.closeCallback = closeCallback;
        }
    }
}