using UnityEngine;
using System.Collections;

public class BallDestroy: MonoBehaviour {
	public GMBaseballController baseball;
	public GameObject ball;

	void Start () {
	}

	void Update () {
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
		Destroy (ball);
		baseball.flag = true;
    }
}
