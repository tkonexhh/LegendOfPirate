using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;

namespace GameWish.Game
{
    public class UIBlinkImage : MonoBehaviour
    {
        private Material m_Mat;
        private bool m_StartBlink = false;
        private bool m_IsBlinking = false;
        private float m_CurOffset = 0f;

        [SerializeField] private float m_Speed = 1f;
        [SerializeField] private float m_Tilling_x = 1f;
        [SerializeField] private float m_StartOffset_x = 1f;
        [SerializeField] private float m_StartDelayTime = 1f;
        [SerializeField] private float m_BlinkInterval = 3f;
        [SerializeField] private Texture m_BlinkTexture;

        private float m_CurrentDelayTime = 0f;
        private float m_CurrentIntervalTime = 0f;

        private void Start()
        {
            Material mat = new Material(Shader.Find("UIBlinkImage"));


            GetComponent<Image>().material = mat;
            mat.SetTexture("_BlinkTexture", m_BlinkTexture);
            mat.SetFloat("_CurrentOffset", -1);
            mat.SetTextureScale("_BlinkTexture", new Vector2(m_Tilling_x, 1));

            m_Mat = mat;
            //ResLoader resLoader = ResLoader.Allocate("MatLoader");
            //UnityEngine.Object obj = resLoader.LoadSync("UIBlinkImageMat");
            //m_Mat = obj as Material;
            //GetComponent<Image>().material = m_Mat;
        }

        private void OnEnable()
        {
            m_StartBlink = false;
            m_CurrentDelayTime = 0f;
            m_CurrentIntervalTime = 0f;
        }

        public void StartBlink()
        {
            //Log.i("Start blink");
            m_CurOffset = 1f;
            m_IsBlinking = true;
            m_CurrentIntervalTime = 0f;
        }

        public void ResetOffset()
        {
            if (m_Mat)
                m_Mat.SetFloat("_CurrentOffset", 1);
        }

        private void Update()
        {
            if (m_StartBlink == false)
            {
                m_CurrentDelayTime += Time.deltaTime;
                if (m_CurrentDelayTime > m_StartDelayTime)
                {
                    m_StartBlink = true;
                    StartBlink();
                }

                return;
            }

            if (m_IsBlinking)
            {
                m_CurOffset += -Time.deltaTime * m_Speed;
                m_Mat.SetFloat("_CurrentOffset", m_CurOffset + m_StartOffset_x);

                if (m_CurOffset <= 0)
                {
                    m_IsBlinking = false;
                }
            }
            else
            {
                m_CurrentIntervalTime += Time.deltaTime;
                if (m_CurrentIntervalTime > m_BlinkInterval)
                {
                    StartBlink();
                }
            }
        }

        public void SetOffset(float offset)
        {
            m_Mat.SetFloat("_CurrentOffset", offset);
        }

    }
}