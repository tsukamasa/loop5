using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GMCatchController : BaseGameController {

	public GameObject prefabItem;
	public Transform parentItems;
	public Transform wallTop;
	public Transform wallBtm;
	public Transform wallLeft;
	public Transform wallRight;

	public override string gameTitle {
		get {
			return string.Format(this.gameInfo.titleFormat, 
			                     DEFINE.COLORS[this.targetColorId].color16, 
			                     DEFINE.COLORS[this.targetColorId].name, 
			                     DEFINE.COLORS[DEFINE.COLOR_ID.BLACK].color16);
		}
	}

	private DEFINE.COLOR_ID targetColorId = DEFINE.COLOR_ID.RED;
	private int selectCount { 
		get {
			return (Mgrs.gameMgr.gameSpeed+2)*this.selectColorIds.Length;
		}
	}
	private DEFINE.COLOR_ID[] selectColorIds = new DEFINE.COLOR_ID[]{
		DEFINE.COLOR_ID.ORANGE,
		DEFINE.COLOR_ID.RED,
		DEFINE.COLOR_ID.GREEN,
		//		DEFINE.COLOR_ID.WHITE,
	};

	private LinkedList<GMCatchItemComponent> targetItemList;

	public override void GameInit (){
		this.targetColorId = (DEFINE.COLOR_ID)Random.Range (0, selectColorIds.Length-1);
		this.targetItemList = new LinkedList<GMCatchItemComponent> ();
		this.wallTop.localPosition   = Vector3.up * (ScreenScale.MANUAL_SIZE.y / 2f - 50f);
		this.wallBtm.localPosition   = Vector3.down * ScreenScale.MANUAL_SIZE.y / 2f;
		this.wallLeft.localPosition  = Vector3.left * ScreenScale.MANUAL_SIZE.x / 2f;
		this.wallRight.localPosition = Vector3.right * ScreenScale.MANUAL_SIZE.x / 2f;

		DEFINE.COLOR_ID colorId = this.targetColorId;
		for (int i=0; i<selectCount; i++) {
			colorId = (DEFINE.COLOR_ID)(i%selectColorIds.Length);
			GMCatchItemComponent itemComp = Util.InstantiateComponent<GMCatchItemComponent>( this.prefabItem, this.parentItems );
			itemComp.Init( colorId, this.OnSelectItem); 
			if (colorId.Equals (this.targetColorId)) {
				this.targetItemList.AddLast(itemComp);
			}
//			colorId = (DEFINE.COLOR_ID)Random.Range (0, selectColorIds.Length);
		}
		base.GameInit ();

	}

	protected override void GameStart ()
	{
		base.GameStart ();
	}

	private void OnSelectItem(GMButtonCommon btn) {
		GMCatchItemComponent item = btn as GMCatchItemComponent;
		if (!item.colorId.Equals (this.targetColorId)) {
//			Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_NG );
			this.GameOver();
			return;
		}
		if (this.targetItemList.Contains (item)) {
			Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );
			this.targetItemList.Remove(item);
		}
		if (this.targetItemList.Count.Equals (0)) {
			this.GameClear();
		}

	}
}
