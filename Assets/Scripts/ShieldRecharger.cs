using UnityEngine;
using System.Collections;

public class ShieldRecharger : MonoBehaviour {

	public GameObject charge;

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			StartCoroutine(RechargeShield (other.gameObject));
		}
	}

	IEnumerator RechargeShield(GameObject player){
		var em = GetComponent<ParticleSystem> ().emission;
		em.enabled = false;
		for (int i = 0; i < 3; i++) {
			GameObject newCharge = Instantiate (charge, transform.position, Quaternion.LookRotation(transform.forward + transform.up)) as GameObject;
//			newCharge.GetComponent<Rigidbody> ().velocity = newCharge.transform.forward * 5f;
			newCharge.GetComponent<ShieldCharge> ().player = player;
			yield return new WaitForSeconds (0.33f);
		}
		GetComponent<Light> ().enabled = false;
	}
}
