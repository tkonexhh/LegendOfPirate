using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Qarth;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameWish.Game
{

    public static class UnityExtensions
    {
        /// <summary>
        /// MathPow方法对float有精度转换问题，要用double
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static double GetPowNumber(float x,int y)
        {
            double d = double.Parse(x.ToString());
            double tmp = 1;
            for (int i = 0; i < y; i++)
            {
                tmp *= d;
            }

            string t = tmp.ToString("f2");

            return double.Parse(t);
        }

        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        public static string GetTimeSecondsStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        //public static void ClickRewardBonus(string key, AdDisplayer.AdDisplayerBuilder.AdShowResultDelegate callback)
        //{
        //    DataAnalysisMgr.S.CustomEvent(Define.BONUS_CLICKED, key);
        //    AdDisplayer.Builder()
        //        .SetOnAdShowResultCallback(callback)
        //        .SetRewardID(key)
        //        .SetPlacementID(AdDefine.AD_PLACEMENT_BOUNS_REWARD)
        //        .Show(AdDefine.AD_TAG_BOUNSPANEL);
        //}

        //public static void DoBuzz()
        //{
        //    if (PlayerPrefs.GetInt(Define.BUZZ_STATE) > 0)
        //    {
        //        Qarth.UtilityMgr.S.Vibrate();
        //    }
        //}

        public static T GetStringEnum<T>(string val)
        {
            return (T)Enum.Parse(typeof(T), val);
        }

        public static Coroutine CallWithDelay(this MonoBehaviour obj, System.Action call, float delay)
        {
            return obj.StartCoroutine(doCallWithDelay(call, delay));
        }

        static IEnumerator doCallWithDelay(System.Action call, float delay)
        {
            if (delay <= 0)
                yield return null;
            else
            {
                float start = Time.realtimeSinceStartup;
                while (Time.realtimeSinceStartup < start + delay)
                {
                    yield return null;
                }
            }

            if (call != null)
                call.Invoke();
        }

        static public void SetLocalPos(this GameObject obj, Vector3 pos)
        {
            obj.transform.localPosition = pos;
        }
        static public void SetPos(this GameObject obj, Vector3 pos)
        {
            obj.transform.position = pos;
        }
        static public void SetAngle(this GameObject obj, Vector3 angle)
        {
            obj.transform.localEulerAngles = angle;
        }
        /// 设置X，只改变X c
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="x"></param>
        static public void SetX(this Transform trans, float x)
        {
            trans.position = new Vector3(x, trans.position.y, trans.position.z);
        }
        /// <summary>
        /// 设置Y，只改变Y c
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="x"></param>
        static public void SetY(this Transform trans, float y)
        {
            trans.position = new Vector3(trans.position.x, y, trans.position.z);
        }
        /// <summary>
        /// 设置Z，只改变Z c
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="x"></param>
        static public void SetZ(this Transform trans, float z)
        {
            trans.position = new Vector3(trans.position.x, trans.position.y, z);
        }
        /// <summary>
        /// 设置LocalX，只改变LocalX c
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="x"></param>
        static public void SetLocalX(this Transform trans, float x)
        {
            trans.localPosition = new Vector3(x, trans.localPosition.y, trans.localPosition.z);
        }
        /// <summary>
        /// 设置LocalY，只改变LocalY c
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="x"></param>
        static public void SetLocalY(this Transform trans, float y)
        {
            trans.localPosition = new Vector3(trans.localPosition.x, y, trans.localPosition.z);
        }
        /// <summary>
        /// 设置LocalZ，只改变LocalZ c
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="x"></param>
        static public void SetLocalZ(this Transform trans, float z)
        {
            trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, z);
        }
        /// <summary>
        /// 在游戏中，朝向的时候只朝向某个坐标，不低头 c
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="target"></param>
        static public void LookAtXZ(this Transform trans, Vector3 target)
        {
            trans.LookAt(new Vector3(target.x, trans.position.y, target.z));
        }
        /// <summary>
        /// 遍历go c
        /// </summary>
        /// <param name="go"></param>
        /// <param name="handle"></param>
        static public void IterateGameObject(this GameObject go, Action<GameObject> handle)
        {
            Queue q = new Queue();
            q.Enqueue(go);
            while (q.Count != 0)
            {
                GameObject tmpGo = (GameObject)q.Dequeue();
                foreach (Transform t in tmpGo.transform)
                {
                    q.Enqueue(t.gameObject);
                }
                if (handle != null)
                {
                    handle(tmpGo);
                }
            }
        }
        /// <summary>
        /// 设置go层级关系 c
        /// </summary>
        /// <param name="go"></param>
        /// <param name="layer"></param>
        static public void SetAllLayer(this GameObject go, int layer)
        {
            IterateGameObject(go, (g) =>
            {
                g.layer = layer;
            });
        }
        /// <summary>
        /// 重置某个物体的三围等 c
        /// </summary>
        /// <param name="go"></param>
        static public void Reset(this GameObject go)
        {
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        static public void ResetLocalAngle(this GameObject go)
        {
            go.transform.localEulerAngles = Vector3.zero;
        }
        static public void ResetLocalPos(this GameObject go)
        {
            go.transform.localPosition = Vector3.zero;
        }

        static public T AddMissingComponent<T>(this GameObject go) where T : Component
        {
#if UNITY_FLASH
		object comp = go.GetComponent<T>();
#else
            T comp = go.GetComponent<T>();
#endif
            if (comp == null)
            {
                comp = go.AddComponent<T>();
            }
#if UNITY_FLASH
		return (T)comp;
#else
            return comp;
#endif
        }

        static public List<T> CheckInSphere<T>(this Vector3 startPos, float radius) where T : Component
        {
            List<T> list = new List<T>();
            //模拟圆形 c
            /**
             GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
             obj.transform.localScale = new Vector3(2f * radius, 2f * radius, 2f * radius);
             obj.transform.position = startPos;
             obj.GetComponent<Collider>().enabled = false;
            **/
            Collider[] colliders = Physics.OverlapSphere(startPos, radius);
            for (int i = 0; i < colliders.Length; i++)
            {
                T objCom = colliders[i].gameObject.GetComponent<T>();
                if (objCom != null)
                {
                    list.Add(objCom);

                }
            }
            return list;
        }

        static public List<T> CheckInTriangle<T>(this Vector3 startPos, Vector3 endPos, float angle) where T : Component
        {
            float distance = Vector3.Distance(startPos, endPos);
            List<T> list = CheckInSphere<T>(startPos, distance * 2f);
            //
            Quaternion q = new Quaternion();
            q.SetFromToRotation(Vector3.forward, (endPos - startPos).normalized);   //置为朝向
            Quaternion r = q * Quaternion.AngleAxis(angle / 2f, Vector3.up);
            Quaternion l = q * Quaternion.AngleAxis(angle / 2f, Vector3.down);
            Vector3 lP = startPos + (l * Vector3.forward) * distance;
            Vector3 rP = startPos + (r * Vector3.forward) * distance;

            for (int i = list.Count - 1; i > -1; i--)
            {
                if (!IsINTriangle(list[i].transform.position, lP, endPos, startPos) && !IsINTriangle(list[i].transform.position, rP, endPos, startPos))
                {
                    //如果在扇形外，则删除 c
                    //Debug.DrawLine(startPos, endPos, Color.red);
                    list.Remove(list[i]);
                }
            }
            return list;
        }


        static bool IsINTriangle(Vector3 point, Vector3 v0, Vector3 v1, Vector3 v2)
        {
            float x = point.x;
            float y = point.z;

            float v0x = v0.x;
            float v0y = v0.z;

            float v1x = v1.x;
            float v1y = v1.z;

            float v2x = v2.x;
            float v2y = v2.z;

            float t = TriangleArea(v0x, v0y, v1x, v1y, v2x, v2y);
            float a = TriangleArea(v0x, v0y, v1x, v1y, x, y) + TriangleArea(v0x, v0y, x, y, v2x, v2y) + TriangleArea(x, y, v1x, v1y, v2x, v2y);

            if (Mathf.Abs(t - a) <= 0.01f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static float TriangleArea(float v0x, float v0y, float v1x, float v1y, float v2x, float v2y)
        {
            return Mathf.Abs((v0x * v1y + v1x * v2y + v2x * v0y
                - v1x * v0y - v2x * v1y - v0x * v2y) / 2f);
        }

        static public List<T> CheckInRect<T>(this Vector3 startPos, Vector3 endPos, float width) where T : Component
        {
            //求敌人
            float x = Vector3.Distance(startPos, endPos);
            float radius = Mathf.Sqrt(x * x + width * width * 0.25f);
            Vector3 center = (startPos + endPos) / 2f;
            List<T> enemies = CheckInSphere<T>(center, radius * 2f);
            List<T> resultEnemies = new List<T>();
            //求范围
            Quaternion qr = Quaternion.LookRotation(endPos - startPos);

            Vector3 l = startPos + (qr * Vector3.left) * width / 2f;
            //Debug.DrawLine(startPos, l, Color.red);
            Vector3 le = l + (qr * Vector3.forward) * x;
            //Debug.DrawLine(l, le, Color.red);
            Vector3 r = startPos + (qr * Vector3.right) * width / 2f;
            //Debug.DrawLine(startPos, r, Color.red);
            Vector3 re = r + (qr * Vector3.forward) * x;
            //Debug.DrawLine(r, re, Color.red);
            Vector3 f = startPos + (qr * Vector3.forward) * x;


            for (int i = enemies.Count - 1; i > -1; i--)
            {
                Vector3 forward = (endPos - startPos).normalized;
                Vector3 toOther = enemies[i].transform.position - startPos;

                if (Vector3.Dot(forward, toOther) >= 0)
                {
                    Vector3 e = enemies[i].transform.position;
                    Vector3 l0e = e - l; //e到l的向量
                    Vector3 r0e = e - r;//e到r的向量
                    Vector3 f0e = e - f;//e到f的向量

                    Vector3 h0l = startPos - l;//h到l的向量
                    Vector3 h0r = startPos - r;//h到r的向量
                    Vector3 h0f = startPos - f;//h到f的向量
                    if (Vector3.Angle(l0e, h0l) <= 90 && Vector3.Angle(r0e, h0r) <= 90 && Vector3.Angle(f0e, h0f) <= 90)
                    {
                        //enemies.RemoveAt(i);
                        resultEnemies.Add(enemies[i]);
                    }
                }
            }
            return resultEnemies;
        }

        static public void SampleAnim(this Animator animator, string stateName, float normalizedTime, int layer = -1)
        {
            animator.StopPlayback();
            animator.Play(stateName, layer, normalizedTime);
            animator.StartPlayback();
        }
        static public void SampleAnim(this Animation anim, string stateName, float normalizedTime)
        {
            anim.Play(stateName);
            anim[stateName].speed = 1;
            anim[stateName].normalizedTime = normalizedTime;
            anim[stateName].speed = 0;
        }

        static public void SetRigidBodiesKinematic(this GameObject obj, bool state)
        {
            var bodies = obj.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < bodies.Length; i++)
            {
                if (bodies[i].gameObject.tag == "StayKinematic")
                    bodies[i].isKinematic = true;
                else
                    bodies[i].isKinematic = state;
            }
        }
        static public void SetColliderEnable(this GameObject obj, bool state)
        {
            var cols = obj.GetComponentsInChildren<Collider>();
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i].enabled = state;
            }
        }

        static public void AddRigidBodiesForce(this GameObject obj, Vector3 force, ForceMode mode = ForceMode.Force)
        {
            var bodies = obj.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < bodies.Length; i++)
                bodies[i].AddForce(force, mode);
        }
        static public void SetRigidBodiesDrag(this GameObject obj, float dragVal = 0)
        {
            var bodies = obj.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < bodies.Length; i++)
                bodies[i].drag = dragVal;
        }
        static public void SetRigidBodiesAngularDrag(this GameObject obj, float dragVal = 0.05f)
        {
            var bodies = obj.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < bodies.Length; i++)
                bodies[i].angularDrag = dragVal;
        }

        static public List<Transform> GetChildTrsList(this Transform trsRoot)
        {
            List<Transform> parts = new List<Transform>();
            for (int i = 0; i < trsRoot.childCount; i++)
                parts.Add(trsRoot.GetChild(i));
            return parts;
        }

        static public bool IsAllRigidBodyQuiet(this GameObject obj)
        {
            bool isQuiet = true;
            var bodies = obj.GetComponentsInChildren<Rigidbody>();
            if (bodies != null)
            {
                for (int i = 0; i < bodies.Length; i++)
                {
                    if (Mathf.Abs(bodies[i].velocity.x) > 0.5f || Mathf.Abs(bodies[i].velocity.y) > 0.5f || Mathf.Abs(bodies[i].velocity.z) > 0.5f)
                    {
                        isQuiet = false;
                        break;
                    }
                }
            }

            return isQuiet;
        }

        //获取网格的size
        static public Vector3 GetMeshSize(this GameObject objRoot)
        {
            var mesh = objRoot.GetComponent<Renderer>();
            return mesh == null ? Vector3.zero : mesh.bounds.size;
        }
        //获取网格的中心
        static public Vector3 GetMeshCenter(this GameObject objRoot)
        {
            Vector3 boundsCenter = Vector3.zero;
            var meshes = objRoot.GetComponentsInChildren<Renderer>();
            for (int j = 0; j < meshes.Length; j++)
            {
                boundsCenter += meshes[j].bounds.center;
            }
            boundsCenter /= meshes.Length * 1.0f;
            return boundsCenter;
        }

        //sharedMat不需要new,效率更高,但是会改变原文件,因此,不能在editor中用
        public static Material GetMaterial(this Renderer render)
        {
#if UNITY_EDITOR
            return render.material;
#else
        return render.sharedMaterial;  
#endif
        }
        public static Mesh GetMesh(this MeshFilter filter)
        {
#if UNITY_EDITOR
            return filter.mesh;
#else
        return filter.sharedMesh;  
#endif
        }

        private static List<RaycastResult> m_Result = new List<RaycastResult>();

        public static bool CheckClickOn(Transform target)
        {
            PointerEventData pd = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            pd.position = Input.mousePosition;

            var graphicRaycaster = target.parent.GetComponentInParent<GraphicRaycaster>();

            if (graphicRaycaster == null)
            {
                return false;
            }

            graphicRaycaster.Raycast(pd, m_Result);

            if (m_Result.Count == 0)
            {
                return false;
            }

            for (int i = m_Result.Count - 1; i >= 0; --i)
            {
                GameObject go = m_Result[i].gameObject;
                if (go != null)
                {

                    if (go.transform.IsChildOf(target))
                    {
                        return true;
                    }

                    return false;
                }
            }
            m_Result.Clear();
            return false;
        }

    }
}