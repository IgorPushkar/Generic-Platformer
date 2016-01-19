using UnityEngine;
using System.Collections;

public class UISoundPlayer : MonoBehaviour {

	private AudioSource audioSource;
	private AudioClip audioClip;

	void Start(){
		DontDestroyOnLoad (gameObject);
		audioSource = GetComponent<AudioSource> ();
		audioClip = audioSource.clip;
	}

	public void PlayUISound(){
		audioSource.PlayOneShot (audioClip);
	}
}
