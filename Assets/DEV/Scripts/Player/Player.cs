using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR;
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
    public List<AudioSource> effectSources;
    public AudioSource bgMusicSource;
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
    public List<Image> speedBar;
    private float barTimer;
    private int speedBarState = 0;
    private float speedBarInterval = 50;
    private float loadAmount = 0f;
    public List<BoxCollider> glassTunnelColliders;
    [Header("Bbonuslar ve feedbackler")]
    public Text resourceTxt;
    public Text speedXTxt;
    public Text travelledDistanceTxt;
    public Text gainedResourceSumTxt;
    public PostProcessVolume postProcess;
    public RocketDestroyManager rocketDestroyManager;
    public PowerUpController powerUpController;
    public Canvas dashBoard;
    public AudioSource bonusRecieved;
    private void Start()
    {
        foreach (var item in effectSources)
        {

            item.volume = SettingsManager.effectVolume;
        }
       // bgMusicSource.volume = SettingsManager.musicVolume;
        XRSettings.eyeTextureResolutionScale = SettingsManager.currenRenderScale;
       // playerPawnTransform.position = new Vector3(13f, 5f, -850f);
        dashBoard.GetComponent<RectTransform>().position = new Vector3(transform.position.x, dashBoard.GetComponent<RectTransform>().position.y, dashBoard.GetComponent<RectTransform>().position.z);
        Time.timeScale = 1f;
        isGameRunning = true;
        StartCoroutine(IncreaseVelocityZ());
      
    }
 
    private void Update()
    {
        RuntimeEvents();

    }
    public static bool IsGameRunning()
    {
        return isGameRunning;
    }
    private void RuntimeEvents()
    {
        if (IsGameRunning())
        {
            difficulty = Mathf.Pow(3, ((Time.timeSinceLevelLoad * 2) / (90 + Time.timeSinceLevelLoad)) + 1) - 3;
            travelledDistance = (Time.timeSinceLevelLoad * velocityZBase);
            GainResource();
            PrintValueToText(resourceTxt, ((int)gainedResource).ToString(), "");
            PrintValueToText(speedXTxt, (velocityZBase).ToString(), "");
            PrintValueToText(travelledDistanceTxt, ((int)travelledDistance).ToString(), "");
            PrintValueToText(gainedResourceSumTxt, ((int)PlayerPrefs.GetFloat("gResource")).ToString(), "");
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
            GameOver(other);
        }
        if (other.tag == "Powerup")
        {
            bonusRecieved.Play();
            GetPowerup(other);

        }
    }

    private void GetPowerup(Collider other)
    {
        
        Destroy(other.gameObject);
        PowerUp powerUp = other.GetComponent<PowerUp>();
        powerUpController.SetPowerUp(powerUp.sprite, powerUp.tagName);
    }

    private void GameOver(Collider other)
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

    public void PrintValueToText(Text textObject, string value, string name)
    {
        textObject.text = name + "" + value;
    }
 
    private IEnumerator IncreaseVelocityZ()
    {
        yield return new WaitForSeconds(.1f);
        if (velocityZBase < velocityZMax && IsGameRunning())
        {
            loadAmount += 0.003f;

            speedBar[speedBarState].color = new Color(1f, 1f, 1f, loadAmount);
            if (velocityZBase>speedBarInterval)
            {

                loadAmount = 0f;
                speedBarInterval += 10;
                speedBarState++;

            }
            velocityZBase += velocityIncreaseAmount;
            StartCoroutine(IncreaseVelocityZ());
        }
    }
    public void ActivateGlassTunnel(bool state)
    {
        foreach (var item in glassTunnelColliders)
        {
            item.isTrigger = state;
        }
    }
}
