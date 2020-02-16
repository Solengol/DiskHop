using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    // State Variables
    private float playerSpeed;
    private float angle;
    private float obstacleRadius;
    private bool isClockwise;
    private bool isCollided;
    public Vector3 lastPlayerPosition;
    public Vector3 instantiatePosition;

    // Cached Object References //
    GameObject player;
    Rigidbody2D playerRigidBody;
    GameSession gameSession;
    PlayerController playerController;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            playerRigidBody = player.GetComponent<Rigidbody2D>();
            playerController = player.GetComponent<PlayerController>();
            playerSpeed = playerController.playerMovementSpeed;
        }
        gameSession = FindObjectOfType<GameSession>();
        instantiatePosition = transform.position;
    }

    public void FixedUpdate()
    {
        if (player != null)
        {
            obstacleRadius = transform.localScale.x / 2 + player.transform.localScale.x / 2;
        }
    }

    public void Oscillate()
    {
        playerRigidBody.isKinematic = true;
        lastPlayerPosition = player.transform.position;
        if (isClockwise)
        {
            angle += (playerSpeed / (obstacleRadius * 2 * Mathf.PI)) * Time.deltaTime;
        }
        else
        {
            angle -= (playerSpeed / (obstacleRadius * 2 * Mathf.PI)) * Time.deltaTime;
        }
        float x = Mathf.Cos(angle) * obstacleRadius;
        float y = Mathf.Sin(angle) * obstacleRadius;
        player.transform.position = new Vector3(x, y, transform.position.z) + transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isCollided)
        {
            isCollided = true;
            Vector3 playerDirection = other.transform.position - transform.position;
            Vector3 playerPosition = other.transform.position;
            lastPlayerPosition = playerController.lastPosition;
            angle = Mathf.Atan2(playerDirection.y, playerDirection.x) + 2 * Mathf.PI;
            if ((lastPlayerPosition.x - transform.position.x) * (player.transform.position.y - transform.position.y) -
                (lastPlayerPosition.y - transform.position.y) * (player.transform.position.x - transform.position.x) > 0)
            {
                isClockwise = true;
            }
            else
            {
                isClockwise = false;
            }
            playerController.isCollided = true;
            gameSession.AddToScore(1);
        }
    }
}

