using UnityEngine;
using System.Collections;

public class GameClearArea : MonoBehaviour {

	public GameObject ball;
	public GMBaseballController baseball;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D cllision)
	{
		//GameObject.Destroy (ball);	
		baseball.GameClear ();
	}
}
