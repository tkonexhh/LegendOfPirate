using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace GameWish.Game
{

    public class LibraryModel : ShipUnitModel
    {
        public LibraryUnitConfig tableConfig; 
        public List<LibrarySlotModel> slotModelList = new List<LibrarySlotModel>();

        private LibraryData m_DbData;

        public LibraryModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityLibraryTable.GetConfig(level.Value);

            level.Subscribe(val => {
                tableConfig = TDFacilityLibraryTable.GetConfig(level.Value);
                for (int i = 0; i < slotModelList.Count; i++)
                {
                    slotModelList[i].OnLibraryLevelUp();
                }
            });

            m_DbData = GameDataMgr.S.GetData<LibraryData>();
            for (int i = 0; i < m_DbData.libraryItemList.Count; i++)
            {
                LibrarySlotModel slotModel = new LibrarySlotModel(this, m_DbData.libraryItemList[i]);
                slotModelList.Add(slotModel);
            }
        }

        private float m_RefreshTime = 0;
        private float m_RefreshInterval = 0.3f;
        public override void OnUpdate()
        {
            m_RefreshTime += Time.deltaTime;
            if (m_RefreshTime >= m_RefreshInterval)
            {
                m_RefreshTime = 0;

                for (int i = 0; i < slotModelList.Count; i++)
                {
                    slotModelList[i].RefreshRemainTime();
                }
            }
        }
    }

   public class LibrarySlotModel : Model
    {

        public int slotId;
        public int heroId = -1;
        public FloatReactiveProperty libraryRemainTime = new FloatReactiveProperty(-1);

        public ReactiveProperty<LibrarySlotState> libraryState;

        private DateTime m_StartTime = default(DateTime);
        private DateTime m_EndTime = default(DateTime);

        private LibraryModel m_LibraryMode;
        private LibraryData.LibraryDataItem m_DbItem;

        public LibrarySlotModel(LibraryModel libraryModel, LibraryData.LibraryDataItem dbItem)
        {
            m_LibraryMode = libraryModel;
            m_DbItem = dbItem;

            this.slotId = dbItem.slotId;
            this.heroId = dbItem.heroId;
            this.libraryState = new ReactiveProperty<LibrarySlotState>(dbItem.libraryState);

            switch (libraryState.Value)
            {
                case LibrarySlotState.Free:
                    break;
                case LibrarySlotState.Reading:
                    SetTime(dbItem.ReadingStartTime);
                    RefreshRemainTime();
                    break;
                case LibrarySlotState.Locked:
                    break;
            }
        }

        #region Public Set
        public void StartReading(DateTime startTime)
        {
            SetTime(startTime);

            libraryState.Value = LibrarySlotState.Reading;

            m_DbItem.OnStartReading(heroId, startTime);
        }

        public void EndReading()
        {
            heroId = -1;
            libraryRemainTime.Value = -1f;
            libraryState.Value = LibrarySlotState.Free;
            m_StartTime = default(DateTime);
            m_EndTime = default(DateTime);

            m_DbItem.OnEndTraining();
            //TODO...   增加人物经验
            //ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(heroId).AddCurExp(m_TrainingRoomMode.tableConfig.experience);

        }

        public void OnHeroUnselected()
        {
            heroId = -1;
            libraryState.Value = LibrarySlotState.Free;
            m_DbItem.OnHeroUnselected();
        }

        public void OnHeroSelected(int id)
        {
            heroId = id;
            libraryState.Value = LibrarySlotState.HeroSelected;
            m_DbItem.OnHeroSelected(id);
        }

        public void RefreshRemainTime()
        {
            if (m_DbItem.libraryState != LibrarySlotState.Reading)
                return;

            double remainTime = (m_EndTime - DateTime.Now).TotalSeconds;

            libraryRemainTime.Value = (float)remainTime;
            if (libraryRemainTime.Value <= 0f)
            {
                EndReading();
            }
        }

        public void OnLibraryLevelUp()
        {
            if (libraryState.Value == LibrarySlotState.Locked && m_LibraryMode.tableConfig.capacity >= slotId)
            {
                libraryState.Value = LibrarySlotState.Free;

                m_DbItem.OnUnlocked();
            }
        }
        #endregion

        private void SetTime(DateTime startTime)
        {
            m_StartTime = startTime;
            int totalTime = m_LibraryMode.tableConfig.readingSpeed;
            m_EndTime = m_StartTime + TimeSpan.FromSeconds(totalTime);
        }
    }

    public enum LibrarySlotState
    {
        /// <summary>
        /// 空闲中
        /// </summary>
        Free = 0,
        /// <summary>
        /// 训练中
        /// </summary>
        Reading = 1,
        /// <summary>
        /// 未解锁
        /// </summary>
        Locked = 2,
        /// <summary>
        /// 选择但是未开始
        /// </summary>
        HeroSelected = 3,
    }
}