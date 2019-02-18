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
    private bool isAvailable = true;
    private void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
       
        GetComponent<Button>().GetComponentInChildren<Text>().text = upgradeType.ToString() + " " + cost + " Res";
        CheckAvailabity();
        if (isAvailable == false)
        {
            GetComponent<Button>().GetComponentInChildren<Text>().text = "Satın alındı daha önce";
            GetComponent<Button>().GetComponentInChildren<Text>().color = Color.red;
            GetComponent<Button>().enabled = false;
        }
    }
    public void ApplyUpgrade()
    {
     
        if (PlayerPrefs.GetFloat("gResource") > cost)
        {
            switch (upgradeType)
            {
                case UpgradeType.PHASE:
                    if (upgradeManager.phaseUpgradeLevelIndex == index)
                    {
                        PlayerPrefs.SetFloat("phasePowerUpDuringTime", PlayerPrefs.GetFloat("phasePowerUpDuringTime") + upgradeAmount);
                        DoTheseBeforeUpgrade();
                        upgradeManager.phaseUpgradeLevelIndex++;
                        PlayerPrefs.SetInt("phaseUpgradeLevelIndex", upgradeManager.phaseUpgradeLevelIndex);
                    }
                    break;
                case UpgradeType.ROCKET:
                    if (upgradeManager.rocketUpgradeLevelIndex == index)
                    {
                        PlayerPrefs.SetFloat("rocketEffectAreaSize", PlayerPrefs.GetFloat("rocketEffectAreaSize") + upgradeAmount);
                        DoTheseBeforeUpgrade();
                        upgradeManager.rocketUpgradeLevelIndex++;
                        PlayerPrefs.SetInt("rocketUpgradeLevelIndex", upgradeManager.rocketUpgradeLevelIndex);
                    }
                    break;
                case UpgradeType.BULLET_TIME:
                    if (upgradeManager.bulletTimeUpgradeLevelIndex == index)
                    {
                        PlayerPrefs.SetFloat("bulletTimeDuringTime", PlayerPrefs.GetFloat("bulletTimeDuringTime") + upgradeAmount);
                        DoTheseBeforeUpgrade();
                        upgradeManager.bulletTimeUpgradeLevelIndex++;
                        PlayerPrefs.SetInt("bulletTimeUpgradeLevelIndex", upgradeManager.bulletTimeUpgradeLevelIndex);
                    }
                    break;
                case UpgradeType.MOVEMENT_X:
                    if (upgradeManager.movementXUpgradeLevelIndex == index)
                    {
                        PlayerPrefs.SetFloat("turnConstant", PlayerPrefs.GetFloat("turnConstant") + upgradeAmount);
                        DoTheseBeforeUpgrade();
                        upgradeManager.movementXUpgradeLevelIndex++;
                        PlayerPrefs.SetInt("movementXUpgradeLevelIndex", upgradeManager.movementXUpgradeLevelIndex);
                    }
                    break;
                case UpgradeType.MOVEMENT_Y:
                    if (upgradeManager.movementYUpgradeLevelIndex == index)
                    {
                        PlayerPrefs.SetFloat("accelerationYConstant", PlayerPrefs.GetFloat("accelerationYConstant") + upgradeAmount);
                        DoTheseBeforeUpgrade();
                        upgradeManager.movementYUpgradeLevelIndex++;
                        PlayerPrefs.SetInt("movementYUpgradeLevelIndex", upgradeManager.movementYUpgradeLevelIndex);
                    }
                    break;
                case UpgradeType.MOVEMENT_Z:
                    if (upgradeManager.movementZUpgradeLevelIndex == index)
                    {
                        PlayerPrefs.SetFloat("velocityIncreaseAmount", PlayerPrefs.GetFloat("velocityIncreaseAmount") + upgradeAmount);
                        DoTheseBeforeUpgrade();
                        upgradeManager.movementZUpgradeLevelIndex++;
                        PlayerPrefs.SetInt("movementZUpgradeLevelIndex", upgradeManager.movementZUpgradeLevelIndex);
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("ACCIK DAHA PARA KAZAN.");
        }
    }
    private void CheckAvailabity()
    {
       
        switch (upgradeType)
        {
            case UpgradeType.PHASE:
                if (upgradeManager.phaseUpgradeLevelIndex > index)
                {
                    isAvailable = false;
                }
                else
                {
                    isAvailable = true;
                }

                break;
            case UpgradeType.ROCKET:
                if (upgradeManager.rocketUpgradeLevelIndex > index)
                {
                    isAvailable = false;
                }
                else
                {
                    isAvailable = true;
                }
                break;
            case UpgradeType.BULLET_TIME:
                if (upgradeManager.bulletTimeUpgradeLevelIndex > index)
                {
                    isAvailable = false;
                }
                else
                {
                    isAvailable = true;
                }
                break;
            case UpgradeType.MOVEMENT_X:
                if (upgradeManager.movementXUpgradeLevelIndex > index)
                {
                    isAvailable = false;
                }
                else
                {
                    isAvailable = true;
                }
                break;
            case UpgradeType.MOVEMENT_Y:
                if (upgradeManager.movementYUpgradeLevelIndex > index)
                {
                    isAvailable = false;
                }
                else
                {
                    isAvailable = true;
                }
                break;
            case UpgradeType.MOVEMENT_Z:
                if (upgradeManager.movementZUpgradeLevelIndex > index)
                {
                    isAvailable = false;
                }
                else
                {
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
        GetComponent<Button>().GetComponentInChildren<Text>().text = "Satın alındı daha önce";
        GetComponent<Button>().enabled = false;
    }
}
