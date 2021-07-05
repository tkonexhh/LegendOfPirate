using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    /// <summary>
    /// 角色模型的MonoReference
	/// 用于检测动画事件
	/// 
    /// </summary>
    public class RoleModelMonoReference : MonoBehaviour
    {
        //事件说明
        //Attack 近战攻击事件 远程攻击发射子弹

        public Run onAnimAttack;
        public Run onAnimSkillStart;
        public Run onAnimSkillEnd;


        /// <summary>
        /// 开始施法
        /// </summary>
        public void SkillStart()
        {
            if (onAnimSkillStart != null)
            {
                onAnimSkillStart();
            }
        }

        public void SkillEnd()
        {
            if (onAnimSkillEnd != null)
            {
                onAnimSkillEnd();
            }
        }

        public void Attack()//攻击动画事件
        {
            if (onAnimAttack != null)
            {
                onAnimAttack();
            }
        }


    }

}