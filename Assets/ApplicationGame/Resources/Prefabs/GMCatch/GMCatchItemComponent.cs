using UnityEngine;
using System.Collections;

public class GMCatchItemComponent : GMButtonCommon {

	public Rigidbody rigidbody;
	private const float randomRange1 = -15.0f;
	private const float randomRange2 = 15.0f;

	public void Start() {
		this.rigidbody.AddForce (new Vector3(Random.Range(randomRange1,randomRange2),Random.Range(randomRange1,randomRange2),0));
	}

	public void Init(DEFINE.COLOR_ID colorId, System.Action<GMButtonCommon> callback) {
		this.SetColor (colorId);
		this.InitCallBack (callback);

		Vector3 position = Vector3.zero;
		position.x = Random.Range (-(ScreenScale.MANUAL_SIZE.x-100f)/2f, (ScreenScale.MANUAL_SIZE.x-100f)/2f);
		position.y = Random.Range (-(ScreenScale.MANUAL_SIZE.y-100f)/2f, (ScreenScale.MANUAL_SIZE.y-100f)/2f);
		this.transform.localPosition = position;
	}

}
