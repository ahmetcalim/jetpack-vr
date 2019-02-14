using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGraph : MonoBehaviour
{
    public KNode headNode;
    public KNode tailNode;
    public KNode currentNode;
    public CorridorGenerator corridorGenerator;
    public KNode.ThemeType currentThemeType;
    public PhaseType currentPhaseType;
    public enum PhaseType
    {
       Phase_1_1,Phase_1_2,Phase_2
    }
    int linkCount;

    private void Start()
    {
        StartCoroutine(GetTime());
    }

    public void Generate()
    {
        if (Next() == headNode)
            corridorGenerator.Create(currentNode, true);
        else
            corridorGenerator.Create(currentNode, false);

    }

    public KNode Next()
    {
       
        linkCount = tailNode.links.Count;

        if (linkCount != 0)
        {
            currentNode = tailNode.links[Random.Range(0, linkCount)];

            if (currentPhaseType==PhaseType.Phase_1_1|| currentPhaseType == PhaseType.Phase_2)
            {              
                if (time >= const_time)
                {
                    ChangeTheme();
                    const_time = Random.Range(20, 40);

                    for (int i = 0; i < linkCount; i++)
                    {
                        if (tailNode.links[i].themeType != currentThemeType)
                        {
                            currentNode = tailNode.links[i];
                            break;
                        }
                    }
                    time = 0;
                }
                else
                {
                    if (currentNode.themeType != currentThemeType)
                    {
                        for (int i = 0; i < linkCount; i++)
                        {
                            if (tailNode.links[i].themeType == currentThemeType)
                            {
                                currentNode = tailNode.links[i];
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                currentThemeType = KNode.ThemeType.OpenSpace;
          
                if (currentNode.themeType != currentThemeType)
                {
                    for (int i = 0; i < linkCount; i++)
                    {
                        if (tailNode.links[i].themeType == currentThemeType)
                        {
                            currentNode = tailNode.links[i];
                            break;
                        }
                    }
                }

            }
            

            if (currentNode.tunnelType == headNode.tunnelType)
                currentNode = headNode;

                return currentNode;
        }
        else
        {
            Debug.Log("NULL ATIYOMMU HİÇ???");
            return tailNode;
        }
    }

    void ChangeTheme()
    {
        KNode.ThemeType c;
        c = GetRandomEnum<KNode.ThemeType>();
        if (currentThemeType == c||c==KNode.ThemeType.OpenSpace)
            ChangeTheme();
        else
            currentThemeType = c;

        Debug.Log(currentThemeType);
    }


    T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    int time;
    public int totalTime;
    int const_time = 10;
    IEnumerator GetTime()
    {
        yield return new WaitForSeconds(1);
        time++;
        totalTime++;
        switch (totalTime)
        {
            case 30:
                currentPhaseType = PhaseType.Phase_1_2;
                break;
            case 50:
                currentPhaseType = PhaseType.Phase_2;
                break;
            case 100:

                break;
            default:
                break;
        }
        StartCoroutine(GetTime());
    }


}
