using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    public int enemyCount;
    public int waveCount = 1;


    public GameObject player;

    private EnemyX enemySpeedscript;

    private void Start()
    {
        enemySpeedscript = GameObject.Find("Enemy").GetComponent<EnemyX>();
    }
    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyX>().Length;

        if (enemyCount == 0)
        {
            waveCount++;
            SpawnEnemyWave(waveCount);
            PowerUpSpawn();
            ResetPlayerPosition(); // put player back at start
            enemySpeedscript.speed++;
            Debug.Log(enemySpeedscript.speed);
        }

    }

    // Generate random spawn position for powerups and enemy balls
    private Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        Vector3 randomPos = new Vector3(xPos, 0, zPos);
        return randomPos;
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }
    void PowerUpSpawn()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }
}
