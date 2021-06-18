using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using UnityEngine.EventSystems;
using TMPro;

namespace GameWish.Game
{
    public class BattleFieldRole : IUListItemView, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private TextMeshProUGUI m_TMPTitle;

        private GameObject m_ObjShow;

        public void SetRole(int index)
        {
            m_TMPTitle.text = index.ToString();
            //根据Data 创建对应的物体
            //TODO
            m_ObjShow = GameObjectPoolMgr.S.Allocate("Pirate_Role_Enemy_OneEye_001@skin Variant");
            m_ObjShow.transform.SetParent(BattleMgr.S.transform);
            m_ObjShow.transform.localPosition = Vector3.zero;
            m_ObjShow.transform.localRotation = Quaternion.identity;
            m_ObjShow.gameObject.SetActive(false);
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.LogError("OnBeginDrag");
            m_ObjShow.gameObject.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.LogError("OnDrag");
            //射线检测地板

            Ray ray = GameCameraMgr.S.CurrentCamera.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线  
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject gameObj = hitInfo.collider.gameObject;

                if (gameObj.tag == TagDefine.TAG_BATTLE_GROUND || gameObject.tag == TagDefine.TAG_BATTLE_FIELD)
                {
                    Debug.LogError("Tag:" + gameObj.tag);
                    Vector3 pos = hitInfo.point;
                    pos.y = BattleMgr.S.transform.position.y;
                    m_ObjShow.transform.position = pos;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.LogError("OnEndDrag");

            //检测是否在BattleField
            Ray ray = GameCameraMgr.S.CurrentCamera.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线  
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject gameObj = hitInfo.collider.gameObject;
                if (gameObj.tag == TagDefine.TAG_BATTLE_FIELD)//当射线碰撞目标为boot类型的物品 ，执行拾取操作  
                {
                    var battleField = gameObject.GetComponentInParent<BattleField>();
                    Debug.LogError(gameObj.name + ":" + battleField);
                }
            }

            m_ObjShow.gameObject.SetActive(false);
        }
    }

}