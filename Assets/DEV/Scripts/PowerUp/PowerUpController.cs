using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class PowerUpController : MonoBehaviour
{
    [Header("Bonus")]
    public Image powerUpSlot1;
    public Image powerUpSlot2;
    public int powerUpCount = 0;
    private float timer;
    public Text bonusFeedBackTxt;
    [Header("Phase Bonus")]
    public Sprite phaseSprite;
    public List<float> phasePowerUpDuringTime;
    public bool isPhaseActive;
    public Text phaseTimer;
    public AudioSource bipAudio;
    public Material m_Controller_Fade;
    public Material m_Controller_Default;
    [Header("Roket Bonus")]
    public Sprite rocketSprite;
    public RocketDestroyManager rocketDestroyManager;
   
    public bool isRocketActive;
    [Header("Diğer")]
    public PlayerMovementController playerMovementController;
    public List<PowerUp> powerUps;
    private int lastSlot = 1;

    // Start is called before the first frame update
    void Start()
    {
        ChangeControllerMaterialAlpha(m_Controller_Default);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isGameRunning == true)
        {
            if (isPhaseActive)
            {
                PhaseTimeCount();
            }
        }
    }
  
    public void SetPowerUp(Sprite powerUpSprite, string pUpTag)
    {
        if (powerUpSlot1.sprite == null)
        {
            powerUpSlot1.sprite = powerUpSprite;
            powerUpSlot1.gameObject.tag = pUpTag;
            powerUpSlot1.gameObject.GetComponent<AudioSource>().Play();
            //         VibrateController(playerMovementController.ControllerL, 1f, 10);
        }
        else if(powerUpSlot2.sprite == null)
        {
            powerUpSlot2.sprite = powerUpSprite;
            powerUpSlot2.gameObject.GetComponent<AudioSource>().Play();
            powerUpSlot2.gameObject.tag = pUpTag;
            ////        VibrateController(playerMovementController.ControllerR, 1f, 10);
        }
        else
        {
            switch (lastSlot)
            {
                case 0:

                    powerUpSlot2.sprite = powerUpSprite;
                    powerUpSlot2.gameObject.tag = pUpTag;
                    powerUpSlot2.gameObject.GetComponent<AudioSource>().Play();
                    playerMovementController.ControllerL.TriggerHapticPulse(50000);
                    
                    lastSlot = 1;
                    break;
                case 1:
                    powerUpSlot1.sprite = powerUpSprite;
                    powerUpSlot1.gameObject.tag = pUpTag;
                    powerUpSlot1.gameObject.GetComponent<AudioSource>().Play();
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
       
            bonusFeedBackTxt.GetComponent<Animator>().SetTrigger("Feedback");
            PrintValueToText(bonusFeedBackTxt, "Roket Kullanıldı.", "- ");
            foreach (var item in rocketDestroyManager.TriggerList)
            {
                Destroy(item.gameObject);
            }
         
       

    }
    public void UsePhase()
    {
       
            bonusFeedBackTxt.GetComponent<Animator>().SetTrigger("Feedback");
            PrintValueToText(bonusFeedBackTxt, "Phase Kullanıldı.", "- ");
            ChangeControllerMaterialAlpha(m_Controller_Fade);
            
            isPhaseActive = true;
    
    }
    public void ActivateMalfunction()
    {
        Player.isMalfunctionActive = true;
    }
    private void PhaseTimeCount()
    {
        if (timer <= phasePowerUpDuringTime[0])
        {
            timer += Time.deltaTime * 1f;
            if (timer >= 4.08f && timer < 4.1f)
            {
                StartCoroutine(HapticFeedBack());
            }
            PrintValueToText(phaseTimer, timer.ToString(), "Phase: ");
        }
        else
        {
            timer = 0f;
            isPhaseActive = false;
            
            ChangeControllerMaterialAlpha(m_Controller_Default);
        }
    }
    private void PrintValueToText(Text textObject, string value, string name)
    {
        textObject.text = name + ": " + value;
    }
    private void ChangeControllerMaterialAlpha(Material mat)
    {
        playerMovementController.leftController.GetComponent<VR_ControllerManager>().joystickRenderer.sharedMaterial = mat;

    }
    IEnumerator HapticFeedBack()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(1f);
            bipAudio.Play();
            playerMovementController.ControllerL.TriggerHapticPulse(50000);
            playerMovementController.ControllerR.TriggerHapticPulse(50000);
        }
    }
  

}
