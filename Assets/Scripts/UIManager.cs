using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public Image fuelBar;
	public Image shieldBar;
	public Text scoreText;

	public Image GetFuelBar(){
		return fuelBar;
	}

	public Image GetShieldBar(){
		return shieldBar;
	}

	public Text GetScoreText(){
		return scoreText;
	}
}
