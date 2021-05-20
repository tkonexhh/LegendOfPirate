using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth.Extension
{
    public interface IEntityPresenter<TView> where TView : class, IEntityView
    {
        TView View { get;}

        void Init(TView view);
    }
}