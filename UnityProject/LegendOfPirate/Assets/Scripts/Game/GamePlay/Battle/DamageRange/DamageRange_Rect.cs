﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameWish.Game
{
    /// <summary>
    /// 伤害范围 - 矩形
    /// </summary>
    public class DamageRange_Rect : DamageRange
    {

        private float m_Width;
        private float m_Height;

        public DamageRange_Rect(IDealDamage owner, float width, float height) : base(owner)
        {
            m_Width = width;
            m_Height = height;
        }

        public override List<BattleRoleController> PickTargets(BattleCamp camp)
        {
            List<BattleRoleController> targets = new List<BattleRoleController>();
            Vector3 right = Quaternion.Euler(0, 90, 0) * owner.DamageForward();
            Vector3 leftup = owner.DamageCenter() + owner.DamageForward() * m_Height * 0.5f - right * m_Width * 0.5f;
            Vector3 rightup = owner.DamageCenter() + owner.DamageForward() * m_Height * 0.5f + right * m_Width * 0.5f;
            Vector3 leftdown = owner.DamageCenter() + -owner.DamageForward() * m_Height * 0.5f - right * m_Width * 0.5f;
            Vector3 rightdown = owner.DamageCenter() + -owner.DamageForward() * m_Height * 0.5f + right * m_Width * 0.5f;

            //利用叉乘来框定区域
            //上左
            Vector3 upLine = leftup - rightup;
            Vector3 leftLine = leftup - leftdown;

            //右下
            Vector3 downLine = rightdown - leftdown;
            Vector3 rightLine = rightdown - rightup;

            var enemys = BattleMgr.S.Role.GetControllersByCamp(BattleHelper.GetOppositeCamp(camp));
            for (int i = 0; i < enemys.Count; i++)
            {
                Vector3 p = enemys[i].transform.position;
                float dUp = Vector3.Dot(upLine, leftup - p);
                float dLeft = Vector3.Dot(leftLine, leftup - p);

                if (!(dUp >= 0 && dUp <= 1 && dLeft >= 0 && dLeft <= 1))//都没有在右下角 下面就不用算了
                {
                    continue;
                }

                float dRight = Vector3.Dot(rightLine, rightdown - p);
                float dDown = Vector3.Dot(downLine, rightdown - p);
                if (dRight >= 0 && dRight <= 1 && dDown >= 0 && dDown <= 1)
                {
                    enemys.Add(enemys[i]);
                }
            }

            return targets;
        }


    }

}