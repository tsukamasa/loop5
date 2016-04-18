using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityAdsManager : MonoBehaviour {
#if UNITY_PRO_LICENSE
	private const string APP_ID_ANDROID = "1022069";
	private const string APP_ID_IOS = "1022070";

	private System.Action callback;

	private bool isAdsShowing = false;
	private void Update() {

		if (this.isAdsShowing.Equals (Advertisement.isShowing)) {
			return;
		}
		this.isAdsShowing = Advertisement.isShowing;
		//広告閲覧終了
		if (!Advertisement.isShowing) {
			if( this.callback != null ) this.callback();
			this.callback = null;
		}
	}

	private void Awake() {
		this.InitAds ();
	}

	private void InitAds() {
		bool isDebug = true;
		#if IS_RELEASE
		isDebug = false;
		#endif
		string appId = APP_ID_ANDROID;
		#if UNITY_IOS
		appId = APP_ID_IOS;
		#endif
		Advertisement.Initialize (appId, isDebug);
	}
#endif
	public void Show(System.Action successCallback, System.Action errCallback){
		#if !UNITY_PRO_LICENSE
		if (successCallback != null)
			successCallback ();
		#else
		if (Advertisement.isReady ()) {
			Advertisement.Show ();
			this.callback = successCallback;
		} else {
			if( errCallback != null ) errCallback();
		}
		#endif
	}
}
