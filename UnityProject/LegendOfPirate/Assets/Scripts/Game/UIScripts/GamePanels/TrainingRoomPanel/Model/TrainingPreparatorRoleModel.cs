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
        private TrainingRoomModel m_TrainingModel;
        private ReactiveProperty<TrainingSlotModel> m_TrainingSlotModel;

        public IReadOnlyReactiveProperty<bool> IsBindSlot;
        public TrainingSlotModel TrainingSlotModel { get { return m_TrainingSlotModel.Value; } }

        private IDisposable m_IsBindSlot;
        private IDisposable m_TrainingState;

        ~TrainingPreparatorRoleModel()
        {
            m_IsBindSlot?.Dispose();
            m_TrainingState?.Dispose();
        }

        public TrainingPreparatorRoleModel(RoleModel roleModel, TrainingRoomModel trainingRoomModel)
        {
            m_RoleModel = roleModel;
            m_TrainingModel = trainingRoomModel;
            m_TrainingSlotModel = new ReactiveProperty<TrainingSlotModel>(trainingRoomModel.GetSlotModelByRoleID(roleModel.id));

            IsBindSlot = m_TrainingSlotModel.Select(val => val != null).ToReactiveProperty();
        }
        #region Public
        /// <summary>
        /// 获得角色ID
        /// </summary>
        /// <returns></returns>
        public int GetRoleID()
        {
            return m_RoleModel.id;
        }

        /// <summary>
        /// 是否选择状态
        /// </summary>
        /// <returns></returns>
        public bool IsHeroSelect()
        {
            return IsBindSlot.Value ? (TrainingSlotModel.IsHeroSelected()) : false;
        }

        /// <summary>
        ///  自动选择弟子
        /// </summary>
        /// <param name="librarySlotModel"></param>
        public void AutoSelectRole(TrainingSlotModel trainingSlotModel)
        {
            if (!IsBindSlot.Value)
            {
                m_TrainingSlotModel.Value = trainingSlotModel;
            }
        }

        /// <summary>
        /// 点击角色
        /// </summary>
        public void OnClickRoleModel()
        {
            if (IsBindSlot.Value)
            {
                m_TrainingSlotModel.Value.ClearRoleSelect();
                m_TrainingModel.ReduceSelectedNumer();
                m_TrainingSlotModel.Value = null;
            }
            else
            {
                TrainingSlotModel trainingSlotModel = m_TrainingModel.GetFreeSlot();
                if (trainingSlotModel != null)
                {
                    m_TrainingSlotModel.Value = trainingSlotModel;
                    m_TrainingSlotModel.Value.RoleSelect(m_RoleModel.id);
                    m_TrainingModel.AddSelectedNumer();
                }
                else
                    FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.LIBRARYROOM_CONT_Ⅰ);
            }
        }

        /// <summary>
        /// 结束训练
        /// </summary>
        public void EndTask()
        {
            m_TrainingSlotModel.Value = null;
        }

        /// <summary>
        /// 清除选择的角色
        /// </summary>
        public void ClearRoleSelect()
        {
            if (IsBindSlot.Value && TrainingSlotModel.IsHeroSelected())
            {
                m_TrainingSlotModel.Value.ClearRoleSelect();
                m_TrainingSlotModel.Value = null;
            }
        }
        #endregion
    }
}