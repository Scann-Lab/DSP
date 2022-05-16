using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;

public class rankingLevelScript : MonoBehaviour
{

public InputField inputField;
public Button button;

String directory;

    // Start is called before the first frame update
    void Start()
    {
      Cursor.visible = true;
  		Cursor.lockState = CursorLockMode.None;

      directory = Application.dataPath + "\\..\\Participant_" + GameControl.control.participantNumber + "Ranking.txt";

      //If the directory does not exist, create it and write the two initial lines to it. If not, do not write the initial lines.
      if(GameControl.control.currentLevelNo == 1)
      {
        using(System.IO.StreamWriter file = new System.IO.StreamWriter(directory, true))
        {
          file.WriteLine ("ParticipantRankingAfterEachTrial");
          file.WriteLine ("ParticipantNo: " + GameControl.control.participantNumber);
        }
      }
    }

    // Update is called once per frame
    void Update()
    {
        // button.onClick.AddListener(taskOnClick);
        if(Input.GetKeyDown("return"))
        {
          taskOnClick();
        }
    }

    public void taskOnClick()
    {
      int ranking = int.Parse(inputField.text);
      if(ranking >= 1 && ranking <= 7)
      {
        Debug.Log("Answer is: " + ranking);
        Cursor.visible = false;
    		Cursor.lockState = CursorLockMode.Locked;
        using(System.IO.StreamWriter file = new System.IO.StreamWriter(directory, true))
        {
          file.WriteLine ("TrialNo: " + GameControl.control.currentLevelNo);
          file.WriteLine ("Answer: " + ranking);
        }
        // UnityEngine.SceneManagement.SceneManager.LoadScene ("Pre-Level Screen");
        GameControl.control.rollNextLevel ();
      }else{
        Debug.Log("Incorrect answer, please try again.");
      }
      // Debug.Log("Clicked: " + ranking);
      //
      // GameControl.control.rollNextLevel ();

    }
}
