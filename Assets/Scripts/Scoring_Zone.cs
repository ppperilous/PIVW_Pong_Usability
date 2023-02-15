using UnityEngine;
using UnityEngine.Events;

public class Scoring_Zone : MonoBehaviour
{
    public UnityEvent scoreTrigger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            this.scoreTrigger.Invoke();
        }
    }

}
