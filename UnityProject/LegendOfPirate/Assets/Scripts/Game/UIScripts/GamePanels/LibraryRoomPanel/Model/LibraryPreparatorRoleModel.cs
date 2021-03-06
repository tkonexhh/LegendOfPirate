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
    public class LibraryPreparatorRoleModel
    {
        private RoleModel m_RoleModel;
        private LibraryModel m_LibraryModel;
        private ReactiveProperty<LibrarySlotModel> m_LibrarySlotModel;

        public IReadOnlyReactiveProperty<bool> IsBindSlot;
        public LibrarySlotModel LibrarySlotModel { get { return m_LibrarySlotModel.Value; } }

        private IDisposable m_IsBindSlot;
        private IDisposable m_LibraryState;
        ~LibraryPreparatorRoleModel()
        {
            m_IsBindSlot?.Dispose();
            m_LibraryState?.Dispose();
        }

        public LibraryPreparatorRoleModel(RoleModel roleModel, LibraryModel libraryModel)
        {
            m_RoleModel = roleModel;
            m_LibraryModel = libraryModel;
            m_LibrarySlotModel = new ReactiveProperty<LibrarySlotModel>(libraryModel.GetSlotModelByRoleID(roleModel.id));

            IsBindSlot = m_LibrarySlotModel.Select(val => val != null).ToReactiveProperty();
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
            return IsBindSlot.Value ? (LibrarySlotModel.IsHeroSelected()) : false;
        }

        /// <summary>
        ///  自动选择弟子
        /// </summary>
        /// <param name="librarySlotModel"></param>
        public void AutoSelectRole(LibrarySlotModel librarySlotModel)
        {
            if (!IsBindSlot.Value)
            {
                m_LibrarySlotModel.Value = librarySlotModel;
            }
        }

        /// <summary>
        /// 点击角色
        /// </summary>
        public void OnClickRoleModel()
        {
            if (IsBindSlot.Value)
            {
                m_LibrarySlotModel.Value.ClearRoleSelect();
                m_LibraryModel.ReduceSelectedNumer();
                m_LibrarySlotModel.Value = null;
            }
            else
            {
                LibrarySlotModel librarySlotModel = m_LibraryModel.GetFreeSlot();
                if (librarySlotModel != null)
                {
                    m_LibrarySlotModel.Value = librarySlotModel;
                    m_LibrarySlotModel.Value.RoleSelect(m_RoleModel.id);
                    m_LibraryModel.AddSelectedNumer();
                }
                else
                    FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.LIBRARYROOM_CONT_Ⅰ);
            }
        }

        /// <summary>
        /// 结束阅读
        /// </summary>
        public void EndTask()
        {
            m_LibrarySlotModel.Value = null;
        }

        /// <summary>
        /// 清除选择的角色
        /// </summary>
        public void ClearRoleSelect()
        {
            if (IsBindSlot.Value && LibrarySlotModel.IsHeroSelected())
            {
                m_LibrarySlotModel.Value.ClearRoleSelect();
                m_LibrarySlotModel.Value = null;
            }
        }
        #endregion
    }
}