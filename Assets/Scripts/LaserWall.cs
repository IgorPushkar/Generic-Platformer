using UnityEngine;
using System.Collections;

public class LaserWall : MonoBehaviour {

	public GameObject sparks;
	public float damage;

	private LineRenderer line;
	private BoxCollider box;
	private float maxDistance;
	private Vector3 initialPoint;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer> ();
		box = GetComponent<BoxCollider> ();
		Initialize ();
		line.SetPosition(0, transform.position);
		Vector3 newBoxCenter = new Vector3(0, -maxDistance * 2f, 0);
		box.center += newBoxCenter;
		Vector3 newBoxSize = new Vector3 (box.size.x ,maxDistance * 4, box.size.z);
		box.size = newBoxSize;

	}

	void Update(){
		line.material.mainTextureOffset = new Vector2 (-0.5f * Time.time, 0);
	}

	void OnTriggerEnter(Collider other){
//		GetComponent<AudioSource> ().Play ();
	}

	void OnTriggerStay (Collider other){
		if(other.CompareTag("Drone")) return;
		LaserHit (other);
	}

	void OnTriggerExit(Collider other){
		ResetLaser();
//		GetComponent<AudioSource> ().Stop();
	}

	void LaserHit (Collider other){
		GameObject spark;
		RaycastHit hit;
		other.Raycast (new Ray(transform.position, -transform.up), out hit, maxDistance);
		switch (other.tag) {
		case "Missile":
			spark = Instantiate (sparks, hit.point, Quaternion.identity) as GameObject;
			Destroy (spark, 1.5f);
			other.gameObject.GetComponent<MissileController> ().Terminate ();
			break;
		case "Shield":
		case "Robot":
		case "Player":
			GameObject player = other.gameObject;
			if (hit.point != Vector3.zero)
				line.SetPosition (1, hit.point);
			else
				line.SetPosition (1, initialPoint);
			spark = Instantiate (sparks, hit.point, Quaternion.identity) as GameObject;
			Destroy (spark, 1.5f);
			PlayerController controller;
			if(controller = player.GetComponentInParent<PlayerController> ()){
					controller.ApplyDamage (damage * Time.deltaTime);
			}
			break;
		default:
			line.SetPosition (1, initialPoint);
			break;
		}
	}

	void Initialize(){
		RaycastHit hit;
		Physics.Raycast (transform.position, -transform.up, out hit);
		initialPoint = hit.point;
		maxDistance = Vector3.Distance (transform.position, hit.point);
		ResetLaser ();
	}

	void ResetLaser(){
		line.SetPosition (1, initialPoint);
	}
}
