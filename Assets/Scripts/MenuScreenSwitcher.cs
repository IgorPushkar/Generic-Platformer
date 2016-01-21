using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScreenSwitcher : MonoBehaviour {

	public float transitionSpeed = 5f;

	private const float SCREEN_HEIGHT = 622f;
	private RectTransform mainPanel;
	private float newY;
	private CurrentState currentState;

	enum CurrentState {
		Main,
		Settings,
		LevelSelect,
		Workshop
	}

	void Start(){
		currentState = CurrentState.Main;
		mainPanel = GameObject.Find ("Main panel").GetComponent<RectTransform> ();
	}

	void Update(){

		if(Input.GetKeyDown(KeyCode.Escape)){
			switch(currentState){
			case CurrentState.Main:
				FindObjectOfType<StartMenuManager> ().OnQuitClick ();
				break;
			case CurrentState.Workshop:
				ToLevelSelect ();
				break;
			default:
				ToMain ();
				break;
			}
		}

		Vector2 newPosition = mainPanel.anchoredPosition;
		newPosition.y = Mathf.Lerp (mainPanel.anchoredPosition.y, newY, transitionSpeed * Time.deltaTime);
		mainPanel.anchoredPosition = newPosition;
	}

	public void ToMain(){
		currentState = CurrentState.Main;
		newY = SCREEN_HEIGHT * 0f;
	}

	public void ToSettings(){
		currentState = CurrentState.Settings;
		newY = SCREEN_HEIGHT * 1f;
	}

	public void ToLevelSelect(){
		currentState = CurrentState.LevelSelect;
		newY = SCREEN_HEIGHT * -1f;
	}

	public void ToWorkshop(){
		currentState = CurrentState.Workshop;
		newY = SCREEN_HEIGHT * -2f;
	}
}
