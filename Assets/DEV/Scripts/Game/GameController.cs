using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
