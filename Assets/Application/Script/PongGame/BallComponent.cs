using UnityEngine;
using System.Collections;

public class BallComponent : MonoBehaviour {

	public Vector3 scale = Vector3.one*32;
	private PongGamePanelController pongCtrl;

	public Rigidbody rigidbody;
	private const float randomRange1 = 150.0f;
	private const float randomRange2 = 250.0f;

	public void Start() {
		this.rigidbody.AddForce (new Vector3(Random.Range(randomRange1,randomRange2),Random.Range(randomRange1,randomRange2),0));
	}

	public void Init(PongGamePanelController pongCtrl) {
		this.pongCtrl = pongCtrl;
		this.pongCtrl.ballList.AddLast( this );
		this.transform.localScale = this.scale;
	}

	public void OnCollisionEnter(Collision collision){
		if( collision.gameObject.tag == "Goal" ) {
			GoalComponent goal = collision.gameObject.GetComponent<GoalComponent>();
			this.pongCtrl.CollisionEnter( this, goal );
			Destroy( this.gameObject );
			this.pongCtrl.ballList.Remove( this );
		}
	}

}
