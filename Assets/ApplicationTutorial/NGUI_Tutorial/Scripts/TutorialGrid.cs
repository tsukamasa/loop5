using UnityEngine;
using System.Collections;

public class TutorialGrid : BasePanelController
{
	public TutorialCreateGameObject createGameObject;

	public UIGrid grid;
	private bool isRepositionEnd;
	
	// Use this for initialization
	void Start () {
		this.createGameObject.parent = this.grid.transform;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(!this.isRepositionEnd && this.createGameObject.IsCreateAllEnd ())
		{
			this.isRepositionEnd = true;
			this.grid.transform.localPosition = new Vector3(this.grid.transform.localPosition.x, 160F);
			this.grid.Reposition();
		}
	}

	private void OnBackButton()
	{
		Mgrs.pnlMgr.Display("Prefabs/NGUITutorialTitlePanel");
	}
}
