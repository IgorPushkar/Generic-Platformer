using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour {

	public float fadeTime;
	public LevelManager manager;
	public Image image;

	private Color color = Color.black;
	private float activateTime;

	// Use this for initialization
	void Start () {
		activateTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - activateTime < fadeTime) {
			float alphaChange = Time.deltaTime / fadeTime;
			color.a -= alphaChange;
			image.color = color;
		} else {
			image.enabled = false;
		}
	}
}
