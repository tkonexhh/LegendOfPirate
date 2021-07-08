using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using UnityEngine.UI;
using TMPro;
using UniRx;
using System;
namespace GameWish.Game
{
    public class BattleShipSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_LockText;
        [SerializeField] private GameObject m_UnlockObj;
        [SerializeField] private TextMeshProUGUI m_ShipAtk;
        [SerializeField] private TextMeshProUGUI m_ShipHp;
        [SerializeField] private TextMeshProUGUI m_ShipArmor;
        [SerializeField] private TextMeshProUGUI m_ShipName;
        [SerializeField] private Button m_ShipBtn;

        private List<IDisposable> m_ListenerList = new List<IDisposable>();
        private BattleShipModel m_BattleShipModel;

        private int m_BattleShipId;
        private BattleShipFleetModel m_BattleShipFleetModel;


        public void InitSlot(int SlotId)
        {
            foreach (var listener in m_ListenerList)
            {
                listener.Dispose();
            }
            m_ListenerList.Clear();

            if (m_BattleShipFleetModel == null)
            {
                m_BattleShipFleetModel = ModelMgr.S.GetModel<BattleShipFleetModel>();
            }
            
            m_BattleShipModel = m_BattleShipFleetModel.battleShipModels[SlotId];

            m_BattleShipId = m_BattleShipModel.GetShipConfig().warShipId;

            AddEventListener();

        }

        private void AddEventListener() 
        {
            m_ListenerList.Add(m_BattleShipModel.isLocked.AsObservable().Subscribe(_ => UpdateMsg()).AddTo(this));
            m_ListenerList.Add(m_BattleShipModel.shipLevel.AsObservable().Subscribe(_ => UpdateMsg()).AddTo(this));
            m_ListenerList.Add(ModelMgr.S.GetModel<ShipModel>().shipLevel.AsObservable().Subscribe(level => OnMainShipLevelChange(level)).AddTo(this));
            m_ListenerList.Add(m_ShipBtn.OnClickAsObservable().Subscribe(_=>UIMgr.S.OpenPanel(UIID.WarshipUpgradePanel,m_BattleShipId)));
        }

        private void OnMainShipLevelChange(int level)
        {
            if (level >= m_BattleShipModel.GetShipConfig().unlockAccountLevel) 
            {
                m_BattleShipModel.UnlockShip();
            }
        }

        private void UpdateMsg() 
        {
            m_ShipName.text = m_BattleShipModel.GetShipName();
            if (m_BattleShipModel.isLocked.Value)
            {
                m_UnlockObj.SetActive(false);
                m_LockText.gameObject.SetActive(true);
                m_ShipBtn.interactable = false;
            }
            else
            {
                m_ShipBtn.interactable = true;
                m_UnlockObj.SetActive(true);
                m_LockText.gameObject.SetActive(false);
                m_ShipArmor.text = m_BattleShipModel.GetArmor().ToString("f1") + "%";
                m_ShipAtk.text=m_BattleShipModel.GetAtk().ToString("f1") + "%";
                m_ShipHp.text=m_BattleShipModel.GetHp().ToString("f1") + "%";
            }
        }
    }
}