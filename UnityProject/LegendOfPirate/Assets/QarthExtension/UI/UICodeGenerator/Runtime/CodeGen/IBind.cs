
namespace Qarth.Extension
{
    using UnityEngine;

    public enum BindType
    {
        DefaultUnityElement,
        Element,
        //Component
    }
    
    public interface IBind
    {
        string ComponentName { get; }
        
        string Comment { get; }

        Transform Transform { get; }

        BindType GetBindType();
    }
}