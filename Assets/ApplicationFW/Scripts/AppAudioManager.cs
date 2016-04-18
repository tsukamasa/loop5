using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppAudioManager : MonoBehaviour {

	public AppAudioSourse sourceBGM;
	public AppAudioSourse sourceSE;

	public void InitValume() {
		this.sourceBGM.volumeBase = 1f;
		this.sourceSE.volumeBase = 1f;
	}

	#region BGM

	public void PlayBGM( DEFINE_AUDIO.BGM_TYPE bgmType ){
		this.PlayBGM (DEFINE_AUDIO.BGM_PATH [bgmType]);
	}
	private void PlayBGM( string path ) {
		this.sourceBGM.Play( path );
	}
	public void StopBGM() {
		this.sourceBGM.Stop();
	}
	#endregion

	#region BGM
	public void PlaySE( DEFINE_AUDIO.SE_TYPE seType ){
		this.PlaySE (DEFINE_AUDIO.SE_PATH [seType]);
	}
	private void PlaySE( string path ) {
		this.sourceSE.Play( path );
	}
	public void StopSE() {
		this.sourceSE.Stop();
	}
	#endregion
}
