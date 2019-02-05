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
        spawnPoint = new Vector3(-0.57f, 5.9f, startTransform.position.z + 38.4f);

        GameObject copy = Instantiate(objs[0], spawnPoint, Quaternion.identity);
        copy.transform.SetParent(parentTransform);
        copy.transform.position = new Vector3(0.57f, 5.9f, startTransform.position.z + 38.4f);

        startTransform = copy.transform;
        spawnPoint = new Vector3(0.57f, 5.9f, startTransform.position.z + 38.4f);

        GameObject copy2= Instantiate(objs[1], spawnPoint, Quaternion.identity);
        copy2.transform.SetParent(parentTransform);
        copy2.transform.position = new Vector3(0.57f, 5.9f, startTransform.position.z + 38.4f);

        startTransform = copy2.transform;

    }
}
