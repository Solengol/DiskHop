using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] public List<GameObject> waveObstacles;

    void Start()
    {
        waveObstacles = new List<GameObject>();
    }

}
