using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using TMPro;
using UnityEngine.UI;
using UniRx;

namespace GameWish.Game
{
	public class ForgeEquipItem :UListItemView
	{
        #region SerializeField
        [SerializeField] private Button m_ForgeItemBtn;
        [SerializeField] private Image m_ForgeItemIcon;
        [SerializeField] private TextMeshProUGUI m_ForgeItemName;
        [SerializeField] private GameObject m_Lock;
        [SerializeField] private TextMeshProUGUI m_UnLockLevel;
        #endregion

        #region Data
        private ForgeEquipModel m_ForgeEquipModel;

        private List<IDisposable> m_DisCache = new List<IDisposable>();
        #endregion

        #region Public
        public void OnRefresh(ForgeEquipModel forgeEquipModel)
        {
            OnReset();

            m_ForgeEquipModel = forgeEquipModel;

            BindModelToUI();
        }
        #endregion

        #region Private
        private void BindModelToUI()
        {
            IDisposable itenBtnDis =  m_ForgeItemBtn.OnClickAsObservable().Subscribe(_ => ForgeItemBtn()).AddTo(this);
            IDisposable selectDis = m_ForgeEquipModel.isSelected.Subscribe(val => HandleSelected(val)).AddTo(this);
            IDisposable modelNameDis = m_ForgeEquipModel.selectedModelName.SubscribeToTextMeshPro(m_ForgeItemName).AddTo(this);
            IDisposable unlockLevelDis = m_ForgeEquipModel.unlockLevel.SubscribeToTextMeshPro(m_UnLockLevel).AddTo(this);
            IDisposable unlockLevelBtnDis = m_ForgeEquipModel.isUnlock.SubscribeToPositiveInteractable(m_ForgeItemBtn).AddTo(this);

            List<IDisposable> forgeStateList = m_ForgeEquipModel.isUnlock.SubscribeToActive(m_ForgeItemIcon.gameObject, m_Lock);
            forgeStateList.ForEach(i => i.AddTo(this));

            m_DisCache.Add(modelNameDis);
            m_DisCache.Add(itenBtnDis);
            m_DisCache.Add(selectDis);
            m_DisCache.Add(unlockLevelDis);
            m_DisCache.Add(unlockLevelBtnDis);
            m_DisCache.AddRange(forgeStateList);
        }

        private void ForgeItemBtn()
        {
            if (m_ForgeEquipModel.ForgeRoomModel.ForgeStageReactive.Value == ForgeStage.Forging)
            {
                FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.FORGE_CONT_¢ñ);
                return;
            }

            if (m_ForgeEquipModel.isSelected.Value)
            {
                m_ForgeEquipModel.CancelSelected();
            }
            else
                m_ForgeEquipModel.SetCurSelectedModel();
        }

        private void HandleSelected(bool selected)
        {
            if (selected)
            {
                Enlarge();
            }
            else
            {
                Narrow();
            }
        }

        private void Enlarge()
        {
            RectTransform rect = transform as RectTransform;
            rect.sizeDelta = new Vector2(110, 110);
        }

        private void Narrow()
        {
            RectTransform rect = transform as RectTransform;
            rect.sizeDelta = new Vector2(100, 100);
        }

        private void ClearCache()
        {
            foreach (var item in m_DisCache)
                item.Dispose();
        }

        private void OnReset()
        {
            RectTransform rect = transform as RectTransform;
            rect.sizeDelta = new Vector2(100, 100);
            m_ForgeItemBtn.onClick.RemoveAllListeners();
            ClearCache();
        }

        private void OnDestroy()
        {
            if (m_ForgeEquipModel.isSelected.Value)
            {
                m_ForgeEquipModel.CancelSelected();
            }
        }
        #endregion
    }
}