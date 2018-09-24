using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    //单利模式
    private static UIManager _Ins;

    public static UIManager Ins
    {
        get
        {
            if (_Ins == null)
            {
                _Ins = new UIManager();
            }

            return _Ins;
        }
    }

    private UIManager()
    {
        
    }

    //初始化
    private Transform _CanvasTransform;
    private Dictionary<UIPanelType, string> _UIPanelPaths;
    private Dictionary<UIPanelType, BasePanel> _UIPanelObjects;
    private Stack<BasePanel> _UIPanelShowStack;

    public void Init()
    {
        _CanvasTransform = GameObject.Find("Canvas").transform;

        _UIPanelPaths = new Dictionary<UIPanelType, string>();
        _UIPanelObjects = new Dictionary<UIPanelType, BasePanel>();
        _UIPanelShowStack = new Stack<BasePanel>();

        ParseUIPanelTypeJonsonFile();
        LoadUIPanels();
    }

    //Json序列化类
    class UIPanelObject
    {
        public List<UIPanelInfo> _UIPanelInfos = new List<UIPanelInfo>();
    }

    //解析Json文件
    private void ParseUIPanelTypeJonsonFile()
    {
        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");

        UIPanelObject oUIPanelObject = JsonUtility.FromJson<UIPanelObject>(ta.text);

        foreach (UIPanelInfo info in oUIPanelObject._UIPanelInfos)
        {
            _UIPanelPaths.Add(info._UIPanelType, info._UIPanelPath);
        }
    }

    //加载面板
    private void LoadUIPanels()
    {
        foreach (KeyValuePair<UIPanelType, string> panelPath in _UIPanelPaths)
        {
            GameObject go = GameObject.Instantiate(Resources.Load(panelPath.Value)) as GameObject;
            go.transform.SetParent(_CanvasTransform, false);
            BasePanel panel = go.GetComponent<BasePanel>();
            panel.Init();
            _UIPanelObjects.Add(panelPath.Key, panel);
        }
    }

    //获取面板
    private BasePanel GetBasePanel(UIPanelType ePanelType)
    {
        BasePanel value;
        _UIPanelObjects.TryGetValue(ePanelType, out value);
        return value;
    }

    public void PushPanel(UIPanelType ePanelType)
    {
        BasePanel panel = GetBasePanel(ePanelType);
        if (panel == null)
            return;

        if (_UIPanelShowStack.Count > 0)
        {
            BasePanel top = _UIPanelShowStack.Peek();
            top.OnPause();
        }

        panel.OnEnter();
        _UIPanelShowStack.Push(panel);
    }

    public void PopPanel()
    {
        if (_UIPanelShowStack.Count <= 0)
        {
            return;
        }

        BasePanel top = _UIPanelShowStack.Pop();
        top.OnLeave();

        if (_UIPanelShowStack.Count > 0)
        {
            top = _UIPanelShowStack.Peek();
            top.OnResume();
        }
    }

    //测试
    public void Test()
    {
        foreach (KeyValuePair<UIPanelType, string> panel in _UIPanelPaths)
        {
            Debug.Log(panel.Value);
        }
    }
}
