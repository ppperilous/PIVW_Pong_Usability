using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRacket : MonoBehaviour
{
    public float speed = 5;
    public float maxSpeed = 35;
    public string axis = "Vertical2";

    GameObject highlight;

    GameObject upArrow;
    GameObject downArrow;
    GameObject racketUpEffect;
    GameObject racketDownEffect;

    //GameObject errorEffect;

    public GameObject upMagnet;
    public GameObject downMagnet;
    GameObject upMagnetWaves;
    GameObject downMagnetWaves;

    bool racketState = false;
    float v;
    float prevV = 0;
    float accFactor = 0;


    void Start()
    {
        highlight = transform.GetChild(2).gameObject;

        //Racket Movement Effects
        racketUpEffect = transform.GetChild(5).gameObject;
        racketDownEffect = transform.GetChild(6).gameObject;
        racketUpEffect.SetActive(false);
        racketDownEffect.SetActive(false);

        //Error Effect on Simultaneous Press
        //errorEffect = transform.GetChild(9).gameObject;
        //errorEffect.SetActive(false);


        ////////////////////////////////////////////////////
        //REQUIRED FOR RACKET PONG
        //Arrows for RacketPong
        upArrow = transform.GetChild(3).gameObject;
        downArrow = transform.GetChild(4).gameObject;
        downArrow.SetActive(false);
        upArrow.SetActive(false);

        ////////////////////////////////////////////////////
        //REQUIRED FOR MAGNET PONG

        //Magnet Waves for MagnetPong
        upMagnetWaves = upMagnet.transform.GetChild(0).gameObject;
        downMagnetWaves = downMagnet.transform.GetChild(0).gameObject;
        upMagnetWaves.SetActive(false);
        downMagnetWaves.SetActive(false);

        //Magnets
        upMagnet.SetActive(false);
        downMagnet.SetActive(false);

    }

    void FixedUpdate()
    {
        v = Input.GetAxisRaw(axis);

        //SET FEEDBACK ARROWS 
        if ((v < 0) && (racketState))
        {
            racketUpEffect.SetActive(false);
            racketDownEffect.SetActive(true);

            //Racket Pong
            downArrow.SetActive(true);
            upArrow.SetActive(false);

            ////Magnet Pong
            //downMagnetWaves.SetActive(true);
            //upMagnetWaves.SetActive(false);

        }
        else if ((v > 0) && (racketState))
        {
            racketUpEffect.SetActive(true);
            racketDownEffect.SetActive(false);

            //Racket Pong
            downArrow.SetActive(false);
            upArrow.SetActive(true);

            ////Magnet Pong
            //downMagnetWaves.SetActive(false);
            //upMagnetWaves.SetActive(true);

        }
        else
        {
            racketUpEffect.SetActive(false);
            racketDownEffect.SetActive(false);

            //Racket Pong
            downArrow.SetActive(false);
            upArrow.SetActive(false);

            ////Magnet Pong
            //downMagnetWaves.SetActive(false);
            //upMagnetWaves.SetActive(false);
        }


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

    void StopRacket()
    {
        highlight.SetActive(false);
        racketState = false;

    }

    void StartRacket()
    {
        highlight.SetActive(true);
        racketState = true;
    }
}
