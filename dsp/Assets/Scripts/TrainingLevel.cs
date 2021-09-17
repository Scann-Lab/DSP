using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TrainingLevel : MonoBehaviour {

	public UnityEngine.UI.Text instructionText;
	public Text moveText;
	public Text trainingTaskInstructions;
	//Import cubes for training task
	public GameObject cube1;
	public GameObject cube2;
	public GameObject cube3;
	public GameObject cube4;

	public bool writeToDisk = true;
	private Transform playerTransform;
	double currTime = 0.0;
	string directory;
	string directory2;


	// public Text instructionText;

	int i = 0;
	bool UsingGamePad;
	bool pressed = true;

	int count = 0;
	int count1 = 0;
	int trainingTask = 0;

	private int foundBool;
	private int endBool;

	void Awake(){
		foundBool = 0;
		endBool = 0;
		PlayerPrefs.SetInt("foundBool", foundBool);
		//PlayerPrefs.SetInt("endBool", endBool);
	}


	void Start(){
		GameControl.control.currentLevelPosition = new Vector3 (40f, -19.83f, 43.13f);
		cube1.SetActive(false);
		cube2.SetActive(false);
		cube3.SetActive(false);
		cube4.SetActive(false);
		PlayerPrefs.SetInt("trainingTask", trainingTask);
		string name = GameControl.control.participantNumber;

		//writing data
		directory = Application.dataPath +
		"\\..\\Participant_" + name +
		"_Date_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm") +
		"Training.txt";
		// Debug.Log(directory);
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory, true))
			{
					//write in elements
					file.WriteLine ("ParticipantNo: " + GameControl.control.participantNumber);
					if(PlayerPrefs.GetInt("UsingGamePad") == 1){
						file.WriteLine ("Controller type: Gamepad");
					}else if(PlayerPrefs.GetInt("UsingGamePad") == 0){
						file.WriteLine ("Controller type: Keyboard and Mouse");
					}

				file.WriteLine("Training Level:");
			}

		directory2 = Application.dataPath +
		"\\..\\Participant_" + name +
		"_Date_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm") +
		"TrainingGoals.txt";
		// Debug.Log(directory);
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory2, true))
			{
					//write in elements
					file.WriteLine ("ParticipantNo: " + GameControl.control.participantNumber);
					if(PlayerPrefs.GetInt("UsingGamePad") == 1){
						file.WriteLine ("Controller type: Gamepad");
					}else if(PlayerPrefs.GetInt("UsingGamePad") == 0){
						file.WriteLine ("Controller type: Keyboard and Mouse");
					}

				file.WriteLine("Training Level:");
			}

		// instructionText.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		//If G is pressed any time during the training phase, goes to the learning phase
		if(Time.timeSinceLevelLoad > 30){
			instructionText.enabled = true;
			instructionText.text = "When you are ready to begin the experiment, please press G.";
		}else{
			instructionText.enabled = false;
		}

		//Go to environment Task
		PracticeEnvironmentTask();
		trainingTask = PlayerPrefs.GetInt("trainingTask");
		foundBool = PlayerPrefs.GetInt("foundBool");
		// Debug.Log("found bool: " + foundBool);
		//endBool = PlayerPrefs.GetInt("endBool");

		if(Input.GetKey(KeyCode.T)){
			// Debug.Log("Clicking T");
			trainingTask = 1;
			PlayerPrefs.SetInt("trainingTask", trainingTask);
		}
		if (Input.GetKey (KeyCode.G)) {
			GameControl.control.goToLearning ();
		}
		i = PlayerPrefs.GetInt("UsingGamePad");
		if(i == 1){
			//Debug.Log("i is 1");
			UsingGamePad = true;
		}else{
			//Debug.Log("i is 0");
			UsingGamePad = false;
		}
		if(UsingGamePad && count == 0){
			moveText.text = "Use the left joystick to walk.\nUse the right joystick to look around.";
		}else if(count ==0){
			moveText.text = "Use WASD to walk.\nUse the mouse to look around.";
		}
		if(Input.GetKey (KeyCode.X) || Input.GetKey("joystick button 1")){
			// System.Threading.Thread.Sleep(500);
			if(pressed){
				if(count%2 == 0){
					moveText.text = "";
				}else{
					if(UsingGamePad){
						moveText.text = "Use the left joystick to walk.\nUse the right joystick to look around.";
					}else{
						moveText.text = "Use WASD to walk.\nUse the mouse to look around.";
					}
				}
				pressed = false;
				count += 1;
			}

		}else{
			pressed = true;

		}
			playerTransform = GameObject.Find ("Player").transform;
			Vector3 playerPos = playerTransform.position;
			Quaternion playerRot = playerTransform.rotation;
			using (System.IO.StreamWriter file = new System.IO.StreamWriter (directory, true)) {
				currTime += Time.deltaTime;
				file.WriteLine (Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z + "," + playerRot.eulerAngles.y);
				//Debug.Log ("WROTE THE STEP: " + Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z);
			}
			if (foundBool == 1 && endBool == 0){
				using (System.IO.StreamWriter file = new System.IO.StreamWriter (directory2, true)) {
					currTime += Time.deltaTime;
					file.WriteLine (Math.Round (currTime, 2) + ":  " + cube1.transform.position.x + "," + cube1.transform.position.y + "," + cube1.transform.position.z);
					endBool = 1;
					//Debug.Log ("WROTE THE STEP: " + Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z);
				}
			}
			else if (foundBool == 2 && endBool == 1){
				using (System.IO.StreamWriter file = new System.IO.StreamWriter (directory2, true)) {
					currTime += Time.deltaTime;
					file.WriteLine (Math.Round (currTime, 2) + ":  " + cube2.transform.position.x + "," + cube2.transform.position.y + "," + cube2.transform.position.z);
					endBool = 2;
					//Debug.Log ("WROTE THE STEP: " + Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z);
				}
			}
			else if (foundBool == 3 && endBool == 2){
				using (System.IO.StreamWriter file = new System.IO.StreamWriter (directory2, true)) {
					currTime += Time.deltaTime;
					file.WriteLine (Math.Round (currTime, 2) + ":  " + cube3.transform.position.x + "," + cube3.transform.position.y + "," + cube3.transform.position.z);
					endBool = 3;
					//Debug.Log ("WROTE THE STEP: " + Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z);
				}
			}
			else if (foundBool == 4 && endBool == 3){
				using (System.IO.StreamWriter file = new System.IO.StreamWriter (directory2, true)) {
					currTime += Time.deltaTime;
					file.WriteLine (Math.Round (currTime, 2) + ":  " + cube4.transform.position.x + "," + cube4.transform.position.y + "," + cube4.transform.position.z);
					endBool = 0;
					foundBool = 0;
					PlayerPrefs.SetInt("foundBool",foundBool);
					//Debug.Log ("WROTE THE STEP: " + Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z);
				}
			}

	}

	void PracticeEnvironmentTask(){
		//Debug.Log("I am in new task");
		//Debug.Log("trainingTask value: " + trainingTask);
		if(trainingTask == 1){
			cube1.SetActive(true);
			trainingTaskInstructions.text = "Please navigate to box 1.";

			//OnTriggerEnter(cube1collider);
		}
		else if(trainingTask == 2){
			cube1.SetActive(false);
			cube2.SetActive(true);
			trainingTaskInstructions.text = "Please navigate to box 2.";

			//OnTriggerEnter(cube2collider);
		}
		else if(trainingTask == 3){
			cube2.SetActive(false);
			cube3.SetActive(true);
			trainingTaskInstructions.text = "Please navigate to box 3.";

			//OnTriggerEnter(cube3collider);
		}
		else if(trainingTask == 4){
			cube3.SetActive(false);
			cube4.SetActive(true);
			trainingTaskInstructions.text = "Please navigate to box 4.";
			//OnTriggerEnter(cube4collider);
		}
		else if(trainingTask == 5){
			cube4.SetActive(false);
			trainingTaskInstructions.text = "Press T to restart the training task.";
		}

	}


	// IEnumerator ShowInstructions(){
	// 	yield return new WaitForSeconds (15);
	// 	instructionText.enabled = true;
	// }
}
