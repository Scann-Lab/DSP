using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class FirstPersonScript : MonoBehaviour {
	private Transform playerTransform;
	public CharacterController cc;
	int count = 0;
	float verticalRotation = 0;
	float upDownRange = 60.0f;
	double currTime = 0.0;
	string directory;

	int i = 0;
	int a = 0;
	int k = 0;

	//Changed speed from 18 to 15
	float movementSpeed = 15.0f;
	float mouseSensitivity = 2.0f;

	//Toggles whether write to disk is enabled.
	public bool writeToDisk = true;
	Vector3 speed;

	int UsingGamePad;

	// Use this for initialization
	void Start () {

		Debug.Log ("Loaded Player onto: " + SceneManager.GetActiveScene().name);
		Debug.Log ("Trial Number: " + GameControl.control.currentLevelNo);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		cc = GetComponent<CharacterController> ();

		if (GameControl.control.startedTrials == true){
			GameObject.Find ("Player").transform.position = GameControl.control.currentLevelPosition;
		}



		//writing data
		if(writeToDisk==true && GameControl.control.directory != null && GameControl.control.startedTrials == true)
		{
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(GameControl.control.directory, true))
			{
				if (GameControl.control.metadata == false) {
					//write in elements
					file.WriteLine ("ParticipantNo: " + GameControl.control.participantNumber);
					file.WriteLine ("Time for Trials (seconds): " + PlayerPrefs.GetFloat("trialTime"));
					if(PlayerPrefs.GetInt("UsingGamePad") == 1){
						file.WriteLine ("Controller type: Gamepad");
					}else if(PlayerPrefs.GetInt("UsingKeyboard") == 1){
						file.WriteLine ("Controller type: Keyboard and Mouse");
					}else if(PlayerPrefs.GetInt("UsingArrow") == 1){
						file.WriteLine ("Controller type: Arrows Only");
					}
					file.WriteLine ("Repetition?: " + GameControl.control.RepType);
					file.WriteLine ("DSPType: " + GameControl.control.DSPtype);
					file.WriteLine ("Encoding Tours: " + GameControl.control.LapsNumber);
					GameControl.control.metadata = true;
				}
				if (GameControl.control.currentLevelNo > 0) {
					file.WriteLine ("!!" + GameControl.control.currentLevelNo + "_" + GameControl.control.currentLevelName);
					currTime = 0.0;
					count++;
					Debug.Log ("WROTE THE TRIAL " + "!!" + GameControl.control.currentLevelNo + "_" + GameControl.control.currentLevelName);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {

		//Movement Mechanics
		//rotation
		i = PlayerPrefs.GetInt("UsingGamePad");
		a = PlayerPrefs.GetInt("UsingArrow");
		k = PlayerPrefs.GetInt("UsingKeyboard");
		// Debug.Log("Using Game Pad value of i: " + i);
		if(i == 1){
			//Debug.Log("i is 1");
			UsingGamePad = 1;
		}else if(k == 1){
			//Debug.Log("i is 0");
			UsingGamePad = 0;
		}else if(a == 1){
			UsingGamePad = 2;
		}

		if(UsingGamePad == 1){
			float rotLeftRight = Input.GetAxis ("Horizontal_look_joystick") * mouseSensitivity;
			transform.Rotate (0, rotLeftRight, 0);
			verticalRotation -= Input.GetAxis ("Vertical_look_joystick") * mouseSensitivity;
			verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
			Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

			//movement
			float forwardSpeed = Input.GetAxis ("Vertical_walk_joystick") * movementSpeed;
			float sideSpeed = Input.GetAxis ("Horizontal_walk_joystick") * movementSpeed;
			speed = new Vector3 (sideSpeed, 0, forwardSpeed);
			speed = transform.rotation * speed;
		}else if(UsingGamePad == 0){
			float rotLeftRight = Input.GetAxis ("Mouse X") * mouseSensitivity;
			transform.Rotate (0, rotLeftRight, 0);
			verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
			verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
			Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

			//movement
			float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
			float sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;
			speed = new Vector3 (sideSpeed, 0, forwardSpeed);
			speed = transform.rotation * speed;
		}else if(UsingGamePad == 2){
			//Set mouse sensitivity lower
			mouseSensitivity = 1.5f;
			float rotLeftRight = Input.GetAxis("RightLeftArrowRotate") * mouseSensitivity;
			// float rotRight = Input.GetAxis("RightArrowRotate") * mouseSensitivity;
			// transform.Rotate (0,-rotLeft, 0);
			transform.Rotate(0,rotLeftRight,0);
			// verticalRotation -= Input.GetAxis ("Vertical_look_joystick") * mouseSensitivity;
			// verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
			// Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

			//movement
			float forwardSpeed = Input.GetAxis ("UpDownArrowWalk") * movementSpeed;
			// float forwardSpeedNeg = Input.GetAxis ("DownArrowWalk") * movementSpeed;
			// float sideSpeed = Input.GetAxis ("Horizontal_walk_joystick") * movementSpeed;
			speed = new Vector3 (0, 0, forwardSpeed);
			speed = transform.rotation * speed;
		}

		if (cc.enabled) {
			cc.Move (speed * Time.deltaTime);
		}

		//disables the character controller if movementLocked == true
		if (GameControl.control.movementLocked == true) {
			cc.enabled = false;
			GameControl.control.startWriting = false;
		}
		else {
			cc.enabled = true;
			GameControl.control.startWriting = true;

		}
		// if (Input.GetKeyDown (KeyCode.LeftShift)) {
		// 	movementSpeed = 30.0f;
		// }
		//
		// if (Input.GetKeyUp (KeyCode.LeftShift)){
		// 	movementSpeed = 18.0f;
		// }

		//writing data
		playerTransform = GameObject.Find ("Player").transform;
		Vector3 playerPos = playerTransform.position;
		Quaternion playerRot = playerTransform.rotation;

		if (writeToDisk == true && GameControl.control.directory != null && GameControl.control.startedTrials == true) {
			if (GameControl.control.startWriting == true && GameControl.control.currentLevelNo > 0) {
				using (System.IO.StreamWriter file = new System.IO.StreamWriter (GameControl.control.directory, true)) {
					currTime += Time.deltaTime;
					file.WriteLine (Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z + "," + playerRot.eulerAngles.y + "," + GameControl.control.currentBox);
					Debug.Log("This is the currentBox: " + GameControl.control.currentBox);
					count++;
					//Debug.Log ("WROTE THE STEP: " + Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z);
				}
			}
		}

	}
}
