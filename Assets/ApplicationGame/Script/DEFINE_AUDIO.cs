using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DEFINE_AUDIO {


	public enum BGM_TYPE {
		NORMAL_01,
		GAME_01,
		GAME_02,
	}

	public enum SE_TYPE {
		DONE		= 1,
		CANCEL		= 2,
		GAME_START	= 10,
		GAME_OVER,
		GAME_CLEAR,
		GAME_TAP	= 20,
		GAME_NG,
		GAME_JUMP,
	}

	public static IDictionary<BGM_TYPE,string> BGM_PATH = new Dictionary<BGM_TYPE, string>(){
		{BGM_TYPE.NORMAL_01, "Audio/BGM_0001"},
		{BGM_TYPE.GAME_01 , "Audio/GAME_0001"},
		{BGM_TYPE.GAME_02, "Audio/GAME_0002"},
	};
	public static IDictionary<SE_TYPE,string> SE_PATH = new Dictionary<SE_TYPE, string>(){
		{SE_TYPE.DONE, "Audio/SE_DONE"},
		{SE_TYPE.CANCEL, "Audio/SE_CANCEL"},
		{SE_TYPE.GAME_START, "Audio/SE_GAME_START"},
		{SE_TYPE.GAME_OVER, "Audio/SE_GAME_OVER"},
		{SE_TYPE.GAME_CLEAR, "Audio/SE_GAME_CLEAR"},
		{SE_TYPE.GAME_TAP, "Audio/SE_GAME_TAP"},
		{SE_TYPE.GAME_NG, "Audio/SE_GAME_NG"},
		{SE_TYPE.GAME_JUMP, "Audio/SE_GAME_JUMP"},
	};

}
