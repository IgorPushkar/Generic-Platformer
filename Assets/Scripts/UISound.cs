using UnityEngine;
using System.Collections;

public class UISound : MonoBehaviour {

	public void PlayUISound(){
		FindObjectOfType<UISoundPlayer> ().PlayUISound ();
	}
}
