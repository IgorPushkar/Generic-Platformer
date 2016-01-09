using UnityEngine;
using System.Collections;

public class FireFieldController : MonoBehaviour {

	public float damage;

	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Robot") || other.CompareTag ("Player")) {
			other.gameObject.GetComponentInParent<PlayerController> ().ApplyDamage (damage * Time.deltaTime);
		}
	}
}
