using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using HTC.UnityPlugin.Utility;
public class PowerUpController : MonoBehaviour
{
    [Header("Bonus")]
    public Image powerUpSlotLeft;
    public Image powerUpSlotRight;
    public int powerUpCount = 0;
    private float timer;
    private bool timeCountFinished = true;

    public AudioSource mainAudioSource;
    public AudioSource countDownAudioSource;
    public SoundController soundController;
    [Header("Phase Bonus")]
    public float phasePowerUpDuringTime;
    public bool isPhaseActive;
    public Material m_Controller_Fade;
    public Material m_Controller_Default;
    public Animator phaseCountDownBar;
    [Header("Roket Bonus")]
    public RocketDestroyManager rocketDestroyManager;
    public bool isRocketActive;
    private float rocketEffectAreaSize;
    [Header("Bullet Time Bonus")]
    public static float bulletTimeMultipleValue = 1f;
    public float bulletTimeDuringTime;
    public bool isBulletTimeActive;

    [Header("Diğer")]
    public PlayerMovementController playerMovementController;
    private int lastSlot = 1;


    // Start is called before the first frame update
    void Start()
    {
        UpdatePrefs();
        ChangeControllerMaterialAlpha(m_Controller_Default);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (Player.isGameRunning == true)
        {
            CheckActivePowerups();
        }
    }
    public void UpdatePrefs()
    {
        if (PlayerPrefs.GetFloat("bulletTimeDuringTime") > bulletTimeDuringTime)
        {
            bulletTimeDuringTime = PlayerPrefs.GetFloat("bulletTimeDuringTime");
        }
        else
        {
            PlayerPrefs.SetFloat("bulletTimeDuringTime", bulletTimeDuringTime);
        }

        if (PlayerPrefs.GetFloat("phasePowerUpDuringTime") > phasePowerUpDuringTime)
        {
            phasePowerUpDuringTime = PlayerPrefs.GetFloat("phasePowerUpDuringTime");
        }
        else
        {
            PlayerPrefs.SetFloat("phasePowerUpDuringTime", phasePowerUpDuringTime);
        }
        if (PlayerPrefs.GetFloat("rocketEffectAreaSize") > rocketEffectAreaSize)
        {
            rocketEffectAreaSize = PlayerPrefs.GetFloat("rocketEffectAreaSize");
        }
        else
        {
            PlayerPrefs.SetFloat("rocketEffectAreaSize", rocketEffectAreaSize);
        }

    }
    private void CheckActivePowerups()
    {
        if (isPhaseActive)
        {
            isPhaseActive = !TimeCount(phasePowerUpDuringTime, 3.92f, 3.9f, 1f);
        }
        else
        {
            ChangeControllerMaterialAlpha(m_Controller_Default);
        }
        if (isBulletTimeActive)
        {
            isBulletTimeActive = !TimeCount((bulletTimeDuringTime/5), 3.92f/5f, 3.9f/5f, 5f);
        }
        else
        {
            if (bulletTimeMultipleValue != 1f)
            {
                bulletTimeMultipleValue = 1f;
                Time.timeScale = 1f;
                PlaySound(soundController.bulletTimeOut);
               
            }
        }
    }
    public void SetPowerUp(Sprite powerUpSprite, string pUpTag)
    {
        
        if (powerUpSlotLeft.sprite == null)
        {
            powerUpSlotLeft.sprite = powerUpSprite;
            powerUpSlotLeft.gameObject.tag = pUpTag;
           
            playerMovementController.ControllerL.TriggerHapticPulse(50000);
          
        }
        else if(powerUpSlotRight.sprite == null)
        {
            playerMovementController.ControllerR.TriggerHapticPulse(50000);
            powerUpSlotRight.sprite = powerUpSprite;
           
            powerUpSlotRight.gameObject.tag = pUpTag;
      
        }
        else
        {
            switch (lastSlot)
            {
                case 0:
                    powerUpSlotRight.sprite = powerUpSprite;
                    powerUpSlotRight.gameObject.tag = pUpTag;
                  
                    playerMovementController.ControllerL.TriggerHapticPulse(50000);
                    lastSlot = 1;
                    break;
                case 1:
                    powerUpSlotLeft.sprite = powerUpSprite;
                    powerUpSlotLeft.gameObject.tag = pUpTag;

                  
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
        PlaySound(soundController.rocketUsing);

      
            foreach (var item in rocketDestroyManager.TriggerList)
            {
                Destroy(item.gameObject);
            }
    }
    public void UsePhase()
    {
        //TO DO KULLANIM İÇİN SES ÇAL
        PlaySound(soundController.phaseUsing);
        phaseCountDownBar.SetTrigger("StartPhaseBar");
      
        ChangeControllerMaterialAlpha(m_Controller_Fade);
        isPhaseActive = true;
    
    }
    public void UseBulletTime()
    {
        //TO DO KULLANIM İÇİN SES ÇAL
        PlaySound(soundController.bulletTimeUsing);
       
        bulletTimeMultipleValue = 5f;
        Time.timeScale = 0.2f;
       
        isBulletTimeActive = true;

    }
    public void ActivateMalfunction()
    {
        Player.isMalfunctionActive = true;
    }
    private bool TimeCount(float duringTime, float a, float b, float divide)
    {
        
        if (timer <= duringTime)
        {
            timeCountFinished = false;
            timer += Time.deltaTime * 1f;
            if (timer >= duringTime-a && timer < duringTime-b)
            {
                StartCoroutine(HapticFeedBack(divide));
            }
           
        }
        else
        {
            timer = 0f;
            timeCountFinished = true;
        }
        return timeCountFinished;
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
    IEnumerator HapticFeedBack(float divide)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f/divide);

            countDownAudioSource.Play();
            playerMovementController.ControllerL.TriggerHapticPulse(50000);
            playerMovementController.ControllerR.TriggerHapticPulse(50000);
        }
    }
    private void PlaySound(AudioClip clip)
    {
        mainAudioSource.clip = clip;
        mainAudioSource.Play();
    }
}
