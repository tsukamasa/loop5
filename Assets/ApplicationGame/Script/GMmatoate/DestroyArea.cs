using UnityEngine;
using System.Collections;

public class DestroyArea : MonoBehaviour {
	public GMmatoateControllor mtat;
	public shot shot;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D collider){
		mtat.isMiss = true;
		Destroy (shot.gameObject);
//		if (mtat.bullet > 0) {
//			shot shot2 = Util.InstantiateComponent<shot>(createprefab,mtat.transform);
//			shot2.transform.localPosition = yaPostion;
//			Debug.Log("shot作成");
//			mtat.bullet--;
//		}
	}
}
