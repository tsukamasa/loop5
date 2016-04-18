using UnityEngine;
using System.Collections;

public class TutorialButton : BasePanelController
{
	public int pushCount;
	public UILabel lblPushCount;
	
	// Use this for initialization
	void Start () {
		this.pushCount = 0;
		this.lblPushCount.text = this.pushCount.ToString();
	}
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnPushButton()
	{
		Debug.LogError("Debug.LogError - OnPushButton");
		++this.pushCount;
		this.lblPushCount.text = this.pushCount.ToString();
	}

	private void OnBackButton()
	{
		Mgrs.pnlMgr.Display("Prefabs/NGUITutorialTitlePanel");
	}
}
