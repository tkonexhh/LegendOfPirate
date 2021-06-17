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
        public List<RoleModel> roleModelList = new List<RoleModel>();

        public RoleGroupPanelData()
        {
            roleModelList = ModelMgr.S.GetModel<RoleGroupModel>().GetSortRoleItemList();
        }
    }

    public partial class RoleGroupPanel
    {
        private RoleGroupPanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<RoleGroupPanelData>();

            ScrollView.SetCellRenderer(OnCellRenderer);
            ScrollView.SetDataCount(m_PanelData.roleModelList.Count);

            CloseBtn.onClick.AddListener(() =>
            {
                CloseSelfPanel();
            });
        }

        private void OnCellRenderer(Transform root, int index)
        {
            //root.GetComponent<RoleGroupItem>().OnInit(m_PanelData.roleModelList[index]);
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
