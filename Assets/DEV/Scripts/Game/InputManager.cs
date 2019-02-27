﻿using System.Collections;
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
    public List<Image> dashBoardImageElements;
    public List<Text> dashBoardTextElements;
    public bool isDashboardVisible = true;
    private float accelerationY;
    public static float yMovementConstant_1 = 44;
    public static float yMovementConstant_2 = 100;
    public static float yMovementConstant_3 = 3;
    public GameObject playerTransform;
    public AudioSource jetpackThrottle;
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
        if (Player.IsGameRunning())
        {
            CheckTouchpadInput();
            CheckTriggerInput();
            CheckGripInput();
        }
    }
    private void Start()
    {
       
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
            if (!jetpackThrottle.isPlaying)
            {
              
            }
            if (timer <=1f)
            {
                
                timer += Time.deltaTime;
                jetpackThrottle.volume += timer/50f;
                if (throttleLoadingBar[(int)(timer * 10f)].enabled == false)
                {
                    throttleLoadingBar[(int)(timer * 10f)].enabled = true;
                }
                throttlePowerTxt.text = "%" + ((int)(timer * 100f)).ToString();

            }
            if (angleXController > 0f)
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
            if (timer >= 0f)
            {
                
                timer -= Time.deltaTime;
                jetpackThrottle.volume -= timer/50f;
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
            ChangeDashboardVisibleAfterFrame();
        }
        
    }
    private void ChangeDashboardVisibility(List<Text> texts, List<Image> images, float visibilityAmount)
    {
        foreach (var item in images)
        {
            item.color = new Color(1f, 1f, 1f, visibilityAmount);
        }
        foreach (var item in texts)
        {
            item.color = new Color(1f, 1f, 1f, visibilityAmount);
        }
    }
    public void ChangeDashboardVisibleAfterFrame()
    {
        if (isDashboardVisible == true)
        {
            StartCoroutine(DashboardVisible(false));
            ChangeDashboardVisibility(dashBoardTextElements, dashBoardImageElements, .05f);
        }
        else
        {
            StartCoroutine(DashboardVisible(true));
            ChangeDashboardVisibility(dashBoardTextElements, dashBoardImageElements, 1f);
        }
    }
    IEnumerator DashboardVisible(bool state)
    {
        yield return new WaitForSeconds(.1f);
        isDashboardVisible = state;
    }
    public void Up(float side)
    {
        if (Player.isGameRunning == true && Player.isMalfunctionActive == false)
        {
            
            accelerationY = Mathf.Pow(yMovementConstant_3 * (Time.deltaTime + 0.03f), yMovementConstant_1 / yMovementConstant_2);
            if (side < 0)
            {
                accelerationY *= 4f;
            }
            switch (side)
            {
                case 1:
                    playerTransform.GetComponent<Rigidbody>().velocity += Vector3.up *accelerationY * PowerUpController.bulletTimeMultipleValue;
                    //playerTransform.GetComponent<Rigidbody>().AddForce(Vector3.up * accelerationY * PowerUpController.bulletTimeMultipleValue, ForceMode.Impulse);
                    break;
                case -1:
                   // playerTransform.GetComponent<Rigidbody>().AddForce(Vector3.down * accelerationY * PowerUpController.bulletTimeMultipleValue, ForceMode.Impulse);
                   playerTransform.GetComponent<Rigidbody>().velocity += Vector3.down * accelerationY * PowerUpController.bulletTimeMultipleValue;
                    break;
                default:
                    break;
            }
            
        }
    }
}
