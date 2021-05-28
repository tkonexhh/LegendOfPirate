using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public abstract class ItemBase 
    {
        /// <summary>
        /// id
        /// </summary>
        public ItemID ID { get; set; }
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
        public long Capacity { get; set; }
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
        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool CapacityFull { get; set; }
        public ItemBase() 
        { }
        public ItemBase(ItemType itemType, ItemID id, long capacity) {
            this.ID = id;
            this.Type = itemType;
            this.Capacity = capacity;
            CheckCapacityIsFull();
            OnInitOtherInfo();
        }

        public abstract void OnInitOtherInfo();
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="number"></param>
        public void OnAddNumber(long number)
        {
            Capacity += number;
            CheckCapacityIsFull();
        }
        /// <summary>
        /// ��������Ƿ�����
        /// </summary>
        private void CheckCapacityIsFull()
        {
            if (Capacity >= InventroyMgr.MAXITEMNUMBER)
            {
                CapacityFull = true;
            }
            else
            {
                CapacityFull = false;
            }
        }
    }

}