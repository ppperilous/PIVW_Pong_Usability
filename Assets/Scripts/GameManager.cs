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
    public Text computerScoreText;
    public TextMeshProUGUI totalScoreText;

    private int _playerScore;
    private int _computerScore;
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
        initials_input = SaveInitials.name;
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
        GlobalControl.Instance.trialNum = trialNum;
        GlobalControl.Instance.trialName = trialName;
        GlobalControl.Instance.trials = trials;
    }


    public void PlayerScores()
    {
        _playerScore++;
        _totalScore++;
        this.playerScoreText.text = _playerScore.ToString();
        this.totalScoreText.text = _totalScore.ToString();
        ResetRound();
        
    }

    public void ComputerScores()
    {
        _computerScore++;
        _totalScore--;
        this.computerScoreText.text = _computerScore.ToString();
        this.totalScoreText.text = _totalScore.ToString();
        this.ball.ResetPosition();
        StartCoroutine(pauseBall()); //waits .5 sec to serve the ball
        ResetRound();

    }
   

    public void ResetRound()
    {
        if (Timer.currentTime <= 0)
        {
            if (_playerScore > _computerScore)
            {
                playerWinOrLose = "win";
            }
            if (_playerScore < _computerScore)
            {
                playerWinOrLose = "lose";
            }
            trialNum = trialNum + 1;
            Tinylytics.AnalyticsManager.LogCustomMetric("trialName", trialName);
            Tinylytics.AnalyticsManager.LogCustomMetric("Computer Score", _computerScore.ToString());
            Tinylytics.AnalyticsManager.LogCustomMetric("Total Score", _totalScore.ToString());
            Tinylytics.AnalyticsManager.LogCustomMetric("Player Score", _playerScore.ToString());
            Tinylytics.AnalyticsManager.LogCustomMetric("Left Wall Misses Above Racket ",  ballStats.L_missCounter_Over.ToString());
            Tinylytics.AnalyticsManager.LogCustomMetric("Left Wall Misses Above Racket ", ballStats.L_missCounter_Under.ToString());
            Debug.Log("misses over " + ballStats.L_missCounter_Over);

            Tinylytics.AnalyticsManager.LogCustomMetric("Time Taken", Timer.currentTime.ToString());
            Debug.Log("Round Over!");
            SaveGame();
            newTrial();
           // this.ball.ResetPosition(); //ball should stop moving once game is over
            timerIsActive = false;
            // Tinylytics.AnalyticsManager.LogCustomMetric(initials_input + "_" + trialNum.ToString() + "_" + trials[trialNum-1], playerWinOrLose + "_" + trialTimer.ToString());
            //Tinylytics.AnalyticsManager.LogCustomMetric("playerWinOrLose", playerWinOrLose);

        }
        else
        {
          // this.ball.ResetPosition();
          // StartCoroutine(pauseBall()); //waits .5 sec to serve the ball
        }

        
    }

    void newTrial()
    {
        if (trialNum < trials.Count)
        {
            trialName = trials[trialNum];
            SaveGame();

            sceneName = "interstitial"; //this name is used in the Coroutine, which is basically just a pause timer for 3 seconds.

            StartCoroutine(WaitForSceneLoad());
        }
        else { endGame(); }


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
