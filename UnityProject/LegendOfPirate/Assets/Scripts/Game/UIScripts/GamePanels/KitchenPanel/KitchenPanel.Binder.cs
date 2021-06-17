using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using TMPro;

namespace GameWish.Game
{
    public class KitchenPanelData : UIPanelData
    {
        public KitchenModel kitchenModel;
        public KitchenPanelData()
        {

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
        }

        private void BindUIToModel()
        {
            AddItemBtn.OnClickAsObservable().Subscribe(_ => AddCookSlotBtnClick()).AddTo(this);
            CookBtn.OnClickAsObservable().Subscribe(_ => OnCookBtnClick()).AddTo(this);
            LevelUpBtn.OnClickAsObservable().Subscribe(_ => OnLevelUpBtnClick()).AddTo(this);
            CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
        }
        private void OnKitchenLevelChange(int level)
        {
            BuildingLevel.text = "Lv." + level;
            CookList.GetComponent<KitchenSlotList>().SetUnlockSlot(TDFacilityKitchenTable.dataList[level - 1].unlockCookSpace);
            Content.GetComponent<MenuSlotList>().SetMenuSlot(level);
            AddItemBtn.GetComponentInChildren<TextMeshProUGUI>().text = TDFacilityKitchenTable.dataList[level - 1].unlockSpaceCost.ToString();
        }

        private void OnCookBtnClick()
        {

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
