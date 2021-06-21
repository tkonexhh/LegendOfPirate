using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class Bullet : IDealDamage
    {
        public GameObject gameObject { get; private set; }
        public Transform transform { get; private set; }

        public GameObject prefab;
        public IDealDamage owner;//所有者，可以是Role也可以是Skill

        public BulletMove move;

        public RangeDamage RangeDamage { get; set; }//伤害范围
        public int Damage { get; set; }


        public void Init(Transform root)//初始位置
        {
            gameObject = GameObject.Instantiate(prefab);//TODO 改为池
            transform = gameObject.transform;
            transform.SetParent(BattleMgr.S.transform);//TODO 改为子容器
            transform.position = root.position;
            transform.rotation = root.rotation;

            move.BulletTrans = this.transform;
        }

        public void Update()
        {
            move.MoveToTarget();
            if (move.Reached())
            {
                Debug.LogError("Reached");
                DealDamage();
                BattleMgr.S.Bullet.RemoveBullet(this);
            }
        }

        public void Release()
        {
            BattleMgr.Destroy(gameObject);
            gameObject = null;
            transform = null;
            owner = null;
            move = null;
        }

        #region IDealDamage
        public void DealDamage()
        {
            Debug.LogError("DealDamage");

            if (RangeDamage == null)
            {

            }
            else
            {
                int damage = BattleHelper.CalcAtkDamage(GetATKModel());
                RoleDamagePackage damagePackage = new RoleDamagePackage();
                damagePackage.damageType = BattleDamageType.Normal;
                damagePackage.damage = damage;
                var roles = BattleMgr.S.Role.GetControllersByCamp(GetBattleCamp());
                RangeDamage.DealDamage(roles, transform, damagePackage);
            }


        }

        public Transform DamageTransform()
        {
            return null;
        }

        public RangeDamage GetRangeDamage()
        {
            return null;
        }
        #endregion

        private BattleCamp GetBattleCamp()
        {
            if (owner is BattleRoleController role)
            {
                return role.camp;
            }
            else if (owner is Skill skill)
            {
                return skill.Owner.camp;
            }

            return BattleCamp.Enemy;
        }

        private BattleRoleRuntimeModel GetATKModel()
        {
            if (owner is BattleRoleController role)
            {
                return role.Data.buffedData;
            }
            else if (owner is Skill skill)
            {
                return skill.Owner.Data.buffedData;
            }
            return null;
        }
    }

}