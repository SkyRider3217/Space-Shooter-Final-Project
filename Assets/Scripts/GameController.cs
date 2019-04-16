using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject boss;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                SceneManager.LoadScene("SpaceShooterGame_01");
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards [Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (score >= 100)
            {
                SpawnBoss();
                break;
            }

            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }
        }
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(startWait);
        Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(boss, spawnPosition, spawnRotation);
        

        if (gameOver)
        {
            restartText.text = "Press 'R' to Restart";
            restart = true;
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 1000)
        {
            winText.text = "Game created by Camille Morales";
            gameOver = true;
            restart = true;
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

    void FixedUpdate()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
