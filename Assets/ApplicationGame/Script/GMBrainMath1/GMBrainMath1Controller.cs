using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GMBrainMath1Controller : BaseGameController {

	public UIGrid gridNumbers;
	public GameObject prefabNumber;

	public override string gameTitle {
		get {
			return string.Format(this.gameInfo.titleFormat, this.resultSum);
		}
	}

	private List<int> intList;
	private int resultSum;
	private int currentSum;
	public override void GameInit () {

		int listCount = 9;
		int randomMin = 1;
		int randomMax = 9;
		int resultMin = 10;
		randomMin = Mgrs.gameMgr.gameSpeed;
		resultMin = 10 - DEFINE.SPEED_MAX - Mgrs.gameMgr.gameSpeed;
		this.intList = new List<int> ();
		for (int i=0; i<listCount; i++) {
			int random = Random.Range(randomMin, randomMax);
			intList.Add( random );
		}

		// 和の抽選
		int lotteryCount = Random.Range (2, 3);
		this.resultSum = 0;
		List<int> lotteryIdxList = new List<int> (); 

		do{
			int idx = Random.Range(0,listCount);
			if( !lotteryIdxList.Contains( idx ) ){
				lotteryIdxList.Add( idx );
				this.resultSum += intList[idx];
			}
		}while( lotteryIdxList.Count < lotteryCount || this.resultSum <= resultMin );

		// instance
		foreach (int number in this.intList) {
			GMButtonCommon btn = Util.InstantiateComponent<GMButtonCommon>( this.prefabNumber, this.gridNumbers.transform );
			btn.InitCallBack( this.OnClickItem );
			btn.SetLabel( number.ToString() );
			btn.SetParam( number );
			btn.SetColor( DEFINE.COLOR_ID.RED );
		}
		this.currentSum = 0;
		base.GameInit ();
	}

	protected override void GameStart () {
		base.GameStart ();
	}

	private void OnClickItem( GMButtonCommon btn ) {
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );
		int number = btn.GetParam<int> ();
		this.currentSum += number;
		if( this.currentSum.Equals( this.resultSum ) ){
			this.GameClear();
			return;
		}
		if (this.currentSum > this.resultSum) {
			this.GameOver();
			return;
		}
	}

}
