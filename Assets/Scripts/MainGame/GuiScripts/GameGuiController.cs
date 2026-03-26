using System;
using TMPro;
using UnityEngine;

public class GameGuiController : MonoBehaviour
{
    public float timeElapsed;
    public TextMeshProUGUI timer;

    void Start()
    {
        
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(timeElapsed);
        timer.text = "Timer : " + string.Format("{0:00}:{1:00}", t.Minutes, t.Seconds);
    }
}
