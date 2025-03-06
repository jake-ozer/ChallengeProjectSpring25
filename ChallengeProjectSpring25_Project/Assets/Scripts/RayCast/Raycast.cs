using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Raycast : MonoBehaviour { 

    

    void Update() 
    {
       if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 100f)) //array will start at same position as object and travel forward
        {
            Debug.Log("raycast has hit an object");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red);
        }
        else
        {
            Debug.Log("No object has been hit");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100f, Color.blue);
        }
                
    }
}
