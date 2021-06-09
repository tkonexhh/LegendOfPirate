using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Qarth;

namespace GFrame
{
    public class GTextMeshProUGUI : TextMeshProUGUI
    {

        public string I18Nkey;


        protected override void Awake()
        {
            base.Awake();
            Translate();
        }

        private void Translate()
        {
            if (!string.IsNullOrEmpty(I18Nkey))
            {
                text = string.Format(TDLanguageTable.Get(I18Nkey));
            }
        }
    }

}