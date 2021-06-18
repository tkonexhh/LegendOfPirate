using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
    public class ReadPostionModel
    {
        public LibrarySlotModel librarySlotModel;
        public ReadPosition readPos;
        public ReadPostionModel(LibrarySlotModel librarySlotModel)
        {
            this.librarySlotModel = librarySlotModel;
        }
        public void SetTraPosData(ReadPosition readPos)
        {
            this.readPos = readPos;
        }
    }
}