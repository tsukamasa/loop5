using UnityEngine;
using System.Collections;

public class SeButtonComponent : MonoBehaviour {

	public DEFINE_AUDIO.SE_TYPE seType = DEFINE_AUDIO.SE_TYPE.DONE;

	public void OnClick() {
		Mgrs.audioMgr.PlaySE (this.seType);
	}
}
