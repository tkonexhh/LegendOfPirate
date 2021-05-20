using UnityEngine;
using UnityEngine.UI;
using Qarth;
using System.Reflection;
using System;

namespace GameWish.Game
{
	public class WorldGuideClickPanel : AbstractPanel
	{

        [SerializeField]
        private Button m_GuideBtn;

        private string m_MethodName;

        private MethodInfo m_GuideBtnMethod;

        protected override void OnUIInit()
        {
            base.OnUIInit();
            m_GuideBtn.onClick.AddListener(OnGuideBtnClick);
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            if(args.Length > 0)
            {
                m_MethodName = args[0].ToString();
            }

        }


        private void OnGuideBtnClick()
        {
            Type type = this.GetType();

            m_GuideBtnMethod = type.GetMethod(m_MethodName, new Type[] { });

            if (m_GuideBtnMethod != null)
            {
                object[] parameters = null;
                m_GuideBtnMethod.Invoke(this, parameters);
            }

        }





    }
	
}