using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ToLevelSelectSwitcher : MonoBehaviour {

	void Start() {
		DontDestroyOnLoad (gameObject);
		StartCoroutine (SwitchToLevelSelect ());
	}

	IEnumerator SwitchToLevelSelect(){
		yield return new WaitUntil (() => SceneManager.GetActiveScene ().name == "01a Start");
		GameObject.Find ("Main menu canvas").GetComponent<MenuScreenSwitcher> ().ToLevelSelect ();
		Destroy (gameObject);
	}
}