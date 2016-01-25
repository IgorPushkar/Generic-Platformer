using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorkshopController : MonoBehaviour {

	public Text partsCount;
	public Sprite droneNeutral;
	public Sprite dronePositive;
	public Sprite droneNegative;
	public Sprite magnetNeutral;
	public Sprite magnetPositive;
	public Sprite magnetNegative;
	public Button droneButton;
	public Button magnetButton;
	public Image droneImage;
	public Image magnetImage;
	public Text droneCost;
	public Text magnetCost;

	private const int DRONE_COST = 300;
	private const int MAGNET_COST = 200;

	// Use this for initialization
	void Start () {
		droneCost.text = DRONE_COST.ToString();
		magnetCost.text = MAGNET_COST.ToString();
		UpdateScene ();
	}

	void SetupPartsText(){
		partsCount.text = PlayerPrefsManager.GetScore ().ToString();
	}

	void SetupDroneButton(){
		if(PlayerPrefsManager.GetDrone()){
			droneImage.sprite = dronePositive;
			droneButton.interactable = false;
		} else if (PlayerPrefsManager.GetScore () < DRONE_COST){
			droneImage.sprite = droneNegative;
			droneButton.interactable = false;
		} else {
			droneImage.sprite = droneNeutral;
			droneButton.interactable = true;
		}
	}

	void SetupMagnetButton(){
		if(PlayerPrefsManager.GetMagnet()){
			magnetImage.sprite = magnetPositive;
			magnetButton.interactable = false;
		} else if (PlayerPrefsManager.GetScore () < MAGNET_COST){
			magnetImage.sprite = magnetNegative;
			magnetButton.interactable = false;
		} else {
			magnetImage.sprite = magnetNeutral;
			magnetButton.interactable = true;
		}
	}

	public void OnDroneClick(){
		if(PlayerPrefsManager.GetScore () >= DRONE_COST && !PlayerPrefsManager.GetDrone()){
			PlayerPrefsManager.SetScore (PlayerPrefsManager.GetScore() - DRONE_COST);
			PlayerPrefsManager.SetDrone (true);
			UpdateScene ();
		}
	}

	public void OnMagnetClick(){
		if(PlayerPrefsManager.GetScore () >= MAGNET_COST && !PlayerPrefsManager.GetMagnet()){
			PlayerPrefsManager.SetScore (PlayerPrefsManager.GetScore() - MAGNET_COST);
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
