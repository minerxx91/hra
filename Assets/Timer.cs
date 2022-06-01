using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    TMP_Text timer;
    int minutes;
    int seconds;
    int minutesLeft;
    int secondsLeft;
    void Start()
    {
        timer = GetComponent<TMP_Text>();
    }

    void Update()
    {
        minutes = Mathf.FloorToInt(Time.timeSinceLevelLoad / 60);
        seconds = Mathf.FloorToInt(Time.timeSinceLevelLoad % 60);
        minutesLeft = 9 - minutes;
        secondsLeft = 59 - seconds;
        if(secondsLeft < 0)
        {
            secondsLeft = 59;
        }
        string time = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft);
        timer.text = time;

        if(minutes >= 10)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
