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

        private BattleRoleController m_Role;

        public void SetRole(int index, RoleConfigSO configSO)
        {
            // m_ConfigSO = configSO;
            m_TMPTitle.text = index.ToString();
            //根据Data 创建对应的物体

            m_Role = BattleRoleControllerFactory.CreateBattleRole(configSO);
            //TODO
            // m_ObjShow = GameObjectPoolMgr.S.Allocate("Pirate_Role_Enemy_OneEye_001@skin Variant");
            m_Role.transform.SetParent(BattleMgr.S.transform);
            m_Role.transform.localPosition = Vector3.zero;
            m_Role.transform.localRotation = Quaternion.identity;
            m_Role.gameObject.SetActive(false);
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            m_Role.gameObject.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
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
                    m_Role.transform.position = pos;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            m_Role.gameObject.SetActive(false);
            //检测是否在BattleField
            Ray ray = GameCameraMgr.S.CurrentCamera.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线  
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject gameObj = hitInfo.collider.gameObject;
                if (gameObj.tag == TagDefine.TAG_BATTLE_FIELD)//当射线碰撞目标为boot类型的物品 ，执行拾取操作  
                {
                    var battleField = gameObj.GetComponentInParent<BattleField>();
                    if (battleField != null)
                    {
                        m_Role.gameObject.SetActive(true);
                        battleField.SetBattleRoleController(m_Role);

                    }
                }
            }


        }
    }

}