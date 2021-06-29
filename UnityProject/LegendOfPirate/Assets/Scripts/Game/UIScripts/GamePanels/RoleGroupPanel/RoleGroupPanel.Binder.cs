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
           // m_PanelData.roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
           
        }

        private void OnOpenInit(params object[] args)
        {
            roleModelList = ModelMgr.S.GetModel<RoleGroupModel>().GetSortRoleItemList();
            m_ScrollView.SetCellRenderer(OnCellRenderer);
            m_ScrollView.SetDataCount(roleModelList.Count);
        }
        private void InitUIListener() 
        {

            m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
            var toggles = m_ToggleGroup.GetComponentsInChildren<Toggle>();
            for (int i = -1; i < toggles.Length - 1; i++) 
            {
                toggles[0].OnValueChangedAsObservable().Subscribe(_ => SetGroupList(i)).AddTo(this);
            }
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

        private void SetGroupList(int type) 
        {
            if (type < 0)
            {
                roleModelList = ModelMgr.S.GetModel<RoleGroupModel>().GetSortRoleItemList();
                m_ScrollView.SetDataCount(roleModelList.Count);
            }
            else 
            {
                var roleType = (RoleType)type;
                roleModelList = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModelsByType(roleType);
                m_ScrollView.SetDataCount(roleModelList.Count);
            }
        }

    }
}
