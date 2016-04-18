using UnityEngine;
using System.Collections;

public class GMSwimmingController : BaseGameController {

	public GMSwimmingPlayer GMSplayer;
	public GameObject player;
	public GameObject Button;
	public GameObject Goal;
	public bool isClear = false;
	public bool isSetResult = false;

	private Rigidbody2D rigidGoal;
	private bool isMoveUp = true;



	private void Start () {
		this.rigidGoal = this.Goal.GetComponent<Rigidbody2D> ();
		this.GMSplayer.Init (this);
	}

	private float moveSpeed{ 
		get {
			float sign = isMoveUp ? 1 : -1;
			float speed = (Mgrs.gameMgr.gameSpeed - 1) * 0.1F;
			return  sign * speed;
		}
	}
	
	protected override void Update () {
		if (Goal.transform.localPosition.y > 300.0f) {
			isMoveUp = false;
		}
		if (Goal.transform.localPosition.y < -200.0f) {
			isMoveUp = true;
		}

		this.rigidGoal.velocity = new Vector2 (0f, moveSpeed);
		if (isClear) {
			StartCoroutine(toGameClear());
			return;
		}
		if (gameTimer >= gameSetTime) {
			if (!isClear) {
				StartCoroutine(toGameOver());
			}
			else {
				
			}
		}
	}
	
	protected override void GameStart () {
		this.isMoveUp = (Random.Range (0, 2) == 1);
		isClear = false; 
		base.GameStart();
	}
	public void Push (){
		GMSplayer.OnPush ();
	}



	IEnumerator toGameClear()
	{
		if (!isSetResult) {
			isSetResult = true;
			yield return new WaitForSeconds (1.0F);
			base.GameClear ();
		}
	}
	
	IEnumerator toGameOver()
	{
		if (!isSetResult) {
			isSetResult = true;
			yield return new WaitForSeconds (0.5F);
			base.GameOver ();
		}
	}
}