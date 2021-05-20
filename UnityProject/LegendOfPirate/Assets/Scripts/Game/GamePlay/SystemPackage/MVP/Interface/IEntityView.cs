using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth.Extension
{
	public interface IEntityView
    {
        IEntityPresenter<EntityView> Presenter { get; }

        void SetPresenter(IEntityPresenter<EntityView> presenter);
	}
	
}