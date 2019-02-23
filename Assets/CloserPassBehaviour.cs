using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloserPassBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (Player.IsGameRunning())
        {
            if (other.tag == "Obstacle" && FindObjectOfType<PowerUpController>().isPhaseActive == false)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
 
}
