using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBuilder : MonoBehaviour
{
    public List<GameObject> objs;
    private Vector3 spawnPoint;
    public Transform startTransform;
    public Transform parentTransform;
    public void BuildObject()
    {
        spawnPoint = new Vector3(1.4305e-06f, 0f, startTransform.position.z + 19.2f);

        GameObject copy = Instantiate(objs[Random.Range(0,2)], spawnPoint, Quaternion.identity);
        copy.transform.SetParent(parentTransform);
        startTransform = copy.transform;

    }
}
