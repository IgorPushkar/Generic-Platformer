using UnityEngine;
using System.Collections;

public class FuelChargeController : MonoBehaviour {

	public float speed;
	public GameObject player;
	public float gainAmmount;

	private bool active;
	private Transform target;
	private TrailRenderer trail;

	void Start () {
		trail = GetComponent<TrailRenderer> ();
		transform.localScale = Vector3.zero;
		active = false;
		foreach (Transform part in player.transform) {
			if (part.name == "Body5") {
				foreach (Transform point in part.transform) {
					if (point.name == "FX") {
						target = point.transform;
						break;
					}
				}
				break;
			}
		}
	}
		
	void Update () {
		if (Time.timeScale > 0) {
			if (player) {
				if (active) {
					float distance = Vector3.Distance (transform.position, target.position);
					trail.time = Mathf.Clamp (distance * 0.5f, 0f, 0.5f);
					transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
					transform.localScale = Vector3.one * Mathf.Clamp (distance * 0.25f, 0.25f, 1f);
				} else {
					transform.localScale += Vector3.one * Time.deltaTime * 2;
					if (transform.localScale.sqrMagnitude >= Vector3.one.sqrMagnitude) {
						active = true;
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			player.GetComponent<PlayerController> ().ApplyGain ("Fuel", gainAmmount);
			gainAmmount = 0f;
			trail.enabled = false;
			var em = GetComponent<ParticleSystem>().emission;
			em.enabled = false;
			Destroy (gameObject, 1f);	
		}
	}
}

