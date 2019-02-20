using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBehaviour : MonoBehaviour
{
    public float powerOfAnarchy;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.right * (powerOfAnarchy + Player.difficulty), ForceMode.Impulse);
        }
    }
}
