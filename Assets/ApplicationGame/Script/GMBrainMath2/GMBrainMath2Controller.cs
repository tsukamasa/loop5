using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GMBrainMath2Controller : BaseGameController {

	public UIGrid gridNumbers;
	public GameObject prefabNumber;
	private enum MODE{
		HIGH = 1,
		LOW  = 2
	}

	public override string gameTitle {
		get {
			if( this.mode.Equals( MODE.HIGH ) ){
				return string.Format("数の大きい順にタップせよ！");
			}
			return string.Format("数の小さい順にタップせよ！");
		}
	}

	private int resultSum;
	private int currentNum;
	private int limit;
	private MODE mode;
	private const int GRID_COUNT = 9;
	public override void GameInit () {
		this.mode = (MODE)(Random.Range (1, 3));
		this.limit = Random.Range (3, 6);
		List<int> intList = new List<int> ();
		for (int i=1; i<=GRID_COUNT; i++) {
			if( i <= this.limit ) {
				intList.Add( i );
			}else{
				intList.Add( 0 );
			}
		}

		int[] intArr = this.Shuffle (intList.ToArray ());


		// instance
		foreach (int number in intArr) {
			GMButtonCommon btn = Util.InstantiateComponent<GMButtonCommon>( this.prefabNumber, this.gridNumbers.transform );
			if( number.Equals(0) ) {
				btn.gameObject.SetActive(false);
				continue;
			}
			btn.InitCallBack( this.OnClickItem );
			btn.SetLabel( number.ToString() );
			btn.SetParam( number );
			if( this.mode.Equals( MODE.HIGH ) ) {
				btn.SetColor( DEFINE.COLOR_ID.GREEN );
			}else{
				btn.SetColor( DEFINE.COLOR_ID.RED );
			}

		}
		if (this.mode.Equals (MODE.HIGH)) {
			this.currentNum = this.limit+1;
		} else {
			this.currentNum = 0;
		}
		base.GameInit ();
	}

	protected override void GameStart () {
		base.GameStart ();
	}

	private int[] Shuffle(int[] ary) {
		//Fisher-Yatesアルゴリズムでシャッフルする
		System.Random rng = new System.Random();
		int n = ary.Length;
		while (n > 1) {
			n--;
			int k = rng.Next(n + 1);
			int tmp = ary[k];
			ary[k] = ary[n];
			ary[n] = tmp;
		}
		return ary;
	}

	private void OnClickItem( GMButtonCommon btn ) {
		int number = btn.GetParam<int> ();
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );

		if (this.mode.Equals (MODE.HIGH)) {
			this.currentNum--;
			if (!number.Equals (this.currentNum)) {
				this.GameOver ();
				return;
			}
			
			if (number.Equals (1)) {
				this.GameClear ();
				return;
			}
		} else {
			this.currentNum++;
			if (!number.Equals (this.currentNum)) {
				this.GameOver ();
				return;
			}
			
			if (number.Equals (this.limit)) {
				this.GameClear ();
				return;
			}
		}
	}

}
