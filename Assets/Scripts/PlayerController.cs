using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//System
	public LevelManager levelManager;

	private RuntimePlatform platform;

	private CharacterController controller;
	
	//Movement
	public float speed;
	public float jump;
	public float jet;
	public float maxJet;
	public float gravity;

	private bool isControlable;
	private Vector3 moveDirection = Vector3.zero;

	//SFX & Animation
	public float barSpeed;
	public GameObject FX, jetBeam, flames;
	public AudioClip[] sounds;

	private AudioSource audioSource;
	private ParticleSystem[] flamesFX;
	private ParticleSystem[] jetBeamFX;
	private Animator anim;

	//Shield
	public Color positiveShieldColor;
	public Color negativeShieldColor;
	public float shieldColorLerp;
	public GameObject personalShield;

	private MeshRenderer shield;
	private SphereCollider shieldSphere;
	private float timeHitted;

	//Drone
	private bool hasDrone;

	//Magnet
	public GameObject magnet;

	private bool hasMagnet;

	//Dead self instance
	public GameObject deadRobot;

	//Other variables
	public float fuelLossRate;

	private float magnetTimer;
	private bool isDead;
	private float fuel;
	private float energy;
	private int score;

	//GUI
	private Image fuelBar;
	private Image shieldBar;
	private Button menuButton;
	private Text scoreText;

	private Rect menu;

	//Debug
	private bool immortal;

//	Draw rectangle on pause menu position to not jump when clicked/touched menu button
	void OnGUI(){
		menu = new Rect (menuButton.transform.position.x, menuButton.transform.position.y, Screen.width, Screen.height);
	}
		
	void Start () {
		immortal = false;
		isControlable = false;
		fuelBar = FindObjectOfType<UIManager> ().GetFuelBar ();
		shieldBar = FindObjectOfType<UIManager> ().GetShieldBar ();
		menuButton = FindObjectOfType<GameMenuController> ().GetMenuButton ();
		scoreText = FindObjectOfType<UIManager> ().GetScoreText ();
		platform = Application.platform;
		controller = GetComponent<CharacterController>();
		audioSource = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		fuel = 100f;
		energy = 100f;
		isDead = false;
		Time.timeScale = 1f;
		FX.SetActive (true);
		flamesFX = flames.GetComponentsInChildren<ParticleSystem> ();
		jetBeamFX = jetBeam.GetComponentsInChildren<ParticleSystem> ();
		Unjet ();
		shieldSphere = personalShield.GetComponent<SphereCollider> ();
		shield = personalShield.GetComponent<MeshRenderer> ();
		hasDrone = PlayerPrefsManager.GetDrone ();
		hasMagnet = PlayerPrefsManager.GetMagnet ();
		score = PlayerPrefsManager.GetScore ();
		scoreText.text = score.ToString ();
	}

	void Update () {

		if(Time.timeScale <= 0){
			audioSource.Pause ();
		} else {
			
			if (energy > 0) {
				shieldSphere.enabled = true;
			} else {
				shieldSphere.enabled = false;
			}

			if (isDead) {
				Death ();
			} else {

				if (controller.isGrounded || (controller.collisionFlags & CollisionFlags.Below) != 0) {
					Unjet ();
					anim.SetBool ("inAir", false);
					anim.SetBool ("jump", false);
					if(!audioSource.isPlaying || audioSource.clip != sounds [0]){
						audioSource.loop = true;
						audioSource.clip = sounds [0];
						audioSource.Play ();	
					}
				} else {
					anim.SetBool ("inAir", true);
//			anim.SetBool ("jump", false);
				}
				moveDirection.z = speed;	
				moveDirection.y -= controller.isGrounded ? 0 : gravity * Time.deltaTime;

				if (isControlable) {
					if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
						if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began && controller.isGrounded && !menu.Contains (Input.GetTouch (0).position)) {
							Jump ();
						} else if ((controller.collisionFlags & CollisionFlags.Above) != 0 && !controller.isGrounded && moveDirection.y > 0) {
							moveDirection.y = 0f;
						} else if (controller.velocity.y <= maxJet && Input.touchCount > 0 && fuel > 0 && (controller.collisionFlags & CollisionFlags.Above) == 0 && !menu.Contains (Input.GetTouch (0).position)) {
							Jet ();
						} else if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {
							Unjet ();
						} else if (Input.touchCount == 0 || controller.velocity.y < maxJet || fuel <= 0) {
							Unjet ();
						}
					} else {
						if (Input.GetMouseButtonDown (0) && controller.isGrounded && !menu.Contains (Input.mousePosition)) {
							Jump ();
						} else if ((controller.collisionFlags & CollisionFlags.Above) != 0 && !controller.isGrounded && moveDirection.y > 0) {
							moveDirection.y = 0f;
						} else if (controller.velocity.y <= maxJet && Input.GetMouseButton (0) && fuel > 0	&& (controller.collisionFlags & CollisionFlags.Above) == 0 && !menu.Contains (Input.mousePosition)) {
							Jet ();
						} else if (Input.GetMouseButtonUp (0) || controller.velocity.y < maxJet || fuel <= 0) {
							Unjet ();
						}
					}
				}
		



//				if (magnetTimer > 0) {
//					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("Pick Up")) {
//						if (Vector3.Distance (obj.transform.position, transform.position) <= 5) {
//							PickUp (obj);
//						}
//					}
//					magnetTimer -= Time.deltaTime;
//				}
				if (!isDead)
					controller.Move (moveDirection * Time.deltaTime);

			}

//			fuelBar.fillAmount = Mathf.Lerp (0.0f, 1.0f, fuel / 100);
//			fuelBar.fillAmount = Mathf.Lerp (0.0f, 1.0f, fuel / 100);
//			if (fuelBar.fillAmount < fuel / 100) {
//				fuelBar.fillAmount += Time.deltaTime * barSpeed;
//			} else if (fuelBar.fillAmount > fuel / 100){
//				fuelBar.fillAmount -= Time.deltaTime * barSpeed;
//			}
//			if (shieldBar.fillAmount < energy / 100) {
//				shieldBar.fillAmount += Time.deltaTime * barSpeed;
//			} else if (shieldBar.fillAmount > energy / 100){
//				shieldBar.fillAmount -= Time.deltaTime * barSpeed;
//			}

			if (fuelBar.fillAmount != fuel / 100) {
				fuelBar.fillAmount = Mathf.Lerp (fuelBar.fillAmount, fuel / 100, barSpeed * Time.deltaTime);
			}
			if (shieldBar.fillAmount != energy / 100) {
				shieldBar.fillAmount = Mathf.Lerp (shieldBar.fillAmount, energy / 100, barSpeed * Time.deltaTime);
			}

			if (shield.material.GetColor ("_TintColor").a > 0f) {
				Color tempColor = shield.material.GetColor ("_TintColor");
				tempColor.a = 0f;
				float lerp = (Time.time - timeHitted) / shieldColorLerp;
				Color newShieldColor = Color.Lerp (shield.material.GetColor ("_TintColor"), tempColor, lerp);
				shield.material.SetColor ("_TintColor", newShieldColor);
			}
		}
	}

	void Jump(){
		moveDirection.y = jump;
		anim.SetBool ("jump", true);
		anim.SetBool ("inAir", true);
		audioSource.loop = false;
		audioSource.clip = sounds [1];
		audioSource.Play ();
	}

	void Jet(){
		moveDirection.y += jet * Time.deltaTime;
		fuel -= fuelLossRate * Time.deltaTime;
		fuel = Mathf.Clamp (fuel, 0, 100);
		foreach (ParticleSystem system in flamesFX) {
			var em = system.emission;
			em.enabled = false;
		}
		foreach (ParticleSystem system in jetBeamFX) {
			var em = system.emission;
			em.enabled = true;
		}
		if (!audioSource.isPlaying || audioSource.clip != sounds [2]) {
			audioSource.loop = true;
			audioSource.clip = sounds [2];
			audioSource.PlayOneShot (sounds [2]);
		}
	}

	void Unjet(){
		foreach (ParticleSystem system in flamesFX) {
			var em = system.emission;
			em.enabled = true;
		}
		foreach (ParticleSystem system in jetBeamFX) {
			var em = system.emission;
			em.enabled = false;
		}
		if (audioSource.isPlaying && audioSource.clip == sounds [2]) {
			audioSource.Stop ();
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Start")) {
			FindObjectOfType<CameraController> ().Follow ();
			isControlable = true;
		} else if (other.CompareTag ("Finish")) {
			PlayerPrefsManager.SetScore (score);
			levelManager.UnlockNextLevel ();
			EndLevel ();
		} else if (other.CompareTag ("Drone") && hasDrone) {
			other.GetComponent<DroneController> ().Activate (gameObject);
		} else if (other.CompareTag ("Magnet") && hasMagnet) {
			magnet.GetComponent<MagnetController> ().Activate ();
		} else if (other.CompareTag ("Pick Up")) {
			ApplyGain("Score", other.gameObject.GetComponent<PickUpController> ().ammount);
		} else if (other.CompareTag ("Void")) {
			isDead = true;
		}
	}

	public void ApplyGain(string type, float ammount){
		switch (type) {
		case "Shield":
			energy += ammount;
			energy = Mathf.Clamp (energy, 0, 100);
			ShieldFlash (true);
			break;
		case "Fuel":
			fuel += ammount;
			fuel = Mathf.Clamp (fuel, 0, 100);
			break;
		case "Score":
			score += (int)ammount;
			score = Mathf.Clamp (score, 0, 9999);
			scoreText.text = score.ToString ();
			break;
		}
	}

	public void ApplyDamage(float dmg){
		if (immortal)
			return;
		if (energy > 0) {			
			ShieldFlash (false);
			energy -= dmg;
			energy = Mathf.Clamp (energy, 0, 100);
		} else
			isDead = true;
	}

	void ShieldFlash(bool isPositive){
		Color newShieldColor;
		if (isPositive) {
			newShieldColor = positiveShieldColor;
		} else {
			newShieldColor = negativeShieldColor;
		}
		//			newShieldColor.a = 1.0f;
		newShieldColor.a = energy / 100;
		shield.material.SetColor ("_TintColor", newShieldColor);
		timeHitted = Time.time;
	}

	public void Death(){
		menuButton.interactable = false;
		energy = 0.0f;
		shieldBar.fillAmount = energy / 100;
		FX.SetActive (false);
		controller.enabled = false;
		tag = "Untagged";
		FindObjectOfType<DeathTextFader> ().enabled = true;
		FindObjectOfType<CameraController> ().Blur (true);
		GameObject deadInstance = Instantiate (deadRobot, transform.position, transform.rotation) as GameObject;
		deadInstance.GetComponent<DeadRobotController> ().playerVelocity = controller.velocity;
		Destroy (gameObject);
	}

	public void InvokeDeath(){
		isDead = true;
	}

	void EndLevel(){
		isControlable = false;
		immortal = true;
		FindObjectOfType<CameraController> ().Unfollow ();
		FindObjectOfType<CameraController> ().Blur (true);
		FindObjectOfType<LevelEndController> ().OpenMenu ();
		menuButton.interactable = false;
	}

	public GameObject GetPlayer(){
		return gameObject;
	}
}