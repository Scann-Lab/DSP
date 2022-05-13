using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSelect : MonoBehaviour
{

    //What is the time that will cause the trials to time out? in seconds
    public float trialTime;
    InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
      inputField = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        trialTime = float.Parse(inputField.text);
        //Debug.Log("This is the trial time: " + trialTime);
        PlayerPrefs.SetFloat("trialTime", trialTime);
    }
}
