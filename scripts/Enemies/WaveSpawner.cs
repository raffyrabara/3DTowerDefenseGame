using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;
    private int waveIndex = 0;

    public float timeBetweenWaves = 5f;
    private float countdown = 1f;

    public TMP_Text waveCoundownText;
    public GameObject waveCountdown;
    public TMP_Text waveCountText;
    public TMP_Text waveSummary;
    private int waveCounter = 0;
    private int waveSummaryCounter;
    
    public float waveDelay = 0.5f;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public GameObject warningBoss;

    public GameManager gameManager;

    public bool isSpawn1 = false;
    public bool isSpawn2 = false;

    public float preparationTime = 10f; // New variable for preparation time
    private bool isPreparing = true; // Flag to indicate if we're in the preparation phase
    public GameObject preparationWarn;

    void Start()
    {
        EnemiesAlive = 0;
        waveCounter = 0;
        // Initialize the countdown to preparation time
        countdown = preparationTime;
    }

    void Update()
    {
        if (EnemiesAlive > 0)
        {  
            return;
        }
        
        // If the preparation time is not over yet
        if (isPreparing)
        {
            if (countdown <= 0f)
            {
                // Preparation time is over, start spawning waves
                isPreparing = false;
                countdown = timeBetweenWaves;
               // waveCountdown.SetActive(true);
                preparationWarn.SetActive(false);
            }
            else
            {
                // Countdown during preparation time
                waveCountdown.SetActive(true);
                waveCoundownText.text = Mathf.Round(countdown).ToString();
                countdown -= Time.deltaTime;
                preparationWarn.SetActive(true);
            }
        }
        else // After preparation time, start spawning waves
        {
            if (waveIndex == waves.Length)
            {
                gameManager.GameWon();
                this.enabled = false;
            }
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                waveCounter++;
                waveIndex++;
                countdown = timeBetweenWaves;
                waveCountdown.SetActive(false);
                warningBoss.SetActive(false);
            }
            else
            {
                if (waveIndex == 6)
                {
                    warningBoss.SetActive(true);
                }
                waveCountdown.SetActive(true);
                waveCoundownText.text = Mathf.Round(countdown).ToString();
            }
            countdown -= Time.deltaTime;
            waveCountText.text = "WAVE " + waveCounter + " / 10";
            waveSummaryCounter = waveCounter - 1;
            waveSummary.text =  waveSummaryCounter.ToString();
        }
    }

    public IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.count; i++)
        {
            Transform spawnPoint;
            if (i % 2 == 0) {
                spawnPoint = spawnPoint1;
                isSpawn1 = true;
                isSpawn2 = false;
            } else {
                spawnPoint = spawnPoint2;
                isSpawn1 = false;
                isSpawn2 = true;
            }
            SpawnEnemy(wave.enemy, spawnPoint);
            yield return new WaitForSeconds(1f / wave.rate);
        }
    }

    void SpawnEnemy(GameObject enemy, Transform spawnPoint)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
