using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleBuff : BattleRoleComponent
    {
        private List<Buff> m_BuffList = new List<Buff>();
        private Dictionary<int, Buff> m_BuffMap = new Dictionary<int, Buff>();


        public BattleRoleBuff(BattleRoleController controller) : base(controller) { }

        public override void OnBattleStart()
        {
            base.OnBattleStart();
            m_BuffMap.Clear();
            m_BuffList.Clear();
        }

        public override void OnUpdate()
        {
            if (!battleStarted) return;

            for (int i = m_BuffList.Count - 1; i >= 0; i--)
            {
                if (m_BuffList[i].time != SkillDefine.INFINITETIME)//-1 是永久持续
                {
                    m_BuffList[i].time -= Time.deltaTime;
                    if (m_BuffList[i].time <= 0)
                    {
                        RemoveBuff(m_BuffList[i]);
                    }
                }
            }
        }

        public void AddBuff(Buff buff)
        {
            buff.Owner = controller;
            if (m_BuffMap.ContainsKey(buff.id))
            {
                //Handle Append
                var buffStaticInfo = BuffFactory.GetBuffStaticInfo(buff.id);
                if (buffStaticInfo != null && buffStaticInfo.appendHandler != null)
                {
                    //层数处理
                    m_BuffMap[buff.id].nowAppendNum++;
                    m_BuffMap[buff.id].nowAppendNum = Mathf.Min(m_BuffMap[buff.id].nowAppendNum, buffStaticInfo.maxAppendNum);
                    m_BuffMap[buff.id].OnAddAppendNum(controller.Data.buffedData);
                    buffStaticInfo.appendHandler?.HandleApped(m_BuffMap[buff.id], buff);

                }
                return;
            }
            // Debug.LogError("AddBuff");
            //不同ID的buff
            m_BuffList.Add(buff);
            m_BuffMap.Add(buff.id, buff);
            buff.OnAddBuff();
        }

        public void RemoveBuff(Buff buff)
        {
            Debug.LogError("RemoveBuff");
            if (!m_BuffList.Contains(buff))
            {
                return;
            }

            var buffStaticInfo = BuffFactory.GetBuffStaticInfo(buff.id);
            if (buffStaticInfo != null && buffStaticInfo.appendHandler != null)
            {
                m_BuffMap[buff.id].nowAppendNum = 0;
            }


            buff.OnRemoveBuff();
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