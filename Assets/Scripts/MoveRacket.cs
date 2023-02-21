using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRacket : MonoBehaviour
{
    public float speed = 0;
    public float maxSpeed = 40;
    public string axis = "Vertical";

    //GameObject highlight;
    //GameObject upArrow;
    //GameObject downArrow;

    bool racketState = false;
    float v;
    float prevV = 0;
    float accFactor = 0;


    void Start()
    {
        //highlight = transform.GetChild(0).gameObject;
        //upArrow = transform.GetChild(1).gameObject;
        //downArrow = transform.GetChild(2).gameObject;

        //downArrow.SetActive(false);
        //upArrow.SetActive(false);
    }

    void FixedUpdate()
    {
        v = Input.GetAxisRaw(axis);

        //SET FEEDBACK ARROWS 
        //if ((v < 0) && (racketState))
        //{
        //    downArrow.SetActive(true);
        //    upArrow.SetActive(false);

        //}
        //else if ((v > 0) && (racketState))
        //{
        //    downArrow.SetActive(false);
        //    upArrow.SetActive(true);

        //}
        //else
        //{
        //    downArrow.SetActive(false);
        //    upArrow.SetActive(false);
        //}


        //SET RACKET SPEED
        if (racketState)
        {
            if (v == 0)
            {
                if (speed > 0) speed -= 5;
                if (speed < 0) speed = 0;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, prevV) * speed;
            }
            else if (v != prevV)
            {
                accFactor = 1;
                speed = 0;
                prevV = v;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;

            }
            else if (v == prevV)
            {
                accFactor++;
                if (speed < maxSpeed) speed += accFactor;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;
            }
        }
        else if (!racketState)
        {
            speed = 0;
            accFactor = 1;
            prevV = 0;

    
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, prevV) * speed;
        }

    }

    void stopRacket()
    {
        //highlight.SetActive(false);
        racketState = false;

    }

    void startRacket()
    {
        //highlight.SetActive(true);
        racketState = true;
    }
}
