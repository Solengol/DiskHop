using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] public float playerMovementSpeed;

    // State
    public Vector3 playerLastPosition;
    public bool hasCollided;
    private bool jumping;

    // Cached Component References
    Rigidbody2D playerRigidBody;
    public Disk disk;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        jumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            jumping = true;
            hasCollided = false;
        }
    }

    private void HandleMovement()
    {
        if (jumping && !hasCollided)
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
            playerLastPosition = transform.position;
            Vector3 direction = transform.position - disk.playerLastPosition;
            direction.Normalize();
            playerRigidBody.isKinematic = false;
            transform.position += direction * (playerMovementSpeed / (2 * Mathf.PI)) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Disk")
        {
            disk = other.gameObject.GetComponent<Disk>();
            jumping = false;
            hasCollided = true;
        }
    }
}
