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
        public RoleGroupModel roleGroupModel;
        public int roleType;
        public RoleGroupPanelData()
        {
            //roleModelList = ModelMgr.S.GetModel<RoleGroupModel>().GetSortRoleItemList();
        }
    }

    public partial class RoleGroupPanel
    {
        private RoleGroupPanelData m_PanelData = null;
        public List<RoleModel> roleModelList = null;

        private void AllocatePanelData()
        {
            m_PanelData = UIPanelData.Allocate<RoleGroupPanelData>();
           m_PanelData.roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
           
        }

        private void OnOpenInit(params object[] args)
        {
            roleModelList = ModelMgr.S.GetModel<RoleGroupModel>().GetSortRoleItemList();
            m_ScrollView.SetCellRenderer(OnCellRenderer);
            m_ScrollView.SetDataCount(roleModelList.Count);
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
            m_PanelData.roleGroupModel.m_RoleItemList.ObserveCountChanged().Subscribe(count => OnRoleValueChange(count)).AddTo(this);
            m_PanelData.roleGroupModel.m_RoleUnlockedItemList.ObserveCountChanged().Subscribe(count => OnRoleValueChange(count)).AddTo(this);
        }

        private void OnRoleValueChange(int count)
        {
            roleModelList = m_PanelData.roleGroupModel.GetSortRoleItemList(m_PanelData.roleType);
            m_ScrollView.SetDataCount(roleModelList.Count);
        }

        private void BindUIToModel()
        {
        }

        private void SetGroupList(int type) 
        {
            
            if (type <= 0)
            {
                roleModelList = ModelMgr.S.GetModel<RoleGroupModel>().GetSortRoleItemList();
                m_ScrollView.SetDataCount(roleModelList.Count);
                m_PanelData.roleType = 0;
            }
            else 
            {
                var roleType = (RoleType)type;
                roleModelList = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModelsByType(roleType);
                m_ScrollView.SetDataCount(roleModelList.Count);
                m_PanelData.roleType = type;
            }
        }

    }
}
