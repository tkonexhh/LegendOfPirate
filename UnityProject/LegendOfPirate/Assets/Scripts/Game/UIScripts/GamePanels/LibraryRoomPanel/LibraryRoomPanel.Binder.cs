using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Collections.Generic;

namespace GameWish.Game
{
    public class LibraryRoomPanelData : UIPanelData
    {
        public ShipModel shipModel;
        public LibraryModel libraryModel;
        public LibraryRoomPanelData()
        {
        }

        public int GetLibSlotMCount()
        {
            return libraryModel.slotModelList.Count;
        }
    }

    public partial class LibraryRoomPanel
    {
        private LibraryRoomPanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<LibraryRoomPanelData>();

            try
            {
                m_PanelData.shipModel = ModelMgr.S.GetModel<ShipModel>();

                m_PanelData.libraryModel = m_PanelData.shipModel.GetShipUnitModel(ShipUnitType.Library) as LibraryModel;
            }
            catch (Exception e)
            {
                Debug.LogError("e = " + e);
            }
        }

        private void ReleasePanelData()
        {
            ObjectPool<LibraryRoomPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.libraryModel
             .level
             .Select(level => CommonMethod.GetStringForTableKey(LanguageKeyDefine.Fixed_Title_Lv) + level.ToString())
             .SubscribeToTextMeshPro(LibraryLevelTMP).AddTo(this);

            m_SelectedCount.Select(count => count + "/" + 10).SubscribeToTextMeshPro(RoleSelectNumberTMP).AddTo(this);
        }

        private void BindUIToModel()
        {
            LibraryUpgradeBtn.OnClickAsObservable().Subscribe(_ =>
            {
                m_PanelData.libraryModel.OnLevelUpgrade(1);
            }).AddTo(this);

            TrainBtn.OnClickAsObservable().Subscribe(_ =>
            {
                foreach (var item in m_ReadPosDatas)
                {
                    if (item.librarySlotModel.libraryState.Value == LibrarySlotState.HeroSelected)
                    {
                        item.librarySlotModel.StartReading(DateTime.Now);
                        SelectedRoleSort();
                    }
                }
            }).AddTo(this);
        }
    }
}
