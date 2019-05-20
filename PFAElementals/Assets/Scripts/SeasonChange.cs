using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonChange : MonoBehaviour
{

    public Material[] materialToChange;
    public Renderer[] gameObjectToChange;



         // Update is called once per frame
         void Update()
         {
             if(Input.GetKeyDown (KeyCode.Space))
             {
                 gameObjectToChange[0].material = materialToChange[0];
             }
         }
         
}