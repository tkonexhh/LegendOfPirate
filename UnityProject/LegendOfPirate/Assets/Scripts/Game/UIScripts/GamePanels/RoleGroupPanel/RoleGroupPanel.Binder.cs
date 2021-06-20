using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GameWish.Game
{
    public class RoleGroupPanelData : UIPanelData
    {
        //public List<RoleModel> roleModelList = new List<RoleModel>();

        public RoleGroupPanelData()
        {
            //roleModelList = ModelMgr.S.GetModel<RoleGroupModel>().GetSortRoleItemList();
        }
    }

    public partial class RoleGroupPanel
    {
        private RoleGroupPanelData m_PanelData = null;
        public List<RoleModel> roleModelList = new List<RoleModel>();

        private void AllocatePanelData()
        {
            //m_PanelData = UIPanelData.Allocate<RoleGroupPanelData>();

           
        }

        private void OnOpenInit(params object[] args)
        {
            roleModelList = ModelMgr.S.GetModel<RoleGroupModel>().GetSortRoleItemList();
            ScrollView.SetCellRenderer(OnCellRenderer);
            ScrollView.SetDataCount(roleModelList.Count);

            CloseBtn.onClick.AddListener(() =>
            {
                CloseSelfPanel();
            });
        }

        private void OnCellRenderer(Transform root, int index)
        {
            root.GetComponent<RoleGroupItem>().OnInit(roleModelList[index]);
        }

        private void ReleasePanelData()
        {
            ObjectPool<RoleGroupPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
        }

        private void BindUIToModel()
        {
        }

    }
}
