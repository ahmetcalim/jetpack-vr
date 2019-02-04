using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerUp : MonoBehaviour
{
    public enum PowerUpType {PHASE, ROCKET}
    public PowerUpType powerUpType;
    public bool hasTime;
    public List<float> duringTimes;
    public Sprite phaseImage;
    public AudioClip takingSound;
    public AudioClip usingSound;

    

}
