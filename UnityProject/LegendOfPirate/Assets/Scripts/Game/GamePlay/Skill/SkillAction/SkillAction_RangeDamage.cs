using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_RangeDamage : SkillAction
    {
        private RangeDamage m_RangeDamage;
        private SkillTargetType m_TargetType;
        private int m_Damage;

        public SkillAction_RangeDamage(RangeDamage rangeDamage, SkillTargetType targetType, int damage) //: base(owner)
        {
            this.m_RangeDamage = rangeDamage;
            this.m_TargetType = targetType;
        }

        public override void ExcuteAction(Skill skill)
        {
            var oppoCamp = BattleHelper.GetOppositeCamp(skill.Owner.camp);
            var roles = BattleMgr.S.Role.GetControllersByCamp(oppoCamp);
            Transform transform = skill.TargetInfo.Target.transform;
            switch (m_TargetType)
            {
                case SkillTargetType.Caster: transform = skill.TargetInfo.Caster.transform; break;
                case SkillTargetType.Target: transform = skill.TargetInfo.Target.transform; break;

            }

            RoleDamagePackage damagePackage = new RoleDamagePackage();
            damagePackage.damageType = BattleDamageType.Skill;
            damagePackage.damage = m_Damage;

            m_RangeDamage.DealDamage(roles, transform, damagePackage);

            skill.SkillActionStepEnd();
        }
    }

    public abstract class RangeDamage
    {
        private List<BattleRoleController> GetTargets(List<BattleRoleController> roles, Transform transform)
        {
            List<BattleRoleController> hits = new List<BattleRoleController>();
            for (int i = 0; i < roles.Count; i++)
            {
                if (InRange(roles[i], transform))
                {
                    hits.Add(roles[i]);
                }
            }
            return hits;
        }

        public void DealDamage(List<BattleRoleController> roles, Transform transform, RoleDamagePackage damagePackage)
        {
            var hits = GetTargets(roles, transform);
            for (int i = 0; i < hits.Count; i++)
            {
                BattleMgr.S.SendDamage(hits[i], damagePackage);
            }
        }

        public abstract bool InRange(BattleRoleController role, Transform center);
    }

    public class RangeDamage_Circle : RangeDamage
    {
        private float m_Radius;

        public RangeDamage_Circle(float radius)
        {
            this.m_Radius = radius;
        }

        public override bool InRange(BattleRoleController role, Transform center)
        {
            return Vector3.Distance(role.transform.position, center.position) <= m_Radius; ;
        }
    }

    public class RangeDamage_Rect : RangeDamage
    {
        private float m_Width;
        private float m_Height;
        public RangeDamage_Rect(float width, float height)
        {
            this.m_Width = width;
            this.m_Height = height;
        }


        public override bool InRange(BattleRoleController role, Transform transform)
        {
            Vector3 right = Quaternion.Euler(0, 90, 0) * transform.forward;
            Vector3 leftup = transform.position + transform.forward * m_Height * 0.5f - right * m_Width * 0.5f;
            Vector3 rightup = transform.position + transform.forward * m_Height * 0.5f + right * m_Width * 0.5f;
            Vector3 leftdown = transform.position + -transform.forward * m_Height * 0.5f - right * m_Width * 0.5f;
            Vector3 rightdown = transform.position + -transform.forward * m_Height * 0.5f + right * m_Width * 0.5f;

            //利用叉乘来框定区域
            //上左
            Vector3 upLine = leftup - rightup;
            Vector3 leftLine = leftup - leftdown;

            //右下
            Vector3 downLine = rightdown - leftdown;
            Vector3 rightLine = rightdown - rightup;


            Vector3 p = role.transform.position;
            float dUp = Vector3.Dot(upLine, leftup - p);
            float dLeft = Vector3.Dot(leftLine, leftup - p);

            if (!(dUp >= 0 && dUp <= 1 && dLeft >= 0 && dLeft <= 1))//都没有在右下角 下面就不用算了
            {
                float dRight = Vector3.Dot(rightLine, rightdown - p);
                float dDown = Vector3.Dot(downLine, rightdown - p);
                if (dRight >= 0 && dRight <= 1 && dDown >= 0 && dDown <= 1)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class RangeDamage_Sector : RangeDamage
    {
        private float m_Radius;
        private float m_Degree;

        public RangeDamage_Sector(float radius, float degree)
        {
            this.m_Radius = radius;
            this.m_Degree = degree;
        }

        public override bool InRange(BattleRoleController role, Transform center)
        {
            float distance = Vector3.Distance(role.transform.position, center.position);
            float angle = Mathf.Acos(Vector3.Dot(center.forward, role.transform.position.normalized)) * Mathf.Rad2Deg;
            if (distance <= m_Radius && angle * 2 < m_Degree)//是否在半径内
            {
                return true;
            }

            return false;
        }
    }

}