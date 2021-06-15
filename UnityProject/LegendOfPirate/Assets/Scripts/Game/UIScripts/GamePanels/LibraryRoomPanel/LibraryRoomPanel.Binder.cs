using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

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

        }

        private void BindUIToModel()
        {
        }
        private void RegisterEvents()
        {

        }

        private void OnClickAddListener()
        {
            LeftArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                LeftArrowBtnEvent();
            });
            RightArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                RightArrowBtnEvent();
            });
            LibraryUpgradeBtn.OnClickAsObservable().Subscribe(_ =>
            {
                LibraryUpgradeBtnEvent();
            });
            TrainBtn.OnClickAsObservable().Subscribe(_ =>
            {
                TrainBtnEvent();
            });
            AutoTrainBtn.OnClickAsObservable().Subscribe(_ =>
            {
                AutoTrainBtnEvent();
            });
            BgBtn.OnClickAsObservable().Subscribe(_ =>
            {
                BgBtnEvent();
            });
        }
        private void UnregisterEvents()
        {

        }
    }
}
