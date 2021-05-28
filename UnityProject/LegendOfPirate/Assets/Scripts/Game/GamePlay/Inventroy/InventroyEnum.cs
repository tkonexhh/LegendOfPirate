using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// ����Ʒ
        /// </summary>
        Consumable,
        /// <summary>
        /// װ��
        /// </summary>
        Equipment,
        /// <summary>
        /// ����
        /// </summary>
        Weapon,
        /// <summary>
        /// ����
        /// </summary>
        Material
    }
    /// <summary>
    /// Ʒ��
    /// </summary>
    public enum ItemQuality
    {
        /// <summary>
        /// ��ͨ��
        /// </summary>
        Common,
        /// <summary>
        /// ����ͨ��
        /// </summary>
        Uncommon,
        /// <summary>
        /// ϡ�е�
        /// </summary>
        Rare,
        /// <summary>
        /// ʷʫ��
        /// </summary>
        Epic,
        /// <summary>
        /// ��˵��
        /// </summary>
        Legendary,
        /// <summary>
        /// ������Ʒ
        /// </summary>
        Artifact
    }

    public enum WeaponType
    {
        /// <summary>
        /// ��
        /// </summary>
        None,
        /// <summary>
        /// ������
        /// </summary>
        OffHand,
        /// <summary>
        /// ������
        /// </summary>
        MainHand
    }
    public enum EquipmentType
    {
        None,
        /// <summary>
        /// ͷ��
        /// </summary>
        Head,
        /// <summary>
        /// ����
        /// </summary>
        Neck,
        /// <summary>
        /// �ز�
        /// </summary>
        Chest,
        /// <summary>
        /// ��ָ
        /// </summary>
        Ring,
        /// <summary>
        /// �Ȳ�
        /// </summary>
        Leg,
        /// <summary>
        /// ����
        /// </summary>
        Bracer,
        /// <summary>
        /// Ь��
        /// </summary>
        Boots,
        /// <summary>
        /// ����
        /// </summary>
        Shoulder,
        /// <summary>
        /// ����
        /// </summary>
        Belt,
        /// <summary>
        /// ����
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