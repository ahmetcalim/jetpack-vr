using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
//Bu class Player ile ilgili özellikleri barındırmakta.
public class Player : MonoBehaviour
{
    [Header("Malfunction")]
    public static bool isMalfunctionActive = false;
    [Header("Z Hız")]
    public float velocityZBase = 40;
    public static float velocityZMax = 100f;
    public float velocityIncreaseAmount = 0.03f;
   
    [Header("Player Özellikler")]
    public float travelledDistance;
    public static float totalDistance;
    private float gainedResource;
    private float gainedResourceSum;
    public static bool isGameRunning = true;
    public static float difficulty = 0;
    public static float resourceMultipleValue = .5f;
    public Transform playerPawnTransform;
    public GameObject UIVROrigin;
    public GameObject VRGameOrigin;
    public Transform tunnelTransform;
    public PlayerMovementController playerMovementController;
    public static bool isGlassTunnelActive =false;
    [Header("Bbonuslar ve feedbackler")]
    public Text resourceTxt;
    public Text speedXTxt;
    public PostProcessVolume postProcess;
    public RocketDestroyManager rocketDestroyManager;
    public PowerUpController powerUpController;


    private void Start()
    {
        Time.timeScale = 1f;
        isGameRunning = true;
        StartCoroutine(IncreaseVelocityZ());
        UpdatePlayerPrefs();
    }
    private void Update()
    {
        if (isGameRunning == true)
        {
           difficulty = Mathf.Pow(3, ((Time.timeSinceLevelLoad * 2) / (90 + Time.timeSinceLevelLoad)) + 1) - 3;
           GainResource();
           PrintValueToText(resourceTxt, (gainedResource).ToString(), "Kaynak");
           PrintValueToText(speedXTxt, velocityZBase.ToString(), "Hız");
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


                //Bu çok sağlıksız
                foreach (var item in rocketDestroyManager.TriggerList)
                {
                    Destroy(item.gameObject);
                }
                foreach (var item in GameObject.FindGameObjectsWithTag("Powerup"))
                {
                    Destroy(item.gameObject);
                }
                //-----/------//
                UIVROrigin.SetActive(true);
                VRGameOrigin.SetActive(false);
                isGameRunning = false;

                gainedResourceSum = PlayerPrefs.GetFloat("gResource") + gainedResource;
                PlayerPrefs.SetFloat("gResource", gainedResourceSum);
            }
        }
        if (other.tag == "Powerup")
        {
            other.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            PowerUp powerUp = other.GetComponent<PowerUp>();
            powerUpController.SetPowerUp(powerUp.sprite, powerUp.tagName);
           
        }
    }
    public void PrintValueToText(Text textObject, string value, string name)
    {
        textObject.text = name + ": " + value;
    }
    private void UpdatePlayerPrefs()
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
    private IEnumerator IncreaseVelocityZ()
    {
        yield return new WaitForSeconds(.1f);
        if (velocityZBase < velocityZMax && isGameRunning == true)
        {
            velocityZBase += velocityIncreaseAmount;
            StartCoroutine(IncreaseVelocityZ());
        }
    }
}
