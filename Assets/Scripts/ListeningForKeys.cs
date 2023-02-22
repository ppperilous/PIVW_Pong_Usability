using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeningForKeys : MonoBehaviour
{

    private bool theyDeadlocked;
    private float deadlockedTime;

    private bool wIsDown;
    private bool downIsDown;

    private float counterForTaps;
    private bool wTapListener;
    private float wCounter;
    private float w_timeLog_A;
    private float w_timeLog_B;
    private float maxTimeBetweenClicks_W;
    private float w_timeDifference;




    // Start is called before the first frame update
    void Start()
    {
        wIsDown = false;
        downIsDown = false;
        theyDeadlocked = false;
        wTapListener = false;
        wCounter = 0;
        w_timeLog_B = 0;
        maxTimeBetweenClicks_W = 0.25f;
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
        if (Input.GetKeyDown("w") && Input.GetKeyDown("down"))
        {
            theyDeadlocked = true;
            deadlockedTime = +Time.deltaTime;
            Debug.Log("Pressed Both");
        }

        if (theyDeadlocked == true && (Input.GetKeyUp("w") || Input.GetKeyUp("down")))
        {
            theyDeadlocked = false;
            Tinylytics.AnalyticsManager.LogCustomMetric("Deadlocked " , deadlockedTime.ToString());
            deadlockedTime = 0;
        }
    }
    private void trackingTappers_W()
    {
        if (Input.GetKeyDown("w"))
        {         
            w_timeLog_A = Time.deltaTime;
            w_timeDifference = w_timeLog_A - w_timeLog_B;
         //   Debug.Log("the time since last press is " + w_timeDifference);
         //   Debug.Log("now w has been pressed " + wCounter + " times");

            if (w_timeDifference <= 0.003) {
                wCounter++;
                w_timeLog_B = Time.deltaTime;
            }
            else 
            {
                Debug.Log("W was tapped " + wCounter + " times");
                wCounter = 0;
                w_timeLog_A = 0;
                w_timeLog_B = 0;
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
