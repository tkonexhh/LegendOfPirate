using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{

    public abstract class Picker
    {
        protected List<BattleRoleController> GetTargets(List<BattleRoleController> roles, Transform transform)
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

        public void DealWithRange(List<BattleRoleController> roles, Transform transform, Action<BattleRoleController> callback)
        {
            var hits = GetTargets(roles, transform);
            for (int i = 0; i < hits.Count; i++)
            {
                callback(hits[i]);
            }
        }

        public abstract bool InRange(BattleRoleController role, Transform center);
    }

    public abstract class Picker1
    {
        protected List<BattleRoleController> GetTargets()
        {
            List<BattleRoleController> targets = new List<BattleRoleController>();
            List<BattleRoleController> roles = BattleMgr.S.Role.GetControllersByCamp(BattleCamp.Our);
            for (int i = 0; i < roles.Count; i++)
            {
                if (DealWithTarget(roles[i]))
                {
                    targets.Add(roles[i]);
                }
            }
            return targets;
        }

        public void DealWithPicker(Action<BattleRoleController> callback)
        {
            var picks = GetTargets();
            for (int i = 0; i < picks.Count; i++)
            {
                callback(picks[i]);
            }
        }

        protected virtual bool DealWithTarget(BattleRoleController role)
        {
            return false;
        }
    }

    public abstract class RangePicker : Picker1
    {

        protected override bool DealWithTarget(BattleRoleController role)
        {
            return false;
        }
    }


    public class RangeDamage_Circle : Picker
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

    public class RangeDamage_Rect : Picker
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

    public class RangeDamage_Sector : Picker
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

    public class RangeDamage_All : Picker
    {
        private BattleCamp m_Camp;

        public RangeDamage_All(BattleCamp camp)
        {
            m_Camp = camp;
        }

        public override bool InRange(BattleRoleController role, Transform center)
        {
            return true;
        }
    }


}