using UnityEngine;
using System.Collections;

public class LigtningWall2 : MonoBehaviour {

	public GameObject lightning;
	public int segments;
	public float damage;
	public Transform start;
	public Vector3 startRange;
	public Transform end;
	public Vector3 endRange;
	public float range;
	public float frequency;

	private BoxCollider boxCollider;

	void OnDrawGizmos(){
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(start.position, 1);
		Gizmos.DrawWireSphere(end.position, 1);
	}

	void Start(){
		boxCollider = GetComponent<BoxCollider> ();
		float distance = Vector3.Distance (end.position, start.position);
		Vector3 direction = end.position - start.position;
		Vector3 newBoxCenter = direction.normalized * (distance * 0.5f);
		boxCollider.center += newBoxCenter;
		Vector3 newBoxSize = direction.normalized * distance;
		boxCollider.size = newBoxSize;
		InvokeRepeating ("Spawn", 0.000001f, frequency);
	}

	void Spawn(){
		float startX = Random.Range (start.position.x - startRange.x, start.position.x + startRange.x);
		float startY = Random.Range (start.position.y - startRange.y, start.position.y + startRange.y);
		float startZ = Random.Range (start.position.z - startRange.z, start.position.z + startRange.z);
		float endX = Random.Range (end.position.x - endRange.x, end.position.x + endRange.x);
		float endY = Random.Range (end.position.y - endRange.y, end.position.y + endRange.y);
		float endZ = Random.Range (end.position.z - endRange.z, end.position.z + endRange.z);
		Vector3 startPoint = new Vector3 (startX, startY, startZ);
		Vector3 endPoint = new Vector3 (endX, endY, endZ);
		GameObject newLightning = Instantiate (lightning, transform.position, Quaternion.identity) as GameObject;
		newLightning.GetComponent<Lightning2> ().SetPoints (startPoint, endPoint, segments, range);
	}

//	void LateUpdate(){
//		if (Time.timeScale > 0) {
//			Instantiate (lightning, transform.position, Quaternion.identity);
//		}
//	}

	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Robot") || other.CompareTag ("Player")) {
			other.gameObject.GetComponentInParent<PlayerController> ().ApplyDamage (damage * Time.deltaTime);
		}
	}
}
