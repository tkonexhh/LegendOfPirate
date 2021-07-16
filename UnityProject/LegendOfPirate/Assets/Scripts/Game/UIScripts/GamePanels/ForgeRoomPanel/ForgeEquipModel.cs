using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace GameWish.Game
{
    public class ForgeEquipModel
    {
        private int m_ID;
        private ForgeRoomModel m_ForgeRoomModel;
        private EquipmentSynthesisConfig m_EquipmentSynthesisConfig;
        private EquipmentUnitConfig m_EquipmentUnitConfig;
        private ForgeUnitConfig m_ForgeUnitConfig;

        public IReadOnlyReactiveProperty<bool> isSelected;
        public IReadOnlyReactiveProperty<string> selectedModelName;
        public IReadOnlyReactiveProperty<bool> isUnlock;
        public ReactiveProperty<ForgeStage> forgeState;
        public StringReactiveProperty unlockLevel;

        public int ID { get { return m_ID; } }
        public EquipmentSynthesisConfig EquipmentSynthesisConfig { get { return m_EquipmentSynthesisConfig; } }
        public EquipmentUnitConfig EquipmentUnitConfig { get { return m_EquipmentUnitConfig; } }
        public ForgeRoomModel ForgeRoomModel { get { return m_ForgeRoomModel; } }

        public ForgeEquipModel(int id, ForgeStage forgeState, ForgeRoomModel forgeRoomModel)
        {
            this.m_ID = id;
            this.forgeState = new ReactiveProperty<ForgeStage>(forgeState);
            this.m_ForgeRoomModel = forgeRoomModel;

            m_ForgeUnitConfig = TDFacilityForgeTable.GetConfigByEquipID(m_ID);
            m_EquipmentSynthesisConfig = TDEquipmentSynthesisConfigTable.GetEquipmentSynthesisConfigByID(this.m_ID);
            m_EquipmentUnitConfig = TDEquipmentConfigTable.GetEquipmentConfigByID(this.m_ID);

            isSelected = forgeRoomModel.CurSelectedModel.Select(model => model == null ? false : model.forgeState.Value != ForgeStage.Forging && model.ID == m_ID).ToReactiveProperty();
            selectedModelName = forgeRoomModel.CurSelectedModel.Select(model => HandleSelectedModelName(model)).ToReactiveProperty();
            isUnlock = this.forgeState.Select(state => state != ForgeStage.Lock).ToReactiveProperty();
            unlockLevel = new StringReactiveProperty(string.Format(LanguageKeyDefine.FIXED_TITLE_LV_Ⅱ, m_ForgeUnitConfig.baseProperty.level));
        }

        private string HandleSelectedModelName(ForgeEquipModel model)
        {
            return model == null ? Define.DEFAULT_NULL : (model.ID == m_ID ? model.EquipmentUnitConfig.equipName : Define.DEFAULT_NULL);
        }

        #region Public
        /// <summary>
        /// 设置锻造状态
        /// </summary>
        /// <param name="forgeStage"></param>
        public void SetForgeStage(ForgeStage forgeStage)
        {
            this.forgeState.Value = forgeStage;
        }

        /// <summary>
        ///  选择当前的锻造装备
        /// </summary>
        public void SetCurSelectedModel()
        {
            this.forgeState.Value = ForgeStage.Selected;
            m_ForgeRoomModel.SetCurSelectedModel(this);
        }

        /// <summary>
        /// 取消选择
        /// </summary>
        public void CancelSelected()
        {
            this.forgeState.Value = ForgeStage.Free;
            m_ForgeRoomModel.CancaleSelect();
        }
        #endregion
    }
}