using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameWish.Game
{
    public class UIClipImage : MonoBehaviour
    {
        //private Image m_Target;
        private Vector4 m_Center;
        private Material m_Material;
        private float m_Diameter; 
        private float m_Current = 0f;

        Vector3[] corners = new Vector3[4];

        void Awake()
        {
            m_Material=new Material(Shader.Find("UI/UIClipImage"));

            if (m_Material == null)
                return;
            GetComponent<Image>().material = m_Material;
        }

        public void ShowClip(RectTransform target, float radius, Vector3 offset)
        {
            //this.m_Target = target;
            if (m_Material == null)
                return;

            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            target.GetWorldCorners(corners);
            m_Diameter = Vector2.Distance(WorldToCanvasPos(canvas, corners[0]), WorldToCanvasPos(canvas, corners[2])) / 2f;
            m_Diameter = Mathf.Max(m_Diameter, radius);

            float x = corners[0].x + ((corners[3].x - corners[0].x) / 2f);
            float y = corners[0].y + ((corners[1].y - corners[0].y) / 2f);

            m_Center = new Vector3(x, y, 0f);
            Vector2 position = Vector2.zero;
            Camera camera = canvas.GetComponent<Camera>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, m_Center, camera, out position);

            m_Center = new Vector4(position.x, position.y, 0f, 0f);
            
            m_Material.SetVector("_Center", m_Center);
            m_Material.SetVector("_Offset", offset);

            (canvas.transform as RectTransform).GetWorldCorners(corners);
            for (int i = 0; i < corners.Length; i++)
            {
                m_Current = Mathf.Max(Vector3.Distance(WorldToCanvasPos(canvas, corners[i]), m_Center), m_Current);
            }

            m_Material.SetFloat("_Silder", m_Current);
        }

        private float m_VelocityY = 0f;

        private void Update()
        {
            float value = Mathf.SmoothDamp(m_Current, m_Diameter, ref m_VelocityY, 0.3f);
            if (!Mathf.Approximately(value, m_Current))
            {
                m_Current = value;
                m_Material.SetFloat("_Silder", m_Current);
            }

        }

        private Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
        {
            Vector2 position = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, world, canvas.GetComponent<Camera>(), out position);
            return position;
        }

    }
}
