using UnityEngine;
using System.Collections;

public enum InitialDirection {
	Up,
	Down
};

public class MissileController : MonoBehaviour {

	private GameObject player;
//	private Transform body;
	private bool active, triggered;
//	private RaycastHit hit;
	private Vector3 offset;
	private Vector3 origin;

	public float speed;
	public float damage;
	public ParticleSystem explosion;
	public InitialDirection direction;

	// Use this for initialization
	void Start () {
		active = false;
		triggered = false;
	}
	 
	// Update is called once per frame
	void Update () {

		if (triggered && !active) {
			switch (direction) {
			case InitialDirection.Up:
				transform.position += transform.up * 4.5f * Time.deltaTime;
				break;
			case InitialDirection.Down:
				transform.position += transform.up * -1 * 4.5f * Time.deltaTime;
				break;
			}
			origin = transform.position;
		} 
		if(active) {
			if (player) {
				//transform.LookAt (player.transform.position);
//			float distance = Mathf.Clamp(Vector3.Distance (transform.position, player.transform.position)*10,0f,45f);
//			distance *= transform.position.z > player.transform.position.z ? -1 : 1;
//			Quaternion target = Quaternion.Euler(distance, 0, 0);
//			transform.localRotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2);
//			if (LineOfSight (player.transform)) {
				offset = new Vector3 (0, 0.6f, 0);
				if (speed < 8) {
					speed += Time.deltaTime;
				}
//			} else {
//				SetOffset ();
////				Vector3 point = new Vector3 (transform.position.x, transform.position.y, player.transform.position.z);
////				transform.position = Vector3.MoveTowards (transform.position, point + offset, speed * Time.deltaTime);
//				speed = Mathf.Clamp (speed, 0, 6.5f);
//			}
				transform.position = Vector3.MoveTowards (transform.position, player.transform.position + offset, speed * Time.deltaTime);
				if (Time.timeScale > 0f) {
					transform.Rotate (Vector3.up, speed * 0.5f);
				}
			} else {
				transform.position = Vector3.MoveTowards (transform.position, origin, Time.deltaTime);
				if (Time.timeScale > 0f) {
					transform.Rotate (Vector3.up, speed * 0.5f);
				}
			}
		}
//		float newX = distance / 10;
//		print (transform.rotation);
//		transform.Rotate (0, rotSpeed * Time.deltaTime, 0);
	}

//	void SetOffset(){
//		if (Physics.Raycast (transform.position, Vector3.up, out hit, 1f)) {
//			offset.y = (hit.point.y - player.transform.position.y) - 1;
//		} else if (Physics.Raycast (transform.position, Vector3.down, out hit, 1f)) {
//			offset.y = (hit.point.y - player.transform.position.y) + 1;
//		}
//	}

	void OnTriggerEnter(Collider other){
		if (active) {
			if (other.CompareTag ("Shield") || other.CompareTag ("Robot")) {
				other.gameObject.GetComponentInParent<PlayerController> ().ApplyDamage (damage);
			} else {
				return;
			}  
			Terminate (other.ClosestPointOnBounds (transform.position));
		}
	}

	void OnCollisionEnter(Collision collision){
		if (active) {
			if (collision.collider.CompareTag ("Robot")) {
				collision.collider.gameObject.GetComponentInParent<PlayerController> ().ApplyDamage (damage);
			} else {
				return;
			}
			Terminate (collision.contacts [0].point);
		}
	}

	public void Trigger(GameObject target, Vector3 start){
		origin = start;
		player = target;
		triggered = true;
		Invoke ("Activate", 0.25f);
	}

	void Activate(){
		active = true;
	}

//	private bool LineOfSight (Transform target){
//
//		int layerMask = 1 << 8;
//
//		if (Physics.Linecast(transform.position, target.position, out hit, layerMask) &&
//			hit.collider.transform == target) {
//			return true;
//		}
//		return false;
//	}

	public void Terminate(){
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

	public void Terminate(Vector3 point){
		Instantiate (explosion, point, Quaternion.identity);
		Destroy (gameObject);
	}
}
