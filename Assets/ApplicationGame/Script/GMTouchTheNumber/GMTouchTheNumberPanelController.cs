using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GMTouchTheNumberPanelController : BaseGameController
{
	public override float gameSetTime {
		get {
			float time = DEFAULT_TIME * Mathf.Pow(0.853F, (float)(Mgrs.gameMgr.gameSpeed-1));
			return Mathf.Clamp (time, MIN_TIME, DEFAULT_TIME);
		} 
	}
	private const float BASE_TIME = 0.853F;
	private const float MIN_TIME = 4.5F;
	private const float DEFAULT_TIME = 10.0F;

	/// <summary>生成する最小数</summary>
	private const int MIN_NUM = 1;
	/// <summary>生成する最大数</summary>
	private const int MAX_NUM = 9;

	/// <summary>生成するボタンのプレハブ</summary>
	public GameObject prefabObj;
	/// <summary>生成するボタンの親オブジェクト</summary>
	public Transform[] parentTransform = new Transform[MAX_NUM];

	/// <summary>各ボタンのリスト[KEY:buttonIndex, VALUE:component]</summary>
	private Dictionary<int,NumberButtonComponent> btnDict = new Dictionary<int, NumberButtonComponent> ();
	/// <summary>座標情報[KEY:buttonIndex, VALUE:Transform]</summary>
	private Dictionary<int, Transform> parentDict = new Dictionary<int, Transform> ();

	/// <summary>次に押すべき数字</summary>
	private int nextIndex = 1;
	/// <summary>押す数字の最大値</summary>
	private int indexMax = 1;

	protected override void GameStart ()
	{
		this.nextIndex = 1;
		//	(gameSpeed ( 1~5 ) * 2 ) + ( 1 or 2 ) = 3 ~ 9 
		this.indexMax = Mathf.Clamp( ((Mgrs.gameMgr.gameSpeed * 2) + Random.Range(1, 3)) , MIN_NUM, MAX_NUM);

		this.SetCreatePosition ();
		this.CreateNumberButton ();

		base.GameStart ();
	}

	/// <summary>全部で9個分の座標を定義し、1~9のボタンの座標をランダムで割り当て</summary>
	private void SetCreatePosition()
	{
		Transform[] temp = this.parentTransform;
		List<Transform> parentList = new List<Transform>();
		parentList.AddRange (temp);

		for (int index = 0; parentList.Count > 0 && index < indexMax; index++)
		{
			int parentIndex = Random.Range (0, parentList.Count);
			Transform parent = parentList [parentIndex];
			parentList.RemoveAt (parentIndex);
			this.parentDict.Add (index + 1, parent);
		}
	}

	/// <summary>ランダムで割り当てられた座標を元にボタンを生成</summary>
	private void CreateNumberButton()
	{
		foreach (KeyValuePair<int,Transform> parent in this.parentDict)
		{
			NumberButtonComponent btn = Util.InstantiateComponent<NumberButtonComponent> (this.prefabObj, parent.Value);
			btn.Init (parent.Key, this.OnPushButton);
			this.btnDict.Add (parent.Key, btn);
		}
		this.btnDict [this.nextIndex].SetEnable (true);
	}

	/// <summary>ボタンが押された</summary>
	private void OnPushButton(int index)
	{
		//	ゲーム終了済み
		if (!this.isPlaying)
			return;

		//	押すべき数字と一致しない
		if (index != this.nextIndex)
			return;

		//	全ての番号を押した
		if (index == this.indexMax)
		{
			this.nextIndex = this.indexMax + 1;
			this.GameClear ();
			return;
		}

		//	フェードアウト処理
		this.btnDict [index].SetEnable (false);
		this.btnDict [index].FadeOut ();
		//	数字の更新
		++this.nextIndex;
		//	アクティブの切り替え
		this.btnDict [nextIndex].SetEnable (true);
	}
}