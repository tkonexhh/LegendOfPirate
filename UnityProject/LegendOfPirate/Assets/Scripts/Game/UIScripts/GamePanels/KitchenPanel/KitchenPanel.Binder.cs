using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using TMPro;
using System;
namespace GameWish.Game
{
    public class KitchenPanelData : UIPanelData
    {
        public KitchenModel kitchenModel;
        public KitchenPanelData()
        {

        }
        public int GetKitchenSlotCount()
        {
            return Math.Max(kitchenModel.tableConfig.unlockSpaceCount, Define.KITCHEN_DEFAULTS_SLOT_COUNT);
        }

        public int GetFoodSlotCount() 
        {
            return kitchenModel.foodSlotModelLst.Count;
        }

    }

    public partial class KitchenPanel
    {
        private KitchenPanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<KitchenPanelData>();
            m_PanelData.kitchenModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.Kitchen) as KitchenModel;
            
        }

        private void ReleasePanelData()
        {
            ObjectPool<KitchenPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.kitchenModel.level.Subscribe(level => OnKitchenLevelChange(level)).AddTo(this);
            m_PanelData.kitchenModel.level.SubscribeToTextMeshPro(m_BuildingLevel,"Lv.{0}").AddTo(this);
            m_PanelData.kitchenModel.kitchenSlotModelLst.ObserveCountChanged().Subscribe(count => m_KitchenSlotList.SetDataCount(count)).AddTo(this);
        }

        private void BindUIToModel()
        {

        }
        private void InitPanelBtn()
        {
            m_AddItemBtn.OnClickAsObservable().Subscribe(_ => AddCookSlotBtnClick()).AddTo(this);
            m_CookBtn.OnClickAsObservable().Subscribe(_ => OnCookBtnClick()).AddTo(this);
            m_LevelUpBtn.OnClickAsObservable().Subscribe(_ => OnLevelUpBtnClick()).AddTo(this);
            m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
        }
        private void OnKitchenLevelChange(int level)
        {
           
        }

        private void OnCookBtnClick()
        {
            foreach (var item in m_PanelData.kitchenModel.kitchenSlotModelLst)
            {
                switch (item.kitchenSlotState.Value)
                {
                    case KitchenSlotState.Free:
                        break;
                    case KitchenSlotState.Cooking:
                        break;
                    case KitchenSlotState.Locked:
                        break;
                    case KitchenSlotState.Selected:
                        item.StartCooking(DateTime.Now);
                        break;
                }
            }
        }
        private void AddCookSlotBtnClick()
        {

        }
        private void OnLevelUpBtnClick()
        {
            UIMgr.S.OpenTopPanel(UIID.BuildingLevelUpPanel, null, ShipUnitType.Kitchen);
        }
    }
}
