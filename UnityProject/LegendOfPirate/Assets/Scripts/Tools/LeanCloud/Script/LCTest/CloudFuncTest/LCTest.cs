using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GameWish.Game
{
    public class LCTest : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_ShowText;

        public async void OnClick1()
        {
            m_ShowText.text = await GameCloudFunc.S.Ping();
        }
        public async void OnClick2()
        {

            m_ShowText.text = await GameCloudFunc.S.Hello();
        }
        public async void OnClick3()
        {

            m_ShowText.text = await GameCloudFunc.S.GetDraw();
        }
        public async void OnClick4()
        {

            m_ShowText.text = await GameCloudFunc.S.OnGetObject();
        }
        public async void OnClick5()
        {

            m_ShowText.text = await GameCloudFunc.S.OnGetObjects();
        }
        public async void OnClick6()
        {
            m_ShowText.text = await GameCloudFunc.S.OnGetObjectMap();
        }
    }

}