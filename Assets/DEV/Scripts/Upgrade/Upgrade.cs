﻿using System.Collections;
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
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=>ApplyUpgrade());
        GetComponent<Button>().GetComponentInChildren<Text>().text = upgradeType.ToString() + " " + _operator.ToString() + " " + upgradeAmount;
    }
    public void ApplyUpgrade()
    {
        Debug.Log(upgradeType.ToString());
        switch (_operator)
        {
            case Operator.ADDITION:
                break;
            case Operator.ELIMINATION:
                break;
            case Operator.MULTIPLICATION:
                break;
            case Operator.DIVISION:
                break;
            default:
                break;
        }
        switch (upgradeType)
        {
            case UpgradeType.PHASE:
             
                break;
            case UpgradeType.ROCKET:
                break;
            case UpgradeType.BULLET_TIME:
                break;
            case UpgradeType.MOVEMENT_X:
                break;
            case UpgradeType.MOVEMENT_Y:
                break;
            case UpgradeType.MOVEMENT_Z:
                break;
            default:
                break;
        }
    }
}