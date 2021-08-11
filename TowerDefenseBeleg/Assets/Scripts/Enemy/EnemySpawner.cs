using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    
    public static int EnemiesAlive;

    [Header("Setup Fields")]
    
    public GameManager gameManager;
    
    [Tooltip("The position of the spawn point.")]
    [SerializeField] private Transform spawnPoint;
    
    [Tooltip("Wave countdown text object.")]
    [SerializeField] private TMP_Text waveCountdownText;
    
    public WaveSettings[] waves;
    
    [Header("Attributes")]

    [Tooltip("The time between the waves.")]
    [SerializeField] private float timeBetweenWaves = 5f;
    
    [Tooltip("Time between each enemy.")]
    [SerializeField] private float timeBetweenEachEnemy;

    [Tooltip("The time it will take to spawn the first wave.")]
    [SerializeField] private float countdown = 2f;
    
    private int waveIndex;

    private void Update() {
        if (EnemiesAlive > 0) return;
        
        if (waveIndex == waves.Length) {
            gameManager.WinLevel();
            // disable script
            enabled = false;
        }
        
        // starts the coroutine for spawning enemy waves
        if (countdown <= 0f) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        
        UpdateCountdown();
    }
    
    // Spawn enemy wave coroutine
    private IEnumerator SpawnWave() {
        GameManager.Waves++;
        WaveSettings wave = waves[waveIndex];
        EnemiesAlive = wave.enemyCount;
        
        // spawn enemy
        for (int i = 0; i < wave.enemyCount; i++) {
            SpawnEnemyWave(wave.enemy);
            yield return new WaitForSeconds(1 / wave.spawnRate);
        }
        
        waveIndex++;
    }
    
    // Instantiates an enemy at the spawn point position and rotation
    private void SpawnEnemyWave(GameObject enemy) {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    // update wave countdown and text
    private void UpdateCountdown() {
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = $"{countdown:00}";
    }
    
}
