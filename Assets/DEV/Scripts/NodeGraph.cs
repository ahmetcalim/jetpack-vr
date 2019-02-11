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
    int linkCount;

    private void Start()
    {
        StartCoroutine(GetTime());
    }

    public void Generate()
    {
        if (Next() == headNode)
            corridorGenerator.Create(headNode, true);
        else
            corridorGenerator.Create(currentNode, false);
    }
    public KNode Next()
    {
       
        linkCount = tailNode.links.Count;

        if (linkCount != 0)
        {
            currentNode = tailNode.links[Random.Range(0, linkCount)];
            if (time>=const_time)
            {
                ChangeTheme();
                const_time = Random.Range(30,60);

                for (int i = 0; i < linkCount; i++)
                {
                    if (tailNode.links[i].themeType!=currentThemeType)
                    {
                        currentNode = tailNode.links[i];
                        break;
                    }
                }
                time = 0;
            }
            else
            {
                if (currentNode.themeType!=currentThemeType)
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
                return this.headNode;
            else
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
        if (currentThemeType == c)
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

    int time=0 ;
    int const_time = 3;
    IEnumerator GetTime()
    {
        //Debug.Log("süre: " + time);
        yield return new WaitForSeconds(1);
        time++;
        StartCoroutine(GetTime());
    }

}
