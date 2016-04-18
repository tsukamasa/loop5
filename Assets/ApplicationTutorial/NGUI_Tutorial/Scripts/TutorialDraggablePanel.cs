using UnityEngine;
using System.Collections;

public class TutorialDraggablePanel : BasePanelController
{
	public GameObject prefab;
	
	public UIGrid grid;
	
	public int createNum = 0;
	
	// Use this for initialization
	void Start () {
		CreateDragObject();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	private void CreateDragObject ()
	{
		for (int index = 0; index < this.createNum; index++) {
			GameObject dragObject = Instantiate (prefab) as GameObject;
			
			Vector3 originalScale = dragObject.transform.localScale;
			dragObject.transform.SetParent (this.grid.transform);
			dragObject.transform.localScale = originalScale;
			
			dragObject.name += index.ToString().PadLeft(4, '0');
		}
		
		this.grid.Reposition();
	}
	
	private void OnBackButton()
	{
		Mgrs.pnlMgr.Display("Prefabs/NGUITutorialTitlePanel");
	}
}
