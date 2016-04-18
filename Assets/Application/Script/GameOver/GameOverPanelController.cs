using UnityEngine;
using System.Collections;

public class GameOverPanelController : BasePanelController {

	public GameObject objBeforeAds;
	public GameObject objAfterAds;
	public GameObject objNoAds;
	public GameObject objTweet;
	private GameObject[] objWrapBtns;
	public UILabel lblMessage;
	public UISprite[] spriteHearts;
	public UILabel lblBestRecord;


	private const string MESSAGE_FORMAT = "{0}\n\n{1}\n{2}";
	private static string MESSAGE_TITLE_GAMEOVER = string.Format ("[{0}]GAME OVER!![{1}]", DEFINE.COLORS [DEFINE.COLOR_ID.GREEN].color16, DEFINE.COLORS [DEFINE.COLOR_ID.WHITE].color16);
	private static string MESSAGE_TITLE_NEW_RECORD = string.Format ("[{0}]NEW RECORD!![{1}]", DEFINE.COLORS [DEFINE.COLOR_ID.RED].color16, DEFINE.COLORS [DEFINE.COLOR_ID.WHITE].color16);
	private const string MESSAGE_STAGE_CLEAR = "到達Lv.{0}";
	private const string MESSAGE_STAGE_CURRENT = "現在のステージ：{0}";

//	private const string MESSAGE_MONOTONY = "Monotony({0})でLv.{1}までクリア！\n[Loop5]{2}";
//	private const string MESSAGE_VARIETY = "VarietyでLv.{1}までクリア！\n[Loop5]{2}";
	private const string MESSAGE_MONOTONY = "Monotony({0})でLv.{1}までクリア！ #Loop5";
	private const string MESSAGE_VARIETY = "VarietyでLv.{1}までクリア！\n#Loop5";

	private bool isActiveBtn = true;

	private enum TYPE
	{
		CAN_RETRY = 0,
		PLAY_ADS,
		ONLY_GIVEUP,
	}

	void Awake() {
		this.objWrapBtns = new GameObject[]{
			this.objAfterAds,
			this.objBeforeAds,
			this.objNoAds,
		};
	}

	private bool isNewRecord {
		get {
			return ((Mgrs.gameMgr.gameLv-1) > Mgrs.gameMgr.bestRecord);
		}
	}

	public override void Display (System.Collections.Generic.IDictionary<string, object> param) {
		base.Display (param);
		this.lblMessage.text = GetMessage();
		GameManager.SetBestRecordLabel (this.lblBestRecord, Mgrs.gameMgr.bestRecord, Mgrs.gameMgr.gameLv-1);
//		this.lblBestRecord.text = (Mgrs.gameMgr.bestRecord > 0) ? string.Format (DEFINE.FORMAT_BEST_RECORD, Mgrs.gameMgr.bestRecord) : "";
		TYPE type = TYPE.CAN_RETRY;
		if ( Mgrs.gameMgr.heartCount.Equals(0) ) {
			if( Mgrs.gameMgr.adsCount >= DEFINE.ADS_COUNT ) {
				type = TYPE.ONLY_GIVEUP;
			}else{
				type = TYPE.PLAY_ADS;
			}
		}
		this.objTweet.SetActive( (Mgrs.gameMgr.gameLv > 1 )); 
		this.SwitchButton (type);
		this.SetHearts();
	}

	private string GetMessage() {
		string title = this.isNewRecord ? MESSAGE_TITLE_NEW_RECORD : MESSAGE_TITLE_GAMEOVER;
		string formatStageClear = (Mgrs.gameMgr.gameLv > 1) ? MESSAGE_STAGE_CLEAR : "";
		string stageClear = string.Format (formatStageClear, Mgrs.gameMgr.gameLv-1);
		string stageCurrent = string.Format (MESSAGE_STAGE_CURRENT, Mgrs.gameMgr.currentGameInfo.title);

		return string.Format (MESSAGE_FORMAT, title, stageClear, stageCurrent);
	}

	public string GetSocialMessage() {
		string format = Mgrs.gameMgr.isMonotony ? MESSAGE_MONOTONY : MESSAGE_VARIETY;
		return string.Format (format, Mgrs.gameMgr.currentGameInfo.title, Mgrs.gameMgr.gameLv - 1, DEFINE.STORE_URL); 
	}

	private void SetHearts() {
		int i = 0;
		foreach (UISprite sprite in spriteHearts) {
			bool isActive = (i<Mgrs.gameMgr.heartCount);
			sprite.alpha = isActive ? 1f : 0.5f;
			i++;
		}
	}

	private void SwitchButton( TYPE type ) {
		int i = 0;
		foreach (GameObject obj in this.objWrapBtns) {
			obj.SetActive( i.Equals( (int)type ) );
			i++;
		}
	}

	/// <summary>
	/// ゲームを諦める
	/// </summary>
	public void OnGameOver(){
		if (!this.isActiveBtn) {
			return;
		}
		this.isActiveBtn = false;
		Mgrs.sceneMgr.EndGame ();
//		Mgrs.pnlMgr.Display(DEFINE.PREFAB_PATH_PONG_GAME_PANEL);
	}

	/// <summary>
	/// 広告を見る
	/// </summary>
	public void OnPlayAds(){
		if (!this.isActiveBtn) {
			return;
		}
		this.isActiveBtn = false;
		Mgrs.adsMgr.Show (() => {
			// 広告終了
			Mgrs.gameMgr.heartCount += DEFINE.ADS_HEART_HEEL;
			Mgrs.gameMgr.adsCount++;
			this.SwitchButton( TYPE.CAN_RETRY );
			for(int i=0; i<Mgrs.gameMgr.heartCount; i++ ) {
				TweenAlpha twAlpha = UITweener.Begin<TweenAlpha> (this.spriteHearts [i].gameObject, 0.4f);
				twAlpha.from = 0.5f;
				twAlpha.to = 1f;
			}
//			this.SetHearts();
			this.isActiveBtn = true;
		},()=>{
			// 広告失敗
			// game over ?
			// OnGameOver();
			this.isActiveBtn = true;
		});
	}


	/// <summary>
	/// 再挑戦
	/// </summary>
	public void OnGameAgain(){
		if (!this.isActiveBtn) {
			return;
		}
		this.isActiveBtn = false;
		StartCoroutine (GameAgain ());
	}

	private IEnumerator GameAgain() {
		TweenAlpha twAlpha = UITweener.Begin<TweenAlpha> (this.spriteHearts [Mgrs.gameMgr.heartCount - 1].gameObject, 0.2f);
		twAlpha.from = 1f;
		twAlpha.to = 0.5f;
		while (twAlpha.enabled) {
			yield return null;
		}
		yield return new WaitForSeconds (0.3f);
		Mgrs.sceneMgr.StartGameAgain ();
	}


	public void OnTweet() {
		Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(GetSocialMessage()));
	}

}
