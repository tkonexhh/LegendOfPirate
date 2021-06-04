using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using DG.Tweening;

namespace GameWish.Game
{

    public enum InjuryType
    {
        /// <summary>
        /// 普通伤害
        /// </summary>
        CommonInjury,
        /// <summary>
        /// 暴击伤害
        /// </summary>
        CriticalInjury,
        /// <summary>
        /// 异常伤害
        /// </summary>
        AbnormalInjury,
    }

    public class WorldUIPanel : AbstractPanel
    {
        public static WorldUIPanel S;
        public WorldUI_WorkTalk m_WalkTalk;

        [Header("不散乱时用")]
        [Tooltip("上移距离")]
        public float m_RollingDistance = 100f;
        [Tooltip("动画持续时间")]
        public float m_SmoothAnimTime = 3;
        [Header("散乱时用")]
        [Tooltip("散乱幅度(左右)")]
        public float m_DisruptionRange= 100f;
        [Tooltip("散乱持续时间")]
        public float m_DisruptionAnimTime = 0.5f;
        [Tooltip("抛物线最高高度")]
        public float m_DisruptionHeight = 100;
        protected override void OnUIInit()
        {
            S = this;

            GameObjectPoolMgr.S.AddPool("WalkTalk", m_WalkTalk.gameObject, -1, 5);

        }

        // protected override void OnOpen()
        // {
        //     // RegisterEvent(EventID.OnEnterBattle, HandleEvent);
        //     // RegisterEvent(EventID.OnExitBattle, HandleEvent);
        // }

        #region Public
        public void ShowCommonInjuryText(Transform character, string talk,bool isScattered = false)
        {
            var obj = CreateInjuryObj();

            WorldUI_WorkTalk workTalk = ShowWorkText(character, talk, obj);

            workTalk.SetStyle(InjuryType.CommonInjury);

            RetardedText(obj, isScattered);
        }
        public void ShowCriticalInjuryText(Transform character, string talk, bool isScattered = false)
        {
            var obj = CreateInjuryObj();

            WorldUI_WorkTalk workTalk = ShowWorkText(character, talk, obj);

            workTalk.SetStyle(InjuryType.CriticalInjury);

            RetardedText(obj, isScattered);
        }
        public void ShowAbnormalInjuryText(Transform character, string talk, bool isScattered = false)
        {
            var obj = CreateInjuryObj();

            WorldUI_WorkTalk workTalk = ShowWorkText(character, talk, obj);

            workTalk.SetStyle(InjuryType.CriticalInjury);

            RetardedText(obj, isScattered);
        }



        #endregion

        #region Private
        //.SetEase(Ease.OutElastic).SetLoops(-1, LoopType.Yoyo);
        private void RetardedText(GameObject obj, bool isScattered)
        {
            if (isScattered)
            {
                float ran = Random.Range(-m_DisruptionRange, m_DisruptionRange);
                WorldUI_WorkTalk worldUI_WorkTalk = obj.GetComponent<WorldUI_WorkTalk>();
                worldUI_WorkTalk.SetExerciseTime(m_DisruptionAnimTime, m_DisruptionHeight, ran, () => {
                    GameObjectPoolMgr.S.Recycle(obj);
                    obj = null;
                });
            }
            else
            {
                obj.transform.DOLocalMove(obj.transform.localPosition + new Vector3(0, m_RollingDistance, 0), m_SmoothAnimTime).SetEase(Ease.Linear).OnComplete(() =>
                  {
                      GameObjectPoolMgr.S.Recycle(obj);
                      obj = null;
                  });
            }

            //obj.transform.DOScale(obj.transform.localScale+ new Vector3(obj.transform.localScale.x, obj.transform.localScale.y+0.5f, obj.transform.localScale.z)).SetEase()

        }
        public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
        {
            float Func(float x) => 4 * (-height * x * x + height * x);

            var mid = Vector2.Lerp(start, end, t);

            return new Vector2(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t));
        }


        private GameObject CreateInjuryObj()
        {
            var m_WorkTalkGo = GameObjectPoolMgr.S.Allocate("WalkTalk");
            m_WorkTalkGo.transform.SetParent(transform);
            m_WorkTalkGo.transform.localPosition = Vector3.zero;
            m_WorkTalkGo.transform.localScale = Vector3.one;
            m_WorkTalkGo.SetActive(true);
            return m_WorkTalkGo;
        }

        private WorldUI_WorkTalk ShowWorkText(Transform character, string talk, GameObject obj)
        {
            // if (m_IsBattle) return;

            //var m_WorkTalkGo = GameObjectPoolMgr.S.Allocate("WalkTalk");
            //m_WorkTalkGo.transform.SetParent(transform);
            //m_WorkTalkGo.transform.localPosition = Vector3.zero;
            //m_WorkTalkGo.transform.localScale = Vector3.one;
            //m_WorkTalkGo.SetActive(true);

            WorldUI_WorkTalk workTalk = obj.GetComponent<WorldUI_WorkTalk>();
            workTalk.followTransform = character;
            workTalk.UpdatePosition();
            workTalk.SetText(talk);

            return workTalk;

            //Timer.S.Post2Scale(i =>
            //{
            //    GameObjectPoolMgr.S.Recycle(workTalkGo);
            //}, 3.0f);
        }
        #endregion



    }
}
