using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
    public class LibraryPreparatorRoleModel
    {
        public int roleID;
        public LibraryPreparatorRole libPrepRole;
        public LibrarySlotModel librarySlotModel;
        public LibraryPreparatorRoleModel(int roleID)
        {
            this.roleID = roleID;
        }

        public void SetBottomLibRole(LibraryPreparatorRole libPrepRole)
        {
            this.libPrepRole = libPrepRole;
        }
    }
}