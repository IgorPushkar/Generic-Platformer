using UnityEngine;
using System.Collections;

public class LigtningWall : MonoBehaviour {

	public Lightning lightning;
	public float damage;

	void Start(){
		InvokeRepeating ("Spawn", 0.000001f, 0.03f);
	}

	void Spawn(){
		Instantiate (lightning, transform.position, Quaternion.identity);
	}

//	void LateUpdate(){
//		if (Time.timeScale > 0) {
//			Instantiate (lightning, transform.position, Quaternion.identity);
//		}
//	}

	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Robot") || other.CompareTag ("Player")) {
			other.gameObject.GetComponentInParent<PlayerController> ().ApplyDamage (damage * Time.deltaTime);
		}
	}
}
