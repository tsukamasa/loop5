using UnityEngine;
using System.Collections;

public class NodeDestoryArea : MonoBehaviour {
	public GMMusicController music;
	public GameObject sprite;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}


	//画面エリア外の処理　画面外に行ったオブジェクトを消してGameOver処理を実行
	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log ("エリア外");
//		GameObject.Destroy (sprite);
		music.GameOver();
	}
}