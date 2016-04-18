using UnityEngine;
using System.Collections;

public class shot : MonoBehaviour {
	//発射する物体のスピード
    private float strike_speed;
	private bool isTouch = false;
	private int gamespeed = Mgrs.gameMgr.gameSpeed;

	public GMmatoateControllor gm;

	//アタッチしているオブジェクトのRigidbody2d
    private Rigidbody2D rd2d;

	private shot(){
		
	}

	//初期化
	void Start () 
	{
		strike_speed = 280;
		rd2d = GetComponent<Rigidbody2D> ();
	}

	void Update () 
	{
	}

    void Go()
    {
		Mgrs.audioMgr.PlaySE (DEFINE_AUDIO.SE_TYPE.GAME_TAP);

		if (!isTouch) {
			rd2d.AddForce(new Vector2(0,strike_speed));
			isTouch = true;
		}

   		
	}
}
