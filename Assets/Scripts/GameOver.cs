using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = GameObject.Find("Player");
        FindObjectOfType<CameraFollow>().enabled = false;
        GameObject explosion = Instantiate(deathVFX, player.transform.position, player.transform.rotation);
        Destroy(explosion, durationOfExplosion);
        Destroy(player);
        FindObjectOfType<SceneLoader>().LoadGameOver();
    }
}