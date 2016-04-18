using UnityEngine;
using System.Collections;

public class GMThreadingYarnController : MonoBehaviour {

	private GMThreadingController ctrl;
	private bool isTouch = false;
	private float force = 0;
//	private float speed = 1.0f;

	public void Init(GMThreadingController ctrl){
		this.ctrl = ctrl;
	}
		
	void Update () {
		if (this.ctrl.isWait && !Time.timeScale.Equals(0)) {
			force += 0.0005f;
			if (Mgrs.gameMgr.gameSpeed > 4) {
				this.transform.position += new Vector3 (0.013f, 0f, 0f);
			}
			else {
				this.transform.position += new Vector3 (0.01f + Mgrs.gameMgr.gameSpeed / 1000f, 0f, 0f);
			}
			if (isTouch) {
				this.transform.position += new Vector3 (0f, 0.01f +Mgrs.gameMgr.gameSpeed/800f+ force, 0f);
			} else {
				this.transform.position -= new Vector3 (0f, 0.01f +Mgrs.gameMgr.gameSpeed/800f +force, 0f);

			}
		}
	}
		
	void OnCollisionEnter2D(Collision2D col){
		Destroy (this.gameObject);
		if (!this.ctrl.isGameClear) {
			if (col.gameObject.tag == "Wall") {
				this.ctrl.isGameOver = true;
			}
		}
	}


	void OnTriggerExit2D(Collider2D other){
		this.ctrl.count++;
		if (this.ctrl.count == this.ctrl.setObj) {
			this.ctrl.isGameClear = true;
		}
	}

	public void OnPress(){
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );
		isTouch = true;
		force = 0f;
	}

	public void OnRelease(){
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );
		isTouch = false;
		force = 0f;
	}
}
