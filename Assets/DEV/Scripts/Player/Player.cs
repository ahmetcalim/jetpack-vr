using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Bu class Player ile ilgili özellikleri barındırmakta.
public class Player : MonoBehaviour
{
    public GameController gameController;
    public float velocityXBase = 0.1f;
    public static float velocityXMax = 1f;
    public float travelledDistance;
    public static float totalDistance;
    public static float gainedResource;
    public static bool isGameRunning = true;
    public static float difficulty = 0;
    public static float resourceMultipleValue = .5f;
    public static bool isMalfunctionActive;
    public Text difficultyTxt;
    public Text resourceTxt;
    public Text speedXTxt;
    public Text bonusFeedBackTxt;
    public RocketDestroyManager rocketDestroyManager;
    public PlayerMovementController playerMovementController;
    public PowerUpController powerUpController;
    private float timer = 0f;

    private void Start()
    {
        if (PlayerPrefsManager.GetPlayerValueFromPlayerPrefs("gainedResource") != 0)
        {
            gainedResource = PlayerPrefsManager.GetPlayerValueFromPlayerPrefs("gainedResource");
        }
       
    }
    private void Update()
    {
        if (isGameRunning)
        {
            PrintValueToText(difficultyTxt, difficulty.ToString(), "Zorluk");
            PrintValueToText(resourceTxt, gainedResource.ToString(), "Kaynak");
            PrintValueToText(speedXTxt, (travelledDistance / Time.time).ToString(), "Hız");
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
        PlayerPrefsManager.SetToPlayerPrefs(gainedResource, "gainedResource");
    }
    public static void ResetStaticValues()
    {
        isMalfunctionActive = false;
        difficulty = 0f;
        totalDistance = 0f;
        gainedResource = 0f;
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
                gameController.GameOverEvents();
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
            if (powerUp.powerUpType == PowerUp.PowerUpType.PHASE)
            {
                PrintValueToText(bonusFeedBackTxt, "Phase Bonus Alındı.", "");
                bonusFeedBackTxt.GetComponent<Animator>().SetTrigger("Feedback");
                //PHASE
                powerUpController.SetPowerUp(powerUpController.phaseSprite, "Phase");
            }
            if (powerUp.powerUpType == PowerUp.PowerUpType.ROCKET)
            {
                PrintValueToText(bonusFeedBackTxt, "Roket Bonus Alındı.", "");
                bonusFeedBackTxt.GetComponent<Animator>().SetTrigger("Feedback");
                //ROCKET
                powerUpController.SetPowerUp(powerUpController.rocketSprite, "Rocket");
            }
        }
    }
    private void PrintValueToText(Text textObject, string value, string name)
    {
        textObject.text = name + ": " + value;
    }
   
}
