using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class ItemBase 
	{
        /// <summary>
        /// id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public ItemType Type { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public ItemQuality Quality { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// ����۸�
        /// </summary>
        public int BuyPrice { get; set; }
        /// <summary>
        /// ���ۼ۸�
        /// </summary>
        public int SellPrice { get; set; }
        /// <summary>
        /// ͼƬ����
        /// </summary>
        public string Sprite { get; set; }
    }
	
}