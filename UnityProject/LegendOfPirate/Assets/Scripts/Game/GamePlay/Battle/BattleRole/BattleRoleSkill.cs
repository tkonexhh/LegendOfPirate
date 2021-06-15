using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleSkill : BattleRoleComponent
    {
        private List<Skill> m_SkillLst = new List<Skill>();
        public bool skillReady = false;

        public BattleRoleSkill(BattleRoleController controller) : base(controller)
        {
            //TODO Test Skill
            m_SkillLst.Add(SkillFactory.CreateSkill(BattleMgr.S.DemoSkillSO));
        }

        public void AddSkill(Skill skill)
        {
            m_SkillLst.Add(skill);
        }

        public override void OnBattleStart()
        {
            base.OnBattleStart();
            for (int i = 0; i < m_SkillLst.Count; i++)
            {
                m_SkillLst[i].OnCreate(controller);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!battleStarted) return;
            skillReady = false;

            for (int i = 0; i < m_SkillLst.Count; i++)
            {
                m_SkillLst[i].Update();
                if (m_SkillLst[i].isReady)
                {
                    skillReady = true;
                    break;
                }
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            for (int i = 0; i < m_SkillLst.Count; i++)
            {
                var skill = m_SkillLst[i];
                skill.Release();
                m_SkillLst.RemoveAt(i);
                ObjectPool<Skill>.S.Recycle(skill);
            }
        }

        public Skill GetReadySkill()
        {
            for (int i = 0; i < m_SkillLst.Count; i++)
            {
                if (m_SkillLst[i].isReady)
                    return m_SkillLst[i];

            }
            return null;
        }

    }

}