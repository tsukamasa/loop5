using UnityEngine;
using System.Collections;

public class TutorialInput : BasePanelController
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnSubmit ()
	{
		Debug.LogError("OnSubmit");
	}

	private void OnBackButton()
	{
		Mgrs.pnlMgr.Display("Prefabs/NGUITutorialTitlePanel");
	}
}
