using System;
using TMPro;
using UnityEngine;

public class FactoryTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer_text;

    [SerializeField] private float base_remaining_time;

    private float remaining_time;
    private bool timer_done = false;

    public event Action onTimerFinished;

    private void Awake()
    {
        remaining_time = base_remaining_time;
    }

    private void Update()
    {
        if(!timer_done)
            countdown();
    }

    private void countdown()
    {
        if(remaining_time > 0)
        {
            remaining_time -= Time.deltaTime;
            if (remaining_time <= 0)
            {
                remaining_time = 0;
                timer_done = true;
                onTimerFinished?.Invoke();
            }
        }
        
        int minutes = Mathf.FloorToInt(remaining_time / 60);
        int seconds = Mathf.FloorToInt(remaining_time % 60);
        timer_text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void restartPhase()
    {
        remaining_time = base_remaining_time;
        timer_done = false;
    }
}
