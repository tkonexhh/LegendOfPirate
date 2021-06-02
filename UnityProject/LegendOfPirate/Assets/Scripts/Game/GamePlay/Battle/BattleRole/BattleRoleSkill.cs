using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleSkill : BattleRoleComponent
    {
        private List<Skill> m_SkillLst = new List<Skill>();

        public BattleRoleSkill(BattleRoleController controller) : base(controller)
        {
            //TODO Test Skill
            m_SkillLst.Add(SkillFactory.CreateSkill(BattleMgr.S.DemoSkillSO));
        }

        public override void OnBattleStart()
        {
            base.OnBattleStart();
            for (int i = 0; i < m_SkillLst.Count; i++)
            {
                m_SkillLst[i].timer = 0;
                if (m_SkillLst[i] is PassiveSkill)
                {
                    m_SkillLst[i].Cast(controller);
                }
            }
        }

        public override void OnUpdate()
        {
            if (!battleStarted) return;

            for (int i = 0; i < m_SkillLst.Count; i++)
            {
                m_SkillLst[i].timer += Time.deltaTime;
                m_SkillLst[i].timer = Mathf.Clamp(m_SkillLst[i].timer, 0, m_SkillLst[i].cd);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

    }

}