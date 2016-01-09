using UnityEngine;
using System.Collections;

public class DeadRobotController : MonoBehaviour {

	public Vector3 playerVelocity; //get player velocity to simulate same speed at spawn

	void Start () {
		//set players velocity before death to each part of robot
		foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
			rb.AddForce(new Vector3 (Random.Range(-0.5f,0.5f), playerVelocity.y, playerVelocity.z), ForceMode.VelocityChange);
		}
		//separate robot to parts
		transform.DetachChildren ();
	}
}
