using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    Text highScoreText;
    int score;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        highScoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
        score = gameSession.GetScore();

    }

    // Update is called once per frame
    void Update()
    {
        highScoreText.text = gameSession.GetHighScore(score).ToString();
    }
}
