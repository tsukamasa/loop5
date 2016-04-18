using UnityEngine;
using System.Collections;

public class GMThreadingController : BaseGameController {

	public GMThreadingYarnController yarn;
	public GameObject button;
	public bool isGameOver = false;
	public bool isGameClear = false;
	public bool isSetResult = false;
	public bool isWait = false;
	public int count = 0;
	public int setObj = 0;

	[SerializeField]
	private Transform parent;

	private void Start () {
		this.yarn.Init (this);
	}

	protected override void GameStart () {
		isGameOver = false;
		setNeddles();
		base.GameStart();
	}

	public GameObject[] objects;
	void setNeddles(){
		float setXpos = -360f;
		if (Mgrs.gameMgr.gameSpeed > 4) {
			setObj = Random.Range (3, objects.Length);
		} else if (Mgrs.gameMgr.gameSpeed > 2) {
			setObj = Random.Range (2, 4);
		} else {
			setObj = Random.Range (1, 3);
		}
		for (int i = 0; i < objects.Length; i++) {
			float Ypos = Random.Range (-130f, 160f);
			int judge = Random.Range (1, 3);
			float dis = 100f;
			switch (judge) {
			case 1:
				dis = 150f;
				break;
			case 2:
				dis = 200f;
				break;
			case 3:
				dis = 250f;
				break;
			}
			setXpos += dis;
			objects [i].transform.localPosition = new Vector2 (setXpos, Ypos);
			if(i<setObj){
				objects[i].SetActive(true);
			}
		}
	}

	protected override void Update () {
		if (isGameOver) {
			StartCoroutine(toGameOver());
		}
		else{
			if(!isWait){
				StartCoroutine(wait());
			}
		}
		if (gameTimer >= gameSetTime || isGameClear) {
			if(!isGameOver){
				StartCoroutine(toGameClear());
			}
		}
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

	IEnumerator wait()
	{
		if (!isWait) {
			yield return new WaitForSeconds (0.5F);
			isWait = true;
		}
	}

	public void press (){
		yarn.OnPress ();
	}
	public void release (){
		yarn.OnRelease ();
	}

}
