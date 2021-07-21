using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Collections.Generic;

namespace GameWish.Game
{
	public class PubPanelData : UIPanelData
	{
		public PubModel pubModel;
		public PubPanelData()
		{
		}
	}
	
	public partial class PubPanel
	{
		private PubPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<PubPanelData>();

			m_PanelData.pubModel = ModelMgr.S.GetModel<PubModel>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<PubPanelData>.S.Recycle(m_PanelData);
		}	
		
		private void BindModelToUI()
		{
			m_PanelData.pubModel.singleDraw.SubscribeToTextMeshPro(m_RraintTimes).AddTo(this);
			m_PanelData.pubModel.continuousPumping.SubscribeToTextMeshPro(m_RecruitTimes).AddTo(this);
			m_PanelData.pubModel.remainingTimes.SubscribeToTextMeshPro(m_RemainTimes).AddTo(this);

			m_RecruitBtn.OnClickAsObservable().Subscribe(_ => RecruitBtn()).AddTo(this);
			m_TenRecruitBtn.OnClickAsObservable().Subscribe(_ => TenRecruitBtn()).AddTo(this);
		}

		private void BindUIToModel()
		{
			
		}
	}
}
