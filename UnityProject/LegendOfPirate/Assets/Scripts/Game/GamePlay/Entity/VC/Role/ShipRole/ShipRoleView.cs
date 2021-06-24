using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class ShipRoleView : ClickableView
	{
        private RoleMonoReference m_RoleMonoRef;
        private PlayablesAnimation m_PlayableAnim;

        public RoleMonoReference RoleMonoRef { get => m_RoleMonoRef; }

        protected override void Awake()
        {
            base.Awake();

            //m_Collider = GetComponentInChildren<Collider>();
        }

        public override int GetSortingLayer()
        {
            return Define.INPUT_SORTING_ORDER_SHIP_ROLE;
        }

        protected override void OnClicked()
        {
            base.OnClicked();

            Log.i("On Role Clicked");
        }

        public void Init()
        {
            m_RoleMonoRef = GetComponent<RoleMonoReference>();
            m_PlayableAnim = GetComponentInChildren<PlayablesAnimation>();
        }

        public void PlayAnim(string anim)
        {
            m_PlayableAnim.Play(anim);
        }

        public void SetTargetPos(Vector3 pos)
        {
            //Log.e("Role Set Target Pos: " + pos.ToString());
            m_RoleMonoRef.AstarAI.maxSpeed = 10;
            m_RoleMonoRef.AstarAI.canMove = true;
            m_RoleMonoRef.AstarAI.destination = pos;
        }
    }
	
}