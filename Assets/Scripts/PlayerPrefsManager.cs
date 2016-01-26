using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string MUSIC_KEY = "music";
	const string SCORE_KEY = "score";
	const string MAGNET_KEY = "magnet";
	const string DRONE_KEY = "drone";
	const string LEVEL_KEY = "level_unlocked_";
	const string LEVEL_PICKUPS = "level_pickups_";

	#region Master_volume
	public static void SetMasterVolume (float volume){
		if (volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError ("Master volume is out of range");
		}
	}

	public static float GetMasterVolume (){
		if (!PlayerPrefs.HasKey (MASTER_VOLUME_KEY)) {
			SetMasterVolume (0.8f);
		} 
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}
	#endregion

	#region Music
	public static void SetMusic (bool flag){
		int enabled = flag ? 1 : 0;
		PlayerPrefs.SetInt (MUSIC_KEY, enabled); //using 1 for true
	}

	public static bool GetMusic (){
		if (!PlayerPrefs.HasKey (MUSIC_KEY)) {
			SetMusic (true);
		} 
		bool flag = PlayerPrefs.GetInt (MUSIC_KEY) == 1 ? true : false;
		return flag;
	}
	#endregion

	#region Money
	public static void SetScore (int money){
		if (money >= 0 && money <= int.MaxValue) {
			PlayerPrefs.SetInt (SCORE_KEY, money);
		} else {
			Debug.LogError ("Money key is out of range");
		}
	}

	public static int GetScore (){
		if (!PlayerPrefs.HasKey (SCORE_KEY)) {
			SetScore (0);
		} 
		return PlayerPrefs.GetInt(SCORE_KEY);
	}
	#endregion

	#region Magnet
	public static void SetMagnet (bool flag){
		int enabled = flag ? 1 : 0;
		PlayerPrefs.SetInt (MAGNET_KEY, enabled); //using 1 for true
	}

	public static bool GetMagnet (){
		if (!PlayerPrefs.HasKey (MAGNET_KEY)) {
			SetMagnet (false);
		} 
		bool flag = PlayerPrefs.GetInt (MAGNET_KEY) == 1 ? true : false;
		return flag;
	}
	#endregion

	#region Drone
	public static void SetDrone (bool flag){
		int enabled = flag ? 1 : 0;
		PlayerPrefs.SetInt (DRONE_KEY, enabled); //using 1 for true
	}

	public static bool GetDrone (){
		if (!PlayerPrefs.HasKey (DRONE_KEY)) {
			SetDrone (false);
		} 
		bool flag = PlayerPrefs.GetInt (DRONE_KEY) == 1 ? true : false;
		return flag;
	}
	#endregion

	#region Levels
	public static void SetLevelState (string level){
		int i;
		if (int.TryParse(level, out i)) {
			PlayerPrefs.SetInt (LEVEL_KEY + level, 1);
		} else {
			Debug.LogError ("Level is in wrong format");
		}
	}

	public static bool GetLevelState (string level){
		int i;
		if(!int.TryParse(level, out i)){
			Debug.LogError ("Level is in wrong format");
		}
		if (!PlayerPrefs.HasKey (LEVEL_KEY + level) && level == "01") {
			SetLevelState (level);
		} 
		return PlayerPrefs.GetInt (LEVEL_KEY + level) == 1 ? true : false;
	}
	#endregion

	#region level_pickups
	public static void SetLevelPickups (string level, string itemsData){
		int i;
		if (int.TryParse(level, out i)) {
			PlayerPrefs.SetString (LEVEL_PICKUPS + level, itemsData);
		} else {
			Debug.LogError ("Level is in wrong format");
		}
	}

	public static string GetLevelPickups (string level){
		int i;
		if(!int.TryParse(level, out i)){
			Debug.LogError ("Level is in wrong format");
		}
		if (!PlayerPrefs.HasKey (LEVEL_PICKUPS + level)) {
			return "";
		} 
		return PlayerPrefs.GetString (LEVEL_PICKUPS + level);
	}
	#endregion
}
