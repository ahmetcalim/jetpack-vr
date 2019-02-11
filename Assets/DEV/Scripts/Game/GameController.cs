using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public Transform player;
    public GameObject gameOverPanel;

    public int avgFrameRate;
    public Text display_Text;
    public AudioSource aud;
    void Awake()
    {
        Application.targetFrameRate = 90;
    }
}
