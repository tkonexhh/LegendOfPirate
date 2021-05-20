using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth.Extension
{
    /// <summary>
    /// Model used to cache all game data
    /// Read data from database when start
    /// Should change db data form model
    /// </summary>
    public interface IModel
	{
        void LoadDataFromDB();
	}
	
}