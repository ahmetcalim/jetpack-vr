using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PowerUpController : MonoBehaviour
{
    public Image slotLeft;
    public Image slotRight;
    public GameObject phase;
    public AudioSource mainAudioSource;
    public AudioSource countDownAudioSource;
    public SoundController soundController;
    public Material m_Controller_Fade;
    public Material m_Controller_Default;
    public Animator phaseCountDownBar;
    public RocketDestroyManager rocketDestroyManager;
    public InputManager inputManager;
    public bool isPhaseActive;
    public bool isRocketActive;
    public bool isBulletTimeActive;
    public float phaseMaxTimeMultipleValue = 1f;
    public static float PhaseMaxTime { get; set; }
    public static float RocketCount { get; set; }
    public static float BulletTimeMultipleValue { get; set; } = 1f;
    public static float BulletTimeDuringTime { get; set; }
    public int LastSlot { get; set; } = 1;

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
        if (PlayerPrefs.GetFloat("bulletTimeDuringTime") > BulletTimeDuringTime)
        {
            BulletTimeDuringTime = PlayerPrefs.GetFloat("bulletTimeDuringTime");
        }
        else
        {
            PlayerPrefs.SetFloat("bulletTimeDuringTime", 3f);
        }

        if (PlayerPrefs.GetFloat("phasePowerUpDuringTime") > PhaseMaxTime)
        {
            PhaseMaxTime = PlayerPrefs.GetFloat("phasePowerUpDuringTime");
        }
        else
        {
            PlayerPrefs.SetFloat("phasePowerUpDuringTime", 3f);
        }
        if (PlayerPrefs.GetFloat("rocketEffectAreaSize") > RocketCount)
        {
            RocketCount = PlayerPrefs.GetFloat("rocketEffectAreaSize");
        }
        else
        {
            PlayerPrefs.SetFloat("rocketEffectAreaSize", RocketCount);
        }

    }
    public void SetPowerUp(Sprite powerUpSprite, string pUpTag)
    {
        
        if (slotLeft.sprite == null)
        {
            slotLeft.sprite = powerUpSprite;
            slotLeft.gameObject.tag = pUpTag;

            inputManager.ControllerL.TriggerHapticPulse(50000);
          
        }
        else if(slotRight.sprite == null)
        {
            inputManager.ControllerR.TriggerHapticPulse(50000);
            slotRight.sprite = powerUpSprite;

            slotRight.gameObject.tag = pUpTag;
      
        }
        else
        {
            switch (LastSlot)
            {
                case 0:
                    slotRight.sprite = powerUpSprite;
                    slotRight.gameObject.tag = pUpTag;
                    inputManager.ControllerL.TriggerHapticPulse(50000);
                    LastSlot = 1;
                    break;
                case 1:
                    slotLeft.sprite = powerUpSprite;
                    slotLeft.gameObject.tag = pUpTag;
                    inputManager.ControllerR.TriggerHapticPulse(50000);
                    LastSlot = 0;
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
        phaseMaxTimeMultipleValue = 3.5f;
        //TO DO KULLANIM İÇİN SES ÇAL
        PlaySound(soundController.phaseEnter);

        phase.SetActive(true);
        phaseCountDownBar.SetTrigger("StartPhaseBar");

        ChangeControllerMaterialAlpha(m_Controller_Fade);
        isPhaseActive = true;
        StartCoroutine(ActivatePowerup(0, PhaseMaxTime));
    }
    public void UseBulletTime()
    {
        //TO DO KULLANIM İÇİN SES ÇAL
        PlaySound(soundController.bulletTimeEnter);
        BulletTimeMultipleValue = 5f;
        Time.timeScale = 0.2f;
        Physics.gravity = new Vector3(0f, -100f, 0f);
        isBulletTimeActive = true;
        StartCoroutine(ActivatePowerup(1, BulletTimeDuringTime));
    }
    public bool IsPhaseActive()
    {
        return isPhaseActive;
    }
    private void PrintValueToText(Text textObject, string value, string name)
    {
        textObject.text = name + ": " + value;
    }
    private void ChangeControllerMaterialAlpha(Material mat)
    {
        foreach (var item in inputManager.leftController.GetComponent<VR_ControllerManager>().joystickRenderers)
        {
            item.sharedMaterial = mat;
        }
        foreach (var item in inputManager.rightController.GetComponent<VR_ControllerManager>().joystickRenderers)
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
        yield return new WaitForSeconds(1f / BulletTimeMultipleValue);
        timeCount--;
        if (timeCount >= 1f && timeCount <= 4f)
        {
            inputManager.ControllerL.TriggerHapticPulse(50000);
            inputManager.ControllerR.TriggerHapticPulse(50000);
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
                    phaseMaxTimeMultipleValue = 1f;
                    break;
                case 1:
                    isPhaseActive = false;
                    Physics.gravity = new Vector3(0f, -20f, 0f);
                    BulletTimeMultipleValue = 1f;
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
