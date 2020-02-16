using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] private float cameraSpeed = 5f;
    [SerializeField] private float offset = 3f;

    // State Variables
    private float targetY;
    private Vector3 targetPosition;
    private Vector3 lastPosition;

    // Cached Object References
    PlayerController playerController;
    Disk disk;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
            MoveCamera();
    }

    private void MoveCamera()
    {
        disk = playerController.disk;
        if (disk != null)
        {
            targetY = disk.instantiatePosition.y;
        }
        else if (disk == null)
        {
            targetY = transform.position.y;
        }

        lastPosition = transform.position;
        targetPosition = new Vector3(transform.position.x, targetY + offset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        Debug.Log(targetY);
    }
}
