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

            HandleTransmitValue(args);

            BindModelToUI();
            BindUIToModel();

            var item = args[0] as RoleModel;
            InitRoleMsg((item.id));
            InitData();
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
            m_StoryBtn.OnClickAsObservable().Subscribe(_ => { OpenRoleStoryPanel(); });
            m_CloseBtn.OnClickAsObservable().Subscribe(_ => { HideSelfWithAnim(); });
            m_UpgradeMaterialsBtn.OnClickAsObservable().Subscribe(_ => { OpenRoleLevelUpPanel(); });
        }

        private void OpenRoleLevelUpPanel()
        {
            UIMgr.S.OpenTopPanel(UIID.RoleGrowthPanel,null, m_PanelData.curRoleModel.id);
            HideSelfWithAnim();
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

            roleGroupModel.AddSpiritRoleModel(1037, 100);

            m_PanelData.curRoleModel = roleGroupModel.GetRoleModel(1001);
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

            RefreshRoleIsUnclockView(!m_IsLocked);
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
                RoleEquipModel output = null;
                m_PanelData.curRoleModel.equipDic.TryGetValue((EquipmentType)i, out output);
                equipsSubpart[i].InitEquipSubpart(output, m_PanelData.curRoleModel.id);
            }
        }
        #endregion

        #region Public
        #endregion
    }
}
