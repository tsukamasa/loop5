using UnityEngine;
using System.Collections;

public static class Mgrs {

	public static SceneManager sceneMgr;

	public static PanelManager pnlMgr {
		get {
			if( sceneMgr == null ) return null;
			return sceneMgr.pnlMgr;
		}
	}

	public static GameManager gameMgr {
		get {
			if( sceneMgr == null ) return null;
			return sceneMgr.gameMgr;
		}
	}
	public static UnityAdsManager adsMgr {
		get {
			if( sceneMgr == null ) return null;
			return sceneMgr.adsMgr;
		}
	}
	public static AppAudioManager audioMgr {
		get {
			if( sceneMgr == null ) return null;
			return sceneMgr.audioMgr;
		}
	}
}
