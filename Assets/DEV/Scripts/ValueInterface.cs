using UnityEngine;
using System.Collections;
using UnityEditor;

public class ValueInterface : MonoBehaviour
{
    public Player player;
    public float velocityZBase,
        velocityZMax,
        velocityZIncreamentAmount;

    public void SetValues()
    {
        player.velocityIncreaseAmount = velocityZIncreamentAmount;
        player.velocityZBase = velocityZBase;
        Player.velocityZMax = velocityZMax;
        Debug.Log("Değerler Değiştirildi Lütfen, " + player.gameObject + " objesinin üzerinden kontrol ediniz.");
    }
}

