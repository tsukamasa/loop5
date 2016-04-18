using UnityEngine;
using System.Collections;

//TODO ゲームレベルデザインを考える 現在左右に揺れるだけなのでその動き方を増やす

public class GMmatoateControllor : BaseGameController {
	public GameObject ya;
	public GameObject matoObject;
	public float speed;
	public bool isHit;
	public int bullet;
	public bool isMiss;

    public DestroyArea destroy;

    //instantiateするpurefab
    public GameObject shotprefab;
    //prefabを入れる変数
    private shot P_shot;
    //prefabの初期位置
    private Vector3 shotPosition;

    //配置してある操作用ボタン
	public UIButtonMessage button;
	



	// Use this for initialization
 	protected override void GameStart ()
	{
        //this.lblGameLv.text = string.Format("STAGE::{0}", Mgrs.gameMgr.gameLv);
		isHit = false;
		isMiss = false;
		bullet = 3;
		shotPosition = ya.transform.position;
		base.GameStart ();
	}

	protected override void Update ()
	{
        //this.lblTimeLimit.text = string.Format("残り時間::{0:f2}", this.gameTimeLimit);
        //base.Update ();
		if (!this.isPlaying)
			return;
        if(gameTimer >= gameSetTime)
        {
            GameOver();
        }
		if (isHit) {
			this.GameClear ();
		} else if (bullet == 0 ) {
			this.GameOver ();
		}

		if (isMiss && bullet > 0) {

			P_shot = Util.InstantiateComponent<shot>(shotprefab,this.transform);
            P_shot.transform.position = shotPosition;
            //ボタンにP_shotをアタッチしなおす
            button.target = P_shot.gameObject ;
			bullet--;
			isMiss = false;
            destroy.shot = P_shot;
		}
	}
}
