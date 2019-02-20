using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDestroyManager : MonoBehaviour
{
    public List<GameObject> TriggerList = new List<GameObject>();
   
 
    //called when something enters the trigger
     void OnTriggerStay(Collider other)
    {
        //if the object is not already in the list
        if (!TriggerList.Contains(other.gameObject))
        {
            if (other.tag == "Obstacle")
            {
                //add the object to the list
                TriggerList.Add(other.gameObject);
            }
          
        }
    }
   
    //called when something exits the trigger
    void OnTriggerExit(Collider other)
    {
        //if the object is in the list
        if (TriggerList.Contains(other.gameObject))
        {
            if (other.tag == "Obstacle")
            {
                //remove it from the list
                TriggerList.Remove(other.gameObject);
            }
        }
    }
}
