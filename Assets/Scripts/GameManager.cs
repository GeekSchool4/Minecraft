using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameManager: MonoBehaviour
{
    public int enemiesKilled = 0;
    public GameObject Timer;
    public GameObject loseImage;
    public GameObject winImage;

    public TMP_Text enemiesKilledText;
    private void Start()
    {
        enemiesKilledText.text = enemiesKilled.ToString();

    }

    public void IncreaseEnemiesKilled(int increaseValue)
    {
        enemiesKilled += increaseValue;
        enemiesKilledText.text = enemiesKilled.ToString();
    }




}
