using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameManager: MonoBehaviour
{
    public int score = 0;
    public int enemiesKilled = 0;
    public GameObject Timer;
    public GameObject loseImage;
    public GameObject winImage;

    public TMP_Text scoreText;
    public TMP_Text enemiesKilledText;
    public TMP_Text scoreText1;
    public TMP_Text enemiesKilledText1;
    public TMP_Text scoreText2;
    public TMP_Text enemiesKilledText2;
    private void Start()
    {
        scoreText.text = score.ToString();
        enemiesKilledText.text = enemiesKilled.ToString();

    }
    public void IncreaseScore(int increaseValue)
    {
        score += increaseValue;
        scoreText.text = score.ToString();

    }

    public void IncreaseEnemiesKilled(int increaseValue)
    {
        enemiesKilled += increaseValue;
        enemiesKilledText.text = enemiesKilled.ToString();
    }

    private void Update()
    {
        EndGame();
    }
    public void EndGame()
    {
        if (Timer.GetComponent<Timer>().timerScore <= 0)
        {
            if (score >= 100)
            {
                winImage.SetActive(true);
                scoreText2.text = score.ToString();
                enemiesKilledText2.text = enemiesKilled.ToString();
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 0;
                loseImage.SetActive(true);
                scoreText1.text = score.ToString();
                enemiesKilledText1.text = enemiesKilled.ToString();
            }
        } 
    }

}
