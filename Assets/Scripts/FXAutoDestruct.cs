using UnityEngine;
using System.Collections;

public class FXAutoDestruct : MonoBehaviour {

	public float lifetime = -1;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
	}
}
