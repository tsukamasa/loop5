using UnityEngine;
using System.Collections;

public class BaseGMRaiseFlagPlayerController : MonoBehaviour
{
	public enum FLAG : int
	{
		NONE	= 0,
		LEFT	= -1,
		RIGHT	= 1,
	};

	/// <summary>制限時間到達</summary>
	[HideInInspector]
	public bool timeUpFlg = false;

	/// <summary>アニメーション再生中</summary>
	[HideInInspector]
	public bool animationPlaying { get; private set; }

	/// <summary>旗の選択が完了したかどうか。</summary>
	[HideInInspector]
	public bool isFixedChoice { get; private set; }
	/// <summary>選択した旗</summary>
	[HideInInspector]
	public FLAG choice { get; private set; }

	/// <summary>アニメータ</summary>
	public Animator playerAnimator;
	/// <summary>アニメータ制御フラグ：初期状態に戻す</summary>
	public const string TRIGGER_FORCE_STAY			= "TriggerForceStay";
	/// <summary>アニメータ制御フラグ：左旗揚げ</summary>
	public const string TRIGGER_RAISE_FLAG_LEFT		= "TriggerRaiseFlagLeft";
	/// <summary>アニメータ制御フラグ：右旗揚げ</summary>
	public const string TRIGGER_RAISE_FLAG_RIGHT	= "TriggerRaiseFlagRight";
	/// <summary>アニメータ制御フラグ：ゲームクリア</summary>
	public const string TRIGGER_GAME_CLEAR			= "TriggerGameClear";
	/// <summary>アニメータ制御フラグ：ゲームオーバー</summary>
	public const string TRIGGER_GAME_OVER			= "TriggerGameOver";
	/// <summary>フラグアニメーション完了のコールバック</summary>
	private System.Action callback = null;

	/// <summary>生成時初期化処理</summary>
	protected virtual void Awake()
	{
		this.Initialize ();
	}

	/// <summary>アニメーション周りの各パラメータを初期化</summary>
	public void Initialize()
	{
		this.playerAnimator.SetTrigger (TRIGGER_FORCE_STAY);
		this.choice = FLAG.NONE;
		this.isFixedChoice = false;
		this.animationPlaying = false;
	}

	/// <summary>左右どちらかの旗をあげ、アニメーション完了時にコールバックする</summary>
	public void SetFlagAnimation(FLAG choice, System.Action callback = null)
	{
		this.choice = choice;
		this.isFixedChoice = true;
		this.callback = callback;
		this.animationPlaying = true;
		//	この判定は良いか悪いか要検証
		if (this.timeUpFlg)
		{
			this.FinishAnimation ();
			return;
		}

		if (this.choice == FLAG.NONE)
		{
			return;
		}

		this.playerAnimator.SetTrigger ((this.choice == FLAG.LEFT) ? TRIGGER_RAISE_FLAG_LEFT : TRIGGER_RAISE_FLAG_RIGHT);
	}

	/// <summary>ゲームオーバー時のアニメーション</summary>
	public void GameOverAnimation(System.Action callback = null)
	{
		this.callback = callback;
		this.animationPlaying = true;
		this.playerAnimator.SetTrigger (TRIGGER_GAME_OVER);
	}

	/// <summary>ゲームクリア時のアニメーション</summary>
	public void GameClearAnimation(System.Action callback = null)
	{
		this.callback = callback;
		this.animationPlaying = true;
		this.playerAnimator.SetTrigger (TRIGGER_GAME_CLEAR);
	}

	/// <summary>アニメーション終了</summary>
	public void FinishAnimation()
	{
		if (this.callback != null)
		{
			this.callback ();
			this.callback = null;
		}
		this.animationPlaying = false;
	}
}