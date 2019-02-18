using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public NodeGraph nGraph;
    public Transform parentT;
    public GameObject doubleHorizantalLaserRed,doubleHorizantalLaserGreen, doubleVerticalLaserRed, doubleVerticalLaserGreen;
    public GameObject singleHorizantalLaserRed, singleHorizantalLaserGreen, singleVerticalLaserRed, singleVerticalLaserGreen;
    public float frequency = 5.0f;
    public float magnitude = 0.25f;

    private Vector2 pos;
    private Vector2 perpendicularDir;
    int i = 0;
    public bool generateActive;
    float density = .1f;
    public int generateCount;

    public void GenerateObstacle()
    {
        if (!generateActive)
        {
            if (nGraph.currentPhaseType == NodeGraph.PhaseType.Phase_2)
            {
                int rndValue = Random.Range(0, 4);
                switch (rndValue)
                {
                    case 0:
                        SetDoubleLaser(doubleHorizantalLaserRed, new Vector2(3f, 0));
                        break;
                    case 1:
                        SetDoubleLaser(doubleHorizantalLaserGreen, new Vector2(3f, 0));
                        break;
                    case 2:
                        SetDoubleLaser(doubleVerticalLaserRed, new Vector2(0, 7.6f));
                        break;
                    case 3:
                        SetDoubleLaser(doubleVerticalLaserGreen, new Vector2(3, 7.6f));
                        break;
                    default:
                        break;
                }
                generateActive = true;
            }
            else if (nGraph.currentPhaseType == NodeGraph.PhaseType.Phase_1_1)
            {
                int rndValue = Random.Range(0, 5);

                switch (rndValue)
                {
                    case 0:
                        break;
                    case 1:
                        SetSingleLaser(singleVerticalLaserRed, true);
                        break;
                    case 2:
                        SetSingleLaser(singleHorizantalLaserRed, false);
                        break;
                    case 3:
                        SetSingleLaser(singleVerticalLaserGreen, true);
                        break;
                    case 4:
                        SetSingleLaser(singleHorizantalLaserGreen, false);
                        break;
                    default:
                        break;
                }
                generateActive = true;
            }
            
        }
        
    }

    void SetDoubleLaser(GameObject _prefab, Vector2 _dir)
    {
        generateCount = Random.Range(10, 60);
        pos = _prefab.transform.position;
        perpendicularDir = new Vector2(-_dir.y, _dir.x);
        StartCoroutine(GenerateDoubleLaser(_prefab));
    }
    void SetSingleLaser(GameObject laser, bool isVerticalAxis)
    {
        generateCount = Random.Range(1, 5);
        StartCoroutine(GenerateSingleLaser(laser, isVerticalAxis));
    }

    IEnumerator GenerateDoubleLaser(GameObject _prefab)
    {
        GameObject g = Instantiate(_prefab, parentT);
        g.transform.position = pos + perpendicularDir * Mathf.Sin(Time.time * frequency) * magnitude;      
        i++;
        Debug.Log(i);
        yield return new WaitForSeconds(density);
        if (i <= generateCount)
        {
            StartCoroutine(GenerateDoubleLaser(_prefab));
        }
        else
        {
            i = 0;
            generateActive = false;
        }

    }

    IEnumerator GenerateSingleLaser(GameObject laser, bool isVerticalAxis)
    {
        if (isVerticalAxis)
        {
            GameObject laserObj = Instantiate(laser, parentT);
            laserObj.transform.position = new Vector3(Random.Range(2.5f, 24.5f), laserObj.transform.localPosition.y, 0);
        }
        else
        {
            GameObject laserObj = Instantiate(laser, parentT);
            laserObj.transform.position = new Vector3(laserObj.transform.localPosition.x, Random.Range(3, 17), 0);
        }
        i++;
        yield return new WaitForSeconds(1f);
        if (i <= generateCount)
        {
            StartCoroutine(GenerateSingleLaser(laser, isVerticalAxis));
        }
        else
        {
            i = 0;
            generateActive = false;
        }


    }
}
