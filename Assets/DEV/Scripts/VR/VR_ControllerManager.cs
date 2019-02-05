using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VR_ControllerManager : MonoBehaviour {


    
    private SteamVR_TrackedObject trackedObj;
    public delegate void OnTriggerDownDelegate();
    public static OnTriggerDownDelegate onTriggerDown;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public GameObject triggerPrefab;
    private Vector3 targetTransform;
    public Transform playerTransform;
    private float angle;
    private float cameraAngle;
    public float turnConstant = 10f;
    private GameObject collidingObject;
    public MeshRenderer joystickRenderer;
    private GameObject objectInHand;
   
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
  
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    private void SetCollidingObject(Collider col)
    {
        
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
       
        collidingObject = col.gameObject;
    }
    private void Update()
    {
        
        if (Player.isGameRunning == true)
        {
            Turn();
        }
    }
    private void Turn()
    {
        
            float x = Controller.GetAxis(triggerButton).x;
            triggerPrefab.transform.localRotation = Quaternion.Euler(-x * 15, 0, 0);
            angle = this.transform.rotation.eulerAngles.z;


            if (playerTransform.position.x >= -11f && playerTransform.position.x <= 11f)
            {


                if ((angle >= 30f && angle < 150))
                {
                    turnConstant = (((angle - 30))) / 6f;
                    targetTransform = new Vector3(playerTransform.position.x - 3, playerTransform.position.y, playerTransform.position.z);
                    playerTransform.transform.position = Vector3.Lerp(playerTransform.position, targetTransform, Time.deltaTime * turnConstant);
                }
                if (angle >= 210f && angle < 330f)
                {


                    turnConstant = ((Mathf.Abs((330f - angle)))) / 6f;
                    targetTransform = new Vector3(playerTransform.position.x + 3, playerTransform.position.y, playerTransform.transform.position.z);
                    //playerTransform.transform.position += new Vector3(Time.deltaTime * turnConstant / 3f, playerTransform.position.y, playerTransform.position.z);
                    playerTransform.transform.position = Vector3.Lerp(playerTransform.position, targetTransform, Time.deltaTime * turnConstant);
                }
            }
            else
            {
                if (playerTransform.position.x < -11f)
                {
                    playerTransform.position = new Vector3(-11f, playerTransform.position.y, playerTransform.position.z);

                }
                if (playerTransform.position.x > 11f)
                {
                    playerTransform.position = new Vector3(11f, playerTransform.position.y, playerTransform.position.z);

                }
            }

        


    }
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }
}


