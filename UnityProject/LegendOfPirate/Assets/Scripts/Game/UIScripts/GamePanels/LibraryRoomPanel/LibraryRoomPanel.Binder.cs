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

                m_LibrarySlotModels = m_PanelData.libraryModel.LibrarySlotModels;

                m_PanelData.libraryModel.RefreshCurRoleModels();

                m_RoleModelList = m_PanelData.libraryModel.RoleModelList;
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
            m_PanelData.libraryModel.level.SubscribeToTextMeshPro(m_LibraryLevelTMP).AddTo(this);

            m_PanelData.libraryModel.IsShowReadBtn.SubscribeToActive(m_SelectRoleState, m_AutoSelectBtn).ForEach(i => i.AddTo(this));

            foreach (var item in m_PanelData.libraryModel.LibrarySlotModels)
            {
                item.refreshCommand.Subscribe(_ => RefreshCommand()).AddTo(this);
            }
        }

        private void BindUIToModel()
        {
            m_LibraryUpgradeBtn.OnClickAsObservable().Subscribe(_ => UpgradeBtn()).AddTo(this);
            m_RraintBtn.OnClickAsObservable().Subscribe(_ => RraintBtn()).AddTo(this);
            m_RoleAutoSelectBtn.OnClickAsObservable().Subscribe(_ => AutoSelectBtn()).AddTo(this);
            m_AutoSelectBtn.OnClickAsObservable().Subscribe(_ => AutoSelectBtn()).AddTo(this);
        }
    }
}
