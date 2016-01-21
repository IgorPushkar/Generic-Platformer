using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorkshopController : MonoBehaviour {

	public Text partsCount;
	public Sprite dronePositive;
	public Sprite droneNegative;
	public Sprite magnetPositive;
	public Sprite magnetNegative;
	public Button droneButton;
	public Button magnetButton;
	public Image droneImage;
	public Image magnetImage;

	// Use this for initialization
	void Start () {
		UpdateScene ();
	}

	void SetupPartsText(){
		partsCount.text = PlayerPrefsManager.GetScore ().ToString();
	}

	void SetupDroneButton(){
		if(PlayerPrefsManager.GetDrone()){
			droneImage.sprite = dronePositive;
			droneButton.interactable = false;
		} else if (PlayerPrefsManager.GetScore () < 450){
			droneImage.sprite = droneNegative;
			droneButton.interactable = false;
		}
	}

	void SetupMagnetButton(){
		if(PlayerPrefsManager.GetMagnet()){
			magnetImage.sprite = magnetPositive;
			magnetButton.interactable = false;
		} else if (PlayerPrefsManager.GetScore () < 200){
			magnetImage.sprite = magnetNegative;
			magnetButton.interactable = false;
		}
	}

	public void OnDroneClick(){
		if(PlayerPrefsManager.GetScore () >= 450 && !PlayerPrefsManager.GetDrone()){
			PlayerPrefsManager.SetScore (PlayerPrefsManager.GetScore() - 450);
			PlayerPrefsManager.SetDrone (true);
			UpdateScene ();
		}
	}

	public void OnMagnetClick(){
		if(PlayerPrefsManager.GetScore () >= 200 && !PlayerPrefsManager.GetMagnet()){
			PlayerPrefsManager.SetScore (PlayerPrefsManager.GetScore() - 200);
			PlayerPrefsManager.SetMagnet (true);
			UpdateScene ();
		}
	}

	public void UpdateScene(){
		SetupPartsText ();
		SetupDroneButton ();
		SetupMagnetButton ();
	}
}
