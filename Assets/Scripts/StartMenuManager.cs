using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class StartMenuManager : MonoBehaviour {

	public LevelManager levelManager;

	private ModalPanel modalPanel;
	private UnityAction yesAction;

	void Start () {
		modalPanel = ModalPanel.Instance ();
	}

	public void OnStartClick(){
		levelManager.LoadLevelSelect ();
	}

	public void OnSettingsClick(){
		levelManager.LoadLevel ("01b Settings");
	}

	public void OnQuitClick(){
		yesAction = new UnityAction (OnQuitYes);
		string question = "Quit game?";
		modalPanel.Choice(question, yesAction);
	}

	void OnQuitYes(){
		levelManager.QuitRequest ();
	}
}
