using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 物品类型
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 消耗品
        /// </summary>
        Consumable,
        /// <summary>
        /// 装备
        /// </summary>
        Equipment,
        /// <summary>
        /// 武器
        /// </summary>
        Weapon,
        /// <summary>
        /// 材料
        /// </summary>
        Material
    }
    /// <summary>
    /// 品质
    /// </summary>
    public enum ItemQuality
    {
        /// <summary>
        /// 普通的
        /// </summary>
        Common,
        /// <summary>
        /// 不普通的
        /// </summary>
        Uncommon,
        /// <summary>
        /// 稀有的
        /// </summary>
        Rare,
        /// <summary>
        /// 史诗的
        /// </summary>
        Epic,
        /// <summary>
        /// 传说的
        /// </summary>
        Legendary,
        /// <summary>
        /// 人造物品
        /// </summary>
        Artifact
    }

    public enum WeaponType
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 副武器
        /// </summary>
        OffHand,
        /// <summary>
        /// 主武器
        /// </summary>
        MainHand
    }
    public enum EquipmentType
    {
        None,
        /// <summary>
        /// 头部
        /// </summary>
        Head,
        /// <summary>
        /// 脖子
        /// </summary>
        Neck,
        /// <summary>
        /// 胸部
        /// </summary>
        Chest,
        /// <summary>
        /// 戒指
        /// </summary>
        Ring,
        /// <summary>
        /// 腿部
        /// </summary>
        Leg,
        /// <summary>
        /// 护腕
        /// </summary>
        Bracer,
        /// <summary>
        /// 鞋子
        /// </summary>
        Boots,
        /// <summary>
        /// 护肩
        /// </summary>
        Shoulder,
        /// <summary>
        /// 腰带
        /// </summary>
        Belt,
        /// <summary>
        /// 副手
        /// </summary>
        OffHand
    }


    public enum ItemID
    {
        _hello,
        _wiorld,
        _3,
        _4,
        _5,
        _6,
        _7,
    }


}