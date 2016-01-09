using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {


	private Vector3 rotation;
	// Use this for initialization
	void Start () {
		rotation = new Vector3 (0.67f, 0.39f, 0.21f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale > 0) {
			transform.Rotate (rotation);
		}
	}
}
