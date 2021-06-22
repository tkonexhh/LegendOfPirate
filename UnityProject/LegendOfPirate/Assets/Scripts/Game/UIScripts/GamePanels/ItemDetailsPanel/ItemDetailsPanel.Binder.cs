using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class ItemDetailsPanelData : UIPanelData
    {
        public InventoryModel inventoryModel;
        public ItemDetailsPanelData()
        {
        }
    }

    public partial class ItemDetailsPanel
    {
        private ItemDetailsPanelData m_PanelData = null;

        private void AllocatePanelData()
        {
            m_PanelData = UIPanelData.Allocate<ItemDetailsPanelData>();
            try
            {
                m_PanelData.inventoryModel = ModelMgr.S.GetModel<InventoryModel>();
            }
            catch (System.Exception e)
            {
                Log.e("e = " + e);
            }
        }
        private void ReleasePanelData()
        {
            ObjectPool<ItemDetailsPanelData>.S.Recycle(m_PanelData);
        }
        private void BindModelToUI()
        {
            m_SelectedCount.SubscribeToTextMeshPro(m_SellNumber).AddTo(this);
            m_SelectedCount.Select(val => val */*Unit Price*/10).SubscribeToTextMeshPro(m_TotalValue).AddTo(this);
            m_SelectedCount.Where(val => val > 0).Subscribe(val => { HandleMaxAndMinBtnActive(val); }).AddTo(this);
        }
        private void BindUIToModel()
        {
            m_InventoryItemModel.GetReactiveCount().SubscribeToTextMeshPro(m_Own);

            m_SellBtn.OnClickAsObservable().Subscribe(_ => { HandleSellBtnEvt(); });
        }
    }
}
