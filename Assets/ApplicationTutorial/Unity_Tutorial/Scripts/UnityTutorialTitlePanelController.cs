using UnityEngine;
using System.Collections;

public class UnityTutorialTitlePanelController : BasePanelController
{
	private bool isPushed = false;
	
	private void Start()
	{
		this.isPushed = false;
	}
	
	/// <summary>
	/// collision detection tutorial.
	/// </summary>
	public void OnTutorialCollision()
	{
		if(this.isPushed)
			return;
		this.isPushed = true;
		
		Mgrs.pnlMgr.Display("Prefabs/TutorialCollisionDetection");
	}
	
	/// <summary>
	/// create object tutorial.
	/// </summary>
	public void OnTutorialCreateObject()
	{
		if(this.isPushed)
			return;
		this.isPushed = true;
		
		Mgrs.pnlMgr.Display("Prefabs/TutorialCreateGameObject");
	}

	/// <summary>
	/// update tutorial.
	/// </summary>
	public void OnTutorialUpdate()
	{
		if(this.isPushed)
			return;
		this.isPushed = true;
		
		Mgrs.pnlMgr.Display("Prefabs/TutorialUpdateCurve");
	}
}
