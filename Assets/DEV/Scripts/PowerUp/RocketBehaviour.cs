using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    public GameObject rocketPrefab;
    public List<GameObject> targets;
    private List<GameObject> copys = new List<GameObject>();
    private bool isActive;
    public void UseRocket(int numOfRocket)
    {
        
        for (int i = 0; i < numOfRocket; i++)
        {
            var copy = Instantiate(rocketPrefab, transform.position, Quaternion.identity);
            copys.Add(copy);

        }
        isActive = true;
    }
    private void FixedUpdate()
    {
        if (isActive == true)
        {
            copys[0].GetComponent<Rigidbody>().velocity = copys[0].transform.forward * 5f;
            Quaternion targetRotation = Quaternion.LookRotation(targets[0].transform.position - transform.position);
            copys[0].GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(copys[0].transform.rotation, targets[0].transform.rotation, 500));
        }
    }
}
