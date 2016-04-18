using UnityEngine;
using System.Collections;

public class GMRunnerController : BaseGameController {


	public GMRunnerPlayer player;
	public GameObject Button;
	public GameObject backGround;
	public GameObject wall;
	public GameObject ground;
	public bool isGameOver = false;
	public bool isSetResult = false;
	public bool waitFlg = false;


	
	[SerializeField]
	private Transform parent;
	private Rigidbody2D rigidBodyWall;
	private const float DEFINE_SPEED = -1.6f;
	private float speed = 0;

	
	
	private void Start(){
		this.rigidBodyWall = this.backGround.GetComponent<Rigidbody2D> ();
		this.player.Init (this);
		Generate();
	}

	private float addSpeed{ 
		get {
			return (Mgrs.gameMgr.gameSpeed - 1) * -0.1F; 
		}
	}

	protected override void Update () {
		if (isGameOver) {
			this.rigidBodyWall.velocity = Vector2.zero;
			StartCoroutine (ToGameOver());
			return;
		}

		if(!waitFlg){
			StartCoroutine (Wait ());
		}else{
			speed = addSpeed + DEFINE_SPEED;
			this.rigidBodyWall.velocity = new Vector2 (speed, 0f);
		}

		if (gameTimer >= gameSetTime) {
			if(!isGameOver){
				StartCoroutine(ToGameClear());
			}
		}

	}
	
	protected override void GameStart () {
		isGameOver = false;
		base.GameStart();
	}
	
	public UISprite[] sprites;
	void Generate()
	{
		float bottomSetxpos = 500f;
		for (int i = 0; i < sprites.Length; i++) {
			float bottomYpos = Random.Range (-375f, -290f);
			int judge = Random.Range (1, 3);
			float dis = 800f;
			switch (judge) {
			case 1:
				dis = 800f;
				break;
			case 2:
				dis = 900f;
				break;
			case 3:
				dis = 1000f;
				break;
			}
			bottomSetxpos += dis;
			sprites [i].transform.rotation = Quaternion.Euler (0, 0, 180);
			sprites [i].transform.localPosition = new Vector2 (bottomSetxpos, bottomYpos);
			if (i < 5) {
				sprites [i].enabled = true;
			} else {
				StartCoroutine (DelayEnableSprites (i));
			}
		}
	}
	

	public void Push (){
		player.OnPush ();
	}
	IEnumerator ToGameOver()
	{
		if (!isSetResult) {
			isSetResult = true;
			yield return new WaitForSeconds (0.5F);
			base.GameOver ();
		}
	}
	
	IEnumerator ToGameClear()
	{
		if (!isSetResult) {
			isSetResult = true;
			yield return new WaitForSeconds (0.3F);
			base.GameClear ();
		}
	}
	
	IEnumerator Wait()
	{
		if (!waitFlg) {
			yield return new WaitForSeconds (0.5F);
			waitFlg = true;
		}
	}

	IEnumerator DelayEnableSprites(int i)
	{
			yield return new WaitForSeconds (3.0F);
			sprites[i].enabled = true;
	}
	
}