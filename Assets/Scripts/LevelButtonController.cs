using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class LevelButtonController : MonoBehaviour {

	public LevelManager levelManager;
	public GameObject button;
	public GameObject grid;
	public GameObject loadingScreen;
	public Image cogwheel;

	// Use this for initialization
	void Start () {
		string[] levels = LevelsList.GetBasicLevels ();

		for (int i = 0; i < levels.Length; i++) {
			GameObject obj = Instantiate (button, Vector3.zero, Quaternion.identity) as GameObject;
			obj.transform.SetParent (grid.transform, false);
			Text text = obj.GetComponentInChildren<Text> ();
			int levelNumber = i + 1;
			string levelString = levelNumber < 10 ? "0" + levelNumber.ToString () : levelNumber.ToString ();
			text.text = "Level " + levelNumber.ToString();
			string level = "02a Level " + levelString;
			Button newButton = obj.GetComponent<Button> ();
			newButton.onClick.AddListener (() => StartLevel (level));
			newButton.interactable = PlayerPrefsManager.GetBasicLevelState (levelString);
		}
	}
//		text = GetComponentInChildren<Text> ();
//		int level = 0;
//		int.TryParse (SceneManager.GetActiveScene ().name.ToString ().Replace ("02a Level ", ""), out level);
//		string levelText = "Level " + level.ToString();
//		text.text = levelText;

	void StartLevel(string level){
		loadingScreen.SetActive (true);
		StartCoroutine (LoadingScreenAnimation(level));
	}

	IEnumerator LoadingScreenAnimation(string level){
		AsyncOperation loading = levelManager.LoadLevelAsync(level);
		while (!loading.isDone){
			cogwheel.fillAmount = loading.progress;
			yield return null;
		}
	}
}
