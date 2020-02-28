using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] GameObject firstObstacle = default;
    [SerializeField] int spawnCap = default;
    [SerializeField] public float spawnSpreadDistance;
    [SerializeField] List<GameObject> waves = default;

    // State Variables
    private int score;
    private int waveIndex;
    private int obstacleIndex;
    private bool lastWave;
    private Vector3 spawnPosition;

    // Cached Component References
    private GameObject obstacle;
    private GameObject player;
    private Wave currentWave;
    private List<GameObject> waveObstacles;
    private List<GameObject> currentObstacles;
    private List<int> obstacleIndexes;

    void Start()
    {
        currentObstacles = new List<GameObject>();
        obstacleIndexes = new List<int>();
        player = GameObject.Find("Player");
        waveIndex = 0;
        SpawnFirstObstacle();
        GetWaveObstacles(waveIndex);
        obstacleIndex = Random.Range(0, waveObstacles.Count);
    }

    void Update()
    {
        SpawnObstacles();
        DeleteObstacles();
    }

    private void SpawnFirstObstacle()
    {
        obstacle = Instantiate(firstObstacle) as GameObject;
        obstacle.transform.position = spawnPosition;
        currentObstacles.Add(obstacle);
        spawnPosition.y += spawnSpreadDistance;
    }

    private void GetWaveObstacles(int waveIndex)
    {
        currentWave = waves[waveIndex].GetComponent<Wave>();
        waveObstacles = currentWave.waveObstacles;
    }

    private void SpawnObstacles()
    {
        while (currentObstacles.Count <= spawnCap)
        {
            // Go to next wave if all obstacles have been spawned, unless last wave has been reached
            if (waveIndex == waves.Count - 1 && !lastWave)
            {
                lastWave = true;
                obstacleIndexes.Clear();
            }
            else if (obstacleIndexes.Count == waveObstacles.Count)
            { 
                if (!lastWave)
                {
                    waveIndex += 1;
                    GetWaveObstacles(waveIndex);
                }
                obstacleIndexes.Clear();
            }
            obstacleIndex = Random.Range(0, waveObstacles.Count);
            // Rerun RNG until new waveIndex is found
            while (obstacleIndexes.Contains(obstacleIndex))
            {
                obstacleIndex = Random.Range(0, waveObstacles.Count);
            }
            // Cache new waveIndex
            obstacleIndexes.Add(obstacleIndex);
            // Instantiate obstacle
            obstacle = Instantiate(waveObstacles[obstacleIndex]) as GameObject;
            obstacle.transform.position = spawnPosition;
            currentObstacles.Add(obstacle);
            spawnPosition.y += spawnSpreadDistance;
        }
    }

    private void DeleteObstacles()
    {
        if (player != null && Vector3.Distance(currentObstacles[0].transform.position, player.transform.position) > 10f)
        {
            GameObject obstacleToDelete = currentObstacles[0];
            Destroy(obstacleToDelete);
            currentObstacles.Remove(obstacleToDelete);
        }
    }
}