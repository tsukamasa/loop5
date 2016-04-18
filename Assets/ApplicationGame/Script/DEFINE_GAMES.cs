using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DEFINE_GAMES {

	public const string BASE_PATH = "Prefabs/";

	public class GameInfo {
		public readonly GAME_ID id;
		public readonly string prefabPath;
		public readonly string title;
		public readonly string titleFormat;

		public GameInfo( GAME_ID id, string prefabPath, string title, string titleFormat = null){
			this.id = id;
			this.title = title;
			this.titleFormat = titleFormat;
			this.prefabPath = BASE_PATH + prefabPath;
		}

	}

	public enum GAME_ID {
		VARIETY	 	= 0,
		GM_SAMPLE	= 1,
		GM_BASE_BALL,
		GM_MATOATE,
		GM_FLAPPY_BIRD,
		GM_SWIMMING,
		GM_RUNNER,
		GM_THREADING,
		GM_AVOID_CAR,
		GM_AVOID_RAIN,
		GM_SAME_COLOR,
		GM_CATCH_MAIN,
		GM_BRAIN_MATH_1,
		GM_BRAIN_MATH_2,
		GM_TOUCH_THE_NUMBER,
	};

	public static GameInfo[] GAMES = new GameInfo[] {
		// SAMPLE
//		new GameInfo("path", "title"),
//		new GameInfo("GMSample1/GMSample1Main", "Game Clearをタップしろ！"),


		// OTUKA
		new GameInfo(GAME_ID.GM_BASE_BALL,		"GMBaseball/GMBaseball",			"ボールを打て！"),
		new GameInfo(GAME_ID.GM_MATOATE, 		"GMmatoate/GMmatoate",				"3発で的に当てろ！"),


		// YAMAGUCHI
		new GameInfo(GAME_ID.GM_FLAPPY_BIRD,	"GMFlappyBird/GMFlappyBird",		"飛びつづけろ!!"),
		new GameInfo(GAME_ID.GM_SWIMMING, 		"GMSwimming/GMSwimming",			"ダイヤを取れ！"),
		new GameInfo(GAME_ID.GM_RUNNER, 		"GMRunner/GMRunner",				"ジャンプで障害物を避けろ!!"),
		new GameInfo(GAME_ID.GM_THREADING, 		"GMThreading/GMThreading",		"長押しで緑を通れ！"),
//		new GameInfo(GAME_ID.HOGEHOGE, 			"GMBeans/GMBeans",				"title"),
//		new GameInfo(GAME_ID.HOGEHOGE, 			"GMCutBumboo/GMCutBumboo",		"title"),


		// HORII
		new GameInfo(GAME_ID.GM_AVOID_CAR, 		"GMAvoidCar/GMAvoidCar",			"障害物をタップで避けろ!!"),
		new GameInfo(GAME_ID.GM_AVOID_RAIN, 	"GMAvoidRain/GMAvoidRain",			"タップで避けろ!!"),
		new GameInfo(GAME_ID.GM_SAME_COLOR, 	"GMSameColor/GMSameColor",			"タップで色を合わせろ!!"),


		// NAKANISHI
		new GameInfo(GAME_ID.GM_CATCH_MAIN, 	"GMCatch/GMCatchMain", 			"◯◯を捕まえろ！", "[{0}]{1}[{2}]を捕まえろ！"),
		new GameInfo(GAME_ID.GM_BRAIN_MATH_1, 	"GMBrainMath1/GMBrainMath1Main",	"足して◯◯にしろ！", "足して{0}にしろ！"),
//		new GameInfo(GAME_ID.GM_BRAIN_MATH_2, "GMBrainMath2/GMBrainMath2Main",	"数を順にタップせよ!!"),


		// SOUDA
//		new GameInfo(GAME_ID.HOGEHOGE, "GMRaiseFlag/GMRaiseFlag", "title"),
		new GameInfo(GAME_ID.GM_TOUCH_THE_NUMBER, "GMTouchTheNumber/GMTouchTheNumberPanel",	"1から順番にタッチしろ！"),
	};
}
