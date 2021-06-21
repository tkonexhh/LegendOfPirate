using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace GameWish.Game
{
    public class ForgeRoomModel : ShipUnitModel
    {

        public ForgeUnitConfig tableConfig;
        public ForgeModel forgeModel;
        public ReactiveCollection<ForgeWeaponSlotModel> forgeWeaponSlotModels = new ReactiveCollection<ForgeWeaponSlotModel>();

        private ForgeData m_DbData;
        public ForgeRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityForgeTable.GetConfig(level.Value);
            m_DbData = GameDataMgr.S.GetData<ForgeData>();
            forgeModel = new ForgeModel(this, m_DbData.forgeDataItem);
            forgeModel.weaponId = 0;
            forgeModel.forgeState.Value = ForgeStage.Free;
            for (int i = 0; i < TDFacilityForgeTable.dataList.Count; i++) 
            {
                ForgeWeaponSlotModel item = default(ForgeWeaponSlotModel);
                if (level.Value >= TDFacilityForgeTable.dataList[i].level)
                {
                    item = new ForgeWeaponSlotModel(this, i, false);
                }
                else 
                {
                    item = new ForgeWeaponSlotModel(this, i, true);
                }
            }
        }

        private float m_RefreshTime = 0;
        private float m_RefreshInterval = 0.3f;

        public override void OnUpdate()
        {
            m_RefreshTime += Time.deltaTime;
            if (m_RefreshTime >= m_RefreshInterval)
            {
                m_RefreshTime = 0;
                forgeModel.RefreshRemainTime();
            }
        }

        public override void OnLevelUpgrade(int delta)
        {
            base.OnLevelUpgrade(delta);

            tableConfig = TDFacilityForgeTable.GetConfig(level.Value);

            for (int i = 0; i < forgeWeaponSlotModels.Count; i++)
            {
                forgeWeaponSlotModels[i].OnGardenLevelUp();
            }
        }

        public ForgeWeaponSlotModel GetWeaponSlotModel(int slotindex) 
        {
            if (forgeWeaponSlotModels != null)
            {
                return forgeWeaponSlotModels[slotindex];
            }
            else
            {
                return default(ForgeWeaponSlotModel);
            }
        }
    }
    public class ForgeModel : Model 
    {
        public int weaponId;
        public DateTime startTime;
        public FloatReactiveProperty forgeRemainTime = new FloatReactiveProperty(-1);

        public ReactiveProperty<ForgeStage> forgeState;

        private DateTime m_StartTime = default(DateTime);
        private DateTime m_EndTime = default(DateTime);

        private ForgeRoomModel m_ForgeRoomModel;
        private ForgeDataItem m_DbItem;


        public ForgeModel(ForgeRoomModel forgeRoomModel, ForgeDataItem forgeDataItem) 
        {
            m_ForgeRoomModel = forgeRoomModel;
            m_DbItem = forgeDataItem;

            this.weaponId = forgeDataItem.weaponId;
            this.forgeState = new ReactiveProperty<ForgeStage>(forgeDataItem.forgeState);
            switch (forgeState.Value)
            {
                case ForgeStage.Free:
                    break;
                case ForgeStage.Forging:
                    SetTime(m_DbItem.forgingStartTime);
                    RefreshRemainTime();
                    break;
                case ForgeStage.Select:
                    break;
                case ForgeStage.ForgeComplate:
                    break;
            }
        }

        private void SetTime(DateTime forgingStartTime)
        {
            m_StartTime = startTime;
            m_EndTime = startTime + TimeSpan.FromSeconds(TDEquipmentSynthesisConfigTable.GetEquipmentSynthesisById(weaponId).makeTime);
        }

        #region Public 
        public void OnStartForge(DateTime startTime)
        {
            forgeState.Value = ForgeStage.Forging;
            SetTime(startTime);
            m_DbItem.OnStartForge(weaponId, startTime);
        }

        public void OnWeaponSelect(int id)
        {
            weaponId = id;
            forgeState.Value = ForgeStage.Select;
            m_DbItem.OnWeaponSelect(id);
        }

        public void OnWeaponUnSelect()
        {
            weaponId = -1;
            forgeState.Value = ForgeStage.Free;
            m_DbItem.OnWeaponUnSelect();
        }

        public void OnForgeFinish()
        {
            forgeRemainTime.Value = -1;
            forgeState.Value = ForgeStage.ForgeComplate;
            m_StartTime = default(DateTime);
            m_EndTime = default(DateTime);
            m_DbItem.OnForgeFinish();
        }

        public void OnGetWeapon()
        {
            weaponId = -1;
            forgeState.Value = ForgeStage.Free;
            m_DbItem.OnGetWeapon();
        }


        public void RefreshRemainTime()
        {
            double remainTime = (m_EndTime - DateTime.Now).TotalSeconds;

            forgeRemainTime.Value = (float)remainTime;
        }
        #endregion

    }
    public class ForgeWeaponSlotModel : Model 
    {
        public BoolReactiveProperty slotIsUnlock;
        public string plantName;
        public int slotId;
        public int unlockLevel;
        private ForgeRoomModel m_GardenModel;
        public ForgeWeaponSlotModel(ForgeRoomModel forgeRoomModel, int slotid, bool unlockStage)
        {
            this.slotId = slotid;
            this.unlockLevel = TDFacilityGardenTable.dataList[slotid].level;
            this.plantName = TDFacilityGardenTable.dataList[slotid].seedUnlock;
            m_GardenModel = forgeRoomModel;
            slotIsUnlock = new BoolReactiveProperty(unlockStage);
        }
        public void OnGardenLevelUp()
        {

            if (m_GardenModel.level.Value >= unlockLevel)
            {
                slotIsUnlock.Value = true;
            }
            else
            {
                slotIsUnlock.Value = false;
            }

        }
    }
}