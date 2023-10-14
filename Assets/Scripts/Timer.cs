using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public int timerScore = 1000;
    public GameObject player;
    public TMP_Text scoreText;

    public void Start()
    {
        StartCoroutine(time());
        scoreText.text = timerScore.ToString();
    }

    public void HealthPlus()
    {
        player.GetComponent<Health>().IncreaseHealth();
    }

    IEnumerator time()
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
