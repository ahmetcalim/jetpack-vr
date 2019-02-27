using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovementController : MonoBehaviour
{
    public SteamVR_TrackedObject leftController;
    public SteamVR_TrackedObject rightController;


    public Transform playerTransform;
    private Vector3 targetTransform;
    public Player player;
    public float accelerationY;
    public static float yMovementConstant_1 = 44; 
    public static  float yMovementConstant_2 = 100;
    public static float yMovementConstant_3 = 3;
    private float accelerationYConstant = 9f;
   
    public SteamVR_Controller.Device ControllerL
    {
        get { return SteamVR_Controller.Input((int)leftController.index); }
    }
    public SteamVR_Controller.Device ControllerR
    {
        get { return SteamVR_Controller.Input((int)rightController.index); }
    }
    public void Up(float side)
    {
        if (Player.isGameRunning == true && Player.isMalfunctionActive == false)
        {
            accelerationY = Mathf.Pow(yMovementConstant_3 * (Time.deltaTime + 0.01f), yMovementConstant_1 / yMovementConstant_2) + accelerationYConstant;
            if (side < 0)
            {
                accelerationY *= 4f;
            }
            playerTransform.GetComponent<Rigidbody>().velocity = new Vector3(0, side, 0f) * accelerationY * PowerUpController.bulletTimeMultipleValue;
        }
    }
  
  
}
