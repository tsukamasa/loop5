using UnityEngine;
using System.Collections;

public class GMListComponent : MonoBehaviour {

	public UILabel lblName;
	public UILabel lblBestRecord;

	public DEFINE_GAMES.GameInfo gameInfo {
		get;
		private set;
	}

	public void Init(DEFINE_GAMES.GameInfo gameInfo ) {
		this.gameInfo = gameInfo;
		this.lblName.text = gameInfo.title;
		GameManager.SetBestRecordLabel (this.lblBestRecord, gameInfo.id);
	}

	public void OnSelectGame(){
		Mgrs.sceneMgr.StartGame (this.gameInfo);
	}

}
