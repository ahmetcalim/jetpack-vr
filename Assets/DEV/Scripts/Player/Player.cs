using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Bu class Player ile ilgili özellikleri barındırmakta.
public class Player : MonoBehaviour
{
    public GameController gameController;
    public static float velocityXBase = 0.5f;
    public static float velocityXMax = 1f;
    public static float travelledDistance;
    public static float totalDistance;
    public static float gainedResource;
    public static bool isGameRunning = true;
    public static float difficulty = 0;
    public static float resourceMultipleValue = .5f;
    public Text difficultyTxt;
    public Text resourceTxt;
    public Text speedXTxt;
    
  
   
    public RocketDestroyManager rocketDestroyManager;
    public PlayerMovementController playerMovementController;
    public PowerUpController powerUpController;
    private float timer = 0f;
    public static float TravelledDistance
    {
        get
        {
            return travelledDistance;
        }
        set
        {

            travelledDistance = value;
        }
    }
    void Start()
    {
       
    }
    private void Update()
    {
        if (isGameRunning)
        {
            PrintValueToText(difficultyTxt, difficulty.ToString(), "Zorluk");
            PrintValueToText(resourceTxt, gainedResource.ToString(), "Kaynak");
            PrintValueToText(speedXTxt, (Time.deltaTime).ToString(), "Hız");
            gameController.GainResource();
         
            
        }
    }
   
    public static void ResetStaticValues()
    {
        difficulty = 0f;
        TravelledDistance = 0f;
        totalDistance = 0f;
        velocityXBase = 0.5f;
        velocityXMax = 1f;
        travelledDistance = 0f;
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
        if (other.tag == "Powerup")
        {
            Destroy(other.gameObject);

            PowerUp powerUp = other.GetComponent<PowerUp>();
            if (powerUp.powerUpName == PowerUp.PowerUpName.PHASE)
            {
                //PHASE
                powerUpController.SetPowerUp(powerUpController.phaseSprite, "Phase");

                powerUpController.isPhaseUsable = true;
             
            }
            if (powerUp.powerUpName == PowerUp.PowerUpName.ROCKET)
            {
                //ROCKET
                powerUpController.SetPowerUp(powerUpController.rocketSprite, "Rocket");

                powerUpController.isRocketUsable = true;
               
            }
            

        }
    }
  
    private void PrintValueToText(Text textObject, string value, string name)
    {
        textObject.text = name + ": " + value;
    }
   
}
