using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject boss;
    public Vector3 spawnValues;
    public Vector3 spawnValuesBoss;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float startBossWait;
    public float endBossWait;

    public AudioClip musicClipBackground;
    public AudioClip musicClipBoss;
    public AudioClip musicClipWin;
    public AudioClip musicClipDefeat;
    public AudioSource musicSource;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;

    private bool gameOver;
    private bool restart;
    private int score;

    public bool winCondition;

    void Start()
    {
        winCondition = false;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());

        musicSource.clip = musicClipBackground;
        musicSource.Play();
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

        if (gameOver)
        {
            Defeat();
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

                if (score >= 250)
                {
                    yield return StartCoroutine(SpawnBoss());
                    break;
                }
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                break;
            }
        }
    }

    public IEnumerator SpawnBoss()
    {
        musicSource.clip = musicClipBoss;
        musicSource.Play();
        yield return new WaitForSeconds(startBossWait);

        Vector3 spawnPosition = new Vector3(spawnValuesBoss.x, spawnValuesBoss.y, spawnValuesBoss.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(boss, spawnPosition, spawnRotation);
        yield return new WaitForSeconds(endBossWait);

        yield return null;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 2000)
        {
            winCondition = true;
            musicSource.clip = musicClipWin;
            musicSource.Play();
            winText.text = "Game created by Camille Morales";
            gameOver = true;
            restart = true;
        }
    }

    void Defeat()
    {
        restartText.text = "Press 'R' to Restart";
        restart = true;
    }

    public void GameOver()
    {
        musicSource.clip = musicClipDefeat;
        musicSource.Play();

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
