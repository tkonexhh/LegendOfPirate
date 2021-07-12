using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
    public class TrainingPreparatorRoleModel
    {
        private RoleModel m_RoleModel;
        private TrainingRoomPanel m_TrainingRoomPanel;
        private TrainingRoomModel m_TrainingRoomModel;
        private TrainingPreparatorRole m_TrainingPreparatorRole;

        private ReactiveProperty<TrainingSlotModel> m_TrainingSlotModel = new ReactiveProperty<TrainingSlotModel>(null);

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
        }

        public void SetPrepRoleData(TrainingPreparatorRole prepRole)
        {
            this.m_TrainingPreparatorRole = prepRole;
        }

        public void BindSoltAndRole(TrainingSlotModel trainingSlotModel = null)
        {
            m_TrainingSlotModel.Value = trainingSlotModel;
        }
     

        public int GetRoleID()
        {
            return m_RoleModel.id;
        }
        #endregion
    }
}