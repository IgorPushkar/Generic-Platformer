using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float lerp; //LookAtRotation and Position lerp speed

	private Vector3 offset = new Vector3 (7f, 1f, 1f); //camera default offset
	private bool isFollowing = false; //is player get to camera position?
	private GameObject player;
	private BlurOptimized blur;

	void Start(){
		player = FindObjectOfType<PlayerController> ().GetPlayer ();
		blur = GetComponent<BlurOptimized> ();
	}
		
	void LateUpdate () {
		if (isFollowing && player) {
			//move to player position by Z axis
			transform.position = new Vector3 (transform.position.x, transform.position.y, player.transform.position.z + offset.z);
			//make new point on Y axis
			float newY = player.transform.position.y + offset.y;
			//make new position to lerp
			Vector3 newPos = new Vector3 (transform.position.x, newY, transform.position.z); 
			//lerp to new position
			transform.position = Vector3.Lerp (transform.position, newPos, lerp * Time.deltaTime); 
			//look at player
			SmoothLookAt (); 
		}
	}

	void SmoothLookAt ()
	{
		// Create a vector from the camera towards the player.
		Vector3 relPlayerPosition = (player.transform.position + new Vector3(0f, 0f, offset.z)) - transform.position;
		
		// Create a rotation based on the relative position of the player being the forward vector.
		Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);

		// Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
		transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, lerp * Time.deltaTime);
	}

	public void Blur(bool flag){
		blur.enabled = flag;

	}

	public void Follow(){
		isFollowing = true;
	}

	public void Unfollow(){
		isFollowing = false;
	}
}
