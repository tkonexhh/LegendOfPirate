using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
	public class AdEffectParams
	{
        /// <summary>
        /// �Ƿ񿴹���������ҷ�ʿ����HP
        /// </summary>
        public static bool isInHPAdEnhance = false;

        /// <summary>
        /// �Ƿ񿴹���������ҷ�ʿ���ֶ��ٻ�����
        /// </summary>
        public static bool isInSummonCountAdEnhance = false;

        /// <summary>
        /// �Ƿ񿴹���������ҷ�ʿ���Զ��ٻ��ٶ�
        /// </summary>
        public static bool isInSpawnSpeedAdEnhance = false;

        /// <summary>
        /// �Ƿ񿴹���������ҷ�����˫������
        /// </summary>
        public static bool isInDoubleIncomeAdEnhance = false;

        /// <summary>
        /// �Ƿ񿴹���������ҷ�����˫������
        /// </summary>
        public static bool isInOfflineDoubleIncomeAdEnhance = false;

        public static float GetADHPRatio()
        {
            if (isInHPAdEnhance)
            {
                return 1.2f;
            }

            return 1f;
        }
	}	
}