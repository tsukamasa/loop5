using UnityEngine;
using System.Collections;

public class GMRunnerPlayer : MonoBehaviour {
	
	public Rigidbody2D r; 
	
	private GMRunnerController ctrl;
	
	private bool isFreeze = true;
	private int cnt = 0;
	private float addPow = 0f;
	
	public void Init(GMRunnerController ctrl){
		this.ctrl = ctrl;
	}

	void Start () {
		this.r.constraints = RigidbodyConstraints2D.FreezePositionX;
	}

	void Update () {
		if (this.ctrl.waitFlg&&isFreeze){
			isFreeze = false;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		cnt = 0;
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Wall"){
			this.ctrl.isGameOver = true;
		} 
	}
	

	public void OnPush(){
		if(cnt < 2){
			addPow = 5.0f - cnt * 2.0f;
			Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );
			this.r.velocity = new Vector2 (0, addPow);
			cnt++;
		}
	}
	
}