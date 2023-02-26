using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class ListeningForKeys : MonoBehaviour
{
    //For Function listeningForDeadlock()
    private bool theyDeadlocked;
    private float deadlockTime = 0;
    public static int deadlockNum = 0;
    public static float deadlockTotalTime = 0;
    Stopwatch sw = new();

    //public string play_d;
    //private float counterForTaps;

    //private bool wIsDown;
    //private bool downIsDown;
    //private bool wTapListener;
    //private bool dTapListener;

    //For Tracking Tappers Functions
    Stopwatch sw2 = new();

    private int wCounter;
    private float w_timeLog_A;
    private float w_timeLog_B;
    public static int w_totalRapidPresses;
    private float w_timeDifference;

    private int dCounter;
    private float d_timeLog_A;
    private float d_timeLog_B;
    public static int d_totalRapidPresses;
    private float d_timeDifference;


    // Start is called before the first frame update
    void Start()
    {
        //For listeningForDeadlock()
        theyDeadlocked = false;
        deadlockTime = 0;
        deadlockNum = 0;

        //For trackingTappers_W & D();
        sw2.Start();
        wCounter = 0;
        w_timeLog_B = 0;
        dCounter = 0;
        d_timeLog_B = 0;

    }

    // Update is called once per frame
    void Update()
    {
        ListeningForDeadlock();
        TrackingTappers_W();
        TrackingTappers_D();
    }

    private void ListeningForDeadlock()
    {
        if (Input.GetKey("w") && Input.GetKey("down"))
        {
            sw.Start();
            theyDeadlocked = true;
            
            UnityEngine.Debug.Log("PressedBoth");
        }

        if (theyDeadlocked && (!Input.GetKey("w") || !Input.GetKey("down")))
        {
            deadlockNum++;
            deadlockTime = sw.ElapsedMilliseconds; UnityEngine.Debug.Log("Deadlocked " + deadlockTime.ToString());
            deadlockTotalTime += deadlockTime;
            sw.Reset();

            theyDeadlocked = false;
            deadlockTime = 0;
        }
    }

    private void TrackingTappers_W()
    {
        if (Input.GetKeyDown("w"))
        {
            w_timeLog_A = sw2.ElapsedMilliseconds;
            w_timeDifference = w_timeLog_A - w_timeLog_B;

            if (w_timeDifference <= 175) {
            
                wCounter++;
            }
            else 
            {
                if (wCounter >= 2)
                {
                    w_totalRapidPresses++;
                }
                wCounter = 1;
            }

            w_timeLog_B = w_timeLog_A;
        }
    }

    private void TrackingTappers_D()
    {
        if (Input.GetKeyDown("down"))
        {
            d_timeLog_A = sw2.ElapsedMilliseconds;
            d_timeDifference = d_timeLog_A - d_timeLog_B;
            

            if (d_timeDifference <= 175)
            {
                dCounter++;
            }
            else
            {
                if (dCounter >= 2)
                {
                    d_totalRapidPresses++;
                }
                dCounter = 1;
                
            }

            d_timeLog_B = d_timeLog_A;
        }
    }

    //public void listeningForWKey()
    //{
    //    if (Input.GetKeyDown("w"))
    //    {
    //        wIsDown = true;
    //        wTapListener = true;
    //    }
    //    else if (Input.GetKeyUp("w"))
    //    {
    //        wIsDown = false;
    //    }
    //}

    
}
