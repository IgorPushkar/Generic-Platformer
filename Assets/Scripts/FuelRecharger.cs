using UnityEngine;
using System.Collections;

public class FuelRecharger : MonoBehaviour {

	public GameObject charge;
	public Transform nozzle;

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			RechargeFuel (other.gameObject);
		}
	}

	void RechargeFuel(GameObject player){
		GameObject newCharge = Instantiate (charge, nozzle.position, nozzle.rotation) as GameObject;
		newCharge.GetComponent<FuelChargeController> ().player = player;
	}
}
