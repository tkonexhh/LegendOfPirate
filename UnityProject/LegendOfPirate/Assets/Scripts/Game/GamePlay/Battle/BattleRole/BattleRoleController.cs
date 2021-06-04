using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using Pathfinding;


namespace GameWish.Game
{
    public class BattleRoleController : RoleController, IDealDamage
    {
        public GameObject gameObject { get; private set; }
        public Transform transform { get; private set; }
        public BattleRoleData Data { get; private set; }
        public BattleRoleRenderer renderer { get; private set; }
        public BattleRoleAI AI { get; private set; }
        public BattleRoleBuff Buff { get; private set; }
        public BattleRoleSkill Skill { get; private set; }

        private List<BattleRoleComponent> m_Components;

        //---- Mono
        public BattleRoleMonoReference MonoReference { get; private set; }
        //----


        public BattleCamp camp { get; private set; }//阵营


        #region Override
        public override void OnInit()
        {
            gameObject = GameObjectPoolMgr.S.Allocate("BattleRole");
            transform = gameObject.transform;
            transform.SetParent(BattleMgr.S.transform);

            renderer = ObjectPool<BattleRoleRenderer>.S.Allocate();
            renderer.OnInit();
            renderer.transform = transform;


            Data = AddBattleRoleComponent(new BattleRoleData(this)) as BattleRoleData;
            AI = AddBattleRoleComponent(new BattleRoleAI(this)) as BattleRoleAI;
            Buff = AddBattleRoleComponent(new BattleRoleBuff(this)) as BattleRoleBuff;
            Skill = AddBattleRoleComponent(new BattleRoleSkill(this)) as BattleRoleSkill;

            base.OnInit();
        }

        public override void OnFirstInit()
        {
            MonoReference = gameObject.GetComponent<BattleRoleMonoReference>();
        }

        public override void OnUpdate()
        {
            renderer.OnUpdate();
            for (int i = 0; i < m_Components.Count; i++)
            {
                m_Components[i].OnUpdate();
            }
        }

        public override void OnDestroyed()
        {
            ObjectPool<BattleRoleRenderer>.S.Recycle(renderer);
            GameObjectPoolMgr.S.Recycle(gameObject);

            renderer = null;
            for (int i = m_Components.Count - 1; i >= 0; i--)
            {
                m_Components[i].OnDestroy();
                m_Components.RemoveAt(i);
                m_Components[i] = null;
            }

        }

        public override void Recycle2Cache()
        {
            base.Recycle2Cache();
        }
        #endregion

        private BattleRoleComponent AddBattleRoleComponent(BattleRoleComponent component)
        {
            if (m_Components == null)
            {
                m_Components = new List<BattleRoleComponent>();
            }

            m_Components.Add(component);
            return component;
        }

        public void SetCamp(BattleCamp camp)
        {
            this.camp = camp;
        }


        public void BattleStart()
        {
            for (int i = 0; i < m_Components.Count; i++)
            {
                m_Components[i].OnBattleStart();
            }
        }

        #region override
        public void DealDamage()
        {
            if (AI.onAttack != null)
            {
                AI.onAttack();
            }
            var targets = Data.DamageRange.PickTargets(transform.position);
            int damage = BattleHelper.CalcAtkDamage(Data.buffedData);
            for (int i = 0; i < targets.Count; i++)
            {
                RoleDamagePackage damagePackage = new RoleDamagePackage();
                damagePackage.damageType = BattleDamageType.Normal;
                damagePackage.damage = damage;
                BattleMgr.S.SendDamage(targets[i], damagePackage);
            }
        }

        #endregion
    }



}