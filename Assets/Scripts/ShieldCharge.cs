using UnityEngine;
using System.Collections;

public class ShieldCharge : MonoBehaviour {

	public float speed;
//	public float power;
	public GameObject player;
	public float gainAmmount;
//	public float delay;
//	public float step;

//	private Vector3 point;
//	private float distance;
//	private float birthday;

	// Use this for initialization
	void Start () {
//		birthday = Time.time + delay;
//		distance = Vector3.Distance (transform.position, player.transform.position);
//		InvokeRepeating ("ChangePoint", 0.00001f, 0.15f);
	}

	// Update is called once per frame
	void Update () {
		if (player) {
			if (Time.timeScale > 0) {
				transform.position = Vector3.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
//				GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * speed, ForceMode.Impulse);
//				Quaternion toPlayer = Quaternion.LookRotation(player.transform.position - transform.position);
//				transform.rotation = Quaternion.Lerp(transform.rotation, toPlayer, speed * Time.deltaTime);
//				transform.position = Vector3.MoveTowards (transform.position, transform.forward * 3f, speed * Time.deltaTime);
//				transform.Translate(transform.forward.normalized * speed);
//				speed += Time.deltaTime / 10f;
//				speed = Mathf.Clamp (power / Mathf.Pow (distance, 2), 0.01f, float.MaxValue);
//				transform.position = Vector3.MoveTowards (transform.position, point, speed);
			}
		}
//		} else {
//			CancelInvoke ("ChangePoint");
//		}
//				Vector3 targetPos = new Vector3(Mathf.Sin(lerp * Mathf.PI) * 3, player.transform.position.y, player.transform.position.z);
//				transform.localScale = Vector3.Lerp(startScale, new Vector3(0,0,0), lerp);
//				transform.position = Vector3.Lerp(startPos, targetPos, lerp);
//				lerp += Time.deltaTime;
//			}else{
//				Destroy(gameObject);
//			}
//		}

	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			player.GetComponent<PlayerController> ().ApplyGain ("Shield", gainAmmount);
			Destroy (gameObject);
		}
	}

//	void ChangePoint(){
//		if (distance < 1) {
//			point = player.transform.position;
//			CancelInvoke ("ChangePoint");
//			return;
//		}
//		point = Random.insideUnitSphere * Mathf.Clamp(distance, 0, 0.5f) + player.transform.position;
//	}
}

