using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PickupsController : MonoBehaviour {

	private List<GameObject> items = new List<GameObject>();
	private string itemsData;
	private string level;

	void Start () {
		string sceneName = SceneManager.GetActiveScene ().name;
		level = sceneName.Substring (sceneName.Length - 2);
		foreach(Transform child in transform){
			items.Add (child.gameObject);
		}
		itemsData = PlayerPrefsManager.GetLevelPickups (level);
		if(itemsData.Length <= 0){
			string tempData = "";
			for(int i = 0; i < items.Count; i++){
				tempData += "1";
			}
			itemsData = tempData;
			SaveItemsState ();
		}

		for(int i = 0; i < items.Count; i++){
			if(itemsData[i] == '1'){
				items [i].SetActive (true);
			} else {
				items [i].SetActive (false);
			}
		}
	}

	public void SetItemPicked(GameObject obj){
		int index = items.IndexOf (obj);
		char[] tempData = itemsData.ToCharArray ();
		tempData [index] = '0';
		itemsData = "";
		foreach(char ch in tempData){
			itemsData += ch;
		}
	}

	public void SaveItemsState(){
		PlayerPrefsManager.SetLevelPickups (level, itemsData);
	}
}
