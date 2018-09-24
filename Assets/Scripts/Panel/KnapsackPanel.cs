using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnapsackPanel : BasePanel {

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

        transform.localScale = Vector3.zero;
        transform.DOScale(1, 1);
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
        //_CG.alpha = 0;
        _CG.blocksRaycasts = false;

        transform.DOScale(0, 1).OnComplete(() => _CG.alpha = 0);
    }

    public void OnClose()
    {
        UIManager.Ins.PopPanel();
    }

    public void OnItemClick()
    {
        UIManager.Ins.PushPanel(UIPanelType.ItemMessagePanel);
    }
}
