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
        public BattleRoleRenderer Renderer { get; private set; }
        public BattleRoleAI AI { get; private set; }
        public BattleRoleBuff Buff { get; private set; }
        public BattleRoleSkill Skill { get; private set; }

        private List<BattleRoleComponent> m_Components;

        //---- Mono
        public BattleRoleMonoReference MonoReference { get; private set; }
        //----


        public BattleCamp camp { get; private set; }//阵营

        public Run onUpdate;

        public BattleRoleController()
        {
            Renderer = AddBattleRoleComponent(new BattleRoleRenderer(this)) as BattleRoleRenderer;
            Data = AddBattleRoleComponent(new BattleRoleData(this)) as BattleRoleData;
            AI = AddBattleRoleComponent(new BattleRoleAI(this)) as BattleRoleAI;
            Buff = AddBattleRoleComponent(new BattleRoleBuff(this)) as BattleRoleBuff;
            Skill = AddBattleRoleComponent(new BattleRoleSkill(this)) as BattleRoleSkill;
        }

        #region Override
        public override void OnInit()
        {
            gameObject = GameObjectPoolMgr.S.Allocate("BattleRole");
            transform = gameObject.transform;
            transform.SetParent(BattleMgr.S.transform);

            base.OnInit();
            Renderer.OnInit();
            MonoReference.Collider.enabled = false;
        }

        public override void OnFirstInit()
        {
            MonoReference = gameObject.GetComponent<BattleRoleMonoReference>();
            MonoReference.ShootPos = transform;
        }

        public override void OnUpdate()
        {
            for (int i = 0; i < m_Components.Count; i++)
            {
                m_Components[i].OnUpdate();
            }

            if (onUpdate != null)
            {
                onUpdate();
            }
        }

        public override void OnDestroyed()
        {
            for (int i = m_Components.Count - 1; i >= 0; i--)
            {
                var c = m_Components[i];
                c.OnDestroy();
                m_Components.RemoveAt(i);
                c = null;
            }
            GameObjectPoolMgr.S.Recycle(gameObject);
        }

        public override void Recycle2Cache()
        {
            base.Recycle2Cache();
            OnDestroyed();
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
                Debug.LogError("Attack");
                AI.onAttack();
            }

            int damage = BattleHelper.CalcAtkDamage(Data.buffedData);
            RoleDamagePackage damagePackage = new RoleDamagePackage();
            damagePackage.damageType = BattleDamageType.Normal;
            damagePackage.damage = damage;

            if (Data.RangeDamage == null)//不是范围伤害
            {
                BattleMgr.S.SendDamage(AI.Target, damagePackage);
            }
            else
            {
                var roles = BattleMgr.S.Role.GetControllersByCamp(camp);
                Data.RangeDamage.DealDamage(roles, transform, damagePackage);
            }
        }
        #endregion
    }



}