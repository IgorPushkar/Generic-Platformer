using UnityEngine;
using System.Collections;

public class MagnetController : MonoBehaviour {

	public float lifetime;

	private float activateTime;
	private Material refraction;

	// Use this for initialization
	void Start () {
		refraction = GetComponent<MeshRenderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.activeSelf){
			refraction.SetFloat("_BumpAmt", Mathf.Lerp(25, 0, (Time.time - activateTime) / lifetime));

		}
	}

	public void Activate(){
		activateTime = Time.time;
		gameObject.SetActive(true);
		Invoke ("Deactivate", lifetime);
	}

	private void Deactivate(){
		gameObject.SetActive(false);
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Pick Up")) {
			GameObject.FindObjectOfType<PlayerController> ().ApplyGain ("Score", other.gameObject.GetComponent<PickUpController> ().ammount);
		}
	}
}
