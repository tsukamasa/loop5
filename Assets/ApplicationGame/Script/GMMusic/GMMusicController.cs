using UnityEngine;
using System.Collections;

public class GMMusicController : BaseGameController {
	public GameObject blue;
	public GameObject red;
	public GameObject yellow;

	protected override void GameStart ()
	{
//		this.lblGameLv.text = string.Format("STAGE::{0}", Mgrs.gameMgr.gameLv);
		base.GameStart ();
	}
	
	protected override void Update()
	{
		base.Update ();

		if (!this.isPlaying) {
			return;
		}
	}
}
