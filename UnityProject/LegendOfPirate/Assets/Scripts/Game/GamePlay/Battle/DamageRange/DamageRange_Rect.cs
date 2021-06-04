using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 伤害范围 - 矩形
    /// </summary>
    public class DamageRange_Rect : DamageRange
    {
        private float m_Width;
        private float m_Height;

        public DamageRange_Rect(float width, float height) : base()
        {
            m_Width = width;
            m_Height = height;
        }

        public override List<BattleRoleController> PickTargets(Vector3 center)
        {
            Vector3 leftup = center + owner.transform.forward * m_Height * 0.5f - owner.transform.right * m_Width * 0.5f;
            Vector3 rightup = center + owner.transform.forward * m_Height * 0.5f + owner.transform.right * m_Width * 0.5f;
            Vector3 leftdown = center + -owner.transform.forward * m_Height * 0.5f - owner.transform.right * m_Width * 0.5f;
            Vector3 rightdown = center + -owner.transform.forward * m_Height * 0.5f + owner.transform.right * m_Width * 0.5f;
            Debug.LogError(center + ":" + leftup + ":" + rightup + ":" + leftdown + ":" + rightdown);


            List<BattleRoleController> targets = new List<BattleRoleController>();
            var enemys = BattleMgr.S.BattleRendererComponent.GetControllersByCamp(BattleHelper.GetOppositeCamp(owner.camp));
            return targets;
        }
    }

}