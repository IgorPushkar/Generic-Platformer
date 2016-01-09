using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelEndController : MonoBehaviour {

	public Animation anim;
	public Button nextLevelButton;

	void Start(){
		if(SceneManager.GetActiveScene().buildIndex + 1 >= LevelsList.GetAllLevels().Length){
			nextLevelButton.gameObject.SetActive(false);
		}
	}

	public void OpenMenu(){
		anim.Play ();
	}
}
