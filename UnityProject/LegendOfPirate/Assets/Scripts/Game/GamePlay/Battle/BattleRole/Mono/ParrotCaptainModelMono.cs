using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class ParrotCaptainModelMono : RoleModelMonoReference
    {
        public GameObject weapon;//刀
        public GameObject skill;//鹦鹉 使用技能使用


        private void Start()
        {
            onAnimSkillStart += OnSkillStart;
            onAnimSkillEnd += OnSkillEnd;
        }


        private void OnDestroy()
        {
            onAnimSkillStart -= OnSkillStart;
            onAnimSkillEnd -= OnSkillEnd;
        }


        private void OnSkillStart()
        {
            weapon.SetActive(false);
            skill.SetActive(true);
        }

        private void OnSkillEnd()
        {
            weapon.SetActive(true);
            skill.SetActive(false);
        }
    }

}