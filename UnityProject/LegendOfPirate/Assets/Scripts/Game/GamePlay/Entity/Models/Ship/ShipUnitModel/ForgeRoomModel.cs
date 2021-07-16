using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Qarth;
using System.Linq;

namespace GameWish.Game
{
    public class ForgeRoomModel : ShipUnitModel
    {
        private ForgeData m_DbData;
        private ForgeUnitConfig m_TableConfig;

        private ReactiveCollection<ForgeEquipModel> m_ForgeEquipModels = new ReactiveCollection<ForgeEquipModel>();
        private ReactiveCommand<EquipAttributeValue[]> m_EquipAdditions = new ReactiveCommand<EquipAttributeValue[]>();
        private ReactiveProperty<ForgeEquipModel> m_CurSelectedModel = new ReactiveProperty<ForgeEquipModel>(null);
        private ReactiveProperty<ForgeStage> m_ForgeStage = new ReactiveProperty<ForgeStage>();

        public ReactiveCollection<ForgeEquipModel> ForgeEquipModels { get { return m_ForgeEquipModels; } }
        public ReactiveProperty<ForgeEquipModel> CurSelectedModel { get { return m_CurSelectedModel; } }
        public ReactiveProperty<ForgeStage> ForgeStageReactive { get { return m_ForgeStage; } }
        public ReactiveCommand<EquipAttributeValue[]> EquipAdditions { get { return m_EquipAdditions; } }

        public StringReactiveProperty forgeCountDown = new StringReactiveProperty(Define.DEFAULT_NULL);
        public FloatReactiveProperty progressBar = new FloatReactiveProperty(1);

        public IReadOnlyReactiveProperty<bool> onlyReadCurSelectedModel;
        public IReadOnlyReactiveProperty<string> upgradeMaterials;
        public IReadOnlyReactiveProperty<Sprite> upgradeMaterialsIcon;

        public ForgeRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            m_TableConfig = TDFacilityForgeTable.GetConfig(level.Value);
            m_DbData = GameDataMgr.S.GetData<ForgeData>();

            onlyReadCurSelectedModel = m_CurSelectedModel.Select(val => HandleOnlyReadCurSelectedModel(val)).ToReactiveProperty();
            upgradeMaterials = m_CurSelectedModel.Select(val => HandleUpgradeMaterials(val)).ToReactiveProperty();
            //upgradeMaterialsIcon = m_CurSelectedModel.Select(val => HandleUpgradeMaterialsIcon(val)).ToReactiveProperty();
            
            m_ForgeEquipModels.Clear();

            HandleLoadingData();

            InitForgeEquipModels();

            level.Subscribe(level => { HandleForgeLevel(level); });
        }

        private Sprite HandleUpgradeMaterialsIcon(ForgeEquipModel val)
        {
            if (val != null)
            {
                return SpriteHandler.S.GetSprite("##","##");
            }
            else
            {
                return SpriteHandler.S.GetSprite("##", "##");
            }
        }

        private string HandleUpgradeMaterials(ForgeEquipModel val)
        {
            if (val != null)
            {
                return string.Format(LanguageKeyDefine.FORGE_MATERIAL_Ⅰ, "#123145", 3, "#FFFFFF", val.EquipmentSynthesisConfig.makeRes.number);
            }
            else
            {
                return Define.DEFAULT_NULL;
            }
        }

        private bool HandleOnlyReadCurSelectedModel(ForgeEquipModel val)
        {
            if (val != null && val.forgeState.Value != ForgeStage.Forging)
            {
                return true;
            }
            return false;
        }

        public override void OnUpdate()
        {
            if (ForgeStageReactive.Value == ForgeStage.Forging)
            {
                try
                {
                    int remainSeconds = GetForgeCountDown();
                    if (remainSeconds > 0)
                    {
                        EquipmentSynthesisConfig config = TDEquipmentSynthesisConfigTable.GetEquipmentSynthesisConfigByID(m_DbData.forgeDataItem.equipmentId);
                        forgeCountDown.Value = string.Format(LanguageKeyDefine.COMMON_TIME, CommonMethod.SplicingTime(remainSeconds));
                        progressBar.Value = (float)remainSeconds / config.makeTime;
                    }
                    else
                    {
                        TimeEmpty();
                    }
                }
                catch (Exception)
                {
                    TimeEmpty();
                }
            }
        }

        #region Public
        /// <summary>
        /// 锻造装备
        /// </summary>
        public void ForgeEquip()
        {
            if (m_CurSelectedModel.Value != null)
            {
                m_DbData.SetForgeEquip(m_CurSelectedModel.Value);
                m_CurSelectedModel.Value.CancelSelected();
                ForgeStageReactive.Value = ForgeStage.Forging;
            }
            else
                Log.e("Error : It shouldn't be");
        }

        /// <summary>
        /// 根据ID获得EquipModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ForgeEquipModel GetEquipModelByID(int id)
        {
            ForgeEquipModel forgeEquipModel = ForgeEquipModels.FirstOrDefault(i => i.ID == id);
            if (forgeEquipModel != null)
            {
                return forgeEquipModel;
            }
            else
            {
                Log.e("Error : Not find ID = " + id);
                return null;
            }
        }

        /// <summary>
        /// 取消选择
        /// </summary>
        public void CancaleSelect()
        {
            if (onlyReadCurSelectedModel.Value)
            {
                m_CurSelectedModel.Value = null;
                m_EquipAdditions.Execute(null);
                ForgeStageReactive.Value = ForgeStage.Free;
            }
            else
                Log.e("Error : CurSelectedModel is null");
        }

        /// <summary>
        /// 设置当前选中的Model
        /// </summary>
        /// <param name="id"></param>
        public void SetCurSelectedModel(ForgeEquipModel forgeEquipModel)
        {
            m_CurSelectedModel.Value = null;
            m_CurSelectedModel.Value = forgeEquipModel;
            ForgeStageReactive.Value = ForgeStage.Selected;

            m_EquipAdditions.Execute(forgeEquipModel.EquipmentUnitConfig.equipAttributeValues);
        }
        #endregion

        #region Private

        /// <summary>
        /// 结束锻造
        /// </summary>
        private void EndForge()
        {
            m_CurSelectedModel.Value = null;
            ForgeStageReactive.Value = ForgeStage.Free;
            m_DbData.SetForgeEquip();
            m_EquipAdditions.Execute(null);
        }

        /// <summary>
        /// 获得倒计时
        /// </summary>
        /// <returns></returns>
        private int GetForgeCountDown()
        {
            TimeSpan remainTim = m_DbData.ForgeDataItem.forgeEndTime - DateTime.Now;
            if (remainTim.TotalSeconds <= 0)
            {
                EndForge();
                return 0;
            }
            return (int)remainTim.TotalSeconds;
        }

        /// <summary>
        /// 处理加载数据
        /// </summary>
        private void HandleLoadingData()
        {
            ForgeStageReactive.Value = m_DbData.ForgeDataItem.forgeState;

            if (m_DbData.ForgeDataItem.forgeState == ForgeStage.Forging)
            {
                m_ForgeEquipModels.Add(new ForgeEquipModel(m_DbData.ForgeDataItem.equipmentId, m_DbData.ForgeDataItem.forgeState, this));
            }
        }

        /// <summary>
        /// 倒计时为零时，制空时间
        /// </summary>
        private void TimeEmpty()
        {
            forgeCountDown.Value = string.Format(LanguageKeyDefine.COMMON_TIME, CommonMethod.SplicingTime(0));
            progressBar.Value = 0;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitForgeEquipModels()
        {
            foreach (var item in TDFacilityForgeTable.forgeUnitProperties)
            {
                if (level.Value >= item.baseProperty.level)
                {
                    CheckAddEquipModel(item.unlockEuipIDs, ForgeStage.Free);
                }
                else
                    CheckAddEquipModel(item.unlockEuipIDs, ForgeStage.Lock);
            }
        }

        /// <summary>
        /// 处理升级情况
        /// </summary>
        /// <param name="level"></param>
        private void HandleForgeLevel(int level)
        {
            m_TableConfig = TDFacilityForgeTable.GetConfig(level);
            foreach (var id in m_TableConfig.unlockEuipIDs)
            {
                ForgeEquipModel forgeEquipModel = m_ForgeEquipModels.FirstOrDefault(i => i.ID == id);
                if (forgeEquipModel!=null)
                    forgeEquipModel.SetForgeStage(ForgeStage.Free);
                else
                    Log.e("Error : Not find ID = " + id);
            }
        }

        /// <summary>
        /// 检查生成Model
        /// </summary>
        /// <param name="items"></param>
        private void CheckAddEquipModel(List<int> items, ForgeStage forgeStage)
        {
            foreach (var id in items)
            {
                if (!m_ForgeEquipModels.Any(i => i.ID == id))
                {
                    m_ForgeEquipModels.Add(new ForgeEquipModel(id, forgeStage, this));
                }
            }
        }
        #endregion
    }
}