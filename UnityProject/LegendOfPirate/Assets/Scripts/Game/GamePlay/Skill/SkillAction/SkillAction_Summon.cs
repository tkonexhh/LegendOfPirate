using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_Summon : SkillAction
    {
        private RoleConfigSO m_RoleConfigSO;
        private float m_ATKRate;
        private float m_HPRate;
        private float m_LifeTime;


        private BattleRoleController m_SummonRole;
        private float m_LifeTimer;

        public SkillAction_Summon(RoleConfigSO roleConfig, float atkRate, float hpRate, float lifeTime) //: base(owner)
        {
            this.m_RoleConfigSO = roleConfig;
            this.m_ATKRate = atkRate;
            this.m_HPRate = hpRate;
            this.m_LifeTime = lifeTime;
        }

        public override void ExcuteAction(Skill skill)
        {
            Debug.LogError("召唤啦");
            m_LifeTimer = 0;
            Vector3 summonPos = Random.onUnitSphere * 3.0f + skill.Owner.transform.localPosition;
            summonPos.y = skill.Owner.transform.localPosition.y;
            BattleCamp camp = skill.Owner.camp;

            m_SummonRole = BattleRoleControllerFactory.CreateBattleRole(m_RoleConfigSO);
            m_SummonRole.gameObject.layer = BattleHelper.GetLayerByCamp(camp);
            m_SummonRole.SetCamp(camp);

            m_SummonRole.transform.localPosition = summonPos;
            m_SummonRole.transform.localRotation = skill.Owner.transform.localRotation;
            BattleMgr.S.BattleRendererComponent.GetControllersByCamp(camp).Add(m_SummonRole);

            m_SummonRole.Data.buffedData.BasicATK = (int)(skill.Owner.Data.buffedData.BasicATK * m_ATKRate);
            m_SummonRole.Data.buffedData.MaxHp = (int)(skill.Owner.Data.buffedData.MaxHp * m_HPRate);

            m_SummonRole.BattleStart();

            m_SummonRole.AI.onUpdate += OnUpdate;

            skill.SkillActionStepEnd();
        }

        private void OnUpdate()
        {
            m_LifeTimer += Time.deltaTime;
            if (m_LifeTimer >= m_LifeTime)
            {
                Debug.LogError("Summon Dead");
                m_SummonRole.AI.onUpdate = null;
                m_SummonRole.AI.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.Dead);
            }
        }
    }

}