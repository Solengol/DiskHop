using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskSpawner : MonoBehaviour
{
    [SerializeField] GameObject firstObstacle;
    [SerializeField] int spawnCap = 5;
    [SerializeField] public float spawnSpreadDistance = 5f;
    [SerializeField] Vector3 spawnPosition = new Vector3(0f, 0f, 0f);

    private static GameObject[] allObstacles;
    private int whichObstacle;
    private List<GameObject> gameObstacles;
    private List<int> whichObstacles;
    private GameObject obstacle;
    private GameObject player;

    void Start()
    {
        allObstacles = Resources.LoadAll<GameObject>("Obstacles");
        gameObstacles = new List<GameObject>();
        player = GameObject.Find("Player");
        SpawnFirstObstacle();
        whichObstacle = 0;
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
        gameObstacles.Add(obstacle);
        spawnPosition.y += spawnSpreadDistance;
    }

    private void SpawnObstacles()
    {
        if (gameObstacles.Count < spawnCap)
        {
            int obstacleCheck = Random.Range(0, allObstacles.Length);
            // rerun until different obstacle
            while (whichObstacle == obstacleCheck)
            {
                obstacleCheck = Random.Range(0, allObstacles.Length);
            }
            whichObstacle = obstacleCheck;
            obstacle = Instantiate(allObstacles[whichObstacle]) as GameObject;
            obstacle.transform.position = spawnPosition;
            gameObstacles.Add(obstacle);
            spawnPosition.y += spawnSpreadDistance;

        }
    }

    private void DeleteObstacles()
    {
        if (player != null && Vector3.Distance(gameObstacles[0].transform.position, player.transform.position) > 10f)
        {
            GameObject obstacleToDelete = gameObstacles[0];
            Destroy(obstacleToDelete);
            gameObstacles.Remove(obstacleToDelete);
        }
    }
}
