using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] private int spawnCap;
    [SerializeField] private Vector3 spawnStartPosition;
    [SerializeField] public float spawnSpreadDistance;
    [SerializeField] private float despawnDistance;

    // State
    private static GameObject[] allLevels;
    private List<GameObject> gameLevels;

    // Cached Component References
    private GameObject level;
    private GameObject player;

    void Awake()
    {
        allLevels = Resources.LoadAll<GameObject>("Levels");
        gameLevels = new List<GameObject>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        SpawnLevels();
        DeleteLevels();
    }

    private void SpawnLevels()
    {
        if (gameLevels.Count < spawnCap)
        {
            int whichObstacle = Random.Range(0, allLevels.Length);
            level = Instantiate(allLevels[whichObstacle]) as GameObject;
            level.transform.position = spawnStartPosition;
            gameLevels.Add(level);
            spawnStartPosition.y += spawnSpreadDistance;
        }
    }

    private void DeleteLevels()
    {
        if (player != null && Vector3.Distance(gameLevels[0].transform.position, player.transform.position) > despawnDistance)
        {
            GameObject obstacleToDelete = gameLevels[0];
            Destroy(obstacleToDelete);
            gameLevels.Remove(obstacleToDelete);
        }
    }
}
