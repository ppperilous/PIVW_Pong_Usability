using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeningForKeys : MonoBehaviour
{

    private bool theyDeadlocked;
    private float deadlockedTime;

    
  

    // Start is called before the first frame update
    void Start()
    {
        theyDeadlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        listeningForDeadlock();
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

    public void listeningforFails()
    {

    }

    
}
