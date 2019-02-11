﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Bu class Player ile ilgili özellikleri barındırmakta.
public class Player : MonoBehaviour
{
    public GameController gameController;
    public float velocityXBase = 50;
    public static float velocityXMax = 100f;
    public float travelledDistance;
    public static float totalDistance;
    private float gainedResource;
    public static bool isGameRunning = true;
    public static float difficulty = 0;
    public static float resourceMultipleValue = .5f;
    public static bool isMalfunctionActive;

    public Text resourceTxt;
    public Text speedXTxt;
    public Text bonusFeedBackTxt;
    public RocketDestroyManager rocketDestroyManager;
    public PlayerMovementController playerMovementController;
    public PowerUpController powerUpController;
    private float timer = 0f;

    private void Start()
    {
       
    
    }
    private void Update()
    {
        if (isGameRunning)
        {
           
            PrintValueToText(resourceTxt, (gainedResource).ToString(), "");
            PrintValueToText(speedXTxt, velocityXBase.ToString(), "");
            GainResource();
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                ResetStaticValues();
                SceneManager.LoadScene(0);
            }
        }
    }
    public void GainResource()
    {
        gainedResource = (travelledDistance * difficulty) * resourceMultipleValue;
    }
    public static void ResetStaticValues()
    {
        isMalfunctionActive = false;
        difficulty = 0f;
        totalDistance = 0f;
       
        resourceMultipleValue = .5f;
        isGameRunning = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" )
        {
            Destroy(other.gameObject);
            if (powerUpController.isPhaseActive == false)
            {
                isGameRunning = false;
                Debug.Log("GameOver");

                PlayerPrefs.SetFloat("gResource", PlayerPrefs.GetFloat("gResource") + gainedResource);
            }
        }
        if (other.tag == "Malfunction")
        {
            powerUpController.ActivateMalfunction();
        }
        if (other.tag == "Powerup")
        {
            Destroy(other.gameObject);
            PowerUp powerUp = other.GetComponent<PowerUp>();
            PrintValueToText(bonusFeedBackTxt, powerUp.powerUpType.ToString() + " Bonus Alındı.", "");
            bonusFeedBackTxt.GetComponent<Animator>().SetTrigger("Feedback");
            powerUpController.SetPowerUp(powerUp.sprite, powerUp.tagName);
           
        }
    }
    private void PrintValueToText(Text textObject, string value, string name)
    {
        textObject.text = name + ": " + value;
    }
   
}
