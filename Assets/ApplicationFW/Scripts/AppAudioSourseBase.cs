using UnityEngine;
using System.Collections;

public class AppAudioSourseBase : MonoBehaviour {

//	public AudioSource source;
	public bool isSingle;
	public bool isCheckPlayRange = false;
	public string currentName { get; protected set; }

	//対象の箇所のみ再生する為の設定値
	public float fadeTime = 1f;
	protected float playStartTime = 0;
	protected float playEndTime = float.MaxValue;


	private float _volumeBase = 1f;
	public float volumeBase {
		get{
			return this._volumeBase;
		}
		set {
			this._volumeBase = value;
			this.volume = value;
		}
	}
	public virtual float volume { get; set;}
	public virtual bool isPlaying { get; set; }
	public virtual float time { get; set; }

	protected void PlayBase( string name ) {
		this.currentName = name;
	}

	protected void StopBase() {
		this.currentName = string.Empty;
	}

	protected virtual float baseVolume{
		get{
			return 1f;
		}
	}


}
