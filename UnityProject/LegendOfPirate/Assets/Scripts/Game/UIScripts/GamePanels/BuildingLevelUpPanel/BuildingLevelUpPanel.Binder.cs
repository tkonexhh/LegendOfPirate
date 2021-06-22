using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using GFrame;


namespace GameWish.Game
{
	public class BuildingLevelUpPanelData : UIPanelData
	{
		public ShipUnitModel buidingModel;
		public BuildingLevelUpData levelUpData;
		public ShipUnitType unitType;
		public BuildingLevelUpPanelData()
		{

		}

    }
	
	public partial class BuildingLevelUpPanel
	{
		private BuildingLevelUpPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			
			 m_PanelData = UIPanelData.Allocate<BuildingLevelUpPanelData>();
			
        }

		private void SetPanelData(params object[] args) 
		{
            if (args != null && args.Length > 0)
            {
                m_PanelData.unitType = (ShipUnitType)args[0];
                m_PanelData.buidingModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(m_PanelData.unitType);
                m_PanelData.levelUpData = BuildingLevelUpDataFactory.S.GetBuildingLevelUpData(m_PanelData.unitType);
			}
			BindModelToUI();
        }

        private void ReleasePanelData()
		{
			ObjectPool<BuildingLevelUpPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
        {
			m_PanelData.buidingModel.level.AsObservable().Subscribe(level => OnLevelChange(level)).AddTo(this);
		}
		
		private void BindUIToModel()
		{
			CloseBtn.OnClickAsObservable().Subscribe(_ =>HideSelfWithAnim()).AddTo(this);
			LevelUpBtn.OnClickAsObservable().Subscribe(_ => OnLevelUpBtnClick()).AddTo(this);
		}
		private void OnLevelChange(int level) 
		{
			CleanLevelUpElement();
			m_PanelData.buidingModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(m_PanelData.unitType);
			Title.text = m_PanelData.levelUpData.buildingName + " to levelup";
			LevelCount.text = string.Format("lv.{0}¡úlv.{1}", level, level + 1);
			BuildingName.text = m_PanelData.levelUpData.buildingName;
			for (int i = 0; i < m_PanelData.levelUpData.needElementCounts.Count&&i< m_PanelData.levelUpData.needElementIds.Count; i++) 
			{
				if (0 >= m_PanelData.levelUpData.needElementCounts[i]) continue;
				var cobj = Instantiate(Element_Temp);
				cobj.GetComponentInChildren<GTextMeshProUGUI>().text = string.Format("0/{0}", m_PanelData.levelUpData.needElementCounts[i]);
				cobj.transform.SetParent(ElementList.transform);
				cobj.GetComponent<RectTransform>().SetScaleXYZ(1, 1, 1);
			}
		}
		private void OnLevelUpBtnClick() 
		{
#if UNITY_EDITOR
			m_PanelData.buidingModel.OnLevelUpgrade(1);
#endif
		}
		private void CleanLevelUpElement() 
		{
			ElementList.DestroyChildren();
			
		}
    }

}
