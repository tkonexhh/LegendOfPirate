using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth
{
    public static class GameObjectExtensions
    {

        public static RectTransform rectTransform(this Component cp)
        {
            return cp.transform as RectTransform;
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

        static public void ResetLocalScale(this GameObject go)
        {
            go.transform.localScale = Vector3.one;
        }

        static public void SetAngle(this GameObject go, Vector3 angle)
        {
            go.transform.localEulerAngles = angle;
        }


        static public void SetLocalPos(this GameObject go, Vector3 pos)
        {
            go.transform.localPosition = pos;
        }
        static public void SetPos(this GameObject go, Vector3 pos)
        {
            go.transform.position = pos;
        }

        // static public void SetParent(this GameObject go, Transform parent)
        // {
        //     go.transform.SetParent(parent);
        // }

        // static public void SetParent(this GameObject go, GameObject parent)
        // {
        //     go.transform.SetParent(parent.transform);
        // }





        public static GameObject FindGameObject(GameObject root, string path, bool build, bool dontDestroy)
        {
            if (path == null || path.Length == 0)
            {
                return null;
            }

            string[] subPath = path.Split('/');
            if (subPath == null || subPath.Length == 0)
            {
                return null;
            }

            return FindGameObject(null, subPath, 0, build, dontDestroy);
        }

        public static GameObject FindGameObject(GameObject root, string[] subPath, int index, bool build, bool dontDestroy)
        {
            GameObject client = null;

            if (root == null)
            {
                client = GameObject.Find(subPath[index]);
            }
            else
            {
                var child = root.transform.Find(subPath[index]);
                if (child != null)
                {
                    client = child.gameObject;
                }
            }

            if (client == null)
            {
                if (build)
                {
                    client = new GameObject(subPath[index]);
                    if (root != null)
                    {
                        client.transform.SetParent(root.transform);
                    }
                    if (dontDestroy && index == 0)
                    {
                        GameObject.DontDestroyOnLoad(client);
                    }
                }
            }

            if (client == null)
            {
                return null;
            }

            if (++index == subPath.Length)
            {
                return client;
            }

            return FindGameObject(client, subPath, index, build, dontDestroy);
        }
    }

}