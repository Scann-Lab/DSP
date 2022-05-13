using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

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

	//String names of training level and training level timed to call which commands appear on Screen
	string trainingLevel = "Training Level";
	string trainingLevelTimed = "Training Level Timed";

	//Boolean to mark start time being recorded just once.
	bool startTimePrinted = false;


	// public Text instructionText;

	int i = 0;
	int a = 0;
	int k = 0;
	int UsingGamePad;
	//Note that UsingGamePad as a playerprefs was deprecated for bug issues. All UsingGamePad player prefs switched to UsingController.


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
		Debug.Log("this is the player prefs value at wake: " + PlayerPrefs.GetInt("UsingGamePad"));
		// Debug.Log("this is the player prefs value at wake for controller: " + PlayerPrefs.GetInt("UsingController"));
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
		Scene scene = SceneManager.GetActiveScene();
		if(scene.name == trainingLevel)
		{
			//writing data
			directory = Application.dataPath +
			"\\..\\Participant_" + name +
			"_Date_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm") +
			"Training.txt";
			// Debug.Log(directory);
			directory2 = Application.dataPath +
			"\\..\\Participant_" + name +
			"_Date_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm") +
			"TrainingGoals.txt";
			// Debug.Log(directory);
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory, true))
				{
						//write in elements
						file.WriteLine ("ParticipantNo: " + GameControl.control.participantNumber);
						if(PlayerPrefs.GetInt("UsingGamePad") == 1){
							file.WriteLine ("Controller type: Gamepad");
						}else if(PlayerPrefs.GetInt("UsingKeyboard") == 1){
							file.WriteLine ("Controller type: Keyboard and Mouse");
						}else if(PlayerPrefs.GetInt("UsingArrow") == 1){
							file.WriteLine ("Controller type: Arrows Only");
						}
							file.WriteLine("Training Level:");


				}
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory2, true))
				{
						//write in elements
						file.WriteLine ("ParticipantNo: " + GameControl.control.participantNumber);
						if(PlayerPrefs.GetInt("UsingGamePad") == 1){
							file.WriteLine ("Controller type: Gamepad");
						}else if(PlayerPrefs.GetInt("UsingKeyboard") == 1){
							file.WriteLine ("Controller type: Keyboard and Mouse");
						}else if(PlayerPrefs.GetInt("UsingArrow") == 1){
							file.WriteLine ("Controller type: Arrows Only");
						}
							file.WriteLine("Training Level:");

				}
		}else if(scene.name == trainingLevelTimed)
		{
			//writing data
			directory = Application.dataPath +
			"\\..\\Participant_" + name +
			"_Date_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm") +
			"TrainingTimed.txt";
			// Debug.Log(directory);
			directory2 = Application.dataPath +
			"\\..\\Participant_" + name +
			"_Date_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm") +
			"TrainingGoalsTimed.txt";
			// Debug.Log(directory);
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory, true))
				{
						//write in elements
						file.WriteLine ("ParticipantNo: " + GameControl.control.participantNumber);
						if(PlayerPrefs.GetInt("UsingGamePad") == 1){
							file.WriteLine ("Controller type: Gamepad");
						}else if(PlayerPrefs.GetInt("UsingKeyboard") == 1){
							file.WriteLine ("Controller type: Keyboard and Mouse");
						}else if(PlayerPrefs.GetInt("UsingArrow") == 1){
							file.WriteLine ("Controller type: Arrows Only");
						}
							file.WriteLine("Training Level Timed:");

				}
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory2, true))
				{
						//write in elements
						file.WriteLine ("ParticipantNo: " + GameControl.control.participantNumber);
						if(PlayerPrefs.GetInt("UsingGamePad") == 1){
							file.WriteLine ("Controller type: Gamepad");
						}else if(PlayerPrefs.GetInt("UsingKeyboard") == 1){
							file.WriteLine ("Controller type: Keyboard and Mouse");
						}else if(PlayerPrefs.GetInt("UsingArrow") == 1){
							file.WriteLine ("Controller type: Arrows Only");
						}
							file.WriteLine("Training Level Timed:");

				}
		}


		// instructionText.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		//If G is pressed any time during the training phase, goes to the training level timed if current scene is training level and to learning if current scene is training level Timed
		Scene scene = SceneManager.GetActiveScene();

		//Removing instuctions for participant to advance the scenes. Will now be left to experimenter 04/19/2022

		// if(Time.timeSinceLevelLoad > 30 && scene.name == trainingLevel){
		// 	instructionText.enabled = true;
		// 	instructionText.text = "When you are ready to begin the timed training task, please press B.";
		// }else if(Time.timeSinceLevelLoad > 30 && scene.name == trainingLevelTimed)
		// {
		// 	instructionText.enabled = true;
		// 	instructionText.text = "When you are ready to begin the experiment, please press G.";
		// }else{
			instructionText.enabled = false;
		// }

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
		if (Input.GetKey (KeyCode.B)){
			SceneManager.LoadScene(trainingLevelTimed);
		}
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
		// Debug.Log("Using Game Pad value: " + UsingGamePad);
		if(UsingGamePad == 1 && count == 0){
			moveText.text = "Use the left joystick to walk.\nUse the right joystick to look around.";
		}else if(UsingGamePad == 0 && count ==0){
			moveText.text = "Use WASD to walk.\nUse the mouse to look around.";
		}else if(UsingGamePad == 2 && count == 0){
			moveText.text = "Use the top and bottom arrow keys to walk.\nUse the side arrow keys to look around.";
		}
		if(Input.GetKey (KeyCode.X) || Input.GetKey("joystick button 1")){
			// System.Threading.Thread.Sleep(500);
			if(pressed){
				if(count%2 == 0){
					moveText.text = "";
				}else{
					if(UsingGamePad == 1){
						moveText.text = "Use the left joystick to walk.\nUse the right joystick to look around.";
					}else if(UsingGamePad == 0){
						moveText.text = "Use WASD to walk.\nUse the mouse to look around.";
					}else if(UsingGamePad == 2){
						moveText.text = "Use the top and bottom arrow keys to walk.\nUse the side arrow keys to look around.";
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
			if(trainingTask == 1 && startTimePrinted == false){
				using (System.IO.StreamWriter file = new System.IO.StreamWriter (directory2, true)) {
					currTime += Time.deltaTime;
					file.WriteLine ("Start Time: " + Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z + "," + playerRot.eulerAngles.y);
					//Debug.Log ("WROTE THE STEP: " + Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z);
					startTimePrinted = true;
				}
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
					startTimePrinted = false;
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
