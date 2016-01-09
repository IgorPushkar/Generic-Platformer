using UnityEngine;
using System.Collections;

public class StartSectorScript : MonoBehaviour {

	private Animation anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
	}
	
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			anim.Play ();
		}
	}
}
