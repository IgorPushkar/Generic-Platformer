using UnityEngine;
using System.Collections;
using System;

public enum Type{
	Floor, Wall, Ceiling, None
};

public class LaunchPadController : MonoBehaviour {

	public Type type;

	void OnTriggerEnter(Collider col){
		if (col.CompareTag("Player")){
			switch (type) {
			case Type.Ceiling:
				try {
					GetComponentInChildren<MissileController> ().Trigger ();
					transform.DetachChildren ();
				} catch (Exception exc) {
					Debug.Log (exc.Message);
				}
				break;
			case Type.Floor:
				try {
					GetComponentInChildren<MissileController> ().Trigger ();
					transform.DetachChildren ();
				} catch (Exception exc) {
					Debug.Log (exc.Message);
				}
				break;
			case Type.None:
				break;
			case Type.Wall:
				break;
			default:
				break;
			}
		}
	}
}
