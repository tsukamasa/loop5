using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public string firstPanelPath = "";
	public PanelManager pnlMgr;
	public GameManager gameMgr;
	public UnityAdsManager adsMgr;
	public AppAudioManager audioMgr;

	public void Awake() {
		Mgrs.sceneMgr = this;
		if( string.IsNullOrEmpty( this.firstPanelPath ) ) {
			return;
		}

		if( this.pnlMgr == null ) {
			return;
		}

		Mgrs.pnlMgr.Display( this.firstPanelPath );
		this.adsMgr = this.gameObject.AddComponent<UnityAdsManager> ();
		if (Mgrs.audioMgr != null) {
			audioMgr.PlayBGM (DEFINE_AUDIO.BGM_TYPE.NORMAL_01);
		}
	}

	public void StartGame(DEFINE_GAMES.GameInfo gameInfo) {
		this.StartGame (new DEFINE_GAMES.GameInfo[] { gameInfo });
	}
	public void StartGame(DEFINE_GAMES.GameInfo[] gameInfoList = null) {
		this.gameMgr.Init (gameInfoList);
		this.gameMgr.GameNext (true);
		this.pnlMgr.DestroyCurrentController ();
	}

	public void StartGameAgain() {
		Mgrs.gameMgr.heartCount--;
		this.gameMgr.GameAgain ();
		this.pnlMgr.DestroyCurrentController ();
	}

	public void GameOver() {
		Mgrs.pnlMgr.Display( DEFINE.PREFAB_PATH_GAMEOVER_PANEL );
	}

	public void EndGame() {
		Mgrs.pnlMgr.Display( DEFINE.PREFAB_PATH_TITLE_PANEL );
	}

	public void SetTimeScale(float timeScale) {
		Debug.Log ("timeScale:" + timeScale);
		UnityEngine.Time.timeScale = timeScale;
		Mgrs.audioMgr.sourceBGM.source.pitch = timeScale;
	}

	public void ResetTimeScale() {
		UnityEngine.Time.timeScale = 1f;
		Mgrs.audioMgr.sourceBGM.source.pitch = 1f;
	}

	void Destroy() {
		Mgrs.sceneMgr = null;
	}
}
