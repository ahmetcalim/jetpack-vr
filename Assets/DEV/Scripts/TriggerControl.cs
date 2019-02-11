using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    public NodeGraph nGraph;

    private void OnTriggerEnter(Collider other)
    {
           nGraph.Generate();
    }
}
