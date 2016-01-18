using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class PickUpController : MonoBehaviour {

	public GameObject flash;
	public float rotationSpeed;
	public int ammount;

	private bool isPicked;
	private Vector3 scaleModifier;

	void Start(){
		transform.rotation = Random.rotation;
		isPicked = false;
		scaleModifier = transform.localScale;
	}

	void Update(){
		Vector3 newRot = new Vector3 (0, rotationSpeed * Time.deltaTime, 0);
		transform.Rotate(newRot, Space.World);
		if(isPicked){
			transform.localScale -= scaleModifier * Time.deltaTime;
		}
	}

	void SpawnFlash(){
//		Vector3 offset = new Vector3 (0.1f, 0f, 0f);
		Instantiate (flash, transform.position/* + offset*/, Quaternion.identity);
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player") || other.CompareTag("Magnet")){
			rotationSpeed = 720f;
			isPicked = true;
			Invoke ("SpawnFlash", 0.5f);
			Destroy (gameObject, 1f);
			GetComponent<AudioSource> ().Play ();
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
