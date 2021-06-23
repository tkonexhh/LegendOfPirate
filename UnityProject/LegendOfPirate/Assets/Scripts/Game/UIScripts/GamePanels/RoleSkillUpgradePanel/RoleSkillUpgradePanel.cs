using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
    public partial class RoleSkillUpgradePanel : AbstractAnimPanel
    {
        #region Data
        private int m_RoleID;
        private RoleSkillModel m_RoleSkillModel;
        private RoleModel m_RoleModel;
        private RoleGroupModel m_RoleGroupModel;
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

            OpenDependPanel(EngineUI.MaskPanel, -1, null);

            HandleTransmitValue(args);

            BindModelToUI();
            BindUIToModel();

            InitData();
        }

        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();

            CloseSelfPanel();

            CloseDependPanel(EngineUI.MaskPanel);
        }

        protected override void OnClose()
        {
            base.OnClose();

            ReleasePanelData();
        }
        #endregion

        #region OnClickAddListener
        private void OnClickAddListener()
        {
            m_BgExitBtn.OnClickAsObservable().Subscribe(_ => { HideSelfWithAnim(); }).AddTo(this);
        }
        #endregion

        #region Private
        private void HandleTransmitValue(params object[] args)
        {
            m_RoleID = (int)args[0];
            m_RoleSkillModel = args[1] as RoleSkillModel;
            m_RoleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
            m_RoleModel = m_RoleGroupModel.GetRoleModel(m_RoleID);
        }
        private void InitData()
        {
            RefreshPanelTMP();
        }

        private void RefreshPanelTMP()
        {
            m_RoleSkillTitle.text = m_RoleSkillModel.roleSkillUnitConfig.skillName;
            m_RoleSkillExplain.text = m_RoleSkillModel.roleSkillUnitConfig.skillDesc;
        }

        private string HandleRoleSkillLevel(int val)
        {
            if (val == 0)
                val++;
            return CommonMethod.GetStringForTableKey(LanguageKeyDefine.FIXED_TITLE_LV) + val;
        }

        private string HandleSkillDifferentStates(int level)
        {
            if (level == 0)
                return "Unlocking skill requirement ,role level is " + m_RoleSkillModel.GetSkillUnlockingRoleLevel();
            else if (level >= 1)
            {
                //检查是否满足升级条件，满足Or不满足
                //if (true)
                    return "Next Skill Effect " + m_RoleSkillModel.GetSkillUnlockingRoleLevel();
                //else
                    //return "Unlocking skill requirement ,role level is " + m_RoleSkillModel.GetSkillUnlockingRoleLevel();
            }
            else
            {
                Log.e("error : level  = " + level);
                return "Next Skill Effect 0";
            }
        }
        private string HandleSkillUpgradeClip()
        {
            return 0 + Define.SYMBOL_SLASH + m_RoleSkillModel.GetSkillUpgradeClip();
        }  
        private void HandleSkillUpgradeBtn()
        {
            m_RoleModel.UpgradeSkill(m_RoleSkillModel.skillId);
        }  
        private string HandleUpgradeBtnTMP(int val)
        {
            if (val == 0)
                return "Learn";
            else
                return "Upgrade";
        }
        #endregion
    }
}
