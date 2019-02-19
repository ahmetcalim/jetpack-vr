using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{
    public SteamVR_TrackedObject leftController;
    public SteamVR_TrackedObject rightController;
    public PowerUpController powerUpController;
    private float angleXController;
    public Player player;
    
    public PlayerMovementController playerMovementController;
    private bool doOnce;
    public SteamVR_Controller.Device ControllerL
    {
        get { return SteamVR_Controller.Input((int)leftController.index); }
    }
    public SteamVR_Controller.Device ControllerR
    {
        get { return SteamVR_Controller.Input((int)rightController.index); }
    }
    // Update is called once per frame
    private void Start()
    {
       
    }
    void FixedUpdate()
    {
        if (Player.isGameRunning)
        {
            CheckTouchpadInput();
            CheckTriggerInput();
        }
           
    }
    private void CheckTouchpadInput()
    {
        if (ControllerL.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            switch (leftController.powerUpSlot.tag)
            {
                case "Phase":
                    if (!powerUpController.isPhaseActive)
                    {
                        powerUpController.UsePhase();
                        leftController.powerUpSlot.tag = "Untagged";
                        leftController.powerUpSlot.GetComponent<Image>().sprite = null;
                    }
                    break;
                case "Rocket":
                    powerUpController.UseRocket();
                    leftController.powerUpSlot.tag = "Untagged";
                    leftController.powerUpSlot.GetComponent<Image>().sprite = null;
                    break;
                case "BulletTime":
                    powerUpController.UseBulletTime();
                    leftController.powerUpSlot.tag = "Untagged";
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
                        rightController.powerUpSlot.GetComponent<Image>().sprite = null;
                    }
                    break;
                case "Rocket":
                    powerUpController.UseRocket();
                    rightController.powerUpSlot.tag = "Untagged";
                    rightController.powerUpSlot.GetComponent<Image>().sprite = null;
                    break;
                case "BulletTime":
                    powerUpController.UseBulletTime();
                    rightController.powerUpSlot.tag = "Untagged";
                    rightController.powerUpSlot.GetComponent<Image>().sprite = null;

                    break;
                default:
                    break;
            }
        }
    }
    private void CheckTriggerInput()
    {
        

        angleXController = (leftController.transform.rotation.x + rightController.transform.rotation.x) / -2;
        if (ControllerL.GetHairTrigger() && ControllerR.GetHairTrigger())
        {
            Debug.Log(angleXController);
            if (angleXController > .2f)
            {
                playerMovementController.Up(1);
            }
            else
            {
                playerMovementController.Up(-1);
            }
        }
        if (ControllerL.GetHairTrigger() && Player.isMalfunctionActive)
        {
            
            if (ControllerL.velocity.magnitude > 3f)
            {
                Debug.Log("SOLU SALLADIM KIRDIM BEBEYİM");
            }
        }
        if (ControllerR.GetHairTrigger() && Player.isMalfunctionActive)
        {

            
            if (ControllerR.velocity.magnitude > 3f)
            {
                Debug.Log("SAĞI SALLADIM KIRDIM BEBEYİM");
            }
        }
    }
}
