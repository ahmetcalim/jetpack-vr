using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeManager : MonoBehaviour
{
    public Text playerResource;
    public int 
        phaseUpgradeLevelIndex = 0,
        rocketUpgradeLevelIndex = 0,
        bulletTimeUpgradeLevelIndex = 0,
        movementXUpgradeLevelIndex = 0,
        movementYUpgradeLevelIndex = 0,
        movementZUpgradeLevelIndex = 0;
    void Start()
    {
        phaseUpgradeLevelIndex = PlayerPrefs.GetInt("phaseUpgradeLevelIndex");
        rocketUpgradeLevelIndex = PlayerPrefs.GetInt("rocketUpgradeLevelIndex");
        bulletTimeUpgradeLevelIndex = PlayerPrefs.GetInt("bulletTimeUpgradeLevelIndex");
        movementXUpgradeLevelIndex = PlayerPrefs.GetInt("movementXUpgradeLevelIndex");
        movementYUpgradeLevelIndex = PlayerPrefs.GetInt("movementYUpgradeLevelIndex");
        movementZUpgradeLevelIndex = PlayerPrefs.GetInt("movementZUpgradeLevelIndex");

        UpdateResourceText(PlayerPrefs.GetFloat("gResource"));
    }
    public void UpdateResourceText(float resourceAmount)
    {
        playerResource.text ="Resource: " + ((int)resourceAmount).ToString();
    }
    
    void Update()
    {
        
    }
}
