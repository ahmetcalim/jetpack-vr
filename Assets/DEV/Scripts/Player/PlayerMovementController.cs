using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovementController : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 targetTransform;
  
    public enum PlayerDirection { X, Y, Z };
    public PlayerDirection playerDirection;
   
    public bool isVREnabled;
    public Player player;
    public PowerUpController powerUpController;
    private float playerStartPositionZ;
    public float accelerationY;
    public static float constant1 = 44; 
    public static  float constant2 = 100;
    public static float constant3 = 3;
    private bool twoTriggerPressed;
    public SteamVR_TrackedObject leftController;
    public SteamVR_TrackedObject rightController;
    public SteamVR_Controller.Device ControllerL
    {
        get { return SteamVR_Controller.Input((int)leftController.index); }
    }
    public SteamVR_Controller.Device ControllerR
    {
        get { return SteamVR_Controller.Input((int)rightController.index); }
    }

    private void Start()
    {
        playerStartPositionZ = playerTransform.position.z;
        if (isVREnabled == true)
        {
           // SteamVR_Controller.Device.onTriggerPress += Up;
        }
      
    }
    void Update()
    {
       
        if (Player.isGameRunning == true)
        {
            

            Player.difficulty = Mathf.Pow(3, ((Time.time * 2) / (90 + Time.time)) + 1) - 3;
            if (ControllerL.GetHairTrigger() && ControllerR.GetHairTrigger())
            {
                twoTriggerPressed = true;
                Up(0);
            }
            else
            {
                twoTriggerPressed = false;
            }
            if (ControllerL.GetHairTrigger() && twoTriggerPressed == false)
            {
                Up(1);
            }
            if (ControllerR.GetHairTrigger() && twoTriggerPressed == false)
            {
                Up(-1);
            }
            if (Input.GetKey(KeyCode.R))
            {
                FindObjectOfType<GameController>().RestartGame();
            }
            if (ControllerL.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                Debug.Log("Sol taçped");
                //Sol touchpad dokunuldu
                switch (leftController.powerUpSlot.tag)
                {
                    case "Phase":
                        Debug.Log("Phase Kullanılmalı");
                        powerUpController.UsePhase();
                        leftController.powerUpSlot.GetComponent<Image>().sprite = null;
                        break;
                    case "Rocket":
                        Debug.Log("Rocket Kullanılmalı");
                        powerUpController.UseRocket();
                        leftController.powerUpSlot.GetComponent<Image>().sprite = null;
                        break;
                        
                    default:
                        break;
                }
            
            }
            if (ControllerR.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                Debug.Log("Sağ taçped");
                //Sağ touchpad dokunuldu
                switch (rightController.powerUpSlot.tag)
                {
                    case "Phase":
                        Debug.Log("Phase Kullanılmalı");
                        powerUpController.UsePhase();
                        rightController.powerUpSlot.GetComponent<Image>().sprite = null;
                        break;
                    case "Rocket":
                        Debug.Log("Rocket Kullanılmalı");
                        powerUpController.UseRocket();
                        rightController.powerUpSlot.GetComponent<Image>().sprite = null;
                        break;

                    default:
                        break;
                }
            }


            MovePlayerForward(Player.velocityXBase);
           
            IncreaseTravveledDistance();
        }
    }
    public float CalculateaY()
    {
        accelerationY = constant3 * 1f * (constant1 / constant2);
        return accelerationY;
    }
    
    public void Up(float side)
    {
        if (Player.isGameRunning == true)
        {

            accelerationY = Mathf.Pow(constant3 * (Time.deltaTime + 0.01f), constant1 / constant2) + 9;


            playerTransform.GetComponent<Rigidbody>().velocity = new Vector3(side, 1f, 0f) * accelerationY;
        }
    }

    private void MovePlayerForward(float vZValue)
    {
        if (vZValue<=1f)
        {
            vZValue += (Time.realtimeSinceStartup * (0.5f / 300));
        }
       

        playerTransform.Translate(Vector3.forward * vZValue);
    }
    private void IncreaseTravveledDistance()
    {
        Player.TravelledDistance = playerTransform.position.z - playerStartPositionZ;
       
    }
    private void SetTargetTransform(float side)
    {
        targetTransform = new Vector3(playerTransform.position.x + side, playerTransform.position.y, playerTransform.position.z);
    }
  
}
