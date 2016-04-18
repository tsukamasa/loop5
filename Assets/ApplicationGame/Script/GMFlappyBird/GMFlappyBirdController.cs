using UnityEngine;
using System.Collections;

public class GMFlappyBirdController : BaseGameController {

	public GMFlappyBirdPlayer player;
	public GameObject button;
	public GameObject backGround;
	public GameObject wall;
	public GameObject ground;
	public bool isGameOver = false;
	public bool isSetResult = false;
	public bool waitFlg = false;

	[SerializeField]
	private Transform parent;
	private Rigidbody2D rigidBodyWall;
	private const float DEFINE_SPEED = -0.8f;
	private float speed = 0f;




	private void Start(){
		this.rigidBodyWall = this.backGround.GetComponent<Rigidbody2D> ();
		this.player.Init (this);
		Generate();
	}

	private float addSpeed{ 
		get {
			return (Mgrs.gameMgr.gameSpeed - 1) * -0.2F; 
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
		float topSetxpos = -300f;
		float bottomSetxpos = -300f;
		for (int i = 0; i < sprites.Length; i++) {
			float topYpos = Random.Range(375f,540f);
			float bottomYpos = Random.Range(-375f,-180f);
			float topDis = Random.Range(80,300);
			float bottomDis = Random.Range(80,300);
			topSetxpos += topDis;
			bottomSetxpos += bottomDis;
			if(i%2 == 0){
				sprites[i].transform.rotation = Quaternion.Euler(0, 0, 0);
				sprites[i].transform.localPosition = new Vector2(topSetxpos, topYpos);
			}else{
				sprites[i].transform.rotation = Quaternion.Euler(0, 0, 180);
				sprites[i].transform.localPosition = new Vector2(bottomSetxpos, bottomYpos);
			}
			sprites[i].enabled=true;
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
			yield return new WaitForSeconds (1.0F);
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
	
}