using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerUp : MonoBehaviour
{
    public enum PowerUpType {PHASE, ROCKET, BULLET_TIME}
    public PowerUpType powerUpType;
    public Sprite sprite;
    public string tagName;
}
