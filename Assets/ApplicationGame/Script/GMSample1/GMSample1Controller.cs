using UnityEngine;
using System.Collections;

public class GMSample1Controller : BaseGameController {

	public UILabel lblGameLv;
	public UILabel lblTimeLimit;

	protected override void Update () {
		if (!this.isPlaying) {
			return;
		}
		this.UpdateLabel ();
		base.Update ();
	}

	private void UpdateLabel(){
		this.lblTimeLimit.text = string.Format("残り時間::{0:f2}", this.gameTimeLimit);
	}

	public override void GameInit ()
	{
		this.UpdateLabel ();
		base.GameInit ();
	}

	protected override void GameStart () {
		this.lblGameLv.text = string.Format("STAGE::{0}", Mgrs.gameMgr.gameLv);
		base.GameStart ();
	}



}
