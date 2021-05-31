using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleBuff
    {
        private BattleRoleController m_Controller;
        private List<Buff> m_BuffList = new List<Buff>();
        private Dictionary<int, Buff> m_BuffMap = new Dictionary<int, Buff>();


        public BattleRoleBuff(BattleRoleController controller)
        {
            m_Controller = controller;
        }

        public void OnUpdate()
        {
            for (int i = m_BuffList.Count - 1; i >= 0; i--)
            {
                m_BuffList[i].time -= Time.deltaTime;
                if (m_BuffList[i].time <= 0)
                {
                    RemoveBuff(m_BuffList[i]);
                }
            }
        }

        public void AddBuff(Buff buff)
        {
            if (m_BuffMap.ContainsKey(buff.id))
            {
                //Handle Append

                //1 处理层数
                buff.AddAppendNum();
                //2 时间处理
                var appendHandle = BuffFactory.GetAppendHandler(buff.id);
                if (appendHandle != null)
                {
                    appendHandle.HandleApped(m_BuffMap[buff.id], buff);
                }

                return;
            }
            //不同ID的buff
            buff.OnAddBuff(m_Controller.Data.buffedData);
            m_BuffList.Add(buff);
            m_BuffMap.Add(buff.id, buff);
        }

        public void RemoveBuff(Buff buff)
        {
            if (!m_BuffList.Contains(buff))
            {
                return;
            }
            buff.OnRemoveBuff(m_Controller.Data.buffedData);
            m_BuffList.Remove(buff);
            m_BuffMap.Remove(buff.id);
        }

        public void RemoveBuff(int id)
        {
            if (m_BuffMap.ContainsKey(id))
            {
                RemoveBuff(m_BuffMap[id]);
            }
        }

        public void RemoveAllBuff()
        {
            for (int i = m_BuffList.Count - 1; i >= 0; i--)
            {
                RemoveBuff(m_BuffList[i]);
            }
        }
    }

}