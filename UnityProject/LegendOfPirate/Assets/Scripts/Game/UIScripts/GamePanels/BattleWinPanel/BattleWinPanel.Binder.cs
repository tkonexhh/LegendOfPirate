using System.Collections.ObjectModel;
using Qarth.Extension;
using UnityEngine;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class BattleWinPanelData : UIPanelData
    {
        public IntReactiveProperty currentLevelId;
        public BattleWinPanelData()
        {
        }
    }

    public partial class BattleWinPanel
    {
        private BattleWinPanelData m_PanelData = null;

        private void AllocatePanelData()
        {
            m_PanelData = UIPanelData.Allocate<BattleWinPanelData>();
        }

        private void ReleasePanelData()
        {
            ObjectPool<BattleWinPanelData>.S.Recycle(m_PanelData);
        }

        private void OnClickAddListener()
        {
            m_BtnAttack.OnClickAsObservable().Subscribe(_ => OnAttackClicked()).AddTo(this);
            m_BtnQuit.OnClickAsObservable().Subscribe(_ => OnBackClicked()).AddTo(this);
        }

        private void OnBackClicked()
        {
            CloseSelfPanel();
            UIMgr.S.OpenPanel(UIID.MainMenuPanel);
            GameCameraMgr.S.ToSea();
        }

        private void OnAttackClicked()
        {
            CloseSelfPanel();
            UIMgr.S.OpenPanel(UIID.BattlePreparePanel, null, m_PanelData.currentLevelId.Value + 1);
        }

        private async void GetOnlineLevelReward(int levelId)
        {
            ReadOnlyCollection<MarinLevelConfig> list = await StorageHandler.S.QuerySpecialObject<MarinLevelConfig>("MarinLevelConfig", "Level", levelId);
            foreach (MarinLevelConfig item in list)
            {
                if (item.GetState == 0)//未领取
                {
                    CreateReward(item.Reward);
                    StorageHandler.S.UpdateObject("MarinLevelConfig", item.ObjectId, "State", 1);
                }
                else
                {
                    //UIMgr.S.ClosePanelAsUIID(UIID.BattleWinPanel);
                    CloseSelfPanel();
                    UIMgr.S.OpenPanel(UIID.MainSeaLevelPanel);
                }
            }
            // string reward = list[0].Reward;
            m_PanelData.currentLevelId.Value = levelId;
        }
        private void CreateReward(string reward)
        {
            //Item|1002|10;Exp_Role|100;Coin|100
            m_RewardContent.DestroyChildren();
            string[] list = reward.Split(';');

            for (int i = 0; i < list.Length; i++)
            {
                var levelObj = Instantiate(m_RewardItem.gameObject);
                levelObj.transform.SetParent(m_RewardContent);
                levelObj.transform.ResetTrans();
                levelObj.GetComponent<RewardItem>().OnUIInit(this, list[i]);
                levelObj.SetActive(true);
            }
        }
        private void BindModelToUI()
        {
            m_PanelData.currentLevelId = new IntReactiveProperty();
        }

        private void BindUIToModel()
        {

        }

    }
}
