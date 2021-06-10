using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class BuildingLevelUpPanelData : UIPanelData
	{
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
		
		private void ReleasePanelData()
		{
			ObjectPool<BuildingLevelUpPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
	public interface IBuildingLevelUpStrategy 
	{
		Model GetBuildingModel();
		void BuildingLevelUp(Model BuildingModel);
	}
    public class FishingUpLevelStrategy : IBuildingLevelUpStrategy
    {
        public void BuildingLevelUp(Model BuildingModel)
        {
			//TODO:升级对应Model
		}

		public Model GetBuildingModel()
        {
			return null;
			//Todo:返回对应Model
		}
	}
}
