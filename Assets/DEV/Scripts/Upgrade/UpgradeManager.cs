using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeManager : MonoBehaviour
{
    public Text playerResource;
    
    void Start()
    {
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
