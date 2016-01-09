using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroneController : MonoBehaviour {

	//BEAM VARIABLES
	public float fireRate;
	public float shotDistance;

	private float nextFire;
	private bool inDanger; //does recently shooted
	private LineRenderer lineRend;
	private RaycastHit hit;

	public float rotationSpeed;
	public float activeTime;
	public float followMultiplyer;
	public Material deadMaterial;

	private float followSpeed;
	private bool activated;
	private MeshRenderer meshRend; //renderer to change material on death
	private GameObject player;
	private Vector3 offset;
	private Quaternion newRot;
	private BoxCollider bCollider; //box collider for missile detection
	private Rigidbody rb;

	void Start () {
		bCollider = GetComponent<BoxCollider> ();
		meshRend = GetComponentInChildren<MeshRenderer> ();
		rb = GetComponent<Rigidbody> ();
		offset = new Vector3 (1.5f, 0.25f, 0f);
		newRot = new Quaternion (Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
		activated = false;
		//initialize beam
		lineRend = GetComponent<LineRenderer> ();
		lineRend.enabled = false;
		lineRend.SetPosition (0, transform.position);
		lineRend.SetPosition (1, transform.position);
		inDanger = false;
		nextFire = 0f;
	}

	void Update () {
		//if player touched drone and is still alive move to player position, else deactivate
		if (activated) {
			if (player) {
				//PingPong position along Y axis while moving
				Vector3 newPos = new Vector3 (0, Mathf.PingPong (Time.time * 0.1f, 0.1f), 0) + player.transform.position + offset;
				transform.rotation = Quaternion.Lerp (transform.rotation, newRot, rotationSpeed * Time.deltaTime);
				//adjust follow speed based on distance to player
				followSpeed = Mathf.Clamp (Vector3.Distance (transform.position, newPos) * Mathf.Sign(newPos.z - transform.position.z) * followMultiplyer, 0f, 10f); 
				transform.position = Vector3.MoveTowards (transform.position, newPos, followSpeed * Time.deltaTime);
				//if line enabled set start to self
				if (lineRend.enabled) {
					lineRend.SetPosition (0, transform.position);
				}
			} else {
				Deactivate ();
			}
		}
	}

	//test line of sight to missile before shot
	private bool LineOfSight (Transform target){
		int layerMask = 1 << 2;
		layerMask = ~layerMask;
		if (Vector3.Distance (target.position, player.transform.position) <= shotDistance &&
			Physics.Linecast (transform.position, target.position, out hit, layerMask) &&
			hit.collider.transform == target) {
			return true;
		}
		return false;
	}
		
	public void Activate (GameObject target){
		player = target;
		activated = true;
		bCollider.center = Vector3.zero;
		bCollider.size = Vector3.one * 10;
		gameObject.tag = "Untagged";
		Invoke ("Deactivate", activeTime);
		PosRotChange ();
		Destroy (gameObject, activeTime + 3f);
	}

	//change position/rotation at random
	void PosRotChange(){
		if (activated) {
			float token = Random.value;
			if (token < 0.5) {
				offset = new Vector3 (offset.x, Random.Range (0, 0.5f), Random.Range (1f, 2f));
			}
			if(!inDanger){
				newRot = Random.rotation;
			}
			inDanger = false;
		}
		Invoke ("PosRotChange", Random.Range(3, 5));
	}

	void Deactivate (){
		Vector3 lastVelocity = rb.velocity;
		rb.useGravity = true;
		rb.isKinematic = false;
		rb.AddForce (lastVelocity, ForceMode.VelocityChange);
		activated = false;
		meshRend.material = deadMaterial;
	}

	//if missile in box collider, shot it down
	//Stay is for case when many missiles enter box collider at the same time
	void OnTriggerStay(Collider other){
		if (activated) {
			if (nextFire <= Time.time) {
				if(other.CompareTag("Missile")){
					if (LineOfSight (other.transform)) {
						nextFire = Time.time + fireRate;
						newRot = Quaternion.LookRotation (other.transform.position - transform.position);
						transform.LookAt (other.transform);
						lineRend.SetPosition (1, other.transform.position);
						lineRend.enabled = true;
						inDanger = true;
						other.GetComponent<MissileController> ().Terminate ();
						Invoke ("DisableLine", 0.05f);
					}
				}
			}
		}
	}

	void DisableLine (){
		lineRend.enabled = false;
	}
}
