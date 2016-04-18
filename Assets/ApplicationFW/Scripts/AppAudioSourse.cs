using UnityEngine;
using System.Collections;

public class AppAudioSourse : AppAudioSourseBase {

	public AudioSource source;


	protected override float baseVolume {
		get {
			return 1f;
		}
	}

	public override float volume {
		get{ return this.source.volume; }
		set{ this.source.volume = value; }
	}

	public override bool isPlaying {
		get { return this.source.isPlaying; }
	}

	public void Stop() {
		source.Stop();
		this.StopBase();
	}

	public void Play( string filePath ) {
		if( this.source.loop && filePath.Equals( this.currentName ) ){
			return;
		}
		
		AudioClip audioClip = Resources.Load( filePath ) as AudioClip;
		if( audioClip != null ) {
			this.PlayAudioClip (filePath, audioClip);
		}

	}

	private void PlayAudioClip( string filePath, AudioClip audioClip ){
		this.source.Stop();
		this.source.clip = audioClip;
		this.source.Play();
		this.PlayBase( filePath );
	}

}
