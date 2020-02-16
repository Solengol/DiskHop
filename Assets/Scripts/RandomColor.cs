using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    Color[] colors = new Color[3];

    void Start()
    {
        colors[0] = new Color(255f / 255f, 91f / 255f, 78f / 255f, 1f);
        colors[1] = new Color(83f / 255f, 210f / 255f, 241f / 255f, 1f);
        colors[2] = new Color(25f / 255f, 205f / 255f, 70f / 255f, 1f);
        GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
    }

}
