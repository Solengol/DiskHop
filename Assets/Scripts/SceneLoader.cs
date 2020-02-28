using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //  Configuration Parameters
    [SerializeField] float deathDelayInSeconds = 2f;
    [SerializeField] GameObject pauseMenu = default;

    [SerializeField] GameObject deathVFX = default;
    [SerializeField] float durationOfExplosion = 2f;


    // Cached Component References
    AdController adController;
    Rigidbody2D playerRigidBody;

    void Awake()
    {
        adController = FindObjectOfType<AdController>();
    }

    public void LoadStartMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(deathDelayInSeconds);
        int gameCount = PlayerPrefs.GetInt("gameCount", 0);
        if (gameCount > adController.countGamesBeforeAd)
        {
            adController.ShowInterstitialAd();
            PlayerPrefs.SetInt("gameCount", 0);
        }
        else
        {
            PlayerPrefs.SetInt("gameCount", gameCount + 1);
        }
        SceneManager.LoadScene("Game Over");
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOverSequence()
    {
        GameObject player = GameObject.Find("Player");
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerRigidBody.isKinematic = false;
        FindObjectOfType<CameraFollow>().enabled = false;
        GameObject explosion = Instantiate(deathVFX, player.transform.position, player.transform.rotation);
        Destroy(explosion, durationOfExplosion);
        Destroy(player);
        LoadGameOver();
    }
}
