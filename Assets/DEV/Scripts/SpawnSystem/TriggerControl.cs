using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    public NodeGraph nGraph;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("Section")) 
        {
            nGraph.Generate();
        }
        else
        {
            Destroy(other.gameObject);
        }
          
    }
}
