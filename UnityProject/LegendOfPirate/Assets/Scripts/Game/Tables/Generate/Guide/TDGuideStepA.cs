//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDGuideStepA
    {
        
       
        private EInt m_Id = 0;   
        private EInt m_GuideID = 0;   
        private string m_Trigger;   
        private string m_Command;   
        private string m_CommonParam;   
        private EInt m_RequireStepId = 0;   
        private bool m_KeyFrame = false;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  id {get { return m_Id; } }
       
        /// <summary>
        /// GuideID
        /// </summary>
        public  int  guideID {get { return m_GuideID; } }
       
        /// <summary>
        /// Trigger
        /// </summary>
        public  string  trigger {get { return m_Trigger; } }
       
        /// <summary>
        /// Command
        /// </summary>
        public  string  command {get { return m_Command; } }
       
        /// <summary>
        /// CommonParam
        /// </summary>
        public  string  commonParam {get { return m_CommonParam; } }
       
        /// <summary>
        /// RequireStepId
        /// </summary>
        public  int  requireStepId {get { return m_RequireStepId; } }
       
        /// <summary>
        /// KeyFrame
        /// </summary>
        public  bool  keyFrame {get { return m_KeyFrame; } }
       

        public void ReadRow(DataStreamReader dataR, int[] filedIndex)
        {
          //var schemeNames = dataR.GetSchemeName();
          int col = 0;
          while(true)
          {
            col = dataR.MoreFieldOnRow();
            if (col == -1)
            {
              break;
            }
            switch (filedIndex[col])
            { 
            
                case 0:
                    m_Id = dataR.ReadInt();
                    break;
                case 1:
                    m_GuideID = dataR.ReadInt();
                    break;
                case 2:
                    m_Trigger = dataR.ReadString();
                    break;
                case 3:
                    m_Command = dataR.ReadString();
                    break;
                case 4:
                    m_CommonParam = dataR.ReadString();
                    break;
                case 5:
                    m_RequireStepId = dataR.ReadInt();
                    break;
                case 6:
                    m_KeyFrame = dataR.ReadBool();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(7);
          
          ret.Add("Id", 0);
          ret.Add("GuideID", 1);
          ret.Add("Trigger", 2);
          ret.Add("Command", 3);
          ret.Add("CommonParam", 4);
          ret.Add("RequireStepId", 5);
          ret.Add("KeyFrame", 6);
          return ret;
        }
    } 
}//namespace LR