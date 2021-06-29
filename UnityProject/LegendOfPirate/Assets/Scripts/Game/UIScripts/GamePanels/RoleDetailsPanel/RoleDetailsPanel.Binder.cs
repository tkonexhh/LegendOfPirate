using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Linq;
using System;

namespace GameWish.Game
{
    public class RoleDetailsPanelData : UIPanelData
    {
        public RoleGroupModel roleGroupModel;

        public RoleModel curRoleModel = null;
    }

    public partial class RoleDetailsPanel
    {
        public IntReactiveProperty m_RoleIndex;

        private RoleDetailsPanelData m_PanelData = null;

        private bool m_IsLocked;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<RoleDetailsPanelData>();
            m_PanelData.roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
            
        }

        private void InitRoleMsg(int roleId) 
        {
            m_RoleIndex = new IntReactiveProperty(m_PanelData.roleGroupModel.GetRoleIndexById(roleId));
            m_PanelData.curRoleModel = m_PanelData.roleGroupModel.GetRoleModel(roleId);
     
        }

        #region RefreshPanelData
        private void ReleasePanelData()
        {
            ObjectPool<RoleDetailsPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.curRoleModel.level.SubscribeToTextMeshPro(m_RoleLevel, "Lv.{0}").AddTo(this);
            m_PanelData.curRoleModel.equipDic.ObserveCountChanged().Subscribe(count => OnEquipCountChange(count)).AddTo(this);
        }

        private void OnEquipCountChange(int count)
        {
            //var Equips = m_EquipRegion.GetComponentsInChildren<EquipSubpart>();
            //for (int i = 0; i < m_PanelData.curRoleModel.equipDic.Count; i++) 
            //{
            //    if (Equips[(int)m_PanelData.curRoleModel.equipDic[i].equipType].IsLocked) 
            //    {
            //        Equips[(int)m_PanelData.curRoleModel.equipDic[i].equipType].UpdateEquipSubpart(m_PanelData.curRoleModel.equipDic[i]);
            //    }
            //}
        }

        private void BindUIToModel()
        {

        }


        #endregion

        #region RefreshPanelView
        private void RefreshRoleIsUnclockView(bool isUnlock)
        {
            m_StartRegion.gameObject.SetActive(isUnlock);
            m_RoleLevel.gameObject.SetActive(isUnlock);
            m_ExperienceBar.gameObject.SetActive(isUnlock);
            m_EquipRegion.gameObject.SetActive(isUnlock);
        }

        private void RefreshRoleView()
        {

        }

        private void AddSkillItem()
        {
            SoundButton skillBtn = ((GameObject)LoadPageRes("SkillSubpart")).GetComponent<SoundButton>();
            skillBtn.transform.SetParent(m_SkillRegion);
            skillBtn.onClick.AddListener(() =>
            {
                UIMgr.S.OpenPanel(UIID.RoleSkillUpgradePanel, m_PanelData.roleGroupModel.GetRoleModelByIndex(m_RoleIndex.Value).id);
            });
        }
        #endregion
    }
}
