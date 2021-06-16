using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using Qarth;
using TMPro;

namespace GameWish.Game
{
    public class FloatTMPMsg
    {
        private string m_Message;

        public string message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }
    }

    public class FloatMessageTMPanel : AbstractPanel
    {
        [SerializeField]
        private GameObject m_Prefab;
        [SerializeField]
        private Transform m_Root;
        [SerializeField]
        private Vector3 m_StartPos;
        [SerializeField]
        private Vector3 m_EndPos;
        [SerializeField]
        private float m_AnimTime = 1.0f;
        [SerializeField]
        private float m_SendOffsetTime = 0.1f;

        private Stack<FloatTMPMsg> m_MsgList;
        private GameObjectPool m_GameObjectPool;
        private float m_LastSendTime;

        private int m_AnimItemCount = 0;
        private bool m_IsOpen = false;

        private bool m_IsInit = false;

        private Color m_InitColor;

        public override int sortIndex
        {
            get
            {
                return UIRoot.FLOAT_PANEL_INDEX;
            }
        }

        protected override void OnUIInit()
        {
            InitFloatMessage();
        }



        protected override void OnOpen()
        {
            m_IsOpen = true;
        }

        protected override void OnClose()
        {
            m_IsOpen = false;
        }

        protected override void OnPanelOpen(params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                return;
            }

            string msg = args[0] as string;

            if (msg == null)
            {
                return;
            }

            PlayFloatMessage(msg);
        }

        public void ShowMsg(string msg)
        {
            if (msg == null)
            {
                return;
            }

            PlayFloatMessage(msg);
        }

        protected override void BeforDestroy()
        {
            if (m_GameObjectPool != null)
            {
                m_GameObjectPool.RemoveAllObject(true, false);
                m_GameObjectPool = null;
            }
        }

        public void PlayFloatMessage(string msg)
        {
            PlayFloatMessage(msg, Vector3.zero, Vector3.zero);
        }

        public void PlayFloatMessage(string msg, Vector3 from, Vector3 to)
        {
            if (UIMgr.isApplicationQuit)
            {
                return;
            }

            FloatTMPMsg fm = new FloatTMPMsg();
            fm.message = msg;
            ShowMsg(fm);
        }

        private void InitFloatMessage()
        {
            m_MsgList = new Stack<FloatTMPMsg>();
            m_GameObjectPool = GameObjectPoolMgr.S.CreatePool("FloatMessageTMPool", m_Prefab, -1, 5, UIPoolStrategy.S);
            m_Prefab.SetActive(false);
            var text = m_Prefab.GetComponentInChildren<TextMeshProUGUI>();
            m_InitColor = text.color;
            m_IsInit = true;
        }


        private bool CheckIsShowAble()
        {
            if (Time.realtimeSinceStartup - m_LastSendTime > m_SendOffsetTime)
            {
                return true;
            }

            return false;
        }

        private void ShowMsg(FloatTMPMsg msgVo, bool check = true)
        {
            if (!m_IsInit)
            {
                InitFloatMessage();
            }

            if (check)
            {
                if (!CheckIsShowAble())
                {
                    m_MsgList.Push(msgVo);
                    return;
                }
            }

            GameObject obj = m_GameObjectPool.Allocate();
            if (obj)
            {
                obj.SetActive(true);
                ++m_AnimItemCount;
                FloatMessageTMPItem item = obj.GetComponentInChildren<FloatMessageTMPItem>();

                item.SetFloatMsg(msgVo);

                obj.transform.SetParent(m_Root, true);

                obj.transform.localPosition = m_StartPos;

                var bg = obj.GetComponentInChildren<Image>();
                var text = obj.GetComponentInChildren<TextMeshProUGUI>();
                if (bg != null)
                    bg.color = new Color(0, 0, 0, 0.6f);
                text.color = m_InitColor;

                obj.transform.DOLocalMove(m_EndPos, m_AnimTime).SetEase(Ease.Linear).OnComplete(() =>
                {
                    if (bg != null)
                        bg.DOFade(0, m_AnimTime);
                    text.DOFade(0, m_AnimTime).OnComplete(() =>
                    {
                        m_GameObjectPool.Recycle(obj);
                        --m_AnimItemCount;
                    });
                });
                m_LastSendTime = Time.realtimeSinceStartup;
            }
        }

        private void Update()
        {
            if (!m_IsOpen)
            {
                return;
            }

            if (m_MsgList.Count != 0)
            {
                if (CheckIsShowAble())
                {
                    ShowMsg(m_MsgList.Pop(), false);
                }
            }
            else if (m_AnimItemCount == 0)
            {
                CloseSelfPanel();
            }
        }
    }
}
