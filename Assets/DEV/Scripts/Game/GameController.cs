using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
