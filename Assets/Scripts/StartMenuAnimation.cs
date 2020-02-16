using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuAnimation : MonoBehaviour
{
    private float angle;
    private float speed = 50f;
    private float radius;
    private GameObject obstacle;


    void Start()
    {
        obstacle = GameObject.Find("ObstacleAnim");
        radius = transform.localScale.x / 2 + obstacle.transform.localScale.x / 2;
    }


    void Update()
    {
        Oscillate();
    }

    private void Oscillate()
    {
        angle += (speed/ (radius * 2 * Mathf.PI)) * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, transform.position.z) + obstacle.transform.position;
    }
}
