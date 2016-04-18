using UnityEngine;
using System.Collections;

public class NumberButtonComponent : MonoBehaviour
{
	/// <summary>ボタンのスプライト</summary>
	public UISprite sprNumber;
	/// <summary>番号表示ラベル</summary>
	public UILabel lblNumber;

	/// <summary>自身のインデックス</summary>
	private int index = 0;

	/// <summary>自分の番号が次に押されるものかどうか</summary>
	private bool isEnable = false;

	/// <summary>FadeOut開始フラグ</summary>
	private bool isStartFadeOut = false;
	/// <summary>FadeOut終了フラグ</summary>
	private bool isFadeOuted = false;
	/// <summary>FadeIn開始フラグ</summary>
	private bool isStartFadeIn = false;
	private bool isFadeIned = false;

	/// <summary>フェードイン/アウトの増減値</summary>
	private float fadeValue{ get{ return Mgrs.gameMgr.gameSpeed * 0.03F; } }

	/// <summary>ボタン押下時のコールバック(インデックスを返す)</summary>
	private System.Action<int> callback;

	/// <summary>初期化処理</summary>
	public void Init(int index, System.Action<int> callback, bool isEnable = false)
	{
		this.isStartFadeOut = false;
		this.isFadeOuted = false;

		this.index = index;
		string idxStr = this.index.ToString ();
		this.gameObject.name += idxStr.PadLeft (3, '0');
		this.sprNumber.MakePixelPerfect ();
		this.lblNumber.text = idxStr;
		this.callback  = callback;
		this.SetEnable(isEnable);

		this.isStartFadeIn = true;
	}

	/// <summary>isEnableをセット</summary>
	public void SetEnable(bool isEnable)
	{
		this.isEnable = isEnable;
	}

	/// <summary>ボタンがおされた</summary>
	private void OnPushNumber()
	{
		//	正しい番号ではないときはSEを鳴らしておわり
		if (!this.isEnable){
			Mgrs.audioMgr.PlaySE (DEFINE_AUDIO.SE_TYPE.GAME_NG);
			return;
		}

		Mgrs.audioMgr.PlaySE(DEFINE_AUDIO.SE_TYPE.GAME_TAP);
		if (this.callback != null)
			this.callback (this.index);
	}

	/// <summary>フェードアウト処理開始</summary>
	public void FadeOut()
	{
		if (this.isStartFadeOut)
			return;

		this.isStartFadeOut = true;
	}

	private void Update()
	{
		if (this.isStartFadeIn && !this.isFadeIned)
		{
			float lblAlpha = this.lblNumber.alpha;
			float sprAlpha = this.sprNumber.alpha;
			lblAlpha = ((lblAlpha + fadeValue) >= 1.0F) ? 1.0F : lblAlpha + fadeValue;
			sprAlpha = ((sprAlpha + fadeValue) >= 1.0F) ? 1.0F : sprAlpha + fadeValue;
			this.lblNumber.alpha = lblAlpha;
			this.sprNumber.alpha = sprAlpha;
			this.isFadeIned = (lblAlpha >= 1.0F && sprAlpha >= 1.0F);
		}

		if (this.isStartFadeOut && !this.isFadeOuted)
		{
			float lblAlpha = this.lblNumber.alpha;
			float sprAlpha = this.sprNumber.alpha;
			lblAlpha = (lblAlpha >= fadeValue) ? lblAlpha - fadeValue : 0.0F;
			sprAlpha = (sprAlpha >= fadeValue) ? sprAlpha - fadeValue : 0.0F;
			this.lblNumber.alpha = lblAlpha;
			this.sprNumber.alpha = sprAlpha;
			this.isFadeOuted = (lblAlpha <= 0.0F && sprAlpha <= 0.0F);
			if (this.isFadeOuted)
			{
				DestroyObject (gameObject);
			}
		}
	}
}