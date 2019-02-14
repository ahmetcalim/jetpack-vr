using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeGenerator : MonoBehaviour
{
    public enum UpgradeTitle { POWERUP, MOVEMENT }

    public UpgradeTitle upgradeTitle;

    public enum UpgradeFeaturePowerup { PHASE, ROCKET, BULLET_TIME }

    public UpgradeFeaturePowerup upgradeFeaturePowerup;

    public enum UpgradeFeatureMovement { MOVEMENT_X, MOVEMENT_Y, MOVEMENT_Z }

    public UpgradeFeatureMovement upgradeFeatureMovement;

    public enum Operator { ADDITION, ELIMINATION, MULTIPLICATION, DIVISION }
    public Operator _operator;

    public float movement_x;
    public float movement_y;
    public float movement_z;
    public float phase_time;
    public float bullettime_time;
    public float rocket_area_lenght;
    public float cost;
    public Button upgradeButton;
    public Transform upgradeButtonParent;
    
    public void CreateUpgrade()
    {
        var upgradeBtnCopy = Instantiate(upgradeButton) as Button;
        upgradeBtnCopy.transform.SetParent(upgradeButtonParent);
        upgradeBtnCopy.transform.localPosition = new Vector3(upgradeBtnCopy.transform.position.x, upgradeBtnCopy.transform.position.y, 0f);
     
        Upgrade upgradeClass = upgradeBtnCopy.GetComponent<Upgrade>();
        upgradeBtnCopy.transform.SetSiblingIndex(0);

        upgradeClass.cost = cost;
        switch (_operator)
        {
            case Operator.ADDITION:
                upgradeClass._operator = Upgrade.Operator.ADDITION;
                break;
            case Operator.ELIMINATION:
                upgradeClass._operator = Upgrade.Operator.ELIMINATION;
                break;
            case Operator.MULTIPLICATION:
                upgradeClass._operator = Upgrade.Operator.MULTIPLICATION;
                break;
            case Operator.DIVISION:
                upgradeClass._operator = Upgrade.Operator.DIVISION;
                break;
            default:
                break;
        }
        switch (upgradeTitle)
        {
            case UpgradeTitle.POWERUP:
                upgradeClass.upgradeTitle = Upgrade.UpgradeTitle.POWERUP;

                switch (upgradeFeaturePowerup)
                {
                    case UpgradeFeaturePowerup.PHASE:
                        upgradeClass.upgradeType = Upgrade.UpgradeType.PHASE;
                        upgradeClass.upgradeAmount = phase_time;
                        break;
                    case UpgradeFeaturePowerup.ROCKET:
                        upgradeClass.upgradeType = Upgrade.UpgradeType.ROCKET;
                        upgradeClass.upgradeAmount = rocket_area_lenght;
                        break;
                    case UpgradeFeaturePowerup.BULLET_TIME:
                        upgradeClass.upgradeType = Upgrade.UpgradeType.BULLET_TIME;
                        upgradeClass.upgradeAmount = bullettime_time;
                        break;
                    default:
                        break;
                }
                break;
            case UpgradeTitle.MOVEMENT:
                upgradeClass.upgradeTitle = Upgrade.UpgradeTitle.MOVEMENT;
                switch (upgradeFeatureMovement)
                {
                    case UpgradeFeatureMovement.MOVEMENT_X:
                        upgradeClass.upgradeType = Upgrade.UpgradeType.MOVEMENT_X;
                        upgradeClass.upgradeAmount = movement_x;
                        break;
                    case UpgradeFeatureMovement.MOVEMENT_Y:
                        upgradeClass.upgradeType = Upgrade.UpgradeType.MOVEMENT_Y;
                        upgradeClass.upgradeAmount = movement_y;
                        break;
                    case UpgradeFeatureMovement.MOVEMENT_Z:
                        upgradeClass.upgradeType = Upgrade.UpgradeType.MOVEMENT_Z;
                        upgradeClass.upgradeAmount = movement_z;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }


    }
   
    public void QuickSort(List<int> dizi, int baslangic, int bitis)
    {
        int i;
        if (baslangic < bitis)
        {
            i = partition(dizi, baslangic, bitis);
            QuickSort(dizi, baslangic, i - 1);
            QuickSort(dizi, i + 1, bitis);
        }

    }
    public int partition(List<int> A, int baslangic, int bitis)
    {
        int gecici;
        int x = A[bitis];
        int i = baslangic - 1;

        for (int j = baslangic; j <= bitis - 1; j++)
        {
            if (A[j] <= x)
            {
                i++;
                gecici = A[i];
                A[i] = A[j];
                A[j] = gecici;
            }
        }
        gecici = A[i + 1];
        A[i + 1] = A[bitis];
        A[bitis] = gecici;
        return i + 1;
    }

}
