using System.Collections;
using System.Collections.Generic;
using QuickEngine.Extensions;
using UnityEngine;

namespace GameWish.Game
{
    public class ContentSizeFitter : MonoBehaviour
    {
        public float offset;

        public void BuildRect()
        {
            bool m_First = false;
            float m_TopPosy = -1;
            float m_BottomPosy = -1;

            Transform t;

            for (int i = 0; i < transform.childCount; i++)
            {
                t = transform.GetChild(i);
                if (t.gameObject.activeSelf == false)
                    continue;

                if (!m_First)
                {
                    m_First = true;
                    m_TopPosy = t.localPosition.y + t.rectTransform().GetHeight() * (1 - t.rectTransform().pivot.y);
                    m_BottomPosy = t.localPosition.y - t.rectTransform().GetHeight() * t.rectTransform().pivot.y;
                }

                float posy = t.localPosition.y + t.rectTransform().GetHeight() * (1 - t.rectTransform().pivot.y);
                if (posy > m_TopPosy)
                {
                    m_TopPosy = posy;
                }

                posy = t.localPosition.y - t.rectTransform().GetHeight() * t.rectTransform().pivot.y;
                if (posy < m_BottomPosy)
                {
                    m_BottomPosy = posy;
                }
            }
            transform.rectTransform().SetHeight(m_TopPosy - m_BottomPosy + offset);
        }
    }
}