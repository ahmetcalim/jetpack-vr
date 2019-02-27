using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Upgrade : MonoBehaviour
{
    public enum UpgradeTitle {POWERUP, MOVEMENT};
    public UpgradeTitle upgradeTitle;
    public enum UpgradeType {PHASE, ROCKET, BULLET_TIME, MOVEMENT_X, MOVEMENT_Y, MOVEMENT_Z}
    public UpgradeType upgradeType;
    public enum Operator {ADDITION, ELIMINATION, MULTIPLICATION, DIVISION}
    public Operator _operator;
    public float cost;
    public float upgradeAmount;
    public int index;
    private UpgradeManager upgradeManager;
    private bool isAvailable = false;
    public bool isPurchased;
    public GameObject lockObject;
    public GameObject keyObject;
    public GameObject costBox;
    public Text costText;
    public Button yesButton;
    public GameObject popupPanel;
    private void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        CheckPrefs();
        CheckAvailabity();
       
        SetAvailabity();
    }

    private static void CheckPrefs()
    {
        if (PowerUpController.PhaseMaxTime < 3)
        {
            PowerUpController.PhaseMaxTime = 3;
        }
        if (PowerUpController.BulletTimeDuringTime < 3)
        {
            PowerUpController.BulletTimeDuringTime = 3;
        }
        if (PlayerPrefs.GetFloat("bulletTimeDuringTime") > PowerUpController.BulletTimeDuringTime)
        {
            PowerUpController.BulletTimeDuringTime = PlayerPrefs.GetFloat("bulletTimeDuringTime");
        }
        else
        {
            PlayerPrefs.SetFloat("bulletTimeDuringTime", PowerUpController.BulletTimeDuringTime);
        }

        if (PlayerPrefs.GetFloat("phasePowerUpDuringTime") > PowerUpController.PhaseMaxTime)
        {
            PowerUpController.PhaseMaxTime = PlayerPrefs.GetFloat("phasePowerUpDuringTime");
        }
        else
        {
            PlayerPrefs.SetFloat("phasePowerUpDuringTime", PowerUpController.PhaseMaxTime);
        }
        if (PlayerPrefs.GetFloat("rocketEffectAreaSize") > PowerUpController.RocketCount)
        {
            PowerUpController.RocketCount = PlayerPrefs.GetFloat("rocketEffectAreaSize");
        }
        else
        {
            PlayerPrefs.SetFloat("rocketEffectAreaSize", PowerUpController.RocketCount);
        }
    }

    private void SetAvailabity()
    {
        if (isAvailable == false)
        {
            GetComponent<Button>().enabled = false;
            lockObject.SetActive(true);
            keyObject.SetActive(false);
            costText.text = cost.ToString();
        }
        if (isPurchased == true)
        {
            costBox.SetActive(false);
        }
        if (isPurchased == false && isAvailable == false)
        {
            costText.text = cost.ToString();
            keyObject.SetActive(true);
            lockObject.SetActive(false);
        }
    }

    public void ApplyUpgrade()
    {
        if (!popupPanel.activeSelf)
        {
            popupPanel.SetActive(true);
            GetComponent<Button>().enabled = false;
            yesButton.onClick.AddListener(ApplyUpgrade);
        }
        else
        {
            if (PlayerPrefs.GetFloat("gResource") > cost)
            { popupPanel.SetActive(false);
                switch (upgradeType)
                {
                    case UpgradeType.PHASE:
                        if (upgradeManager.phaseUpgradeLevelIndex == index)
                        {
                            PlayerPrefs.SetFloat("phasePowerUpDuringTime", PowerUpController.PhaseMaxTime + upgradeAmount);
                            DoTheseBeforeUpgrade();
                            upgradeManager.phaseUpgradeLevelIndex++;
                            PlayerPrefs.SetInt("phaseUpgradeLevelIndex", upgradeManager.phaseUpgradeLevelIndex);
                            isPurchased = true;
                        }
                        break;
                    case UpgradeType.ROCKET:
                        if (upgradeManager.rocketUpgradeLevelIndex == index)
                        {
                            PlayerPrefs.SetFloat("rocketEffectAreaSize", PlayerPrefs.GetFloat("rocketEffectAreaSize") + upgradeAmount);
                            DoTheseBeforeUpgrade();
                            upgradeManager.rocketUpgradeLevelIndex++;
                            PlayerPrefs.SetInt("rocketUpgradeLevelIndex", upgradeManager.rocketUpgradeLevelIndex);
                            isPurchased = true;
                        }
                        break;
                    case UpgradeType.BULLET_TIME:
                        if (upgradeManager.bulletTimeUpgradeLevelIndex == index)
                        {
                            PlayerPrefs.SetFloat("bulletTimeDuringTime", PlayerPrefs.GetFloat("bulletTimeDuringTime") + upgradeAmount);
                            DoTheseBeforeUpgrade();
                            upgradeManager.bulletTimeUpgradeLevelIndex++;
                            PlayerPrefs.SetInt("bulletTimeUpgradeLevelIndex", upgradeManager.bulletTimeUpgradeLevelIndex);
                            isPurchased = true;
                        }
                        break;
                    default:
                        break;
                }
                SetAvailabity();
            }
            else
            {
                Debug.Log("ACCIK DAHA PARA KAZAN.");
            }
        }
        
    }
    private void CheckAvailabity()
    {
       
        switch (upgradeType)
        {
            case UpgradeType.PHASE:
                if (PlayerPrefs.GetInt("phaseUpgradeLevelIndex") > this.index)
                {
                    isPurchased = true;
                    isAvailable = false;
                }
                else
                {
                    isPurchased = false;
                    isAvailable = true;
                }
               
                break;
            case UpgradeType.ROCKET:
                if (PlayerPrefs.GetInt("rocketUpgradeLevelIndex") > index)
                {
                    isPurchased = true;
                    isAvailable = false;
                }
                else
                {
                    isPurchased = false;
                       isAvailable = true;
                }
                break;
            case UpgradeType.BULLET_TIME:
                if (upgradeManager.bulletTimeUpgradeLevelIndex > index)
                {
                    isPurchased = true;
                    isAvailable = false;
                }
                else
                {
                    isPurchased = false;
                    isAvailable = true;
                }
                break;
            default:
                break;
        }  
    }
    private void DoTheseBeforeUpgrade()
    {
        PlayerPrefs.SetFloat("gResource", PlayerPrefs.GetFloat("gResource") - cost);
        upgradeManager.UpdateResourceText(PlayerPrefs.GetFloat("gResource"));
    }
}
