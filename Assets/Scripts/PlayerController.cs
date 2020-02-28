using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] public float playerMovementSpeed;

    // State
    public Vector3 playerLastPosition;
    public bool hasCollided;
    private bool jumping;
    private int diskInstance;

    // Cached Component References
    Rigidbody2D playerRigidBody;
    GameSession gameSession;
    WaveSpawner waveSpawner;
    public Disk disk;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        gameSession = FindObjectOfType<GameSession>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        diskInstance = 0;
        jumping = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
    }

    void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            jumping = true;
            hasCollided = false;
        }
    }

    private void HandleMovement()
    {
        if (jumping)
        {
            Jump();
        }
        else if (!jumping && hasCollided)
        {
            disk.Oscillate();
        }
    }

    private void Jump()
    {
        if (disk == null)
        {
            transform.position += transform.up * (playerMovementSpeed / (2 * Mathf.PI)) * Time.deltaTime;
        }
        else if (disk != null)
        {
            disk.inCollision = false;
            playerLastPosition = transform.position;
            Vector3 direction = transform.position - disk.playerLastPosition;
            direction.Normalize();
            Vector3 movement = direction * (playerMovementSpeed / (2 * Mathf.PI)) * Time.deltaTime;
            transform.position = playerLastPosition + movement;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Disk")
        {
            disk = other.gameObject.GetComponent<Disk>();
            if (diskInstance != other.gameObject.GetInstanceID())
            {
                diskInstance = other.gameObject.GetInstanceID();
                hasCollided = true;
                jumping = false;
                if (!disk.hasCollided)
                {
                    disk.hasCollided = true;
                    gameSession.AddToScore(1);
                }
            }
        }
    }
}
