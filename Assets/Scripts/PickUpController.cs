using UnityEngine;
using System.Collections;

public class PickUpController : MonoBehaviour {

	public GameObject flash;
	public float rotationSpeed;
	public int ammount;

	void Start(){
		transform.rotation = Random.rotation;
	}

	void Update(){
		Vector3 newRot = new Vector3 (0, rotationSpeed * Time.deltaTime, 0);
		transform.Rotate(newRot, Space.World);
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player") || other.CompareTag("Magnet")){
			Instantiate (flash, transform.position/* + offset*/, Quaternion.identity);
			FindObjectOfType<PickupsController> ().SetItemPicked (gameObject);
			Destroy (gameObject);
		}
	}
	
//	private float lerp = 0f;
//	private Vector3 startPos;
//
//	// Use this for initialization
//	void Start () {
//		startPos = transform.position;
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		lerp = Mathf.Clamp(Mathf.PingPong(Time.time, 2), 1, 2);
//		Vector3 targetPos = new Vector3(transform.position.x, Mathf.Sin(lerp * Mathf.PI), transform.position.z);
//		transform.position = Vector3.Lerp(startPos, targetPos, lerp);
//	}
}
