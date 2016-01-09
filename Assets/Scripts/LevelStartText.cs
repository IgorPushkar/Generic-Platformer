using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelStartText : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {
		int level = 0;
		int.TryParse (SceneManager.GetActiveScene ().name.ToString ().Replace ("02a Level ", ""), out level);
		string levelText = "Level " + level.ToString();
		text.text = levelText;
	}

	void Update(){
		if (Time.timeSinceLevelLoad >= 3) {
			text.enabled = false;
		} else if (Time.timeSinceLevelLoad >= 1) {
			Color color = text.color;
			color.a = 1f - (Time.timeSinceLevelLoad - 1f) / 2f;
			text.color = color;
		}
	}
}
