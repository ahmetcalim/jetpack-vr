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
    public List<AudioSource> effectSources;
    public AudioSource bgMusicSource;
    public static bool isGameRunning = true;
    public Transform playerPawnTransform;
    public GameObject UIVROrigin;
    public GameObject VRGameOrigin;
    public Transform tunnelTransform;
    public List<Image> speedBar;
    public List<BoxCollider> glassTunnelColliders;
    public Text resourceTxt;
    public Text speedXTxt;
    public Text travelledDistanceTxt;
    public Text gainedResourceSumTxt;
    public PostProcessVolume postProcess;
    public RocketDestroyManager rocketDestroyManager;
    public PowerUpController powerUpController;
    public Canvas dashBoard;
    public AudioSource bonusRecieved;
    public float _velocityZBase = 40;

    public float GainedResource { get; set; }
    public float GainedResourceSum { get; set; }
    public float BarTimer { get; set; }
    public static float TotalDistance { get; set; }
    public static float Difficulty { get; set; } = 0;
    public int SpeedBarState { get; set; } = 0;
    public float SpeedBarInterval { get; set; } = 50;
    public float LoadAmount { get; set; } = 0f;
    public float VelocityZBase { get => _velocityZBase; set => _velocityZBase = value; }
    public static bool IsGlassTunnelActive { get; set; } = false;
    public static float ResourceMultipleValue { get; set; } = .5f;
    public float TravelledDistance { get; set; }
    public float VelocityIncreaseAmount { get; set; } = 0.03f;
    public static float VelocityZMax { get; set; } = 100f;

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
            CalculateBaseValues();
            PrintValuesToDashboard();
        }
    }
    private void CalculateBaseValues()
    {
        Difficulty = Mathf.Pow(3, ((Time.timeSinceLevelLoad * 2) / (90 + Time.timeSinceLevelLoad)) + 1) - 3;
        TravelledDistance = (Time.timeSinceLevelLoad * VelocityZBase);
        GainedResource = (TravelledDistance * Difficulty) * ResourceMultipleValue;
    }
    private void PrintValuesToDashboard()
    {
        PrintValueToText(resourceTxt, ((int)GainedResource).ToString(), "");
        PrintValueToText(speedXTxt, (VelocityZBase).ToString(), "");
        PrintValueToText(travelledDistanceTxt, ((int)TravelledDistance).ToString(), "");
        PrintValueToText(gainedResourceSumTxt, ((int)PlayerPrefs.GetFloat("gResource")).ToString(), "");
    }
    public static void ResetStaticValues()
    {

        Difficulty = 0f;
        TotalDistance = 0f;
        ResourceMultipleValue = .5f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
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
        if (!powerUpController.IsPhaseActive())
        {
            ResetStaticValues();
            Destroy(playerPawnTransform.gameObject.GetComponent<Rigidbody>());
            foreach (var item in rocketDestroyManager.TriggerList)
            {
                Destroy(item.gameObject);
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("Powerup"))
            {
                Destroy(item.gameObject);
            }
            UIVROrigin.SetActive(true);
            VRGameOrigin.SetActive(false);
            isGameRunning = false;

            GainedResourceSum = PlayerPrefs.GetFloat("gResource") + GainedResource;
            PlayerPrefs.SetFloat("gResource", GainedResourceSum);
        }
    }
    public void PrintValueToText(Text textObject, string value, string name)
    {
        textObject.text = name + "" + value;
    }
    private IEnumerator IncreaseVelocityZ()
    {
        yield return new WaitForSeconds(.1f);
        if (VelocityZBase < VelocityZMax && IsGameRunning())
        {
            LoadAmount += 0.003f;

            speedBar[SpeedBarState].color = new Color(1f, 1f, 1f, LoadAmount);
            if (VelocityZBase > SpeedBarInterval)
            {

                LoadAmount = 0f;
                SpeedBarInterval += 10;
                SpeedBarState++;

            }
            VelocityZBase += VelocityIncreaseAmount;
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
