using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PongGamePanelController : BasePanelController {

	/// <summary>
	/// ボール作成する親obj
	/// </summary>
	public Transform parentBall;

	/// <summary>
	/// スコア表示(player1)
	/// </summary>
	public UILabel lblPlayer1;

	/// <summary>
	/// スコア表示(player2)
	/// </summary>
	public UILabel lblPlayer2;

	public RacketComponent racket1;
	public RacketComponent racket2;

	/// <summary>
	/// ゲームセット時に表示するobj
	/// </summary>
	public GameObject objGameSet;
	public UILabel lblWinner;

	public LinkedList<BallComponent> ballList = new LinkedList<BallComponent>();
	private ScoreManager scoreMgr;
	private GameObject prefabBall;

	/// <summary>
	/// 初期設定
	/// </summary>
	void Awake() {
		this.prefabBall = Resources.Load<GameObject>( DEFINE.PREFAB_PATH_BALL );
		this.racket1.Init( this );
		this.racket2.Init( this );
	}

	/// <summary>
	/// ここから始まります
	/// </summary>
	/// <param name="param">Parameter.</param>
	public override void Display (System.Collections.Generic.IDictionary<string, object> param) {
		base.Display (param);
		this.GameStart();
	}

	/// <summary>
	/// ゲーム初期化＆スタート
	/// </summary>
	public void GameStart() {
		this.scoreMgr = new ScoreManager( DEFINE.GAME_MATCH_POINT );
		this.objGameSet.SetActive( false );
		this.InitBall();
		this.UpdateScoreLabel();
	}

	/// <summary>
	/// ボール発射
	/// </summary>
	public void InitBall() {
		BallComponent ball = Util.InstantiateComponent<BallComponent>( this.prefabBall, this.parentBall );
		ball.Init( this );
	}

	/// <summary>
	/// ボールがゴールに衝突
	/// </summary>
	/// <param name="ball">Ball.</param>
	/// <param name="goal">Goal.</param>
	public void CollisionEnter(BallComponent ball, GoalComponent goal) {
		this.scoreMgr.AddScore( goal.playerId );
		this.UpdateScoreLabel();

		if( this.scoreMgr.isGameSet ) {
			this.GameSet();
		}else{
			this.InitBall();
		}
	}

	/// <summary>
	/// スコアラベルの更新
	/// </summary>
	private void UpdateScoreLabel() {
		this.lblPlayer1.text = this.scoreMgr.score[1].ToString();
		this.lblPlayer2.text = this.scoreMgr.score[2].ToString();
	}


	/// <summary>
	/// ゲーム終了
	/// </summary>
	private void GameSet() {
		this.lblWinner.text = string.Format("player{0} winner!!", this.scoreMgr.winnerPlayerId);
		this.objGameSet.SetActive( true );
	}

	/// <summary>
	/// タイトルに戻る
	/// </summary>
	public void OnBack() {
		Mgrs.pnlMgr.Display(DEFINE.PREFAB_PATH_TITLE_PANEL);
	}
}
