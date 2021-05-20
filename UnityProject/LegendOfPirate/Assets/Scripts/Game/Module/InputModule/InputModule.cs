using UnityEngine;
using System.Collections;
using Qarth;
using UnityEngine.SceneManagement;

namespace GameWish.Game
{
    public class InputModule : AbstractModule
    {
        private IInputter m_KeyboardInputter;
        private KeyCodeTracker m_KeyCodeTracker;

        public override void OnComLateUpdate(float dt)
        {
            m_KeyboardInputter.LateUpdate();
            m_KeyCodeTracker.LateUpdate();
        }

        protected override void OnComAwake()
        {
            m_KeyCodeTracker = new KeyCodeTracker();
            m_KeyCodeTracker.SetDefaultProcessListener(ShowBackKeydownTips);

            m_KeyboardInputter = new KeyboardInputter();
            m_KeyboardInputter.RegisterKeyCodeMonitor(KeyCode.F1, null, OnClickF1, null);
            m_KeyboardInputter.RegisterKeyCodeMonitor(KeyCode.F2, null, OnClickF2, null);
            m_KeyboardInputter.RegisterKeyCodeMonitor(KeyCode.F3, null, OnClickF3, null);
            m_KeyboardInputter.RegisterKeyCodeMonitor(KeyCode.F4, null, OnClickF4, null);
        }

        private void ShowBackKeydownTips()
        {
            //WeGameSdkAdapter.S.ExitGame(null);
            if (PlayerPrefs.GetInt("channel_exit_key", 0) == 1)
            {
                FloatMessage.S.ShowMsg(TDLanguageTable.Get("Press Again to Quit"));
            }           
        }

        private void OnClickF1()
        {
            MeasureUnitHelper.AddCount(0,0,-50,-500);
            Log.i(MeasureUnitHelper.GetTotalCount());
        }
        int adtype = (int)AdType.PowerUp;
        private void OnClickF2()
        {

           //  EventSystem.S.Send(EventID.OnShowPopAdUI, AdType.SummonGiant);

             EventSystem.S.Send(EventID.OnShowPopAdUI, AdType.SummonReinforcements);

            // adtype++;
            // UIMgrExtend.S.OpenOccupyOverPanel(TDStageTable.GetData(101));
            // UIMgrExtend.S.OpenUnlockSoldierPanel(TDSoldierListTable.GetData(1));
            // EventSystem.S.Send(EventID.GuideEventTrigger, 1);
            //RateMgr.S.RequestReview();
        }

        private void OnClickF3()
        {
            //GameDataMgr.S.GetPlayerInfoData().AddCoinNum(10000);

            //  AdEffectHandleMgr.S.Handle(AdType.AutoDoubleSummon, null);
            // UIMgrExtend.S.OpenAdStaticShowPanel(AdType.AutoDoubleSummon);
            //MagicCloudMgr.S.StartCloudAttack();
        }
        int add = 1;
        private void OnClickF4()
        {
            //UIMgrExtend.S.OpenAdDynamicShowPanel(AdType.SummonReinforcements,2,10);
            // UIMgrExtend.S.OpenOccupyOverPanel(TDStageTable.GetData(101));
            //UIMgrExtend.S.OpenMissionCompletePanel("AAAA", "任务完成信息");
            //  MagicCloudMgr.S.Init();
            //   SoldierMgr.S.ResumeAllSoldierBehave();
            //UIMgr.S.OpenPanel(UIID.MilestonePanel, add++);
            //if(add > 5)
            //{
            //    add = 5;
            //}

        }

        private void OnSceneLoadResult(string sceneName, bool result)
        {
            Log.i("SceneLoad:" + sceneName + " " + result);
        }
    }
}
