using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class Bullet
    {
        public GameObject gameObject { get; private set; }
        public Transform transform { get; private set; }


        public string prefabName;
        public BattleRoleController owner;

        public BulletMove move;

        public RangeDamage RangeDamage { get; set; }//伤害范围
        public int Damage { get; set; }


        public void Init(Transform root)//初始位置
        {
            gameObject = BattleMgr.S.Pool.GetGameObject(prefabName);
            transform = gameObject.transform;
            transform.SetParent(BattleMgr.S.bulletRoot);
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


        private void DealDamage()
        {
            Debug.LogError("DealDamage");

            if (RangeDamage == null)//单体攻击
            {

            }
            else
            {
                int damage = Damage;
                RoleDamagePackage damagePackage = new RoleDamagePackage();
                damagePackage.damageType = BattleDamageType.Normal;
                damagePackage.damage = damage;
                var roles = BattleMgr.S.Role.GetControllersByCamp(owner.camp);
                RangeDamage.DealDamage(roles, transform, damagePackage);
            }
        }



    }

}