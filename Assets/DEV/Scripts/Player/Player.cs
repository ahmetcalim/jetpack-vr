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
    public static bool isPhasePowerUpActive;
    public static int powerUpCount = 0;
    public static bool isPhaseUsable;
    public static bool isRocketUsable;
    public static bool isPhaseActive;
    public static bool isRocketActive;
    public List<float> phasePowerUpDuringTime;
    public Text difficultyTxt;
    public Text resourceTxt;
    public Text speedXTxt;
    public Text phaseTimer;
    public Image powerUpSlot1;
    public Image powerUpSlot2;
    public Sprite phaseSprite;
    public Sprite rocketSprite;
  
    public Button geciciPhaseButton;
    public Button geciciRocketButton;
    public RocketDestroyManager rocketDestroyManager;
    public PlayerMovementController playerMovementController;
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
        ChangeAlpha(1f);
    }
    private void Update()
    {
        if (isGameRunning)
        {
            PrintValueToText(difficultyTxt, difficulty.ToString(), "Zorluk");
            PrintValueToText(resourceTxt, gainedResource.ToString(), "Kaynak");
            PrintValueToText(speedXTxt, (Time.deltaTime).ToString(), "Hız");
            gameController.GainResource();
            if (isPhaseActive)
            {
                PhaseTimeCount();
            }
            
        }
    }
   
    public static void ResetStaticValues()
    {
        TravelledDistance = 0;
        totalDistance = 0;
       
        isGameRunning = true;
    }
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.tag == "Obstacle" )
        {
          
            Destroy(other.gameObject);
            if (isPhaseActive == false)
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
                SetPowerUp(phaseSprite, "Phase");

                isPhaseUsable = true;
                geciciPhaseButton.GetComponentInChildren<Text>().text = "Phase: " + isPhaseUsable.ToString();
            }
            if (powerUp.powerUpName == PowerUp.PowerUpName.ROCKET)
            {
                //ROCKET
                SetPowerUp(rocketSprite, "Rocket");

                isRocketUsable = true;
                geciciPhaseButton.GetComponentInChildren<Text>().text = "Rocket: " + isRocketUsable.ToString();
            }
            

        }
    }
    public void UseRocket()
    {
        foreach (var item in rocketDestroyManager.TriggerList)
        {
            Destroy(item.gameObject);
        }
      
        powerUpCount--;
        
        isRocketUsable = false;
       
    }
    public void UsePhase()
    {
        ChangeAlpha(.2f);
        powerUpCount--;
        isPhaseUsable = false;
        isPhaseActive = true;
    }
    private void PhaseTimeCount()
    {
        if (timer<=phasePowerUpDuringTime[0])
        {
            timer += Time.deltaTime * 1f;
            PrintValueToText(phaseTimer, timer.ToString(), "Phase TimeC: ");
        }
        else
        {
            timer = 0f;
            isPhaseActive = false;
            ChangeAlpha(1f);
        }
    }
    private void SetPowerUp(Sprite powerUpSprite, string pUpTag)
    {
        powerUpCount++;
        switch (powerUpCount)
        {
            case 0:
                powerUpSlot1.sprite = null;
                powerUpSlot1.tag = "Untagged";

                powerUpSlot2.sprite = null;
                powerUpSlot2.tag = "Untagged";
                break;
            case 1:
                powerUpSlot1.sprite = powerUpSprite;
                powerUpSlot1.tag = pUpTag;
                playerMovementController.ControllerL.TriggerHapticPulse(50000);
                break;
            case 2:
                powerUpSlot2.sprite = powerUpSprite;
                powerUpSlot2.tag = pUpTag;
                playerMovementController.ControllerR.TriggerHapticPulse(50000);
                break;
            case 3:
                powerUpSlot1.sprite = powerUpSprite;
                powerUpSlot1.tag = pUpTag;
                break;
            default:
                break;
        }
    }
    private void PrintValueToText(Text textObject, string value, string name)
    {
        textObject.text = name + ": " + value;
    }
    private void ChangeAlpha(float a)
    {
        playerMovementController.leftController.GetComponent<VR_ControllerManager>().joystickRenderer.sharedMaterial.color = new Color(1f, 1f, 1f, a);

    }
}
