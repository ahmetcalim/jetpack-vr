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
    private float amountHeat;
    public GameObject heatFill;
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
    void Update()
    {
        if (Player.isGameRunning)
        {
            heatFill.transform.localScale = new Vector3(heatFill.transform.localScale.x, (player.heat-0f)/(player.maxHeatValue - 0), heatFill.transform.localScale.z);
            

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
            if (amountHeat != 75)
            {
                amountHeat = 75;
            }
            if (doOnce == false)
            {
                doOnce = true;
                player.heat = 75;
                StartCoroutine(EveryXSeconds());
            }
            if (angleXController > 0)
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
            if (amountHeat != -100)
            {
                amountHeat = -100;
            }
        }

    }
    IEnumerator EveryXSeconds()
    {
        yield return new WaitForSeconds(1f);

        if (player.heat >= 75 && player.heat < 1500)
        {
            
            if (player.heat <0 )
            {
                player.heat = 0;
            }
           
           
            player.heat += amountHeat;
        }
       
        
        StartCoroutine(EveryXSeconds());
    }
}