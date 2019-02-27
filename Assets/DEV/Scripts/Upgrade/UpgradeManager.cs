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
        SetPlayerPrefs(PlayerPrefs.GetInt("phaseUpgradeLevelIndex"), "phaseUpgradeLevelIndex");
        SetPlayerPrefs(PlayerPrefs.GetInt("rocketUpgradeLevelIndex"), "rocketUpgradeLevelIndex");
        SetPlayerPrefs(PlayerPrefs.GetInt("bulletTimeUpgradeLevelIndex"), "bulletTimeUpgradeLevelIndex");


        UpdateResourceText(PlayerPrefs.GetFloat("gResource"));
    }
    private void SetPlayerPrefs(float a, string name)
    {
        if (a<1)
        {
            PlayerPrefs.SetInt(name, 0);
        }
    }
    public void UpdateResourceText(float resourceAmount)
    {
        playerResource.text ="Resource: " + ((int)resourceAmount).ToString();
    }
    
    void Update()
    {
        
    }
}
