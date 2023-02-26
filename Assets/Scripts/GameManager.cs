using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public Ball ball;

    public Text playerScoreText;
    //public Text computerScoreText;
    public TextMeshProUGUI totalScoreText;

    private int _playerScore;
    private int _misses;
    private int _totalScore;

    public static string initials_input;

    public int trialNum;
    public string trialName;
    public List<string> trials;

    public int winningScore; //set this value in both scenes!
 

    private string sceneName;

    public float trialTimer = 0;

    private bool timerIsActive = true;

    public string playerWinOrLose;

    private BallMovement ballStats;
   
    void Start()
    {
        trialNum = GlobalControl.Instance.trialNum;
        trialName = GlobalControl.Instance.trialName;
        trials = GlobalControl.Instance.trials;
        initials_input = save2Initials.roundName;
        ballStats = GameObject.Find("Ball").GetComponent<BallMovement>();
    }

    void Update()
    {
        if (timerIsActive)
        {
            trialTimer += Time.deltaTime;
        }

    }


    public void SaveGame()
    {
        Debug.Log("In Save Game starting");
        GlobalControl.Instance.trialNum = trialNum;
        GlobalControl.Instance.trialName = trialName;
        GlobalControl.Instance.trials = trials;
        Debug.Log("In Save Game");
    }


    public void PlayerScores()
    {
        _playerScore++;
        _totalScore++;
        this.playerScoreText.text = _playerScore.ToString();
        this.totalScoreText.text = _totalScore.ToString();
        //ResetRound();
        
    }

    public void ComputerScores()
    {
        _misses++;
        _totalScore--;
        //this.computerScoreText.text = _computerScore.ToString();
        this.totalScoreText.text = _totalScore.ToString();
        //this.ball.ResetPosition();
        StartCoroutine(pauseBall()); //waits .5 sec to serve the ball
        //ResetRound();

    }
   

    public void ResetRound()
    {
        

        if (Timer.currentTime >= 10)
        {
            Debug.Log("In Reset Round if Statement");

            trialNum = trialNum + 1;

            int tempTrialNum = trialNum; //so that tinylytics doesn't mess with trialNum
            string tempTrialName = trialName;

            //Log Trial End
            Tinylytics.AnalyticsManager.LogCustomMetric(save2Initials.roundName + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "TrialEndTime", "End " + System.DateTime.Now);

            //Log Trial's Misses and Scores
            Tinylytics.AnalyticsManager.LogCustomMetric(save2Initials.roundName + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "MissesAndTotalScore", _misses.ToString() +"_"+ _totalScore.ToString());

            //Log Misses on both walls over or under the racket
            Tinylytics.AnalyticsManager.LogCustomMetric(save2Initials.roundName + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "MissesOverOrUnderRacket", "LeftWallOver:" + ballStats.L_missCounter_Over.ToString() + "_" + "LeftWallUnder:_" + ballStats.L_missCounter_Under.ToString() + "_" + "RightWallOver:_" + ballStats.R_missCounter_Over.ToString() + "_" + "RightWallUnder:_" + ballStats.R_missCounter_Under.ToString());

            //Log Rapid Presses
            Tinylytics.AnalyticsManager.LogCustomMetric(save2Initials.roundName + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "RapidPress", "DownKey_" + ListeningForKeys.d_totalRapidPresses + "_wKey_" + ListeningForKeys.w_totalRapidPresses);

            //Log DeadLock instances and Avg Time
            float deadLockAvg = ListeningForKeys.deadlockTotalTime /ListeningForKeys.deadlockNum;
            Tinylytics.AnalyticsManager.LogCustomMetric(save2Initials.roundName + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "DeadLocks", "Instances_" + ListeningForKeys.deadlockNum.ToString() + "_" + "AverageTime_" +deadLockAvg.ToString());

            //  Tinylytics.AnalyticsManager.LogCustomMetric("Time Taken", Timer.currentTime.ToString());

            Debug.Log("Round Over!");
            SaveGame();
            newTrial();

            // this.ball.ResetPosition(); //ball should stop moving once game is over
            timerIsActive = false;
            

        }
        else
        {
          // this.ball.ResetPosition();
          // StartCoroutine(pauseBall()); //waits .5 sec to serve the ball
        }

        
    }

    void newTrial()
    {
        Debug.Log("In New Trial");
        if (trialNum < trials.Count)
        {
            Debug.Log("In New Trial if Statement");
            trialName = trials[trialNum];
            SaveGame();

            sceneName = "interstitial"; //this name is used in the Coroutine, which is basically just a pause timer for 3 seconds.

            StartCoroutine(WaitForSceneLoad());
        }
        else { endGame();

        }

    }

    void endGame()
    {
        //if you want to know how lond the entire set of trials took, you can add your tinyLytics call here
        sceneName = "ending"; //this name is used in the Coroutine, which is basically just a pause timer for 3 seconds.
        StartCoroutine(WaitForSceneLoad());

    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator pauseBall()
    {
        yield return new WaitForSeconds(0.5f);
        this.ball.AddStartingForce();

    }
}
