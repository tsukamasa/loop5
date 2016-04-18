using UnityEngine;
using System.Collections;

public class BaseGameController : MonoBehaviour {

	private float gameStartTime;
	public float gameTimer {
		get { return (this.currentTime - this.gameStartTime); }
	}

	public DEFINE_GAMES.GameInfo gameInfo {
		get;
		private set;
	}

	private float currentTime {
		get {
			switch( this.gameTimeType ) {
			case GAME_TIME_TYPE.TIME_LIMT:{
				return Time.time;
				break;
			}
			case GAME_TIME_TYPE.TIME_SURVIVE:{
				return Time.realtimeSinceStartup;
			}
			}
			return 0f;
		}
	}

	public float gameTimeLimit {
		get {
			return this.gameSetTime - this.gameTimer;
		}
	}

	/// <summary>
	/// 制限時間
	/// </summary>
	/// <value>The game set time.</value>
	public virtual float gameSetTime {
		get { return 5f; }
	}

	public virtual DEFINE_AUDIO.BGM_TYPE bgmType {
		get {
			return DEFINE_AUDIO.BGM_TYPE.GAME_01;
		}
	}

	public bool isPlaying = false;

	public enum GAME_TIME_TYPE {
		TIME_LIMT,	//制限時間内にクリアする
		TIME_SURVIVE	//制限時間耐える
	}

	/// <summary>
	/// 制限時間内にクリアするか、制限時間耐えるか
	/// </summary>
	/// <value>The type of the game time.</value>
	protected virtual GAME_TIME_TYPE gameTimeType{
		get {
			return GAME_TIME_TYPE.TIME_LIMT;
		}
	}

	protected virtual void Update() {
		if (!this.isPlaying) {
			return;
		}
		if (gameTimer >= gameSetTime) {
			switch( this.gameTimeType ) {
			case GAME_TIME_TYPE.TIME_LIMT: {
				this.GameOver();
				break;
			}
			case GAME_TIME_TYPE.TIME_SURVIVE: {
				this.GameClear();
				break;
			}
			}
		}
	}

	public virtual string gameTitle {
		get { return gameInfo.title; }
	}


	public void GameInitBase(DEFINE_GAMES.GameInfo gameInfo) {
		this.gameInfo = gameInfo;
		this.gameStartTime = this.currentTime;
		this.isPlaying = false;
		this.GameInit ();
	}

	public virtual void GameInit() {
	}

	/// <summary>
	/// Games the start.
	/// </summary>
	public void GameStartBase() {
		this.gameStartTime = this.currentTime;
		this.isPlaying = true;
		this.GameStart ();
	}

	/// <summary>
	/// Games the start.
	/// </summary>
	protected virtual void GameStart() {
	}

	/// <summary>
	/// ゲームクリア時に実行
	/// </summary>
	public void GameClear() {
		if (!this.isPlaying) {
			return;
		}
		this.isPlaying = false;
		Mgrs.gameMgr.GameNext ();
	}

	/// <summary>
	/// ゲームオーバー時に実行
	/// </summary>
	public void GameOver() {
		if (!this.isPlaying) {
			return;
		}
		this.isPlaying = false;
		Mgrs.gameMgr.GameOver ();
	}
}
