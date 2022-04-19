using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsSelect : MonoBehaviour
{

      public Toggle arrow;
      int i;
      // Start is called before the first frame update
      void Start()
      {
          arrow = GetComponent<Toggle>();

      }

      // Update is called once per frame
      void Update()
      {
        if(arrow.isOn){
          //Debug.Log("i is 1 in load");
          i = 1;
        }
        // }else{
        //   //Debug.Log("i is 0 in load");
        //   i = 0;
        // }
        PlayerPrefs.SetInt("UsingArrow", i);
      }
}
