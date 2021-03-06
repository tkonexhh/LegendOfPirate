using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Collections.Generic;

namespace GameWish.Game
{
    public partial class RoleDetailsPanel : AbstractAnimPanel
    {
        #region SerializeField
        [SerializeField] private GameObject m_SkillSubpart;
        #endregion
        #region Data
        private List<SkillSubpart> m_RoleSkillSubs = new List<SkillSubpart>();
        #endregion
        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();

            AllocatePanelData();

            OnClickAddListener();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            RegisterEvents();

            // HandleTransmitValue(args);
            var item = args[0] as RoleModel;
            InitRoleMsg((item.id));

            BindModelToUI();
            BindUIToModel();


            InitData();
            if (!m_IsLocked) RefreshRoleIsUnclockView();

            OpenDependPanel(EngineUI.MaskPanel, -1);
        }

        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();

            CloseSelfPanel();
        }

        protected override void OnClose()
        {
            base.OnClose();

            ReleasePanelData();
            UnregisterEvents();
        }
        #endregion

        #region OnClickAddListener
        private void OnClickAddListener()
        {
            m_StoryBtn.OnClickAsObservable().Subscribe(_ => { OpenRoleStoryPanel(); }).AddTo(this);
            m_CloseBtn.OnClickAsObservable().Subscribe(_ => { HideSelfWithAnim(); }).AddTo(this);
            m_UpgradeMaterialsBtn.OnClickAsObservable().Subscribe(_ => { OpenRoleLevelUpPanel(); }).AddTo(this);
            m_PreRoleBtn.OnClickAsObservable().Subscribe(_ => { OnPreRoleBtnClick(); }).AddTo(this);
            m_NextRoleBtn.OnClickAsObservable().Subscribe(_ => { OnNextRoleBtnClick(); }).AddTo(this);
        }



        #endregion

        #region EventSystem
        private void RegisterEvents()
        {

        }

        private void UnregisterEvents()
        {

        }
        #endregion
        #region Private

        private void OnPreRoleBtnClick() 
        {
            FloatMessageTMP.S.ShowMsg("This Is First Role");
        }

        private void OnNextRoleBtnClick() 
        {
            FloatMessageTMP.S.ShowMsg("This Is Last Role");
        }

        private void OpenRoleLevelUpPanel()
        {
            UIMgr.S.OpenTopPanel(UIID.RoleGrowthPanel, null, m_PanelData.curRoleModel.id);
            HideSelfWithAnim();
        }

        private void OpenRoleStoryPanel()
        {
            UIMgr.S.OpenPanel(UIID.RoleStoryPanel, m_PanelData.curRoleModel.id);
        }
        private void InitData()
        {
            InitRoleSkillsData();
            InitEquipSubpart();
        }

        private void HandleTransmitValue(params object[] args)
        {
            RoleGroupModel roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();

           if(roleGroupModel.GetRoleModel(1001)==null) 
            roleGroupModel.AddSpiritRoleModel(1037, 100);

           
            //if (args != null && args.Length > 0)
            //{
            //    if (args.Length >= 1)
            //    {
            //        RoleGroupModel roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
            //        m_PanelData.roleModel = (RoleModel)args[0];
            //    }
            //}
            m_PanelData.curRoleModel.AddSkill(10011);
            m_PanelData.curRoleModel.AddSkill(10012);
            m_PanelData.curRoleModel.AddSkill(10013);

            m_RoleName.text = m_PanelData.curRoleModel.name;
            m_IsLocked = m_PanelData.curRoleModel.isLocked.Value;
            m_PanelData.curRoleModel.AddEquip(EquipmentType.Weapon);


            //if (!m_IsLocked)
            //{
            //    RefreshRoleIsUnclockView(m_IsLocked);
            //    return;
            //}

        }
        private void InitRoleSkillsData()
        {
            foreach (var item in m_PanelData.curRoleModel.skillList)
            {
                SkillSubpart skillSubpart = Instantiate(m_SkillSubpart, m_SkillRegion.transform).GetComponent<SkillSubpart>();
                skillSubpart.OnInit(m_PanelData.curRoleModel.id, item);
                m_RoleSkillSubs.Add(skillSubpart);
            }
        }
        private void InitEquipSubpart()
        {
            var equipsSubpart = m_EquipRegion.GetComponentsInChildren<EquipSubpart>();
            for (int i = 0; i < equipsSubpart.Length; i++)
            {
                equipsSubpart[i].InitEquipSubpart( m_PanelData.curRoleModel.id, (EquipmentType)i);
            }
            
        }
        #endregion

        #region Public
        #endregion
    }
}
