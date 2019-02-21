using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloserPassBehaviour : MonoBehaviour
{
    Collider otherC;
    private void OnTriggerEnter(Collider other)
    {
        otherC = other;
        StartCoroutine(DoWeee());
    }
    IEnumerator DoWeee()
    {
        yield return new WaitForSeconds(.2f);
        if (Player.isGameRunning)
        {
            if (otherC.tag == "Obstacle")
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
