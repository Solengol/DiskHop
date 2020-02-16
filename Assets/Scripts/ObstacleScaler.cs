using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScaler : MonoBehaviour
{

    [SerializeField] private float scaleFactor = 0.3f;

    /// <summary>The speed at which the object moves.</summary>
    private int maxSpeed = 3;
    [SerializeField] private int minSpeed = 1;
    private int speed;

    /// <summary>The maximum distance the object may move in either y direction.</summary>


    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed + 1);
    }

    void Update()
    {
        transform.localScale = (Mathf.PingPong(Time.time * speed, scaleFactor - 1) + 1 ) * Vector3.one;
    }
}
