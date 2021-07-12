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
         
        }

        private void BindUIToModel()
        {
        }
        private void OnClickAddListener()
        {
           m_BgBtn.OnClickAsObservable().Subscribe(_ =>
            {
                ExitBtnEvent();
            });
        }
    }
}
