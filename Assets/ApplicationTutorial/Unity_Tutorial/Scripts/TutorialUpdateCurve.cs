using UnityEngine;
using System.Collections;

public class TutorialUpdateCurve : BasePanelController
{
	public Transform t;
	
	public float sinVelocity = 0.02F;
	public float cosVelocity = 0.015F;
	
	public bool isSinCurveEnable = true;
	public bool isCosCurveEnable = true;
	
	// Update is called once per frame
	void Update ()
	{
		float x = this.t.position.x;
		float y = this.t.position.y;
		
		if (this.isSinCurveEnable) 
		{
			y = Mathf.Sin (Time.frameCount * this.sinVelocity);
		}
		
		if (this.isCosCurveEnable) 
		{
			x = Mathf.Cos(Time.frameCount * this.cosVelocity);
		}
		
		this.t.position = new Vector3(x, y, this.t.position.z);
	}

	private void OnBackButton()
	{
		Mgrs.pnlMgr.Display("Prefabs/UnityTutorialTitlePanel");
	}
}