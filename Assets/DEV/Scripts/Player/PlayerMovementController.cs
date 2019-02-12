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
    public static float constant1 = 44; 
    public static  float constant2 = 100;
    public static float constant3 = 3;
   
    public AudioClip rocketAudioClip;
    public AudioSource rocketAudioSource;
    public SteamVR_Controller.Device ControllerL
    {
        get { return SteamVR_Controller.Input((int)leftController.index); }
    }
    public SteamVR_Controller.Device ControllerR
    {
        get { return SteamVR_Controller.Input((int)rightController.index); }
    }
    void Update()
    {
        if (Player.isGameRunning == true)
        {
            Player.difficulty = Mathf.Pow(3, ((Time.time * 2) / (90 + Time.time)) + 1) - 3;
            //MovePlayerForward();
            IncreaseTravveledDistance();
        }
    }
    public void Up(float side)
    {
        if (Player.isGameRunning == true && Player.isMalfunctionActive == false)
        {
            accelerationY = Mathf.Pow(constant3 * (Time.deltaTime + 0.01f), constant1 / constant2) + 9;
            if (side < 0)
            {
                accelerationY *= 2f;
            }
            playerTransform.GetComponent<Rigidbody>().velocity = new Vector3(0, side, 0f) * accelerationY * PowerUpController.bulletTimeMultipleValue;
        }
    }
    /*private void MovePlayerForward()
    {
        if (player.velocityXBase <= 1f)
        {
            player.velocityXBase += (Time.realtimeSinceStartup * (0.5f / 30000));
        }
        playerTransform.Translate(Vector3.forward * player.velocityXBase);
    }*/
    private void IncreaseTravveledDistance()
    {
        player.travelledDistance = (Time.time * player.velocityXBase);
    }
    private void SetTargetTransform(float side)
    {
        targetTransform = new Vector3(playerTransform.position.x + side, playerTransform.position.y, playerTransform.position.z);
    }
  
}
