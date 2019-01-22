using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform player;
    public GameObject gameOverPanel;

    public AudioSource aud;
    public void GainResource()
    {
      
       Player.gainedResource = (Player.TravelledDistance * Player.difficulty) * Player.resourceMultipleValue;
    }
   
    public void RestartGame()
    {
        
        Player.ResetStaticValues();
        SceneController.OpenSceneByIndex(0);
    }
    public void GameOverEvents()
    {
        Player.isGameRunning = false;
       
        GainResource();
        Debug.Log("Gained Resource: " + Player.gainedResource);

        StartCoroutine(SpawnUI());
    }
    IEnumerator SpawnUI()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(gameOverPanel, new Vector3(player.position.x, 5f, player.position.z + 5f), Quaternion.identity);
    }
    private void Update()
    {
        if (Player.isGameRunning == false && Input.GetKeyUp(KeyCode.R))
        {
            RestartGame();
        }
    }
    void Start()
    {
        StartCoroutine(SesCal());
    }
    IEnumerator SesCal()
    {
        yield return new WaitForSeconds(5f);
        aud.Play();
    }
}
