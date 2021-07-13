using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;
using System;

namespace GameWish.Game
{
    public class TrainingPreparatorRoleModel
    {
        private RoleModel m_RoleModel;
        private TrainingRoomPanel m_TrainingRoomPanel;
        private TrainingRoomModel m_TrainingRoomModel;
        private TrainingPreparatorRole m_TrainingPreparatorRole;

        private ReactiveProperty<TrainingSlotModel> m_TrainingSlotModel = new ReactiveProperty<TrainingSlotModel>(null);

        private IDisposable m_EmptyDis;
        private IDisposable m_TrainingStateDis;

        public IReadOnlyReactiveProperty<bool> IsEmpty;
        public TrainingRoomModel TrainingRoomModel { get { return m_TrainingRoomModel; } }
        public TrainingSlotModel TrainingSlotModel { get { return m_TrainingSlotModel.Value; } }
        public TrainingRoomPanel TrainingRoomPanel { get { return m_TrainingRoomPanel; } }
        public TrainingPreparatorRole TrainingPreparatorRole { get { return m_TrainingPreparatorRole; } }
        #region Public
        public TrainingPreparatorRoleModel(RoleModel roleModel, TrainingRoomModel trainingRoomModel, TrainingRoomPanel trainingRoomPanel)
        {
            this.m_RoleModel = roleModel;
            this.m_TrainingRoomPanel = trainingRoomPanel;
            this.m_TrainingRoomModel = trainingRoomModel;
            m_TrainingSlotModel.Value = m_TrainingRoomModel.GetSlotModelByRoleID(m_RoleModel.id);

            IsEmpty = m_TrainingSlotModel.Select(val => val == null).ToReactiveProperty();

            m_EmptyDis = IsEmpty.Subscribe(val => HandleTrainingStateBind(val));
        }

        ~TrainingPreparatorRoleModel()
        {
            m_EmptyDis?.Dispose();
        }

        public bool IsClick()
        {
            if (!IsEmpty.Value && TrainingSlotModel.IsFree() && TrainingSlotModel.heroId.Value != -1)
            {
                return true;
            }
            return false;
        }

        public void RefreshTrainingSlotModel()
        {
            m_TrainingSlotModel.Value = m_TrainingRoomModel.GetSlotModelByRoleID(m_RoleModel.id);
        }

        /// <summary>
        /// 处理TrainingState的绑定
        /// </summary>
        /// <param name="val"></param>
        private void HandleTrainingStateBind(bool val)
        {
            if (!val)
                m_TrainingStateDis = TrainingSlotModel.trainState.Subscribe(state => HandleTrainState(state));
            else
                m_TrainingStateDis?.Dispose();
        }

        /// <summary>
        /// 处理变为空闲时
        /// </summary>
        /// <param name="state"></param>
        private void HandleTrainState(TrainingSlotState state)
        {
            if (state == TrainingSlotState.Free && TrainingSlotModel.heroId.Value == -1)
            {
                //m_TrainingPreparatorRole?.SetPrepRoleState(true);
                BindSoltAndRole();
                TrainingRoomPanel.SetDataCount();
            }
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="prepRole"></param>
        public void SetPrepRoleData(TrainingPreparatorRole prepRole)
        {
            this.m_TrainingPreparatorRole = prepRole;
        }

        /// <summary>
        /// 给role绑定slot数据
        /// </summary>
        /// <param name="trainingSlotModel"></param>
        public void BindSoltAndRole(TrainingSlotModel trainingSlotModel = null)
        {
            m_TrainingSlotModel.Value = trainingSlotModel;
        }

        /// <summary>
        /// 取消选择状态
        /// </summary>
        public void CancelSelectedState()
        {
            if (!IsEmpty.Value)
            {
                TrainingRoomPanel.ReduceSelectedCount();
                if (TrainingSlotModel.IsTraining())
                    m_TrainingPreparatorRole.SetPrepRoleState(false);
            }
        }

        /// <summary>
        /// 获得角色ID
        /// </summary>
        /// <returns></returns>
        public int GetRoleID()
        {
            return m_RoleModel.id;
        }
        #endregion
    }
}