using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleFieldComponent : AbstractBattleComponent
    {
        private List<BattleField> m_OurBattleFieldLst = new List<BattleField>();
        private List<Vector3> m_EnemyPosLst = new List<Vector3>();

        public override void Init()
        {
            Vector3 center = BattleMgr.S.gameObject.transform.position;
            float totalWidth = BattleDefine.BATTLE_CELL_WIDTH * BattleDefine.BATTLE_WIDTH;

            Vector3 upStartPos = center + new Vector3(-totalWidth / 2, 0, BattleDefine.BATTLE_HALF_CENTERHEIGHT);
            Vector3 downStartPos = center + new Vector3(-totalWidth / 2, 0, -BattleDefine.BATTLE_HALF_CENTERHEIGHT - BattleDefine.BATTLE_CELL_HEIGHT * BattleDefine.BATTLE_HEIGHT);

            for (int y = 0; y < BattleDefine.BATTLE_HEIGHT; y++)
            {
                for (int x = 0; x < BattleDefine.BATTLE_WIDTH; x++)
                {
                    Vector3 posUp = upStartPos + new Vector3(BattleDefine.BATTLE_CELL_WIDTH * (0.5f + x), 0.1f, BattleDefine.BATTLE_CELL_HEIGHT * (0.5f + y));
                    m_EnemyPosLst.Add(posUp);

                    Vector3 posDown = downStartPos + new Vector3(BattleDefine.BATTLE_CELL_WIDTH * (0.5f + x), 0.1f, BattleDefine.BATTLE_CELL_HEIGHT * (0.5f + y));
                    m_OurBattleFieldLst.Add(CreateBattleField(posDown));
                }
            }
        }

        public override void OnBattleInit(BattleFieldConfigSO enemyConfigSO)
        {
            for (int i = 0; i < m_OurBattleFieldLst.Count; i++)
            {
                m_OurBattleFieldLst[i].gameObject.SetActive(true);
            }
        }

        public override void OnBattleStart()
        {
            for (int i = 0; i < m_OurBattleFieldLst.Count; i++)
            {
                m_OurBattleFieldLst[i].gameObject.SetActive(false);
            }
        }

        public BattleField CreateBattleField(Vector3 pos)
        {
            var go = GameObjectPoolMgr.S.Allocate(BattleDefine.POOLNAME_BATTLEFIELD);
            go.transform.position = pos;
            go.transform.SetParent(BattleMgr.S.transform);
            var battleField = go.GetComponent<BattleField>();
            return battleField;
        }

        public BattleField GetOurBattleField(int index)
        {
            if (index < 0 || index > m_OurBattleFieldLst.Count)
                throw new System.IndexOutOfRangeException("index out of range");

            return m_OurBattleFieldLst[index];
        }

        public Vector3 GetEnemyPos(int index)
        {
            if (index < 0 || index > m_EnemyPosLst.Count)
                throw new System.IndexOutOfRangeException("index out of range");

            return m_EnemyPosLst[index];
        }

    }

}