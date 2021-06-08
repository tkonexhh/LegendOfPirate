using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    static class ListExtensions
    {
        static public T Pop<T>(this List<T> list)
        {
            int index = list.Count - 1;

            T r = list[index];
            list.RemoveAt(index);
            return r;
        }
    }

}