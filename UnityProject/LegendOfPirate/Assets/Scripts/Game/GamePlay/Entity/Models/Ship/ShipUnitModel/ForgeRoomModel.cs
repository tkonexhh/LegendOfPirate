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
        public ReactiveCollection<ForgeEquipmentSlotModel> forgeWeaponSlotModels = new ReactiveCollection<ForgeEquipmentSlotModel>();

        private ForgeData m_DbData;
        public ForgeRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityForgeTable.GetConfig(level.Value);
            m_DbData = GameDataMgr.S.GetData<ForgeData>();
            forgeModel = new ForgeModel(this, m_DbData.forgeDataItem);
            forgeModel.equipmentId.Value = 0;
            forgeModel.forgeState.Value = ForgeStage.Free;
            for (int i = 0; i < TDFacilityForgeTable.dataList.Count; i++) 
            {
                ForgeEquipmentSlotModel item = default(ForgeEquipmentSlotModel);
                if (level.Value >= TDFacilityForgeTable.dataList[i].level)
                {
                    item = new ForgeEquipmentSlotModel(this, i, false);
                }
                else 
                {
                    item = new ForgeEquipmentSlotModel(this, i, true);
                }
                forgeWeaponSlotModels.Add(item);
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
                forgeWeaponSlotModels[i].OnForgeRoomLevelUp();
            }
        }

        public ForgeEquipmentSlotModel GetWeaponSlotModel(int slotindex) 
        {
            if (forgeWeaponSlotModels != null)
            {
                return forgeWeaponSlotModels[slotindex];
            }
            else
            {
                return default(ForgeEquipmentSlotModel);
            }
        }
    }
    public class ForgeModel : Model 
    {
        public IntReactiveProperty equipmentId;
        public DateTime startTime;
        public FloatReactiveProperty forgeRemainTime = new FloatReactiveProperty(-1);

        public ReactiveProperty<ForgeStage> forgeState;
        public MakeEquipmentMsgModel makeEquipmentMsgModel;

        private DateTime m_StartTime = default(DateTime);
        private DateTime m_EndTime = default(DateTime);

        private ForgeRoomModel m_ForgeRoomModel;
        private ForgeDataItem m_DbItem;


        public ForgeModel(ForgeRoomModel forgeRoomModel, ForgeDataItem forgeDataItem) 
        {
            m_ForgeRoomModel = forgeRoomModel;
            m_DbItem = forgeDataItem;

            this.equipmentId =  new IntReactiveProperty( forgeDataItem.equipmentId);
            this.forgeState = new ReactiveProperty<ForgeStage>(forgeDataItem.forgeState);
            this.makeEquipmentMsgModel = new MakeEquipmentMsgModel(forgeDataItem.equipmentId);
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
            m_StartTime = forgingStartTime;
            m_EndTime = forgingStartTime + TimeSpan.FromSeconds(TDEquipmentSynthesisConfigTable.GetEquipmentSynthesisById(equipmentId.Value).makeTime);
        }

        #region Public 
        public void OnStartForge(DateTime startTime)
        {
            forgeState.Value = ForgeStage.Forging;
            SetTime(startTime);
            m_DbItem.OnStartForge(equipmentId.Value, startTime);
        }

        public void OnWeaponSelect(int id)
        {
            makeEquipmentMsgModel.ResetEquipmentMsg(id);
            equipmentId.Value = id;
            forgeState.Value = ForgeStage.Select;
            m_DbItem.OnWeaponSelect(id);
        }

        public void OnWeaponUnSelect()
        {
            equipmentId.Value = -1;
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
            equipmentId.Value = -1;
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
    public class ForgeEquipmentSlotModel : Model 
    {
        public BoolReactiveProperty slotIsUnlock;
        public string equipmentName;
        public int slotId;
        public int unlockLevel;


        private ForgeRoomModel m_ForgeRoomModel;
        public ForgeEquipmentSlotModel(ForgeRoomModel forgeRoomModel, int slotid, bool unlockStage)
        {
            this.slotId = slotid;
            this.unlockLevel = TDFacilityForgeTable.dataList[slotid].level;
            this.equipmentName = TDEquipmentConfigTable.GetEquipmentNameById(TDFacilityForgeTable.dataList[slotid].unlockEquipmentID); 
           
            m_ForgeRoomModel = forgeRoomModel;
            slotIsUnlock = new BoolReactiveProperty(unlockStage);
        }
        public void OnForgeRoomLevelUp()
        {
            if (m_ForgeRoomModel.level.Value >= unlockLevel)
            {
                slotIsUnlock.Value = false;
            }
            else
            {
                slotIsUnlock.Value = true;
            }
        }
    }

    public class MakeEquipmentMsgModel : Model 
    {
        public int makeTime;
        public List<ResPair> makeResList;
        public string equipmentName;
        public TDEquipmentSynthesisConfig equipmentConfig;

        public MakeEquipmentMsgModel(int equipmentId) 
        {
            equipmentConfig = TDEquipmentSynthesisConfigTable.GetEquipmentSynthesisById(equipmentId);
            makeTime = equipmentConfig.makeTime;
            makeResList = equipmentConfig.GetEquipmentResPairs();
            equipmentName = TDEquipmentConfigTable.GetEquipmentNameById(equipmentId);
           
        }
        public void  ResetEquipmentMsg(int equipmentId) 
        {
            equipmentConfig = TDEquipmentSynthesisConfigTable.GetEquipmentSynthesisById(equipmentId);
            makeTime = equipmentConfig.makeTime;
            makeResList = equipmentConfig.GetEquipmentResPairs();
            equipmentName = TDEquipmentConfigTable.GetEquipmentNameById(equipmentId);
        }
    }
    public struct ResPair 
    {
        public int resId;
        public int resCount;
        
        public ResPair(int resid,int rescount) 
        {
            resCount = rescount;
            resId = resid;
        }
    }
}