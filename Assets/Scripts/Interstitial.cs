using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Interstitial : MonoBehaviour
{
    public int trialNum;
    public string trialName;
    public List<string> trials;

    public TextMeshProUGUI message;

    void Start()
    {
        trialNum = GlobalControl.Instance.trialNum;
        trialName = GlobalControl.Instance.trialName;
        trials = GlobalControl.Instance.trials;

        MessagePlayer();
    }

    public void SaveGame()
    {
        GlobalControl.Instance.trialNum = trialNum;
        GlobalControl.Instance.trialName = trialName;
        GlobalControl.Instance.trials = trials;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            newTrial();
        }

    }

    void MessagePlayer()
    {
        var _trialNumberForHumans = trialNum + 1; //lol this is because trialNum starts at 0 (as do all array values) but people don't start counting with zero...mostly...
        message.text = "Trial " + _trialNumberForHumans + "/10";
    }

    void newTrial()
    {
        int actualTrialNum = trialNum + 1;
        Tinylytics.AnalyticsManager.LogCustomMetric(save2Initials.roundName + "_" + trialName + "_" + actualTrialNum.ToString() + "_" + "TrialStartTime", "Start " + System.DateTime.Now);

        SceneManager.LoadScene(trialName);

    }
}
