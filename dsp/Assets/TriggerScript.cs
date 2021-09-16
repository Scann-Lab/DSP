using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public Collider person;

    public int nextCubeNum;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider person){
      if(person.gameObject.name.Contains("Player")){
          Debug.Log("Collision");
          PlayerPrefs.SetInt("trainingTask",nextCubeNum);
          //PlayerPrefs.SetFloat("cubeLocation",transform.position);
      }

  	}
}
