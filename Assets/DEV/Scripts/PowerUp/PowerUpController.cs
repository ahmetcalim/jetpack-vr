using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerUpController : MonoBehaviour
{
    [Header("Bonus")]
    public Image powerUpSlot1;
    public Image powerUpSlot2;
    public int powerUpCount = 0;
    private float timer;
    [Header("Phase Bonus")]
    public Sprite phaseSprite;
    public List<float> phasePowerUpDuringTime;
    public bool isPhaseUsable;
    public bool isPhaseActive;
    public Text phaseTimer;
    [Header("Roket Bonus")]
    public Sprite rocketSprite;
    public RocketDestroyManager rocketDestroyManager;
    public bool isRocketUsable;
    public bool isRocketActive;
    [Header("Diğer")]
    public PlayerMovementController playerMovementController;



    // Start is called before the first frame update
    void Start()
    {
        ChangeControllerMaterialAlpha(1f);
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
    private void PhaseTimeCount()
    {
        if (timer <= phasePowerUpDuringTime[0])
        {
            timer += Time.deltaTime * 1f;
            PrintValueToText(phaseTimer, timer.ToString(), "Phase TimeC: ");
        }
        else
        {
            timer = 0f;
            isPhaseActive = false;
            ChangeControllerMaterialAlpha(1f);
        }
    }
    private void PrintValueToText(Text textObject, string value, string name)
    {
        textObject.text = name + ": " + value;
    }
    private void ChangeControllerMaterialAlpha(float a)
    {
        playerMovementController.leftController.GetComponent<VR_ControllerManager>().joystickRenderer.sharedMaterial.color = new Color(1f, 1f, 1f, a);

    }
    public void SetPowerUp(Sprite powerUpSprite, string pUpTag)
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
                playerMovementController.ControllerR.TriggerHapticPulse(50000);
                powerUpCount = 2;
                break;
            default:
                break;
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
        ChangeControllerMaterialAlpha(.2f);
        powerUpCount--;
        isPhaseUsable = false;
        isPhaseActive = true;
    }
}
