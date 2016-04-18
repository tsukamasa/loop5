using UnityEngine;
using System.Collections;

public class GMFlappyBirdPlayer : MonoBehaviour {
	public Animator anim;
	public Rigidbody2D r; 

	private GMFlappyBirdController ctrl;

	private bool isFreeze = true;

	public void Init(GMFlappyBirdController ctrl){
		this.ctrl = ctrl;
	}


	void Start () {
		this.r.constraints = RigidbodyConstraints2D.FreezePositionY;
	}

	void Update () {
		if (this.ctrl.waitFlg&&isFreeze){
			isFreeze = false;
			this.r.constraints = RigidbodyConstraints2D. None ;
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag == "Wall"){
			this.anim.SetTrigger ("DieFlg");
		  	this.ctrl.isGameOver = true;
		} 
	}
	

	public void OnPush(){
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );
		this.r.velocity = new Vector2 (0,0.5f);
		this.anim.SetTrigger("JumpFlg");
	}
	
}