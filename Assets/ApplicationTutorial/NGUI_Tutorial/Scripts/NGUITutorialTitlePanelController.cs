using UnityEngine;
using System.Collections;

public class NGUITutorialTitlePanelController : BasePanelController
{
	private bool isPushed = false;
	private bool isModalOpen = false;
	
	private void Start()
	{
		this.isPushed = false;
		this.isModalOpen = false;
	}
	
	/// <summary>
	/// button tutorial.
	/// </summary>
	public void OnTutorialButton()
	{
		if(this.isPushed || this.isModalOpen)
			return;
		this.isPushed = true;
		
		Mgrs.pnlMgr.Display("Prefabs/TutorialButton");
	}
	
	/// <summary>
	/// draggable panel tutorial.
	/// </summary>
	public void OnTutorialDraggablePanel()
	{
		if(this.isPushed || this.isModalOpen)
			return;
		this.isPushed = true;
		
		Mgrs.pnlMgr.Display("Prefabs/TutorialDraggablePanel");
	}

	/// <summary>
	/// grid tutorial.
	/// </summary>
	public void OnTutorialGrid()
	{
		if(this.isPushed || this.isModalOpen)
			return;
		this.isPushed = true;
		
		Mgrs.pnlMgr.Display("Prefabs/TutorialGrid");
	}

	/// <summary>
	/// input tutorial.
	/// </summary>
	public void OnTutorialInput()
	{
		if(this.isPushed || this.isModalOpen)
			return;
		this.isPushed = true;
		
		Mgrs.pnlMgr.Display("Prefabs/TutorialInput");
	}

	/// <summary>
	/// modal tutorial.
	/// </summary>
	public void OnTutorialModal()
	{
		if(this.isPushed || this.isModalOpen)
			return;
		this.isPushed = true;
		this.isModalOpen = true;
		
		GameObject modal = Instantiate(Resources.Load("Prefabs/TutorialModal")) as GameObject;
		Vector3 pos = modal.transform.localPosition;
		Vector3 scale = modal.transform.localScale;
		modal.transform.SetParent(transform);
		modal.transform.localPosition = pos;
		modal.transform.localScale = scale;
		
		TutorialModal tutorialModal = modal.GetComponent<TutorialModal>();
		tutorialModal.callback = this.ClosedModal;
		tutorialModal.isForcePopModal = true;
	}

	/// <summary>
	/// sprite tutorial.
	/// </summary>
	public void OnTutorialSprite()
	{
		if(this.isPushed || this.isModalOpen)
			return;
		this.isPushed = true;
		
		Mgrs.pnlMgr.Display("Prefabs/TutorialSprite");
	}

	/// <summary>
	/// depth tutorial.
	/// </summary>
	public void OnTutorialDepth()
	{
		if(this.isPushed || this.isModalOpen)
			return;
		this.isPushed = true;
		
		Mgrs.pnlMgr.Display("Prefabs/TutorialDepth");
	}
	
	private void ClosedModal()
	{
		this.isPushed = false;
		this.isModalOpen = false;
	}
}
