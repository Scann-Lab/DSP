using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TrainingLevel : MonoBehaviour {

	public UnityEngine.UI.Text instructionText;
	public Text moveText;
	// public Text instructionText;

	int i = 0;
	bool UsingGamePad;
	bool pressed = true;

	int count = 0;
	int count1 = 0;

	void Start(){
		GameControl.control.currentLevelPosition = new Vector3 (40f, -19.83f, 43.13f);
		// instructionText.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		//If G is pressed any time during the training phase, goes to the learning phase
		if(Time.timeSinceLevelLoad > 30){
			instructionText.enabled = true;
			instructionText.text = "When you are ready to begin, please press G.";
		}else{
			instructionText.enabled = false;
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
	}

	// IEnumerator ShowInstructions(){
	// 	yield return new WaitForSeconds (15);
	// 	instructionText.enabled = true;
	// }
}
