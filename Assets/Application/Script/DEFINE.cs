using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DEFINE {

	public static bool isDebug {
		get {
#if APP_DEBUG
			return false;
#endif
			return true;
		}
	}

	public enum COLOR_ID : int {
		ORANGE = 0,
		RED,
		GREEN,
		WHITE,
		BLACK,
	}

	public class ColorInfo{
		public readonly string name;
		public readonly Color color;
		public readonly string color16;

		public ColorInfo(string name,Color color, string color16){
			this.name = name;
			this.color = color;
			this.color16 = color16;
		}
	}

	public static Dictionary<COLOR_ID, ColorInfo> COLORS = new Dictionary<COLOR_ID, ColorInfo> {
		{ COLOR_ID.ORANGE, new ColorInfo( "オレンジ", new Color(238f/256f,170f/256f,102f/256f), "EEAA66") },
		{ COLOR_ID.RED,    new ColorInfo( "赤", new Color(238f/256f,109f/256f,102f/256f), "EE6D66") },
		{ COLOR_ID.GREEN,  new ColorInfo( "緑", new Color(102f/256f,238f/256f,120f/256f), "66EE78") },
		{ COLOR_ID.WHITE,  new ColorInfo( "白", new Color(252f/256f,253f/256f,254f/256f), "FCFDFE") },
		{ COLOR_ID.BLACK,  new ColorInfo( "黒", new Color( 43f/256f, 51f/256f, 60f/256f), "2B333C") },
	};

	/// <summary>
	/// スピードが上がるlv単位
	/// </summary>
	public const int SPEED_STEP_LV = 5;
	/// <summary>
	/// スピードレベルの上限
	/// </summary>
	public const int SPEED_MAX = 5;

	public static Color[] SPPED_COLORS = new Color[]{
		new Color (255f / 255f, 250f / 255f, 205f / 255f),
		new Color (43f/256f, 51f/256f, 60f/256f)
	};

	public static Color SPEED_LV_COLOR_MIN = new Color (255f/256f, 243f/256f, 219f/256f);
	public static Color SPEED_LV_COLOR_MAX = new Color (43f/256f, 51f/256f, 60f/256f);

	/// <summary>
	/// ハート回数
	/// </summary>
	public static int HEART_COUNT = 3;

	/// <summary>
	/// 動画広告回数
	/// </summary>
	public static int ADS_HEART_HEEL = 1;

	/// <summary>
	/// 動画広告回数
	/// </summary>
	public static int ADS_COUNT = 1;


	public const string PREFAB_PATH_TITLE_PANEL = "Prefabs/Title/TitlePanel";
	public const string PREFAB_PATH_GAMEOVER_PANEL = "Prefabs/GameOver/GameOverPanel";
	
	public static string[] GAMES = new string[]{
		"Prefabs/GMSample1/GMSample1Main",
	};

	public const string STORE_URL = "[URL]";



	public const int GAME_MATCH_POINT = 11;
	public const string PREFAB_PATH_PONG_GAME_PANEL = "Prefabs/PongGame/PongGamePanel";
	public const string PREFAB_PATH_BALL = "Prefabs/PongGame/ball";
	
	public const string PREFAB_PATH_GMLIST = "Prefabs/GMList/GMListPanel";

	public const string FORMAT_BEST_RECORD = "最高到達Lv.{0}";

}
