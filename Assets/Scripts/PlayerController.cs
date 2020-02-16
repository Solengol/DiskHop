using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Configuration Parameters
    public float playerMovementSpeed = 50f;

    // State Variables
    public bool isCollided = false;
    public bool isJumping = false;
    public Vector3 lastPosition;

    // Cached Object References
    Rigidbody2D playerRigidbody;
    public Disk disk = null;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null)
        {
            isCollided = false;
        }
    }

    private void HandleMovement()
    {
        if (isCollided && disk != null)
        {
            disk.Oscillate();
        }
        else
        {
            Jump();
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
            isJumping = true;
            lastPosition = transform.position;
            Vector3 direction = transform.position - disk.lastPlayerPosition;
            direction.Normalize();
            playerRigidbody.isKinematic = false;
            transform.position += direction * (playerMovementSpeed / (2 * Mathf.PI)) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        disk = other.gameObject.GetComponent<Disk>();
    }
}
