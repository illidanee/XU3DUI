using System;
using UnityEngine;

[Serializable]
public class UIPanelInfo : ISerializationCallbackReceiver
{
    [NonSerialized]
    public UIPanelType _UIPanelType;
    public string _UIPanelName;
    public string _UIPanelPath;

    public void OnBeforeSerialize()
    {
        //throw new NotImplementedException();
    }

    public void OnAfterDeserialize()
    {
        _UIPanelType = (UIPanelType)Enum.Parse(typeof(UIPanelType), _UIPanelName);
    }
}
