using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameWish.Game
{
	public class SmuggleRoleItem : MonoBehaviour
	{
		public int roleAddttion;

	    [SerializeField] private Image m_RoleImage;
		[SerializeField] private TextMeshProUGUI m_RoleBounsTMP;
		

		public void SetRoleItemData(RoleModel roleModel) 
		{
			//TODOSetRoleImage
			var addition = ((roleModel.starLevel.Value - 1) * 10) > 5 ? ((roleModel.starLevel.Value - 1) * 10) : 5;
			m_RoleBounsTMP.text = string.Format("+{0}%", addition);
			roleAddttion = addition;
		}
	}
	
}