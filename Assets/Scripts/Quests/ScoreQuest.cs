using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms;

public class ScoreQuest : MonoBehaviour
{
    public GameObject questButton;
    public GameObject questObject;
    public GameObject timer;
    public GameObject winImage;
    public GameObject loseImage;
    public GameObject yesText;
    public GameObject noText;

    public int score = 0;
    public TMP_Text scoreText;
    public bool isActive = false;
    public bool isCompleted = false;
    public bool isGoToGame = false;


    private void Update()
    {
        if (timer.GetComponent<TimerQuest>().timerScore <= 0)
        {
            EndQuest();
        }
    }
    public void StartQuest()
    {
        questButton.SetActive(false);
        questObject.SetActive(true);
        isActive = true;
        scoreText.text = score.ToString();
    }

    public void InscreaseScore()
    {
        score += 5;
        scoreText.text = score.ToString();
    }
    public void EndQuest()
    {
        if (score >= 100)
        {
            isCompleted = true;
            isActive = false;
            winImage.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isActive = false;
            Time.timeScale = 0;
            loseImage.SetActive(true);
        }
    }

    public void GoToGame()
    {
        if (winImage != null)
        {
            score = 0;
            questObject.SetActive(false);
            Time.timeScale = 1;
            yesText.SetActive(true);
            noText.SetActive(false);

            winImage.SetActive(false);
        }
        if (loseImage != null)
        {
            score = 0;
            questObject.SetActive(false);
            questButton.SetActive(true);
            timer.GetComponent<TimerQuest>().timerScore = 100;
            timer.GetComponent<TimerQuest>().scoreText.text = timer.GetComponent<TimerQuest>().timerScore.ToString();  
            Time.timeScale = 1;
            loseImage.SetActive(false);
        }
    }
}
