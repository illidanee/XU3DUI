using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIManager.Ins.Init();
        UIManager.Ins.Test();
        UIManager.Ins.PushPanel(UIPanelType.MainMenuPanel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
