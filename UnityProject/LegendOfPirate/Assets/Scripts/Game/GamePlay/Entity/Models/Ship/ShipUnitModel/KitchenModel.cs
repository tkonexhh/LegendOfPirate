using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace GameWish.Game
{
    public class KitchenModel : ShipUnitModel
    {
        public KitchenUnitConfig tableConfig;
        public KitchenData dbData;

        public ReactiveCollection<KitchenSlotModel> kitchenSlotModelLst = new ReactiveCollection<KitchenSlotModel>();
        public List<FoodSlotModel> foodSlotModelLst = new List<FoodSlotModel>();

        public KitchenModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityKitchenTable.GetConfig(level.Value);
            dbData = GameDataMgr.S.GetData<KitchenData>();
            foreach (var item in TDFacilityKitchenTable.dataList)
            {
                foodSlotModelLst.Add(new FoodSlotModel(this, item.unlockFoodID, level.Value < item.level, item.level));
            }
            if (tableConfig.unlockSpaceCount >= Define.KITCHEN_DEFAULTS_SLOT_COUNT)
            {
                for (int i = 0; i < tableConfig.unlockSpaceCount; i++)
                {
                    kitchenSlotModelLst.Add(new KitchenSlotModel(this, dbData.kitchenSlotDataLst[i], i));
                }
            }
            else
            {
                for (int i = 0; i < Define.PROCESSING_ROOM_DEFAULT_SLOT_COUNT; i++)
                {
                    kitchenSlotModelLst.Add(new KitchenSlotModel(this, dbData.kitchenSlotDataLst[i], i));
                    
                }
            }
        }

        public KitchenSlotModel GetSelectModel() 
        {
            foreach (var item in kitchenSlotModelLst) 
            {
                if (item.kitchenSlotState.Value == KitchenSlotState.Selected) 
                {
                    return item;
                }
            }
            return null;
        }

        public KitchenSlotModel GetAvailableSlot()
        {
            foreach (var item in kitchenSlotModelLst)
            {
                if (item.kitchenSlotState.Value == KitchenSlotState.Free)
                {
                    return item;
                }
            }
            return null;
        }

        public override void OnLevelUpgrade(int delta)
        {
            base.OnLevelUpgrade(delta);

            tableConfig = TDFacilityKitchenTable.GetConfig(level.Value);
            for (int i = 0; i < foodSlotModelLst.Count; i++)
            {
                foodSlotModelLst[i].OnKitchenLevelUp();
            }
            var SlotId = kitchenSlotModelLst.Count;
            if (tableConfig.unlockSpaceCount > Define.KITCHEN_DEFAULTS_SLOT_COUNT && tableConfig.unlockSpaceCount > kitchenSlotModelLst.Count) 
            {
                kitchenSlotModelLst.Add(new KitchenSlotModel(this, dbData.kitchenSlotDataLst[SlotId], SlotId));
            }
            for (int i = 0; i < kitchenSlotModelLst.Count; i++) 
            {
                kitchenSlotModelLst[i].OnKitchenSlotLevelUp();
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
                foreach (var item in kitchenSlotModelLst)
                {
                    item.RefreshRemainTime();
                }
            }
        }
    }
    public class KitchenSlotModel : Model
    {
        public int slotId;
        public int foodId;
        public FloatReactiveProperty cookingRemainTime = new FloatReactiveProperty();

        public ReactiveProperty<KitchenSlotState> kitchenSlotState;

        private DateTime m_StartTime=default(DateTime);
        private DateTime m_EndTime = default(DateTime);

        private KitchenModel m_KitchenModel;
        private KitchenSlotData m_DbItem;
        
        public KitchenSlotModel(KitchenModel kitchenModel,KitchenSlotData dbItem,int slotId)
        {
            m_KitchenModel = kitchenModel;
            m_DbItem = dbItem;
            this.slotId = slotId;
            if (slotId + 1 > kitchenModel.tableConfig.unlockSpaceCount)
            {
                kitchenSlotState = new ReactiveProperty<KitchenSlotState>(KitchenSlotState.Locked);
            }
            else
            {
                kitchenSlotState = new ReactiveProperty<KitchenSlotState>(KitchenSlotState.Free);
            }
            
        }

        public void StartCooking(DateTime startTime) 
        {
            SetTime(startTime);

            kitchenSlotState.Value = KitchenSlotState.Cooking;

            m_DbItem.OnStartCooking(foodId, startTime);
        }

        public void EndCooking() 
        {
            foodId = TDFacilityKitchenTable.dataList[0].unlockFoodID;
            cookingRemainTime.Value = -1;
            kitchenSlotState.Value = KitchenSlotState.Free;
            m_StartTime = default(DateTime);
            m_EndTime = default(DateTime);

            m_DbItem.OnEndCooking();
        }

        public void OnFoodUnSelected() 
        {
            foodId = TDFacilityKitchenTable.dataList[0].unlockFoodID;
            kitchenSlotState.Value = KitchenSlotState.Free;
            m_DbItem.OnFoodUnSelected();
        }

        public void OnFoodSelect(int id) 
        {
            foodId = id;
            kitchenSlotState.Value = KitchenSlotState.Selected;
            m_DbItem.OnPartSelected(id);
        }

        public void RefreshRemainTime()
        {
            if (m_DbItem.kitchenSlotState != KitchenSlotState.Cooking)
                return;

            double remainTime = (m_EndTime - DateTime.Now).TotalSeconds;

            cookingRemainTime.Value = (float)remainTime;
            if (cookingRemainTime.Value <= 0f)
            {
                EndCooking();
            }
        }

        public void OnKitchenSlotLevelUp()
        {
            if (kitchenSlotState.Value == KitchenSlotState.Locked && m_KitchenModel.tableConfig.unlockSpaceCount >= slotId + 1)
            {
                kitchenSlotState.Value = KitchenSlotState.Free;

                m_DbItem.OnUnlocked();
            }
        }

        private void SetTime(DateTime startTime) 
        {
            m_StartTime = startTime;
            m_EndTime = startTime + TimeSpan.FromSeconds(TDFoodSynthesisConfigTable.GetConfigById(foodId).makeTime);
        }

        public int GetMakeTime() 
        {
            return TDFoodSynthesisConfigTable.GetConfigById(foodId).makeTime;
        }
    }

    public class FoodSlotModel : Model 
    {
        public TDFoodSynthesisConfig tableConfig;
        public int foodId;
        public BoolReactiveProperty isLocked;
        public int unLockLevel;

        private KitchenModel m_KitchenModel;

        public FoodSlotModel(KitchenModel kitchenModel, int foodId,bool isLocked,int unLockLevel) 
        {
            this.isLocked = new BoolReactiveProperty(isLocked);
            this.unLockLevel = unLockLevel;
            this.foodId = foodId;
            tableConfig = TDFoodSynthesisConfigTable.GetConfigById(foodId);
            m_KitchenModel = kitchenModel;
        }

        public void OnKitchenLevelUp()
        {
            if (m_KitchenModel.level.Value >= unLockLevel)
            {
                isLocked.Value = false;
            }
            else
            {
                isLocked.Value = true;
            }
        }

        public List<ResPair> GetResPairs() 
        {
            return tableConfig.GetResPair();
        }

        public string GetFoodName() 
        {
            return tableConfig.name;
        }

        public string GetSpriteName() 
        {
            return tableConfig.spriteName;
        }

        public int GetMakeTime() 
        {
            return tableConfig.makeTime;
        }
    }

    public enum KitchenSlotState 
    {
        /// <summary>
        /// 空闲中
        /// </summary>
        Free = 0,
        /// <summary>
        /// 制作中
        /// </summary>
        Cooking = 1,
        /// <summary>
        /// 未解锁
        /// </summary>
        Locked = 2,
        /// <summary>
        /// 选择但是未开始
        /// </summary>
        Selected = 3,
    }
}