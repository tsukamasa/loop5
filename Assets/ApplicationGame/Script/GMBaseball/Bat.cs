using UnityEngine;
using System.Collections;

public class Bat : MonoBehaviour {
	public GMBaseballController baseball;
    float z;

	void Start () {
        z = 25;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		baseball.GameClear ();
	}

	public void ClickButton(){
		Mgrs.audioMgr.PlaySE (DEFINE_AUDIO.SE_TYPE.GAME_TAP);
		if (gameObject.transform.eulerAngles.z <= 270) {
			StartCoroutine("RotateBat");
		}
	}

	private IEnumerator RotateBat(){
		while (gameObject.transform.eulerAngles.z <= 270) {
			gameObject.transform.Rotate(0,0,z);
			yield return null;
		}
	}
}
