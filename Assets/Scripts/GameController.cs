using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public int spawnCount;
    public float spawnWait;
    public float startSpawn;
    public float waveWait;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI quitText;
    public int score;

    private bool gameover , restart;


    void Update()
    {
        if(restart == true)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }


    IEnumerator SpawnValues()
    {
        yield return new WaitForSeconds(startSpawn);
        while(true)
        {
            
            for(int i = 0;i < spawnCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-3,4) ,1, 10);
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(hazard,spawnPosition,spawnRotation);
                
                // Coroutine
                // Coroutineler ile metotlar arası farklar
                // 3 temel fark var aralaralarında
                // 1. IEnumerator döndürmek zorundadırlar
                // 2. En az bir adet yield ifadesi bulunmalıdır
                // 3. Coroutineler çapırılırken mutlaka StartCoroutine
                // motoduyla çağırılmalıdır.

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameover)
            {
                restartText.text = "Press 'R' for Restart.";
                quitText.text = "Press 'Q' for Quit";
                restart = true;
                break;
            }
        }        
    }

    public void UpdateScore()
    {
        score+=10;
        scoreText.text = "Score : " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameover = true;
    }

    void Start()
    {   
        quitText.text = "";
        gameOverText.text = "";
        restartText.text = "";
        gameover = false;
        restart = false;
        StartCoroutine(SpawnValues());     
    }

    
}
