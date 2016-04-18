using UnityEngine;
using System.Collections;

public class GMRaiseFlagTitlePanelController : TitlePanelController
{
	private void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Space))
		{
			base.OnGamePlay ();
		}
	}
}
