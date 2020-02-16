using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotator : MonoBehaviour
{
    private float angle;
    private float speed = 10f;
    private float radius = 0.05f;

    void Update()
    {
        Oscillate();
    }

    private void Oscillate()
    {
        angle += (speed / (radius * 2 * Mathf.PI)) * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, transform.position.z) + transform.position;
    }
}
