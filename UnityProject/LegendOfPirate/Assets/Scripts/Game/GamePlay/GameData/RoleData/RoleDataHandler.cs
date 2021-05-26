using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class RoleDataHandler : DataHandlerBase<PlayerInfoData>
    {
        public RoleDataHandler()
        {

        }

        public override void LoadData()
        {
            base.LoadData();
        }

        public virtual void Save()
        {
            base.Save();
        }
    }
}