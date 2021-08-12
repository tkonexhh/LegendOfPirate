using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDPayShopConfigTable
    {
        public static PayShopConfig[] payShopProperties = null;
        private static int m_Index = 0;
        static void CompleteRowAdd(TDPayShopConfig tdData, int rowCount)
        {
            try
            {
                if (payShopProperties == null)
                    payShopProperties = new PayShopConfig[rowCount];
                payShopProperties[m_Index] = new PayShopConfig(tdData);
                m_Index++;
            }
            catch (Exception e)
            {
                Log.e("error : id = " + tdData.id);
                Log.e("e : " + e);
            }
        }

        /// <summary>
        /// 根据 id 获取礼包
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PayShopConfig GetPayShopConfigByID(int id)
        {
            foreach (var item in payShopProperties)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
            Log.e("Not find id = " + id);
            return default(PayShopConfig);
        }

        #region Struct
        public struct PayShopItme
        {
            public int id;
            public int count;
            public PayShopItme(string str)
            {
                string[] strs = str.Split('|');
                this.id = int.Parse(strs[0]);
                this.count = int.Parse(strs[1]);
            }
        }

        public struct PayShopConfig
        {
            public int id;
            public string giftName;
            public float giftPrice;
            public PayShopItme giftCont;
            public string iconName;

            public PayShopConfig(TDPayShopConfig tdData)
            {
                this.id = tdData.id;
                this.giftName = tdData.name;
                this.giftPrice = tdData.price;
                this.giftCont = new PayShopItme(tdData.content);
                this.iconName = tdData.iconName;
            }
        }
        #endregion
    }
}