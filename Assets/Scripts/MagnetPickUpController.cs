using UnityEngine;
using System.Collections;

public class MagnetPickUpController : MonoBehaviour {

	public GameObject wave;

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			Instantiate (wave, transform.position, Quaternion.identity);
		}
		Destroy (gameObject, 2f);
	}
}
