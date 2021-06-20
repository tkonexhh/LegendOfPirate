
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;

namespace GameWish.Game
{
	public partial class RoleGroupItem : IUListItemView
	{
		private void Awake()
		{
		}

        public void OnInit(RoleModel roleModel)
        {
            //if (roleModel.isLocked.Value)
            //{
                NameTex.text = roleModel.name;
                ItemLevelTex.text = string.Format("Lv:{0}", roleModel.level.Value);
                ItemBgBtn.onClick.AddListener(() =>
                {
                    UIMgr.S.OpenPanel(UIID.RoleDetailsPanel, roleModel);
                });
            //}
           
        }

    }
}