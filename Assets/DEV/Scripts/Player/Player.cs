using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Bu class Player ile ilgili özellikleri barındırmakta.
public class Player : MonoBehaviour
{
    [Header("Malfunction")]
    public static bool isMalfunctionActive;
    [Header("Z Hız")]
    public float velocityZBase = 40;
    public static float velocityZMax = 100f;
    public float velocityIncreaseAmount = 0.03f;
    private float timer = 0f;
    private float nextActionTime = 0.0f;
    private float period = 0.1f;

    [Header("Player Özellikler")]
    public float travelledDistance;
    public static float totalDistance;
    private float gainedResource;
    public static bool isGameRunning = true;
    public static float difficulty = 0;
    public static float resourceMultipleValue = .5f;
    public Transform playerPawnTransform;
    public GameObject UIVROrigin;
    public GameObject VRGameOrigin;
    public Transform tunnelTransform;
    public PlayerMovementController playerMovementController;

    [Header("Bbonuslar ve feedbackler")]
    public Text resourceTxt;
    public Text speedXTxt;
    public Text bonusFeedBackTxt;
    public RocketDestroyManager rocketDestroyManager;
    public PowerUpController powerUpController;

    private void Start()
    {
        isGameRunning = true;
        UpdatePrefs();
    }
    private void Update()
    {
        if (isGameRunning)
        {
           PrintValueToText(resourceTxt, (gainedResource).ToString(), "Kaynak");
           PrintValueToText(speedXTxt, velocityZBase.ToString(), "Hız");
           GainResource();
           if (velocityZBase <= velocityZMax)
           {
                if (Time.timeSinceLevelLoad > nextActionTime)
                {
                    nextActionTime += period;
                    velocityZBase += velocityIncreaseAmount;
                }
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" )
        {
            Destroy(other.gameObject);
            if (powerUpController.isPhaseActive == false)
            {
                ResetStaticValues();
                Destroy(playerPawnTransform.gameObject.GetComponent<Rigidbody>());
                foreach (var item in rocketDestroyManager.TriggerList)
                {
                    Destroy(item.gameObject);
                }
                UIVROrigin.SetActive(true);
                VRGameOrigin.SetActive(false);
                isGameRunning = false;
                PlayerPrefs.SetFloat("gResource", PlayerPrefs.GetFloat("gResource") + gainedResource);
            }
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
    private void UpdatePrefs()
    {
        if (PlayerPrefs.GetFloat("velocityIncreaseAmount") > velocityIncreaseAmount)
        {
            velocityIncreaseAmount = PlayerPrefs.GetFloat("velocityIncreaseAmount");
        }
        else
        {
            PlayerPrefs.SetFloat("velocityIncreaseAmount", velocityIncreaseAmount);
        }

    }
}
