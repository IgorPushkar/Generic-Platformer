using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] clips;

	private AudioSource source;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		source = GetComponent<AudioSource> ();
		source.volume = PlayerPrefsManager.GetMasterVolume ();
		source.enabled = PlayerPrefsManager.GetMusic ();
	}

	void OnLevelWasLoaded(int level){
		if (clips [level] && !clips [level].Equals (source.clip)) {
			source.clip = clips [level];
			if(source.enabled){
				source.Play ();
			}
		}
	}

	public void ChangeVolume(float volume){
		source.volume = volume;
	}

	public void ToggleMusic(){
		source.enabled = !source.enabled;
	}

	public void ToggleMusic(bool flag){
		source.enabled = flag;
	}
}
