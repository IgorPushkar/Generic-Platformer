using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsList : MonoBehaviour {

	const string SPLASH = "00 Splash";
	const string START = "01a Start";
	const string SETTINGS = "01b Settings";
	const string LEVEL_SELECT = "01c Level Select";
	const string WORKSHOP = "01d Workshop";
	const string BASIC_LEVEL_01 = "02a Level 01";
	const string BASIC_LEVEL_02 = "02a Level 02";
	const string BASIC_LEVEL_03 = "02a Level 03";
	const string BASIC_LEVEL_04 = "02a Level 04";
	const string BASIC_LEVEL_05 = "02a Level 05";
	const string BASIC_LEVEL_06 = "02a Level 06";
	const string BASIC_LEVEL_07 = "02a Level 07";
	const string BASIC_LEVEL_08 = "02a Level 08";

	public static string[] GetBasicLevels(){
		string[] levels = new string[8] {
			BASIC_LEVEL_01,
			BASIC_LEVEL_02,
			BASIC_LEVEL_03,
			BASIC_LEVEL_04,
			BASIC_LEVEL_05,
			BASIC_LEVEL_06,
			BASIC_LEVEL_07,
			BASIC_LEVEL_08
		};

		return levels;
	}

	public static string[] GetAllLevels(){
		string[] levels = new string[13] {
			SPLASH,
			START,
			SETTINGS,
			LEVEL_SELECT,
			WORKSHOP,
			BASIC_LEVEL_01,
			BASIC_LEVEL_02,
			BASIC_LEVEL_03,
			BASIC_LEVEL_04,
			BASIC_LEVEL_05,
			BASIC_LEVEL_06,
			BASIC_LEVEL_07,
			BASIC_LEVEL_08
		};

		return levels;
	}
}
