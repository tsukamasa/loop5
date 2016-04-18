using UnityEngine;
using System.Collections;

public class TutorialDepth : BasePanelController 
{
	public GameObject objectNG;
	public GameObject objectOK;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnNG()
	{
		objectOK.SetActive(false);
		objectNG.SetActive(true);
	}
	
	void OnOK()
	{
		objectOK.SetActive(true);
		objectNG.SetActive(false);
	}

	private void OnBackButton()
	{
		Mgrs.pnlMgr.Display("Prefabs/NGUITutorialTitlePanel");
	}
}
