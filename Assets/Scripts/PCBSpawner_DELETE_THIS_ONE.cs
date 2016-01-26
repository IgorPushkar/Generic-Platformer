using UnityEngine;
using System.Collections;

public class PCBSpawner_DELETE_THIS_ONE : MonoBehaviour {

	public GameObject pickup;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 5; i++){
			GameObject obj = Instantiate (pickup, new Vector3 (0, 3, 25 / 5 * i + 40), Quaternion.identity) as GameObject;
			obj.transform.SetParent (transform);
		}	
	}
}
