using UnityEngine;
using System.Collections;

public class GMSwimmingPlayer : BaseGameController {

	public Rigidbody2D r;
	private GMSwimmingController ctrl;

	public void Init(GMSwimmingController ctrl){
		this.ctrl = ctrl;
	}
		
	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag == "Goal"){
			this.ctrl.Goal.SetActive (false);
			this.ctrl.isClear = true;
		} 
	}



	public void OnPush(){
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );
		this.r.velocity = new Vector2 (0.6f,0.5f);

	}
	
}
