using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using HTC.UnityPlugin.Utility;
public class PowerUpController : MonoBehaviour
{
    [Header("Bonus")]
    public Image slotLeft;
    public Image slotRight;
    private float timer;
    private bool timeCountFinished = true;
    public GameObject phase;
    public AudioSource mainAudioSource;
    public AudioSource countDownAudioSource;
    public SoundController soundController;
    [Header("Phase Bonus")]
    public static float phasePowerUpMaxTime;
    public bool isPhaseActive;
    public Material m_Controller_Fade;
    public Material m_Controller_Default;
    public Animator phaseCountDownBar;
    [Header("Roket Bonus")]
    public RocketDestroyManager rocketDestroyManager;
    public bool isRocketActive;
    public static float rocketCount;
    [Header("Bullet Time Bonus")]
    public static float bulletTimeMultipleValue = 1f;
    public static float bulletTimeDuringTime;
    public bool isBulletTimeActive;
    public float phaseİkiKati = 1f;
    [Header("Diğer")]
    public PlayerMovementController playerMovementController;
    private int lastSlot = 1;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0f,-20f,0f);
        UpdatePrefs();
        ChangeControllerMaterialAlpha(m_Controller_Default);
    }
    // Update is called once per frame
    public void UpdatePrefs()
    {
        if (PlayerPrefs.GetFloat("bulletTimeDuringTime") > bulletTimeDuringTime)
        {
            bulletTimeDuringTime = PlayerPrefs.GetFloat("bulletTimeDuringTime");
        }
        else
        {
            PlayerPrefs.SetFloat("bulletTimeDuringTime", 3f);
        }

        if (PlayerPrefs.GetFloat("phasePowerUpDuringTime") > phasePowerUpMaxTime)
        {
            phasePowerUpMaxTime = PlayerPrefs.GetFloat("phasePowerUpDuringTime");
        }
        else
        {
            PlayerPrefs.SetFloat("phasePowerUpDuringTime", 3f);
        }
        if (PlayerPrefs.GetFloat("rocketEffectAreaSize") > rocketCount)
        {
            rocketCount = PlayerPrefs.GetFloat("rocketEffectAreaSize");
        }
        else
        {
            PlayerPrefs.SetFloat("rocketEffectAreaSize", rocketCount);
        }

    }
    public void SetPowerUp(Sprite powerUpSprite, string pUpTag)
    {
        
        if (slotLeft.sprite == null)
        {
            slotLeft.sprite = powerUpSprite;
            slotLeft.gameObject.tag = pUpTag;
           
            playerMovementController.ControllerL.TriggerHapticPulse(50000);
          
        }
        else if(slotRight.sprite == null)
        {
            playerMovementController.ControllerR.TriggerHapticPulse(50000);
            slotRight.sprite = powerUpSprite;

            slotRight.gameObject.tag = pUpTag;
      
        }
        else
        {
            switch (lastSlot)
            {
                case 0:
                    slotRight.sprite = powerUpSprite;
                    slotRight.gameObject.tag = pUpTag;
                    playerMovementController.ControllerL.TriggerHapticPulse(50000);
                    lastSlot = 1;
                    break;
                case 1:
                    slotLeft.sprite = powerUpSprite;
                    slotLeft.gameObject.tag = pUpTag;
                    playerMovementController.ControllerR.TriggerHapticPulse(50000);
                    lastSlot = 0;
                    break;
                default:
                    break;
            }
        }
    }
    public void UseRocket()
    {
        //TO DO KULLANIM İÇİN SES ÇAL
        PlaySound(soundController.rocketEnter);

      
        foreach (var item in rocketDestroyManager.TriggerList)
        {
            Destroy(item.gameObject);
        }
    }
    public void UsePhase()
    {
        phaseİkiKati = 3.5f;
        //TO DO KULLANIM İÇİN SES ÇAL
        PlaySound(soundController.phaseEnter);

        phase.SetActive(true);
        phaseCountDownBar.SetTrigger("StartPhaseBar");

        ChangeControllerMaterialAlpha(m_Controller_Fade);
        isPhaseActive = true;
        StartCoroutine(ActivatePowerup(0, phasePowerUpMaxTime));
    }
    public void UseBulletTime()
    {
        //TO DO KULLANIM İÇİN SES ÇAL
        PlaySound(soundController.bulletTimeEnter);
        bulletTimeMultipleValue = 5f;
        Time.timeScale = 0.2f;
        Physics.gravity = new Vector3(0f, -100f, 0f);
        isBulletTimeActive = true;
        StartCoroutine(ActivatePowerup(1, bulletTimeDuringTime));
    }
    public void ActivateMalfunction()
    {
        Player.isMalfunctionActive = true;
    }
    
    private void PrintValueToText(Text textObject, string value, string name)
    {
        textObject.text = name + ": " + value;
    }
    private void ChangeControllerMaterialAlpha(Material mat)
    {
        foreach (var item in playerMovementController.leftController.GetComponent<VR_ControllerManager>().joystickRenderers)
        {
            item.sharedMaterial = mat;
        }
        foreach (var item in playerMovementController.rightController.GetComponent<VR_ControllerManager>().joystickRenderers)
        {
            item.sharedMaterial = mat;
        }


    }
   
    private void PlaySound(AudioClip clip)
    {
        mainAudioSource.clip = clip;
        mainAudioSource.Play();
    }
    private IEnumerator ActivatePowerup(int powerupIndex, float timeCount)
    {
        yield return new WaitForSeconds(1f / bulletTimeMultipleValue);
        timeCount--;
        if (timeCount >= 1f && timeCount <= 4f)
        {
            playerMovementController.ControllerL.TriggerHapticPulse(50000);
            playerMovementController.ControllerR.TriggerHapticPulse(50000);
        }
        if (timeCount < 1)
        {
            switch (powerupIndex)
            {
                case 0:
                    PlaySound(soundController.phaseOut);
                    isPhaseActive = false;
                    ChangeControllerMaterialAlpha(m_Controller_Default);
                   
                    phase.SetActive(false);
                    phaseİkiKati = 1f;
                    break;
                case 1:
                    isPhaseActive = false;
                    Physics.gravity = new Vector3(0f, -20f, 0f);
                    bulletTimeMultipleValue = 1f;
                    Time.timeScale = 1f;
                    PlaySound(soundController.bulletTimeOut);
                    break;
                default:
                    break;
            }
        }
        else
        {
            StartCoroutine(ActivatePowerup(powerupIndex, timeCount));
        }
    }
}
