using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 伤害范围 - 扇形
    /// </summary>
    public class DamageRange_Sector : DamageRange
    {
        private float m_Radius;
        private float m_Degree;
        public DamageRange_Sector(IDealDamage owner, float radius, float degree) : base(owner)
        {
            m_Radius = radius;
            m_Degree = degree;
        }

        public override List<BattleRoleController> PickTargets(BattleCamp camp)
        {
            List<BattleRoleController> targets = new List<BattleRoleController>();
            var enemys = BattleMgr.S.BattleRendererComponent.GetControllersByCamp(BattleHelper.GetOppositeCamp(camp));
            for (int i = 0; i < enemys.Count; i++)
            {
                float distance = Vector3.Distance(owner.DamageCenter(), enemys[i].transform.position);
                float angle = Mathf.Acos(Vector3.Dot(owner.DamageForward().normalized, enemys[i].transform.position.normalized)) * Mathf.Rad2Deg;
                if (distance <= m_Radius && angle * 2 < m_Degree)//是否在半径内
                {
                    targets.Add(enemys[i]);
                }
            }
            return targets;
        }
    }

}