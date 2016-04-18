using UnityEngine;
using System.Collections;

public class TitlePanelController : BasePanelController {

	public GameObject btnDebugGMList;
	public UILabel lblBestRecord;
	public UIPanel[] panels;

	public override void Display (System.Collections.Generic.IDictionary<string, object> param) {
		foreach (UIPanel panel in this.panels) {
			panel.alpha = 0f;
		}
//		this.btnDebugGMList.SetActive (DEFINE.isDebug);
		base.Display (param);
		GameManager.SetBestRecordLabel (this.lblBestRecord, DEFINE_GAMES.GAME_ID.VARIETY);
	}

	/// <summary>
	/// ゲームをスタートする
	/// </summary>
	public void OnGamePlay(){
		Mgrs.sceneMgr.StartGame ();
	}
	
	public void OnDebugGMList() {
		if (!DEFINE.isDebug) {
			return;
		}
		Mgrs.pnlMgr.Display(DEFINE.PREFAB_PATH_GMLIST);
	}
	
}
