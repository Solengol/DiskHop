using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    /// <summary>The objects initial position.</summary>
    private Vector2 startPosition;
    /// <summary>The objects updated position for the next frame.</summary>
    private Vector2 newPosition;

    /// <summary>The speed at which the object moves.</summary>
    [SerializeField] private int maxSpeed = 5;
    [SerializeField] private int minSpeed = 1;
    private int speed;

    /// <summary>The maximum distance the object may move in either y direction.</summary>
    [SerializeField] private int maxDistance = 1;

    void Start()
    {
        startPosition = transform.position;
        newPosition = transform.position;
        speed = Random.Range(minSpeed, maxSpeed + 1);
    }

    void Update()
    {
        newPosition.x = startPosition.x + (maxDistance * Mathf.Sin(Time.time * speed));
        transform.position = newPosition;
    }
}
