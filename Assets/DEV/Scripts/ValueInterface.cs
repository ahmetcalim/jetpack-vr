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
        player.VelocityIncreaseAmount = velocityZIncreamentAmount;
        player.VelocityZBase = velocityZBase;
        Player.VelocityZMax = velocityZMax;
        Debug.Log("Değerler Değiştirildi Lütfen, " + player.gameObject + " objesinin üzerinden kontrol ediniz.");
    }
}

