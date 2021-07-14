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
    public class TrainingSlotModel : Model
    {
        private TrainingRoomModel m_TrainingModel;
        private TrainingDBData m_DbItem;

        private DateTime m_TrainingEndTime;

        public ReactiveProperty<TrainingSlotState> trainingState;
        public IntReactiveProperty heroID;
        public StringReactiveProperty unlockLevel;
        public StringReactiveProperty trainingCountDown;
        public FloatReactiveProperty timeProgressBar;

        public ReactiveCommand refreshCommand = new ReactiveCommand();
        public TrainingDBData DBItem { get { return m_DbItem; } }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (IsReading())
            {
                trainingCountDown.Value = GetLearnCountDown();
                timeProgressBar.Value = GetTimeProgressBar();
            }
        }

        public TrainingSlotModel(TrainingRoomModel TrainingModel, TrainingDBData dbItem)
        {
            m_TrainingModel = TrainingModel;
            m_DbItem = dbItem;

            RefreshEndTime();

            trainingState = new ReactiveProperty<TrainingSlotState>(m_DbItem.trainingState);
            heroID = new IntReactiveProperty(m_DbItem.heroId);
            unlockLevel = new StringReactiveProperty(GetUnlockLevelStr());
            trainingCountDown = new StringReactiveProperty(Define.DEFAULT_SOUND);
            timeProgressBar = new FloatReactiveProperty(1);
        }

        #region Public
        public bool IsHeroSelected()
        {
            return trainingState.Value == TrainingSlotState.HeroSelected;
        }

        public bool IsReading()
        {
            return trainingState.Value == TrainingSlotState.Training;
        }

        public bool IsFree()
        {
            return trainingState.Value == TrainingSlotState.Free;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="TrainingSlotState"></param>
        public void SetTrainingSlotState(TrainingSlotState TrainingSlotState)
        {
            trainingState.Value = TrainingSlotState;

            m_TrainingModel.DBData.SetTrainingSlotState(m_DbItem.slotId, trainingState.Value);
        }

        /// <summary>
        /// 角色选择
        /// </summary>
        /// <param name="id"></param>
        public void RoleSelect(int id)
        {
            heroID.Value = id;

            trainingState.Value = TrainingSlotState.HeroSelected;
        }

        /// <summary>
        /// 清除选择的角色
        /// </summary>
        public void ClearRoleSelect()
        {
            heroID.Value = -1;

            trainingState.Value = TrainingSlotState.Free;
        }

        public void AutoSelectAndStart(int id)
        {
            heroID.Value = id;

            trainingState.Value = TrainingSlotState.HeroSelected;

            StartRead();
        }

        /// <summary>
        /// 开始阅读
        /// </summary>
        public void StartRead()
        {
            if (IsHeroSelected())
            {
                SetTrainingSlotState(TrainingSlotState.Training);

                m_TrainingModel.DBData.SetTrainingSlotState(m_DbItem.slotId, trainingState.Value);

                m_TrainingModel.DBData.SetTrainingRoleID(m_DbItem.slotId, heroID.Value);

                ModelMgr.S.GetModel<RoleGroupModel>().SetRoleManagementState(heroID.Value, ManagementRoleState.TrainingRoom);

                RefreshEndTime();
            }
        }

        /// <summary>
        /// 图书室升级
        /// </summary>
        public void UnlockTrainingSolt()
        {
            trainingState.Value = TrainingSlotState.Free;

            m_TrainingModel.DBData.SetTrainingSlotState(m_DbItem.slotId, trainingState.Value);
        }
        #endregion

        #region Private
        /// <summary>
        /// 刷新结束时间
        /// </summary>
        private void RefreshEndTime()
        {
            int totalTime = m_TrainingModel.TableConfig.trainingTime;
            m_TrainingEndTime = m_DbItem.trainingStartTime + TimeSpan.FromSeconds(totalTime);
        }

        /// <summary>
        /// 获取倒计时
        /// </summary>
        /// <returns></returns>
        private string GetLearnCountDown()
        {
            TimeSpan remain = m_TrainingEndTime - DateTime.Now;
            if (remain.TotalSeconds <= 0)
            {
                EndRead();
                return Define.DEFAULT_SOUND;
            }
            return CommonMethod.SplicingTime((int)remain.TotalSeconds);
        }

        /// <summary>
        /// 结束训练
        /// </summary>
        private void EndRead()
        {
            ModelMgr.S.GetModel<RoleGroupModel>().SetRoleManagementState(heroID.Value);
            TrainingPreparatorRoleModel TrainingPreparatorRole = m_TrainingModel.GetTrainingRoleBySlotID(m_DbItem.slotId);
            SetTrainingSlotState(TrainingSlotState.Free);
            heroID.Value = -1;
            m_TrainingModel.DBData.SetTrainingRoleID(m_DbItem.slotId, heroID.Value);
            if (TrainingPreparatorRole != null)
            {
                TrainingPreparatorRole.EndTask();

                m_TrainingModel.SortRefreshList();
                refreshCommand.Execute();
            }
            else
                Log.e("Error : Not fid Id = " + m_DbItem.slotId);
        }

        /// <summary>
        /// 获取倒计时进度
        /// </summary>
        /// <returns></returns>
        private float GetTimeProgressBar()
        {
            TimeSpan remain = m_TrainingEndTime - DateTime.Now;

            if (remain.TotalSeconds <= 0)
            {
                return 0;
            }
            return (float)remain.TotalSeconds / m_TrainingModel.TableConfig.trainingTime;
        }

        /// <summary>
        /// 获取解锁等级文本
        /// </summary>
        /// <returns></returns>
        private string GetUnlockLevelStr()
        {
            return string.Format(LanguageKeyDefine.FIXED_TITLE_LV_Ⅱ, m_DbItem.slotId);
        }
        #endregion
    }
}