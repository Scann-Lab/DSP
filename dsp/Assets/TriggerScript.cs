using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public Collider person;

    public int nextCubeNum;
    private int foundBool;
    private int endBool = 0;
    // Start is called before the first frame update
    void Start()
    {
      foundBool = PlayerPrefs.GetInt("foundBool");
      PlayerPrefs.SetInt("endBool", endBool);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider person){
      if(person.gameObject.name.Contains("Player")){
          // Debug.Log("Collision");
          if (endBool == 0){
            foundBool++;
            PlayerPrefs.SetInt("foundBool", foundBool);
            endBool = 1;
            PlayerPrefs.SetInt("endBool", endBool);
          }
          PlayerPrefs.SetInt("trainingTask",nextCubeNum);
          //PlayerPrefs.SetFloat("cubeLocation",transform.position);
      }

  	}
}
