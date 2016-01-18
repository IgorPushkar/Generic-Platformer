using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class GameMenuController : MonoBehaviour {

	public LevelManager levelManager;
	public Button menuButton;
	public Animator anim;

	private ModalPanel modalPanel;
	private UnityAction yesAction;
	private UnityAction noAction;

	private bool opened;
	private SettingsController settings;
	private CameraController camCtrl;

	// Use this for initialization
	void Start () {
		camCtrl = FindObjectOfType<CameraController> ();
		settings = FindObjectOfType<SettingsController> ();
		modalPanel = ModalPanel.Instance ();
		noAction = new UnityAction (OnNo);
		opened = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && menuButton.interactable == true){
			OnMenuButtonClick();
		}
	}

	public void OnMenuButtonClick(){
		if (opened) {
			opened = !opened;
			anim.SetBool("isOpened", opened);
			camCtrl.Blur (opened);
			Time.timeScale = 1.0f;
			settings.Save ();
		} else {
			opened = !opened;
			anim.SetBool("isOpened", opened);
			camCtrl.Blur (opened);
			Time.timeScale = 0.0f;
		}
	}

	public void OnRestartClick(){
		menuButton.interactable = false;
		yesAction = new UnityAction (OnRestartYes);
		string question = "Restart level?";
		modalPanel.Choice (question, yesAction, noAction);
	}

	void OnRestartYes(){
		settings.Save ();
		levelManager.ReloadCurrentLevel ();
	}

	public void OnBackClick(){
		menuButton.interactable = false;
		yesAction = new UnityAction (OnBackYes);
		string question = "Want to give up?";
		modalPanel.Choice (question, yesAction, noAction);
	}

	void OnBackYes(){
		settings.Save ();
		OnMenuButtonClick ();
		FindObjectOfType<DeathTextFader> ().enabled = true;
		FindObjectOfType<PlayerController> ().Death();
		FindObjectOfType<CameraController> ().Blur (true);
	}

	public Button GetMenuButton(){
		return menuButton;
	}

	void OnNo(){
		menuButton.interactable = true;
	}
}
