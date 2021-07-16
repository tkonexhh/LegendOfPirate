using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

namespace GameWish.Game
{
    public class ForgePanelData : UIPanelData
    {
        public ForgeRoomModel forgeRoomModel;
        public ForgePanelData()
        {
        }
    }

    public partial class ForgeRoomPanel
    {
        private ForgePanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<ForgePanelData>();
            m_PanelData.forgeRoomModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.ForgeRoom) as ForgeRoomModel;
            m_ForgeEquipModels = m_PanelData.forgeRoomModel.ForgeEquipModels;
        }

        private void ReleasePanelData()
        {
            ObjectPool<ForgePanelData>.S.Recycle(m_PanelData);
        }

        private void BindUIToModel()
        {
            m_ForgeUpgradeBtn.OnClickAsObservable().Subscribe(_ => ForgeUpgradeBtn()).AddTo(this);
        }

        private void BindModelToUI()
        {
            m_PanelData.forgeRoomModel.level.SubscribeToTextMeshPro(m_ForgeLevelTMP).AddTo(this);
            m_PanelData.forgeRoomModel.ForgeStageReactive.Subscribe(state => HandleForgeStageReactive(state)).AddTo(this);
            m_PanelData.forgeRoomModel.onlyReadCurSelectedModel.SubscribeToPositiveActive(m_SelectEquipState).AddTo(this);
            //m_PanelData.forgeRoomModel.upgradeMaterialsIcon.SubscribeToSprite(m_SelectEquipState).AddTo(this);
            m_PanelData.forgeRoomModel.forgeCountDown.SubscribeToTextMeshPro(m_ForgeTime).AddTo(this);
            m_PanelData.forgeRoomModel.upgradeMaterials.SubscribeToTextMeshPro(m_MaterialValue).AddTo(this);
            m_PanelData.forgeRoomModel.progressBar.SubscribeToFillAmount(m_ProgressBar).AddTo(this);
            m_PanelData.forgeRoomModel.EquipAdditions.Subscribe(val => HandleAttribute(val)).AddTo(this);
        }

        private void HandleAttribute(EquipAttributeValue[] val)
        {
            if (val!=null)
            {
                for (int i = 0; i < val.Length; i++)
                {
                    m_AttrItems[i].ShowAttr(val[i]);
                }
                for (int i = val.Length; i < ATTRITEM_NUMBER; i++)
                {
                    m_AttrItems[i].HideSelf();
                }
            }
            else
            {
                foreach (var item in m_AttrItems)
                {
                    item.HideSelf();
                }
            }
        }

        private void HandleForgeStageReactive(ForgeStage state)
        {
            HideAllObj();
            switch (state)
            {
                case ForgeStage.Free:
                    m_TipState.gameObject.SetActive(true);
                    break;
                case ForgeStage.Forging:
                    m_ForgingEquipIcon.gameObject.SetActive(true);
                    break;
                case ForgeStage.Lock:
                    //m_SelectEquipIcon.gameObject.SetActive(true);
                    break;
                case ForgeStage.Selected:
                    m_SelectEquipIcon.gameObject.SetActive(true);
                    break;
            }
        }

        private void HideAllObj()
        {
            m_TipState.gameObject.SetActive(false);
            m_ForgingEquipIcon.gameObject.SetActive(false);
            m_SelectEquipIcon.gameObject.SetActive(false);
        }

        private void ForgeUpgradeBtn()
        {
            m_PanelData.forgeRoomModel.OnLevelUpgrade();
        }
    }
}
