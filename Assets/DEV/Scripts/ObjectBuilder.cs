using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBuilder : MonoBehaviour
{
    public enum UpgradeTitle { POWERUP, MOVEMENT }
    public UpgradeTitle upgradeTitle;
    public UpgradeManager upgradeManager;
    public void AddUpgradeTitle()
    {
        switch (upgradeTitle)
        {
            case UpgradeTitle.POWERUP:
              
                break;
            case UpgradeTitle.MOVEMENT:
              
                break;
            default:
                break;
        }
    }
}
