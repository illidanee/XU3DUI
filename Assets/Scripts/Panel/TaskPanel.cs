using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanel : BasePanel {

    protected CanvasGroup _CG;

    public void Awake()
    {
        _CG = GetComponent<CanvasGroup>();
    }

    public override void Init()
    {
        base.Init();

        _CG.alpha = 0;
        _CG.blocksRaycasts = false;
    }

    public override void OnEnter()
    {
        _CG.alpha = 1;
        _CG.blocksRaycasts = true;
    }

    public override void OnPause()
    {
        _CG.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        _CG.blocksRaycasts = true;
    }

    public override void OnLeave()
    {
        _CG.alpha = 0;
        _CG.blocksRaycasts = false;
    }

    public void OnClose()
    {
        UIManager.Ins.PopPanel();
    }
}
