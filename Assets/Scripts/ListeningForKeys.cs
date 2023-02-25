using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class ListeningForKeys : MonoBehaviour
{

    private bool theyDeadlocked;
    private float deadlockedTime;
    public save2Initials name;
    public save2Initials name2;
    public string play_d;

    private float counterForTaps;

    private bool wIsDown;
    private bool downIsDown;

    private bool wTapListener;
    private float wCounter;
    private float w_timeLog_A;
    private float w_timeLog_B;
    private float maxTimeBetweenClicks_W;
    private float w_timeDifference;

    private bool dTapListener;
    private float dCounter;
    private float d_timeLog_A;
    private float d_timeLog_B;
    private float maxTimeBetweenClicks_D;
    private float d_timeDifference;

    Stopwatch sw = new();
    Stopwatch sw2 = new();

    // Start is called before the first frame update
    void Start()
    {
        sw2.Start();
        wIsDown = false;
        downIsDown = false;
        theyDeadlocked = false;
        wTapListener = false;
        dTapListener = false;
        deadlockedTime = 0;
        wCounter = 0;
        w_timeLog_B = 0;
        maxTimeBetweenClicks_W = 0.25f;
        dCounter = 0;
        d_timeLog_B = 0;
        maxTimeBetweenClicks_D = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        listeningForDeadlock();
        listeningForWKey();
        trackingTappers_W();
    }

    private void listeningForDeadlock()
    {
        if (Input.GetKey("w") && Input.GetKey("down"))
        {
            sw.Start();
            theyDeadlocked = true;
            //deadlockedTime += Time.deltaTime;
            UnityEngine.Debug.Log("Pressed Both");
        }

        if (theyDeadlocked && (!Input.GetKey("w") || !Input.GetKey("down")))
        {
            deadlockedTime = sw.ElapsedMilliseconds;
            sw.Reset();
            theyDeadlocked = false;
            UnityEngine.Debug.Log("Deadlocked ");
            UnityEngine.Debug.Log(deadlockedTime.ToString());
            Tinylytics.AnalyticsManager.LogCustomMetric("Deadlocked ", deadlockedTime.ToString());
            Tinylytics.AnalyticsManager.LogCustomMetric("player was  ", save2Initials.name);
            deadlockedTime = 0;
        }
    }

    private void trackingTappers_W()
    {
        if (Input.GetKeyDown("w"))
        {
            w_timeLog_A = sw2.ElapsedMilliseconds;
            w_timeDifference = w_timeLog_A - w_timeLog_B;
            UnityEngine.Debug.Log("the time since last press is " + w_timeDifference);
         //   Tinylytics.AnalyticsManager.LogCustomMetric("W_SimultaneiusPress_TimeDifference", w_timeDifference.ToString());
            UnityEngine.Debug.Log("now w has been pressed " + wCounter + " times");

            if (w_timeDifference <= 100) {
                wCounter++;
                w_timeLog_B = w_timeLog_A;
            }
            else 
            {
                UnityEngine.Debug.Log("W was tapped " + wCounter + " times");
            //    Tinylytics.AnalyticsManager.LogCustomMetric("W_Press_Count", wCounter.ToString());
                wCounter = 0;
                w_timeLog_A = 0;
                w_timeLog_B = 0;
            }
        }
    }

    private void trackingTappers_D()
    {
        if (Input.GetKeyDown("down"))
        {
            d_timeLog_A = sw2.ElapsedMilliseconds;
            d_timeDifference = d_timeLog_A - d_timeLog_B;
            UnityEngine.Debug.Log("the time since last press is " + d_timeDifference);
            Tinylytics.AnalyticsManager.LogCustomMetric("W_SimultaneiusPress_TimeDifference", d_timeDifference.ToString());
            UnityEngine.Debug.Log("now d has been pressed " + dCounter + " times");

            if (d_timeDifference <= 100)
            {
                dCounter++;
                d_timeLog_B = d_timeLog_A;
            }
            else
            {
                UnityEngine.Debug.Log("d was tapped " + dCounter + " times");
                Tinylytics.AnalyticsManager.LogCustomMetric("d_Press_Count", dCounter.ToString());
                dCounter = 0;
                d_timeLog_A = 0;
                d_timeLog_B = 0;
            }
        }
    }

    public void listeningForWKey()
    {
        if (Input.GetKeyDown("w"))
        {
            wIsDown = true;
            wTapListener = true;
        }
        else if (Input.GetKeyUp("w"))
        {
            wIsDown = false;
        }
    }

    
}
