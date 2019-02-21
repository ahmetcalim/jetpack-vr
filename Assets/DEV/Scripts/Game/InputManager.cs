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
    private float timer;
    public PlayerMovementController playerMovementController;
    public List<Image> throttleLoadingBar;
    public Text throttlePowerTxt;
    public SteamVR_Controller.Device ControllerL
    {
        get { return SteamVR_Controller.Input((int)leftController.index); }
    }
    public SteamVR_Controller.Device ControllerR
    {
        get { return SteamVR_Controller.Input((int)rightController.index); }
    }
    void FixedUpdate()
    {
        if (Player.isGameRunning)
        {
            CheckTouchpadInput();
            CheckTriggerInput();
            CheckGripInput();
        }
    }
    private void CheckTouchpadInput()
    {
        if (ControllerL.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            UsePowerup(leftController);
        }
        if (ControllerR.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            UsePowerup(rightController);
        }
    }
    private void UsePowerup(SteamVR_TrackedObject controller)
    {
     
        switch (controller.powerUpSlot.tag)
        {
            case "Phase":
                if (!powerUpController.isPhaseActive)
                {
                    powerUpController.UsePhase();
                    controller.powerUpSlot.tag = "Untagged";
                    controller.powerUpSlot.GetComponent<Image>().sprite = null;
                }
                break;
            case "Rocket":
                powerUpController.UseRocket();
                controller.powerUpSlot.tag = "Untagged";
                controller.powerUpSlot.GetComponent<Image>().sprite = null;
                break;
            case "BulletTime":
                powerUpController.UseBulletTime();
                controller.powerUpSlot.tag = "Untagged";
                controller.powerUpSlot.GetComponent<Image>().sprite = null;
                break;
            default:
                break;
        }
    }
    private void CheckTriggerInput()
    {
        angleXController = (leftController.transform.rotation.x + rightController.transform.rotation.x) / -2;
        if (ControllerL.GetHairTrigger() && ControllerR.GetHairTrigger())
        {
            
            if (timer <=1f)
            {
                timer += Time.deltaTime;
                if (throttleLoadingBar[(int)(timer * 10f)].enabled == false)
                {
                    throttleLoadingBar[(int)(timer * 10f)].enabled = true;
                }
                throttlePowerTxt.text = "%" + ((int)(timer * 100f)).ToString();

            }
            if (angleXController > 0f)
            {
                playerMovementController.Up(1);
            }
            else
            {
                playerMovementController.Up(-1);
            }
        }
        else
        {
            if (timer >= 0f)
            {
                timer -= Time.deltaTime;
                if (throttleLoadingBar[(int)(timer*10f) + 1].enabled == true)
                {
                    throttleLoadingBar[(int)(timer*10f) + 1].enabled = false;
                }
                if (timer<=0.01f)
                {
                    throttleLoadingBar[0].enabled = false;
                }
                throttlePowerTxt.text = "%" + ((int)(timer * 100f) + 1).ToString();
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
    private void CheckGripInput()
    {
        if (ControllerL.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
        }
        
    }
}
