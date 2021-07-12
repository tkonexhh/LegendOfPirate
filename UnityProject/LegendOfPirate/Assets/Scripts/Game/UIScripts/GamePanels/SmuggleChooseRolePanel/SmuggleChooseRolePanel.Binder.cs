using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GameWish.Game
{
    public class SmuggleChooseRolePanelData : UIPanelData
    {
        public SmuggleOrderModel orderModel;
        public RoleGroupModel roleGroupModel;
        public SmuggleChooseRolePanelData()
        {
        }
    }

    public partial class SmuggleChooseRolePanel
    {
        private SmuggleChooseRolePanelData m_PanelData = null;
        private Transform[] m_SelectRoleItems; 

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<SmuggleChooseRolePanelData>();

        }

        private void ReleasePanelData()
        {
            ObjectPool<SmuggleChooseRolePanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.orderModel.needRefresh.Where(need => need).Subscribe(_ => RefreshSelectedRole()).AddTo(this);
        }

        private void RefreshSelectedRole()
        {
            var RoleItems = m_WarShipSelectedRegion.GetComponentsInChildren<SmuggleSelectRoleItem>();
            for (int i = 0; i < m_PanelData.orderModel.roleList.Count; i++) 
            {
                RoleItems[i].RefreshItem(m_PanelData.orderModel.roleList[i].id);
            }
        }

        private void BindUIToModel()
        {
        }
        private void OnClickAddListener()
        {
            InitRoleSelectItem();
            m_BgBtn.OnClickAsObservable().Subscribe(_ =>
            {
                ExitBtnEvent();
            });
        }

        private void InitRoleSelectItem()
        {
            var RoleItems = m_WarShipSelectedRegion.GetComponentsInChildren<SmuggleSelectRoleItem>();
            foreach (var item in RoleItems) 
            {
                item.InitItem();
            }
        }
    }
}
