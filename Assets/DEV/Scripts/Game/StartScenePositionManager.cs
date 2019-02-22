using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScenePositionManager : MonoBehaviour
{
    public Canvas canvas;
    Ray RayOrigin;
    RaycastHit HitInfo;
    public Transform canvasParent;
    public Transform targetTransform;
    // Use this for initialization
    void Start()
    {
       
        StartCoroutine(SpawnMenuCanvas());
    }
  
    IEnumerator SpawnMenuCanvas()
    {
        yield return new WaitForSeconds(1f);
        //canvas.transform.SetParent(canvasParent);
        //canvas.GetComponent<RectTransform>().localPosition = new Vector3(canvas.GetComponent<RectTransform>().localPosition.x, 1f, 6f);
        //canvas.GetComponent<RectTransform>().LookAt(targetTransform);
        canvas.gameObject.SetActive(true);

        
    }
    
}
