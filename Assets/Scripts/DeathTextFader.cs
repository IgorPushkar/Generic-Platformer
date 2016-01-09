using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathTextFader : MonoBehaviour {

	public float fadeTime;
	public float loadDelay;
	public Text text;
	public LevelManager manager;

	private Color color;

	void Start (){
		color = text.color;
		Invoke ("LoadLevelSelect", loadDelay);
	}
	
	void Update () {
		float alphaChange = Time.deltaTime / fadeTime;
		color.a += alphaChange;
		text.color = color;
	}

	void LoadLevelSelect(){
		manager.LoadLevelSelect ();
	}
}
