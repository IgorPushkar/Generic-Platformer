using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour {


	public Toggle musicToggle;
	public Slider musicSlider;

	public Toggle GetMusicToggle(){
		return musicToggle;
	}

	public Slider GetMusicSlider(){
		return musicSlider;
	}
}
