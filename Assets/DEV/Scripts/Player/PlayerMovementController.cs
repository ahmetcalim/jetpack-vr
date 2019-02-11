using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovementController : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 targetTransform;
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
    private float angleXController;
    public AudioSource powerupUsingAudioSource;
    //Bunlar powerup class ının içerisine alınacak.
    public AudioClip phaseAudioClip;
    public AudioClip rocketAudioClip;
    public AudioClip bulletTimeAudioClip;
    ///-----////------////------///----////
    public AudioSource rocketAudioSource;
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
    }
    void Update()
    {
        if (Player.isGameRunning == true)
        {
            angleXController = (leftController.transform.rotation.x + rightController.transform.rotation.x) / -2;
            Player.difficulty = Mathf.Pow(3, ((Time.time * 2) / (90 + Time.time)) + 1) - 3;
            if (ControllerL.GetHairTrigger() && ControllerR.GetHairTrigger())
            {
                if (!rocketAudioSource.isPlaying)
                {
                    rocketAudioSource.Play();
                }
                if (rocketAudioSource.volume <=.7f)
                {
                    rocketAudioSource.volume += (Time.deltaTime/2f);
                }
                if (angleXController >0)
                {
                    Up(1);
                }
                else
                {
                    Up(-1);
                }
            }
            else
            {
                
                if (rocketAudioSource.volume>=0f)
                {
                    rocketAudioSource.volume -= (Time.deltaTime/3f);
                }
            }
            if (ControllerL.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                switch (leftController.powerUpSlot.tag)
                {
                    case "Phase":
                        if (!powerUpController.isPhaseActive)
                        {
                        powerUpController.UsePhase();
                        leftController.powerUpSlot.tag = "Untagged";
                        powerupUsingAudioSource.clip = phaseAudioClip;
                        powerupUsingAudioSource.Play();
                        leftController.powerUpSlot.GetComponent<Image>().sprite = null;
                        }
                        break;
                    case "Rocket":
                        powerUpController.UseRocket();
                        leftController.powerUpSlot.tag = "Untagged";
                        powerupUsingAudioSource.clip = rocketAudioClip;
                        powerupUsingAudioSource.Play();
                        leftController.powerUpSlot.GetComponent<Image>().sprite = null;
                        break;
                    case "BulletTime":
                        powerUpController.UseBulletTime();
                        leftController.powerUpSlot.tag = "Untagged";
                        powerupUsingAudioSource.Play();
                        leftController.powerUpSlot.GetComponent<Image>().sprite = null;
                    
                        break;
                    default:
                        break;
                }
            
            }
            if (ControllerR.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                switch (rightController.powerUpSlot.tag)
                {
                    case "Phase":
                        if (!powerUpController.isPhaseActive)
                        {
                        powerUpController.UsePhase();
                        rightController.powerUpSlot.tag = "Untagged";
                        powerupUsingAudioSource.clip = phaseAudioClip;
                        powerupUsingAudioSource.Play();
                        rightController.powerUpSlot.GetComponent<Image>().sprite = null;
                        }
                        break;
                    case "Rocket":
                        powerUpController.UseRocket();
                        rightController.powerUpSlot.tag = "Untagged";
                        powerupUsingAudioSource.clip = rocketAudioClip;
                        powerupUsingAudioSource.Play();
                        rightController.powerUpSlot.GetComponent<Image>().sprite = null;
                        break;
                    case "BulletTime":
                        powerUpController.UseBulletTime();
                        rightController.powerUpSlot.tag = "Untagged";
                        powerupUsingAudioSource.Play();
                        rightController.powerUpSlot.GetComponent<Image>().sprite = null;
                       
                        break;
                    default:
                        break;
                }
            }
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
            playerTransform.GetComponent<Rigidbody>().velocity = new Vector3(0, side, 0f) * accelerationY;
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
