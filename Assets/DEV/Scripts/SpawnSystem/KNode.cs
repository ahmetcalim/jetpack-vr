using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNode : MonoBehaviour
{
    public List<KNode> links;
    public GameObject asset;
    public float scaleFactor;
    public KNode nextNode,prevNode;
    public TunnelType tunnelType;
    public ThemeType themeType;


    public enum TunnelType
    {
        Lab_Column, Lab_Section1, Lab_Section2, Lab_Section3, Lab_Section3_R, Lab_Section4, Lab_Section4_R, Lab_Section5, Lab_Section5_R,
        Glass_Column,Glass_Section1,
        Hospital_Column,Hospital_Section1, Hospital_Section2, Hospital_Section3,OpenSpace_Section
    }
    public enum ThemeType
    {
        Lab1,Lab2,Lab3,Hospital,Glass,OpenSpace
    }

}
