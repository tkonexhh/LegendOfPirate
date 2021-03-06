using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class TableModule : AbstractTableModule
    {

        protected override void OnTableLoadFinish()
        {
            TDConstTable.InitArrays(typeof(ConstType));

            //处理所有表的重建

            Log.i("Load table finished");
        }

        protected override void InitPreLoadTableMetaData()
        {
            TableConfig.preLoadTableArray = new TDTableMetaData[]
            {
                // Default table
                TDConstTable.metaData,
                TDLanguageTable.GetLanguageMetaData(),
                TDGuideTable.metaData,
                TDGuideStepTable.metaData,
                TDSocialAdapterTable.metaData,
                TDAdConfigTable.metaData,
                TDAdPlacementTable.metaData,
                TDAppConfigTable.metaData,
                TDRemoteConfigTable.metaData,
                TDPurchaseTable.metaData,
                TDEquipmentConfigTable.metaData,
                TDGlobalConfigTable.metaData,
                TDMaterialConfigTable.metaData,
                TDRoleConfigTable.metaData,
                TDRoleSkillConfigTable.metaData,
                TDMarinLevelConfigTable.metaData,
                TDSmuggleTable.metaData,
                

               
                // Game play table        
                //TDMainTaskTable.metaData,
                //TDLevelConfigTable.metaData,
                #region Facility
                TDFacilityKitchenTable.metaData,
                TDFacilityFishingPlatformTable.metaData,
                TDFacilityForgeTable.metaData,
                TDFacilityGardenTable.metaData,
                //TDFacilityIslandTable.metaData,
                TDFacilityLaboratoryTable.metaData,
                TDFacilityLibraryTable.metaData,
                TDFacilityProcessingRoomTable.metaData,
                TDFacilityTrainingRoomTable.metaData,
                TDFacilityBattleshipTable.metaData,
                #endregion
                #region InternalPurchase
                TDMonthCardConfigTable.metaData,
                TDDailySelectionConfigTable.metaData,
                TDPayShopConfigTable.metaData,
                #endregion
                #region Blackregion
                TDBlackMarketConfigTable.metaData,
                TDBlackMarketRefreshConfigTable.metaData,
                #endregion  
                #region Task
                TDDailyTaskTable.metaData,
                TDDailyTaskRewardTable.metaData,
                TDMainTaskTable.metaData,
                TDAchievementTaskTable.metaData,
                #endregion
                #region Pub
                TDPubTable.metaData,
                #endregion
                #region Synthesis
                TDEquipmentSynthesisConfigTable.metaData,
                TDFoodSynthesisConfigTable.metaData,
                TDPartSynthesisConfigTable.metaData,
                TDPotionSynthesisConfigTable.metaData,
                #endregion
            };
        }
    }
}
