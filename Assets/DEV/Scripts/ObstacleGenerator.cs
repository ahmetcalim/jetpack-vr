using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public NodeGraph nGraph;
    public CorridorGenerator cGenerator;
    public Transform parentT;
    float parentZ= 1652.8f;

    public GameObject doubleHorizantalLaserRed, doubleHorizantalLaserGreen, doubleVerticalLaserRed, doubleVerticalLaserGreen;
    public GameObject singleHorizantalLaserRed, singleHorizantalLaserGreen, singleVerticalLaserRed, singleVerticalLaserGreen;
    public GameObject RotatableLaserGreen, RotatableLaserRed;
    public GameObject SciFi_1, SciFi_2;

    private Vector3 pos;
    private Vector3 perpendicularDir;

    bool generateActive;
    int generateCount;

    float frequency = 5.0f;
    float magnitude = 0.7f;
    int rndValue;
    int i = 0;
    int increase;
    float laserLenght; 
    int reverse = 1; //Rotatable laserin yönü

    public void GenerateObstacle( )
    {
        if (!generateActive)
        {            
            if (nGraph.currentPhaseType==NodeGraph.PhaseType.Phase_1_1|| nGraph.currentPhaseType == NodeGraph.PhaseType.Phase_2)
            {
                if (nGraph.currentThemeType != KNode.ThemeType.Glass && nGraph.currentThemeType != KNode.ThemeType.OpenSpace)
                {
                    GenerateDoubleObstacle();
                }
                else
                {
                    i = generateCount;
                    increase = 0;
                    parentZ = nGraph.tailNode.asset.transform.localPosition.z;
                }
            }
            else
            {
                i = generateCount ;
                increase = 0;
                parentZ = nGraph.tailNode.asset.transform.localPosition.z;
            }
        }
    }

    void GenerateDoubleObstacle()
    {
        rndValue = Random.Range(0, 6);
        switch (rndValue)
        {
            case 0:
                SetDoubleLaser(doubleHorizantalLaserRed, new Vector2(3f, 0),Random.Range(2,6),false);
                break;
            case 1:
                SetDoubleLaser(doubleHorizantalLaserGreen, new Vector2(3f, 0), Random.Range(2, 6),false);
                break;
            case 2:
                SetDoubleLaser(doubleVerticalLaserRed, new Vector2(0, 7.6f), Random.Range(1, 4),false);
                break;
            case 3:
                SetDoubleLaser(doubleVerticalLaserGreen, new Vector2(0, 7.6f), Random.Range(1, 4),false);
                break;
            case 4:
                SetDoubleLaser(RotatableLaserRed, new Vector2(0, 7.6f), Random.Range(1, 4), true);
                break;
            case 5:
                SetDoubleLaser(RotatableLaserGreen, new Vector2(0, 7.6f), Random.Range(1, 4), true);
                break;
            default:
                break;

        }
    }

    void SetDoubleLaser(GameObject _prefab, Vector2 _dir,float _frequency,bool rotatable)
    {
        if (Random.Range(0, 2) == 0)
            reverse = 1;
        else
            reverse = -1;

        laserLenght = parentZ + increase+30;
        if (cGenerator.currPosition.z>=laserLenght)
        {
            generateCount = Random.Range(5, 30);
            pos = _prefab.transform.position;
            perpendicularDir = new Vector3(-_dir.y, _dir.x);
            frequency = _frequency;
            parentT = nGraph.tailNode.asset.transform;
            parentZ = nGraph.tailNode.asset.transform.localPosition.z;
            increase = 0;
            generateActive = true;

            if (!rotatable)
            {
                StartCoroutine(InstantiateDoubleLaser(_prefab,false));
            }
            else
            {
                StartCoroutine(InstantiateDoubleLaser(_prefab,true));
            }

        }  
    }

    IEnumerator InstantiateDoubleLaser(GameObject _prefab,bool rotatable)
    {       
        GameObject g = Instantiate(_prefab,parentT );
        if (!rotatable)
        {
            g.transform.localPosition = pos + perpendicularDir * Mathf.Sin(Time.time * frequency) * magnitude;
            g.transform.localPosition = new Vector3(g.transform.localPosition.x, g.transform.localPosition.y, increase);
        }
        else
        {
            g.transform.localPosition = new Vector3(g.transform.localPosition.x, g.transform.localPosition.y, increase);
            g.transform.Rotate(0, 0, reverse*increase);
        }
        
        increase += 10;
        i++;
        yield return new WaitForSeconds(.1f);
        if (i < generateCount)
        {
            StartCoroutine(InstantiateDoubleLaser(_prefab,rotatable));
        }
        else
        {
            i = 0;
            generateActive = false;
        }

    }
   

}
