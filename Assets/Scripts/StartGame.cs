using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{

    public int trialNum;
    public string trialName;
    public List<string> trials = new();
    public int winningScore;
    public static string initials_input;

    private void Start()
    {
        initials_input = SaveInitials.name;

        string temp;
        int randomIndex;
        //shuffle the list when the game starts
        for (int i = 0; i < trials.Count; i++)
        {
            temp = trials[i];
            randomIndex = Random.Range(i, trials.Count);
            trials[i] = trials[randomIndex];
            trials[randomIndex] = temp;
        }

        GlobalControl.Instance.trials = trials; //set a global list of trials we can use in all of the scenes

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            gameStart(); 
        }
    }

    void gameStart()
    {
        trialNum = 0; //set the trial number to the first item in the list
        GlobalControl.Instance.trialNum = trialNum; //this will set the trialNum to the first in the list
        trialName = GlobalControl.Instance.trials[trialNum];

        //Log Game Start & Trial Start
        int actualTrialNum = trialNum + 1; //for human understanding
        Tinylytics.AnalyticsManager.LogCustomMetric("Trial Start", initials_input + "_" + "Trial#" + actualTrialNum.ToString() + "_" + System.DateTime.Now);

        //Load Scene
        SceneManager.LoadScene(trialName);
        //Debug.Log(trialName);

    }
}
