using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float BGTimer;

    public Text gameOverText;
    public Text restartText;
    public Text ScoreText;
    public Text winText;
    public Text hardText;

    public int score;
    private int winCounter;

    private GameObject background;

    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;

    private Mover mover;
    private Mover mover2;
    private Mover mover3;
    public GameObject asteroid;
    public GameObject asteroid2;
    public GameObject asteroid3;
    private bool restart;
    private bool gameOver;
    public bool hardMode; 

    void Start()
    {
        score = 0;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        winCounter = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        background = GameObject.Find("Background");
        mover = asteroid.GetComponent<Mover>();
        mover2 = asteroid2.GetComponent<Mover>();
        mover3 = asteroid3.GetComponent<Mover>();
        mover.speed = -5;
        mover2.speed = -5;
        mover3.speed = -5;
        hardMode = false;
        hardText.text = ("Press 'H' to toggle Hard Mode!");



    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Space Shooter");
            }
        }
        if (Input.GetKey("escape")) //if escape key is pressed end game
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {

            if (hardMode == false)
            {
                Debug.Log("H Set");
                hardMode = true;
                mover.speed = -10;
                mover2.speed = -10;
                mover3.speed = -10;
            }
            else
            {
                mover.speed = -5;
                mover2.speed = -5;
                mover3.speed = -5;
                hardMode = false;
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
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            
            if (gameOver)
            {
                restartText.text = "Press 'Spacebar' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "POINTS: " + score;
        if (score >= 100)
        {
            StartCoroutine(BGZoom());
            winText.text = "You win! GAME CREATED BY KEVIN H. DAVIS";
            gameOver = true;
            restart = true;
            winCounter++;
            if (winCounter == 1)
            {
                musicSource.clip = musicClipOne;
                musicSource.volume = .25f;
                musicSource.Play();
                musicSource.loop = true;
            }

        }


    }
    IEnumerator BGZoom()
    {
        while (background.GetComponent<BGScroller>().scrollSpeed > -60f)
        {
            yield return new WaitForSeconds(BGTimer);
            background.GetComponent<BGScroller>().scrollSpeed *= 1.51f;
            yield return background;
        }
    }
    
    public void GameOver()
    {
        if (score < 100)
        {
            gameOverText.text = "You lose! GAME CREATED BY KEVIN H. DAVIS";
            gameOver = true;
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = true;

        }
    }
}
