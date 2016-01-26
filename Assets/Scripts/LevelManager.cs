using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	[Tooltip ("Delay in seconds after which will load level with next build index. -1 to disable autoload.")]
	public float autoLoadNextLevelAfter;

	void Start() {
		if (autoLoadNextLevelAfter >= 0) {
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}

	public void LoadLevel(string name){
		SceneManager.LoadScene (name);
	}

	public AsyncOperation LoadLevelAsync(string name){
		AsyncOperation loading = SceneManager.LoadSceneAsync (name);
		return loading;
	}

	public void LoadPreviousLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
	}

	public void LoadNextLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void ReloadCurrentLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void LoadStartLevel(){
		SceneManager.LoadScene ("01a Start");
	}

	public void LoadWorkshopLevel(){
		SceneManager.LoadScene ("01d Workshop");
	}

	public void LoadLevelSelect(){
		//SceneManager.LoadScene ("01c Level Select");
		SceneManager.LoadScene ("01a Start");
		GameObject obj = new GameObject ();
		obj.AddComponent<ToLevelSelectSwitcher> ();
	}

	public void QuitRequest(){
		Application.Quit ();
	}

	public void UnlockNextLevel(){
		if (SceneManager.GetActiveScene ().buildIndex + 1 < SceneManager.sceneCountInBuildSettings) {
			string level = SceneManager.GetActiveScene ().name;
			int levelNum = int.Parse (level.Substring (level.Length - 2)) + 1;
			string newLevelNum = levelNum > 9 ? levelNum.ToString () : "0" + levelNum.ToString ();
			PlayerPrefsManager.SetLevelState (newLevelNum);
		}
	}
}
