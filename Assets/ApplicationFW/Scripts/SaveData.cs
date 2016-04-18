using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreSaveData {

	private IDictionary<string, object> stageScore = null;

	public static string TEMP_PATH {
		get {
			
			#if UNITY_EDITOR
			return Application.persistentDataPath + "/";
			#elif UNITY_ANDROID
			return Application.persistentDataPath + "/";
			#else
			return Application.temporaryCachePath + "/";
			#endif
		}
	}

	public static ScoreSaveData instance = new ScoreSaveData();

	private ScoreSaveData() {
		this.JSON_PATH = TEMP_PATH + "score.json";
	}

	private readonly string JSON_PATH;

	private void Init() {
		if (stageScore == null) {
			stageScore = LoadJson ();
		}
	}

	public void SetScore( DEFINE_GAMES.GAME_ID id, int val ) {
		this.SetScore (((int)id).ToString (), val);
	}

	private void SetScore( string key, int val ) {
		this.Init ();
		if (this.GetScore (key) >= val) {
			return;
		}

		if (this.stageScore.ContainsKey (key)) {
			this.stageScore[key] = val.ToString();
		} else {
			this.stageScore.Add( key, val.ToString() );
		}
		this.SaveJson ();
	}

	public int GetScore( DEFINE_GAMES.GAME_ID id ) {
		return this.GetScore ( ((int)id).ToString () );
	}

	private int GetScore( string key ) {
		this.Init ();
		if (this.stageScore.ContainsKey (key)) {
			return int.Parse( this.stageScore[key].ToString() ); 
		}
		return 0;
	}

	private Dictionary<string, object> LoadJson() {
		return this.LoadJson (JSON_PATH);
	}

	private void SaveJson(){
		this.SaveJson (JSON_PATH, this.stageScore);
	}

	#region
	private Dictionary<string, object> LoadJson(string fileUrl){
		Dictionary<string, object> result = new Dictionary<string, object> ();
		if( !System.IO.File.Exists( fileUrl ) ) {
			return result;
		}

		try {
			string jsonString = System.IO.File.ReadAllText( fileUrl );
			object hoge = MiniJSON.Json.Deserialize( jsonString );
			result = MiniJSON.Json.Deserialize( jsonString ) as Dictionary<string, object>;
		}catch (System.Exception e){
		}

		return result;
	}

	private void SaveJson(string fileUrl, IDictionary<string, object> data) {
		try {
			string jsonString = MiniJSON.Json.Serialize (data);
			System.IO.File.WriteAllText( fileUrl, jsonString );
		}catch{
		}
	}
	#endregion

}
