using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    // Configuration Parameters

    // Player State
    private float playerMovementSpeed;
    private float playerOscillationRadius;
    public Vector3 playerLastPosition;
    private float playerAngle;
    private bool clockwise;

    // Obstacle State
    private Vector3 instantiatePosition;

    // Cached Component References
    GameObject player;
    Rigidbody2D playerRigidBody;
    Player playerController;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            playerRigidBody = player.GetComponent<Rigidbody2D>();
            playerController = player.GetComponent<Player>();
            playerMovementSpeed = playerController.playerMovementSpeed;
        }
        instantiatePosition = transform.position;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            playerOscillationRadius = transform.localScale.x / 2 + player.transform.localScale.x / 2;
        }
    }

    public void Oscillate()
    {
        playerRigidBody.isKinematic = true;
        playerLastPosition = player.transform.position;
        if (clockwise)
        {
            playerAngle += (playerMovementSpeed / (playerOscillationRadius * 2 * Mathf.PI)) * Time.deltaTime;
        }
        else
        {
            playerAngle -= (playerMovementSpeed / (playerOscillationRadius * 2 * Mathf.PI)) * Time.deltaTime;
        }
        float x = Mathf.Cos(playerAngle) * playerOscillationRadius;
        float y = Mathf.Sin(playerAngle) * playerOscillationRadius;
        player.transform.position = new Vector3(x, y, transform.position.z) + transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Vector3 playerDirection = other.transform.position - transform.position;
            Vector3 playerPosition = other.transform.position;
            playerLastPosition = playerController.playerLastPosition;
            playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) + 2 * Mathf.PI;
            if ((playerLastPosition.x - transform.position.x) * (player.transform.position.y - transform.position.y)
                - (playerLastPosition.y - transform.position.y) * (player.transform.position.x - transform.position.x) > 0)
            {
                clockwise = true;
            }
            else
            {
                clockwise = false;
            }
        }
    }
}
