using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using TMPro;

namespace GameWish.Game
{
    public class WorldUI_WorkTalk : WorldUIBindTransform
    {
        [SerializeField] private TextMeshProUGUI m_TxtTalk;


        protected override bool IsNeedUpdate()
        {
            return false;
        }

        public void SetStyle(InjuryType injuryType)
        {
            switch (injuryType)
            {
                case InjuryType.CommonInjury:
                    m_TxtTalk.color = Color.white;
                    break;
                case InjuryType.CriticalInjury:
                    m_TxtTalk.color = Color.yellow;
                    break;
                case InjuryType.AbnormalInjury:
                    m_TxtTalk.color = Color.blue;
                    break;
            }
        }

        public void SetText(string text)
        {
            m_TxtTalk.text = text;
        }

        public void UpdatePosition()
        {
            m_Binding?.Update();
        }
    }
}
