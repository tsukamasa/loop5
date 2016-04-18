using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public DEFINE_GAMES.GameInfo currentGameInfo;
	public Camera bgCamera;

	//1~
	public int gameLv { get; private set; }
	public int sp {
		get {
			return (gameLv-1)/DEFINE.SPEED_STEP_LV + 1;
		}
	}
	
	public int gameSpeed {
		get {
			// 1~5
			int speed = this.sp;
			if( speed > DEFINE.SPEED_MAX ) {
				speed = DEFINE.SPEED_MAX;
			}
			return speed;
		}
	}
	public int heartCount;
	public int adsCount;
	public float timeScale {
		get {
			float scale = 1f;
			if( this.gameSpeed <= 1 ) return scale;
			scale = 1f + ( (float)this.gameSpeed*(0.8f/5f) );
			if( scale >= 3f ) {
				scale = 3f;
			}
			return scale;
		}
	}

	private bool isProcess;

	public Color speedColor {
		get {
			return DEFINE.SPPED_COLORS[ this.sp%DEFINE.SPPED_COLORS.Length ];
//			Color min = DEFINE.SPEED_LV_COLOR_MIN;
//			Color max = DEFINE.SPEED_LV_COLOR_MAX;
//			Color color = new Color( min.r, min.g, min.b );
//			float ratio = ( ((float)gameSpeed-1f)/((float)DEFINE.SPEED_MAX-1f) );
//			color += (max - min)*ratio;
//			return color;
		}
	}

	private List<int> playedIdList = new List<int>();
	private DEFINE_GAMES.GameInfo[] targetGameList;
	private bool isGiveUp = false;

	public bool isMonotony {
		get {
			return this.targetGameList.Length.Equals(1);
		}
	}

	public DEFINE_GAMES.GAME_ID gameId {
		get{
			if( isMonotony ) {
				return this.targetGameList[0].id;
			}
			return DEFINE_GAMES.GAME_ID.VARIETY;
		}
	}

	public int bestRecord { get; private set; }

	public void Update(){
		this.UpdateStageInfo ();
	}

	public void Init(DEFINE_GAMES.GameInfo[] gameList) {
		this.isProcess = false;
		if (gameList == null) {
			gameList = DEFINE_GAMES.GAMES;
		}
		this.targetGameList = gameList;
		this.bestRecord = ScoreSaveData.instance.GetScore (this.gameId);
		this.playedIdList.Clear();
		this.objStageInfo.SetActive (false);
		this.isGiveUp = false;
		this.gameLv = 0;
		this.heartCount = DEFINE.HEART_COUNT;
		this.adsCount = 0;
	}

	public void GameNext(bool isFirst = false) {
		if (this.isProcess) {
			return;
		}
		if (!isFirst) {
			ScoreSaveData.instance.SetScore( this.gameId, this.gameLv);
			Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_CLEAR );
		}
		this.gameLv++;
		Mgrs.sceneMgr.SetTimeScale (this.timeScale);
#if !DEBUG_ONE_GAME
		int gameIdx = GetNextGameIdx ();
		this.currentGameInfo = this.targetGameList [gameIdx];
#endif
		this.Display ( this.currentGameInfo );
	}

	private int GetNextGameIdx() {
		if (this.playedIdList.Count.Equals (this.targetGameList.Length)) {
			this.playedIdList.Clear();
		}
		int gameIdx = Random.Range (0, this.targetGameList.Length);
		if (this.playedIdList.Contains (gameIdx)) {
			return GetNextGameIdx();
		}
//		if (!this.isMonotony && this.currentGameInfo != null) {
//			if( this.currentGameInfo.id.Equals( this.targetGameList[gameIdx].id ) ){
//				return GetNextGameIdx();
//			}
//		}
		this.playedIdList.Add (gameIdx);
		return gameIdx;
	}

	public void GameAgain() {
		this.bestRecord = ScoreSaveData.instance.GetScore (this.gameId);
		Mgrs.sceneMgr.SetTimeScale (this.timeScale);
		this.Display ( this.currentGameInfo );
	}
	
	public void GameOver() {
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_OVER );
		this.btnPause.SetActive (false);
		Mgrs.sceneMgr.ResetTimeScale ();
		this.objStageInfo.SetActive (false);
		this.DestroyCurrentController ();
		Mgrs.sceneMgr.GameOver ();
		if (Mgrs.audioMgr != null) {
			Mgrs.audioMgr.PlayBGM (DEFINE_AUDIO.BGM_TYPE.NORMAL_01);
		}
		this.bgCamera.backgroundColor = DEFINE.COLORS[DEFINE.COLOR_ID.BLACK].color;
	}

	public Transform parentTransform;
	
	private BaseGameController currentGameCtrl;
	
	public void Display( DEFINE_GAMES.GameInfo gameInfo, IDictionary<string, object> param = null) {
		this.btnPause.SetActive (true);
		BaseGameController gmCtrl = Util.InstantiateComponent<BaseGameController>( gameInfo.prefabPath, this.parentTransform );
		if( gmCtrl == null ) {
			Debug.LogError( string.Format("nothing panel ::{0}", gameInfo.prefabPath ) );
			return;
		}
		StartCoroutine (DisplayNext(gmCtrl, gameInfo));
	}

	private IEnumerator DisplayNext( BaseGameController gmCtrl, DEFINE_GAMES.GameInfo gameInfo ) {
		this.isProcess = true;
		gmCtrl.GameInitBase (gameInfo);
		if (Mgrs.audioMgr != null) {
			Mgrs.audioMgr.PlayBGM (gmCtrl.bgmType);
		}
		this.lblGameTitle.text = gmCtrl.gameTitle;
		gmCtrl.gameObject.SetActive (false);
		yield return StartCoroutine (StartTransitionOpen ());
		if (this.isGiveUp) {
			this.isProcess = false;
			yield break;
		}


		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_START );
		this.DestroyCurrentController ();
		this.bgCamera.backgroundColor = this.speedColor;
		yield return new WaitForSeconds (1f);
		if (this.isGiveUp) {
			this.isProcess = false;
			yield break;
		}
		this.SetStageInfo (gmCtrl);
		this.objStageInfo.SetActive (true);
		gmCtrl.gameObject.SetActive (true);
		yield return StartCoroutine (StartTransitionClose ());
		if (this.isGiveUp) {
			this.isProcess = false;
			yield break;
		}
		gmCtrl.GameStartBase ();
		this.currentGameCtrl = gmCtrl;
		this.isProcess = false;
	}

	public void DestroyCurrentController() {
		if( this.currentGameCtrl != null ) {
			this.currentGameCtrl.transform.localScale = Vector3.zero;
			Destroy( this.currentGameCtrl.gameObject );
			this.currentGameCtrl = null;
		}
	}

	#region stageInfo
	public GameObject objStageInfo;
	public UILabel lblStageNo;
	public UILabel lblStageTitle;
	public UISlider sliderStageTime;
	private void UpdateStageInfo() {
		if (this.currentGameCtrl == null) {
			return;
		}
		if (!this.currentGameCtrl.isPlaying) {
			return;
		}
//		Debug.LogError( string.Format("{0}:::{1}:::{2:2f}",this.currentGameCtrl.gameTimer, 
		this.sliderStageTime.sliderValue = (this.currentGameCtrl.gameTimeLimit/this.currentGameCtrl.gameSetTime);
	}

	private void SetStageInfo(BaseGameController gameCtrl){
		this.lblStageNo.text = string.Format ("Lv.{0}  {1}", this.gameLv, gameCtrl.gameTitle);
//		this.lblStageTitle.text = gameCtrl.gameTitle;
		this.lblStageTitle.text = "";
		this.sliderStageTime.sliderValue = 1f;
	}
	#endregion

	#region title
	public GameObject objGameTitle;
	public UILabel lblGameTitle;
	public TweenScale twScaleTitleBg;
	public TweenAlpha twAlphaTitleLbl;
	public GameObject[] objFixSizeScreenWidth;
	private IEnumerator StartTransitionOpen() {
		this.FixSizeWidth ();
		this.objGameTitle.SetActive (true);
		twScaleTitleBg.from = Vector3.zero;
		twScaleTitleBg.to = Vector3.one * 1500;
		twScaleTitleBg.delay = 0f;
		twAlphaTitleLbl.from = 0f;
		twAlphaTitleLbl.to = 1f;
		twAlphaTitleLbl.delay = 0.2f;
		
		UITweener.Begin<TweenScale> (twScaleTitleBg.gameObject, twScaleTitleBg.duration);
		UITweener.Begin<TweenAlpha> (twAlphaTitleLbl.gameObject, twAlphaTitleLbl.duration);
		while (twScaleTitleBg.enabled || twAlphaTitleLbl.enabled ) {
			yield return null;
		}
	}
	
	private IEnumerator StartTransitionClose() {
		twScaleTitleBg.from = Vector3.one * 1200;
		twScaleTitleBg.to = Vector3.zero;
		twScaleTitleBg.delay = 0.2f;
		twAlphaTitleLbl.from = 1f;
		twAlphaTitleLbl.to = 0f;
		twScaleTitleBg.delay = 0f;
		
		UITweener.Begin<TweenScale> (twScaleTitleBg.gameObject, twScaleTitleBg.duration);
		UITweener.Begin<TweenAlpha> (twAlphaTitleLbl.gameObject, twAlphaTitleLbl.duration);
		while (twScaleTitleBg.enabled || twAlphaTitleLbl.enabled ) {
			yield return null;
		}
		this.objGameTitle.SetActive (false);
	}

	private void FixSizeWidth() {
		foreach (GameObject obj in this.objFixSizeScreenWidth) {
			Vector3 scale = obj.transform.localScale;
			scale.x = ScreenScale.MANUAL_SIZE.x;
			obj.transform.localScale = scale;
		}
	}

	#endregion

	public GameObject objPause;
	public GameObject btnPause;
	public void OnPause() {
		if (this.currentGameCtrl == null) {
			return;
		}
		this.objPause.SetActive (true);
		this.btnPause.SetActive (false);
		UnityEngine.Time.timeScale = 0f;
	}

	public void OnGiveUp() {
		this.isGiveUp = true;
		this.objPause.SetActive (false);
		this.btnPause.SetActive (true);
		this.objGameTitle.SetActive (false);
		UnityEngine.Time.timeScale = this.timeScale;
		this.GameOver ();
		Mgrs.sceneMgr.EndGame ();
//		this.currentGameCtrl.GameOver ();
	}

	public void OnReStrart() {
		this.objPause.SetActive (false);
		this.btnPause.SetActive (true);
		UnityEngine.Time.timeScale = this.timeScale;
	}

	public void OnApplicationPause( bool isPause ) {
		if (isPause) {
			this.OnPause ();
		}
	}

	public static void SetBestRecordLabel( UILabel lbl, DEFINE_GAMES.GAME_ID gameId ) {
		int record = ScoreSaveData.instance.GetScore (gameId);
		SetBestRecordLabel (lbl, record);
	}

	public static void SetBestRecordLabel( UILabel lbl, int record, int currentLv = 0 ) {
		if (lbl == null)
			return;

		string recordText = "--";
		if (currentLv > record) {
			record = currentLv;
		}
		if( record > 0 ) {
			recordText = record.ToString();
		}
		lbl.text = string.Format(DEFINE.FORMAT_BEST_RECORD, recordText);

	}
}
