using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	private bool active, triggered;
	private Vector3 offset;
	private Vector3 playerPosition;
	private Vector3 targetPosition;

	public float speed;
	public float damage;
	public ParticleSystem explosion;
	public InitialDirection direction;

	void Start () {
		active = false;
		triggered = false;
	}
	 
	void Update () {

		if (triggered && !active) {
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);
			if (transform.position == playerPosition) {
				active = true;
			} else if (transform.position == targetPosition) {
				targetPosition = playerPosition;
			}
		} 
		if(active && Time.timeScale > 0f) {
			if (speed < 8) {
				speed += Time.deltaTime;
			}
			transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.World);
			transform.Rotate (Vector3.up, speed * 0.5f);
		}
	}

	void OnTriggerEnter(Collider other){
		if (active) {
			if (other.CompareTag ("Shield") || other.CompareTag ("Robot")) {
				other.gameObject.GetComponentInParent<PlayerController> ().ApplyDamage (damage);
			} else {
				return;
			}  
			Terminate ();
		}
	}

	void OnCollisionEnter(Collision collision){
		if (active) {
			if (collision.collider.CompareTag ("Robot")) {
				collision.collider.gameObject.GetComponentInParent<PlayerController> ().ApplyDamage (damage);
			} else if (collision.collider.CompareTag ("Missile")) {
				return;
			}

			Terminate ();
		}
	}

	public void Trigger(){
		targetPosition = new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z);
		playerPosition = new Vector3 (0, targetPosition.y, targetPosition.z);
		triggered = true;
		GetComponent<AudioSource> ().Play ();
	}

	public void Terminate(){
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
