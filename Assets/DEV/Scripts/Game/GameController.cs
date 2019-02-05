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
    public void GameOverEvents()
    {
        Player.isGameRunning = false;
     
        StartCoroutine(SpawnUI());
    }
    IEnumerator SpawnUI()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(gameOverPanel, new Vector3(player.position.x, 5f, player.position.z + 5f), Quaternion.identity);
    }
 
}
