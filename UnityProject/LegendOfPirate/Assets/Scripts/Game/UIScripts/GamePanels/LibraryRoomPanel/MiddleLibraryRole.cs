using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class MiddleLibraryRole : UListItemView
    {
        #region Data
        private MiddleLibraryRoleModule m_MiddleLibraryRoleModule;
        #endregion
        public void OnInit(MiddleLibraryRoleModule middleLibraryRoleModule)
        {
            ResetState();

            if (middleLibraryRoleModule == null)
            {
                Debug.LogWarning("middleLibraryRoleModule is null");
                return;
            }

            m_MiddleLibraryRoleModule = middleLibraryRoleModule;
        }
        private void ResetState()
        {

        }
    }
	
}