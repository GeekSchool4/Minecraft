using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class TimerQuest : MonoBehaviour
{
    public int timerScore = 1000;
    public int score = 0;
    public GameObject player;
    public TMP_Text scoreText;
    public GameObject loseImage;
    public GameObject winImage;
    public void Start()
    {
        StartCoroutine(time());
        scoreText.text = timerScore.ToString();
    }

    public void HealthPlus()
    {
        player.GetComponent<Health>().IncreaseHealth();
    }

    public IEnumerator time()
    {
        while (true)
        {
            HealthPlus();
            timeCount();
            yield return new WaitForSeconds(1);
        }
    }

    void timeCount()
    {
        if (timerScore <= 0)
        {
            timerScore = 0;
        }
        else
        {
            timerScore -= 1;
        }
        scoreText.text = timerScore.ToString();

    }


}
