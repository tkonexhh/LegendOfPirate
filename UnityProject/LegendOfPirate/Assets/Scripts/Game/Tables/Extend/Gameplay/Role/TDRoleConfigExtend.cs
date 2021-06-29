using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDRoleConfig
    {
        public void Reset()
        {

        }
        public RoleType GetRoleType() 
        {
            if (m_Type == "Front")
            {
                return RoleType.Front;
            }
            else if (m_Type == "Mid")
            {
                return RoleType.Mid;
            }
            else 
            {
                return RoleType.Back;
            }
        }
    }
}