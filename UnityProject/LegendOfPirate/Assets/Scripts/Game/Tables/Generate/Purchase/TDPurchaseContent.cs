//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDPurchaseContent
    {
        
       
        private string m_Id;   
        private string m_Gold;   
        private string m_Missile;   
        private string m_BigBoom;   
        private string m_Umbrella;   
        private string m_Hammer;   
        private string m_Scissors;   
        private string m_Glove;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  string  id {get { return m_Id; } }
       
        /// <summary>
        /// ID
        /// </summary>
        public  string  gold {get { return m_Gold; } }
       
        /// <summary>
        /// ID
        /// </summary>
        public  string  missile {get { return m_Missile; } }
       
        /// <summary>
        /// ID
        /// </summary>
        public  string  bigBoom {get { return m_BigBoom; } }
       
        /// <summary>
        /// ID
        /// </summary>
        public  string  umbrella {get { return m_Umbrella; } }
       
        /// <summary>
        /// ID
        /// </summary>
        public  string  hammer {get { return m_Hammer; } }
       
        /// <summary>
        /// ID
        /// </summary>
        public  string  scissors {get { return m_Scissors; } }
       
        /// <summary>
        /// ID
        /// </summary>
        public  string  glove {get { return m_Glove; } }
       

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
                    m_Id = dataR.ReadString();
                    break;
                case 1:
                    m_Gold = dataR.ReadString();
                    break;
                case 2:
                    m_Missile = dataR.ReadString();
                    break;
                case 3:
                    m_BigBoom = dataR.ReadString();
                    break;
                case 4:
                    m_Umbrella = dataR.ReadString();
                    break;
                case 5:
                    m_Hammer = dataR.ReadString();
                    break;
                case 6:
                    m_Scissors = dataR.ReadString();
                    break;
                case 7:
                    m_Glove = dataR.ReadString();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(8);
          
          ret.Add("Id", 0);
          ret.Add("Gold", 1);
          ret.Add("Missile", 2);
          ret.Add("BigBoom", 3);
          ret.Add("Umbrella", 4);
          ret.Add("Hammer", 5);
          ret.Add("Scissors", 6);
          ret.Add("Glove", 7);
          return ret;
        }
    } 
}//namespace LR