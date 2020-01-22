using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Destroy(other.gameObject);
            FindObjectOfType<SceneLoader>().LoadGameOver();
        }
    }
}
