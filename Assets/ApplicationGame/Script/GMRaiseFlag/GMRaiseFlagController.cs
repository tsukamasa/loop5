using UnityEngine;
using System.Collections;

public class GMRaiseFlagController : BaseGameController
{
	/// <summary>判定処理中</summary>
	private bool isInJudgement = false;
	/// <summary>マスタの上げた旗と一致した回数</summary>
	private int matchCount = 0;
	/// <summary>matchCount表示用ラベル</summary>
	public UILabel lblMatchCount;
	/// <summary>1度もミスをせずにクリアしたか</summary>
	private bool isClear = true;
	/// <summary>制限時間到達</summary>
	[HideInInspector]
	public bool timeUpFlg = false;
	/// <summary>タイマー表示</summary>
	public UILabel lblTimer;
	/// <summary>制限時間になったときに表示される画像</summary>
	[Header("タイムアップ時に表示される画像")]
	public GameObject finishObj;
	/// <summary>お手本</summary>
	public GMRaiseFlagMasterController master;
	/// <summary>プレイヤー</summary>
	public GMRaiseFlagPlayerController player;

	/// <summary>残り時間が少なくなったときにタイマー表示を拡大させた時のscale情報</summary>
	private Vector3 afterScale = Vector3.one;
	/// <summary>タイマー表示をベースの何倍大きくするか</summary>
	[Header("タイマー表示を拡大させる倍率")]
	public float addScale = 1.0F;
	/// <summary>判定後にマスターが次の旗をあげるまでの間隔</summary>
	private float masterChoiceInterval = 0.8F;

	/// <summary>ボタン等のオブジェクト(勝敗が決まった時点で消す)</summary>
	public GameObject buttonObjects;

	/// <summary>制限時間</summary>
	public override float gameSetTime	{ get { return 9.9F; } }
	/// <summary>ゲームのタイトル</summary>
	public override string gameTitle	{ get { return "旗揚げ そうちゃん STAGE " + Mgrs.gameMgr.gameLv.ToString(); } }

	private void Awake()
	{
		this.lblMatchCount.text = "combo : " + this.matchCount.ToString ();
		this.afterScale = this.lblTimer.transform.localScale * this.addScale;
	}

	/// <summary>ゲーム開始時の処理</summary>
	protected override void GameStart ()
	{
		//	だんだん早くする
		this.masterChoiceInterval /= Mgrs.gameMgr.timeScale;
		base.GameStart ();
	}

	/// <summary>更新処理</summary>
	protected override void Update ()
	{
#if UNITY_EDITOR
		if(this.buttonObjects.activeSelf)
		{
			if(Input.GetKeyUp(KeyCode.LeftArrow))
			{
				this.OnPlayerChoiceLeft();
			}
			if(Input.GetKeyUp(KeyCode.RightArrow))
			{
				this.OnPlayerChoiceRight();
			}
		}
#endif
		//	タイムアップ
		if (this.timeUpFlg)
		{
			this.TimeUp ();
			return;
		}

		//	タイムアップになった　かつ　判定処理中ではない
		if (base.gameTimeLimit < 0F && !this.isInJudgement) 
		{
			this.SetTimeUp ();
			return;
		}
		this.UpdateLabel ();
	}

	/// <summary>タイムアップ後の処理</summary>
	private void TimeUp()
	{
		//	ゲーム終了表示がされている状態で画面がタップされたら
#if UNITY_EDITOR
		if ((this.finishObj.activeSelf && Input.GetMouseButtonUp (0)) || (this.finishObj.activeSelf && Input.GetKeyUp(KeyCode.Space)))
#else
			if (this.finishObj.activeSelf && Input.GetMouseButtonUp (0))
#endif
		{
			//	ゲーム終了表示を非表示に
			this.finishObj.SetActive (false);
			if (this.isClear)
			{
				//	ノーミスの場合はここでアニメーションから入る
				this.GameFinish ();
				return;
			}
			//	isClearがfalse = ミスをした = アニメーション再生済みなのでそのまま終了処理へ
			this.GameFinishWithWait ();
		}
	}

	/// <summary>タイムアップが来た時の処理</summary>
	private void SetTimeUp()
	{
		this.timeUpFlg = true;
		this.player.timeUpFlg = this.timeUpFlg;
		this.master.timeUpFlg = this.timeUpFlg;
		this.lblTimer.text = "0";
		this.lblTimer.color = Color.red;
		this.buttonObjects.SetActive (false);
		StartCoroutine (this.DisplayFinish ());
	}

	/// <summary>時間表示ラベルの制御</summary>
	private void UpdateLabel()
	{
		//	1秒未満の時、ラベルに1を表示するためにカウントダウンは1加算した状態での表示
		int timer = ((int)base.gameTimeLimit) + 1;
		bool isSoonFinish = (timer < 4);
		this.lblTimer.color = isSoonFinish ? Color.yellow : Color.white;
		this.lblTimer.transform.localScale = isSoonFinish ? this.afterScale : this.lblTimer.transform.localScale;
		this.lblTimer.text = timer.ToString ();
	}

	/// <summary>FINISHラベルを表示 </summary>
	private IEnumerator DisplayFinish()
	{
		yield return new WaitForSeconds (1.0F);
		this.finishObj.SetActive (true);
	}

	/// <summary>再度旗揚げができるように各パラメータを初期値に戻す</summary>
	private void InitializeCharacter()
	{
		this.player.Initialize ();
		this.master.Initialize ();
	}

	/// <summary>プレイヤーが左をタップ</summary>
	private void OnPlayerChoiceLeft()
	{
		if (this.player.isFixedChoice || this.player.animationPlaying || this.timeUpFlg || !this.isClear)
		{
			return;
		}
		this.isInJudgement = true;
		this.player.SetFlagAnimation (BaseGMRaiseFlagPlayerController.FLAG.LEFT, this.JudgeFlag);
	}
	/// <summary>プレイヤーが右をタップ</summary>
	private void OnPlayerChoiceRight()
	{
		if (this.player.isFixedChoice || this.player.animationPlaying || this.timeUpFlg || !this.isClear)
		{
			return;
		}
		this.isInJudgement = true;
		this.player.SetFlagAnimation (BaseGMRaiseFlagPlayerController.FLAG.RIGHT, this.JudgeFlag);
	}

	/// <summary>両者の旗の状態を比較し、合否判定をする</summary>
	private void JudgeFlag()
	{
		//	マスターの上げた旗ととプレイヤーの上げた旗が一致していない場合は失格
		if ((!this.master.isFixedChoice) || (this.master.isFixedChoice && (this.master.choice != this.player.choice)))
		{
			this.isClear = false;
			this.isInJudgement = false;
			this.GameFinish ();
			return;
		}

		//	一致している場合は継続
		this.isClear = true;
		++this.matchCount;
		this.lblMatchCount.text = "combo : " + this.matchCount.ToString ();
		this.isInJudgement = false;
		if (this.timeUpFlg)
		{
			return;
		}
		//	制限時間が残っていれば次の旗揚げ
		StartCoroutine (this.NextChoice ());
	}

	/// <summary>次の旗揚げを開始</summary>
	IEnumerator NextChoice()
	{
		yield return new WaitForSeconds (this.masterChoiceInterval);
		this.InitializeCharacter ();
	}

	/// <summary>ゲーム終了判定</summary>
	private void GameFinish()
	{
		this.buttonObjects.SetActive (false);
		if (this.isClear && this.matchCount > 0)
		{
			this.master.GameOverAnimation ();
			this.player.GameClearAnimation (this.GameFinishWithWait);
			return;
		}
		this.player.GameOverAnimation ();
		this.master.GameClearAnimation (this.GameFinishWithWait);
	}

	private void GameFinishWithWait()
	{
		if (!this.timeUpFlg)
		{
			return;
		}
		StartCoroutine (this.GameFinishIE ());
	}

	/// <summary>少し待ってからゲームクリア/ゲームオーバー処理を実行</summary>
	private IEnumerator GameFinishIE()
	{
		yield return new WaitForSeconds (1.0F);
		if (this.isClear)
		{
			base.GameClear ();
		}
		else
		{
			base.GameOver ();
		}
	}
}
