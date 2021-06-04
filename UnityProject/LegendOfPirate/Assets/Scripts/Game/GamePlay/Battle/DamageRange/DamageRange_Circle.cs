using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 伤害范围 - 圆形
    /// </summary>
    public class DamageRange_Circle : DamageRange
    {
        private float m_Radius;

        public DamageRange_Circle(IDealDamage owner, float radius) : base(owner)
        {
            m_Radius = radius;
        }

        public override List<BattleRoleController> PickTargets(BattleCamp camp)
        {
            List<BattleRoleController> targets = new List<BattleRoleController>();
            var enemys = BattleMgr.S.BattleRendererComponent.GetControllersByCamp(BattleHelper.GetOppositeCamp(camp));
            for (int i = 0; i < enemys.Count; i++)
            {
                if (Vector3.Distance(owner.DamageCenter(), enemys[i].transform.position) <= m_Radius)
                {
                    targets.Add(enemys[i]);
                }
            }
            return targets;
        }
    }

}