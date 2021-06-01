using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class TestPanelData : UIPanelData
	{
        public IntReactiveProperty level;
        public StringReactiveProperty name;

        public TestPanelData()
		{
		}

        public override void OnCacheReset()
        {
            base.OnCacheReset();

            level = null;
            name = null;
        }
    }
	
	public partial class TestPanel
	{
		private TestPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<TestPanelData>();

            LevelModel levelModel = ModelMgr.S.GetModel<LevelModel>();
            m_PanelData.name = levelModel.testName;
            m_PanelData.level = levelModel.curLevel;
        }
		
		private void ReleasePanelData()
		{
			ObjectPool<TestPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
            m_PanelData.name.SubscribeToText(Txt_);

        }
		
		private void BindUIToModel()
		{
            Btn_.OnClickAsObservable().Subscribe(_ => {
                m_PanelData.name.Value = "111";
            });
		}
		
	}
}
