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
    public class LibrarySlotModel : Model
    {
        private LibraryModel m_LibraryModel;
        private LibraryDBData m_DbItem;

        private DateTime m_ReadEndTime;

        public ReactiveProperty<LibrarySlotState> libraryState;
        public IntReactiveProperty heroID;
        public StringReactiveProperty unlockLevel;
        public StringReactiveProperty learnCountDown;
        public FloatReactiveProperty timeProgressBar;

        public ReactiveCommand refreshCommand = new ReactiveCommand();
        public LibraryDBData DBItem { get { return m_DbItem; } }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (IsReading())
            {
                learnCountDown.Value = GetLearnCountDown();
                timeProgressBar.Value = GetTimeProgressBar();
            }
        }

        public LibrarySlotModel(LibraryModel libraryModel, LibraryDBData dbItem)
        {
            m_LibraryModel = libraryModel;
            m_DbItem = dbItem;

            RefreshEndTime();

            libraryState = new ReactiveProperty<LibrarySlotState>(m_DbItem.libraryState);
            heroID = new IntReactiveProperty(m_DbItem.heroId);
            unlockLevel = new StringReactiveProperty(GetUnlockLevelStr());
            learnCountDown = new StringReactiveProperty(Define.DEFAULT_SOUND);
            timeProgressBar = new FloatReactiveProperty(1);
        }

        #region Public
        public bool IsHeroSelected()
        {
            return libraryState.Value == LibrarySlotState.HeroSelected;
        }

        public bool IsReading()
        {
            return libraryState.Value == LibrarySlotState.Reading;
        }

        public bool IsFree()
        {
            return libraryState.Value == LibrarySlotState.Free;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="librarySlotState"></param>
        public void SetLibrarySlotState(LibrarySlotState librarySlotState)
        {
            libraryState.Value = librarySlotState;

            m_LibraryModel.DBData.SetLibrarySlotState(m_DbItem.slotId, libraryState.Value);
        }

        /// <summary>
        /// 角色选择
        /// </summary>
        /// <param name="id"></param>
        public void RoleSelect(int id)
        {
            heroID.Value = id;

            libraryState.Value = LibrarySlotState.HeroSelected;
        }

        /// <summary>
        /// 清除选择的角色
        /// </summary>
        public void ClearRoleSelect()
        {
            heroID.Value = -1;

            libraryState.Value = LibrarySlotState.Free;
        }

        public void AutoSelectAndStart(int id)
        {
            heroID.Value = id;

            libraryState.Value = LibrarySlotState.HeroSelected;

            StartRead();
        }

        /// <summary>
        /// 开始阅读
        /// </summary>
        public void StartRead()
        {
            if (IsHeroSelected())
            {
                SetLibrarySlotState(LibrarySlotState.Reading);

                m_LibraryModel.DBData.SetLibrarySlotState(m_DbItem.slotId, libraryState.Value);

                m_LibraryModel.DBData.SetReadHeroID(m_DbItem.slotId, heroID.Value);

                ModelMgr.S.GetModel<RoleGroupModel>().SetRoleManagementState(heroID.Value, ManagementRoleState.LibraryRoom);

                RefreshEndTime();
            }
        }

        /// <summary>
        /// 图书室升级
        /// </summary>
        public void UnlockLibrarySolt()
        {
            libraryState.Value = LibrarySlotState.Free;

            m_LibraryModel.DBData.SetLibrarySlotState(m_DbItem.slotId, libraryState.Value);
        }
        #endregion

        #region Private
        /// <summary>
        /// 刷新结束时间
        /// </summary>
        private void RefreshEndTime()
        {
            int totalTime = m_LibraryModel.TableConfig.readingSpeed;
            m_ReadEndTime = m_DbItem.ReadingStartTime + TimeSpan.FromSeconds(totalTime);
        }

        /// <summary>
        /// 获取倒计时
        /// </summary>
        /// <returns></returns>
        private string GetLearnCountDown()
        {
            TimeSpan remain = m_ReadEndTime - DateTime.Now;
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
            LibraryPreparatorRoleModel libraryPreparatorRole = m_LibraryModel.GetLibraryRoleBySlotID(m_DbItem.slotId);
            SetLibrarySlotState(LibrarySlotState.Free);
            heroID.Value = -1;
            if (libraryPreparatorRole != null)
            {
                libraryPreparatorRole.EndRead();

                m_LibraryModel.SortRefreshList();
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
            TimeSpan remain = m_ReadEndTime - DateTime.Now;

            if (remain.TotalSeconds <= 0)
            {
                return 0;
            }
            return (float)remain.TotalSeconds / m_LibraryModel.TableConfig.readingSpeed;
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