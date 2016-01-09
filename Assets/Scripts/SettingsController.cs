using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsController : MonoBehaviour {

	public LevelManager levelManager;

	private Toggle musicToggle;
	private Slider musicSlider;
	private MusicManager musicManager;

	// Use this for initialization
	void Start () { 
		musicToggle = FindObjectOfType<SettingsManager> ().GetMusicToggle ();
		musicSlider = FindObjectOfType<SettingsManager> ().GetMusicSlider ();
		musicManager = FindObjectOfType<MusicManager> ();
		musicSlider.value = PlayerPrefsManager.GetMasterVolume ();
		musicToggle.isOn = PlayerPrefsManager.GetMusic ();
	}
	
	// Update is called once per frame
	void Update () {
		musicManager.ToggleMusic(musicToggle.isOn);
		musicManager.ChangeVolume(musicSlider.value);
	
	}

	public void Defaults(){
		musicSlider.value = 0.8f;
		musicToggle.isOn = true;
	}

	public void SaveAndExit(){
		PlayerPrefsManager.SetMasterVolume (musicSlider.value);
		PlayerPrefsManager.SetMusic(musicToggle.isOn);
		levelManager.LoadStartLevel ();
	}

	public void Save(){
		PlayerPrefsManager.SetMasterVolume (musicSlider.value);
		PlayerPrefsManager.SetMusic(musicToggle.isOn);
	}

	public void ResetGame(){
		PlayerPrefs.DeleteAll ();
	}

	public void UnlockAllLevels(){
		for(int i = 0; i <10; i++){
			PlayerPrefsManager.SetBasicLevelState ("0" + (i + 1).ToString ());
		}
		PlayerPrefsManager.SetBasicLevelState ("10");
	}

	public void SetDrone(){
		PlayerPrefsManager.SetDrone (true);
	}

	public void SetMagnet(){
		PlayerPrefsManager.SetMagnet (true);
	}

	public void AddParts(){
		PlayerPrefsManager.SetScore (PlayerPrefsManager.GetScore () + 100);
	}
}
