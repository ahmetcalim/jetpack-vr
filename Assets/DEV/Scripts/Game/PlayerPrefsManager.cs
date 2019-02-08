using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static void SetToPlayerPrefs(float value, string playerPrefsName)
    {
        PlayerPrefs.SetFloat(playerPrefsName, value);
    }
    public static float GetPlayerValueFromPlayerPrefs(string name)
    {
        float returnedValue = PlayerPrefs.GetFloat(name);
        return returnedValue;
    }
}
