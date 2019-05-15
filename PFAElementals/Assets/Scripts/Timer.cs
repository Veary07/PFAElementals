using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer
{
    private float duration;
    private float currentTime = 0;
    private bool isRunning= false;
    private int loops;
    private bool autoReset = true;

    public bool Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= duration)
            {
                loops -= 1;
                if (loops == 0)
                {
                    isRunning = false;
                }
                if (autoReset)
                {
                    currentTime = 0;
                }
                return true;
            }
        }
        return false;
    }

    public void SetDuration(float _duration, int _loops, bool _autoReset = true)
    {
        currentTime = 0;
        duration = _duration;
        loops = _loops;
        isRunning = true;
        autoReset = _autoReset;
    }

    public void EndTimer()
    {
        currentTime = duration;
    }

    public float Progress()
    {
        return ((currentTime / duration));
    }

    public void ModifyCurrenTime(float modifier)
    {
        currentTime += modifier;
    }
}
