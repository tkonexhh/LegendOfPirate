using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleDragComponent : AbstractBattleComponent
    {
        private bool m_IsDraging = false;
        private DragTarget m_DragTarget;

        public override void Init()
        {
            base.Init();
            m_DragTarget = new DragTarget();
        }

        public override void OnBattleUpdate()
        {
            base.OnBattleUpdate();

            if (BattleMgr.Started) return;

            bool tempDrag = m_IsDraging;
            if (Input.GetMouseButton(0))
            {
                m_IsDraging = true;
                if (!tempDrag && m_IsDraging)
                {
                    OnBeginDrag();
                }

            }
            else
            {
                if (tempDrag)
                {
                    m_IsDraging = false;
                    OnEndDrag();
                }
            }

            if (m_IsDraging)
            {
                OnDrag();
            }
        }

        public void OnBeginDrag()
        {
            //打个射线看看是否打到了BattleField
            Ray ray = GameCameraMgr.S.CurrentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject gameObj = hitInfo.collider.gameObject;
                if (gameObj.CompareTag(TagDefine.TAG_BATTLE_FIELD))
                {
                    var battleField = gameObj.GetComponentInParent<BattleField>();
                    if (battleField.Controller != null)
                    {
                        m_DragTarget.battleField = battleField;
                    }
                }
            }
        }

        public void OnDrag()
        {
            if (!m_DragTarget.CanDrag)
            {
                return;
            }
            //射线检测地板
            Ray ray = GameCameraMgr.S.CurrentCamera.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线  
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject gameObj = hitInfo.collider.gameObject;
                if (gameObj.CompareTag(TagDefine.TAG_BATTLE_GROUND) || gameObj.CompareTag(TagDefine.TAG_BATTLE_FIELD))//(gameObj.tag == TagDefine.TAG_BATTLE_GROUND)
                {
                    Vector3 pos = hitInfo.point;
                    pos.y = BattleMgr.S.transform.position.y;
                    m_DragTarget.battleField.Controller.transform.position = pos;
                }
            }
        }

        public void OnEndDrag()
        {
            if (!m_DragTarget.battleField)
                return;

            Ray ray = GameCameraMgr.S.CurrentCamera.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线  
            RaycastHit hitInfo;
            bool isFind = false;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject gameObj = hitInfo.collider.gameObject;
                if (gameObj.CompareTag(TagDefine.TAG_BATTLE_FIELD))//(gameObj.tag == TagDefine.TAG_BATTLE_GROUND)
                {
                    var battleField = gameObj.GetComponentInParent<BattleField>();
                    if (battleField != null)
                    {
                        isFind = true;
                        var switchController = battleField.Controller;
                        battleField.SetBattleRoleController(m_DragTarget.battleField.Controller, false);
                        m_DragTarget.battleField.SetBattleRoleController(switchController, false);
                    }
                }
            }

            if (!isFind)
            {
                m_DragTarget.BackToOrigin();
            }


            m_DragTarget.Clear();
        }

        public struct DragTarget
        {
            public BattleField battleField;

            public bool CanDrag => battleField != null && battleField.Controller != null;

            public void BackToOrigin()
            {
                if (!CanDrag) return;

                battleField.Controller.transform.position = battleField.transform.position;
            }

            public void Clear()
            {
                battleField = null;
            }

        }
    }
}