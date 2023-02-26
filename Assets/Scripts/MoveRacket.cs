using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveRacket : MonoBehaviour
{
    public float speed = 5;
    public float maxSpeed = 35;
    public string axis = "Vertical2";

    GameObject highlight;
    GameObject errorEffect;

    GameObject upArrow;
    GameObject downArrow;
    GameObject racketUpEffect;
    GameObject racketDownEffect;

    public GameObject upMagnet;
    public GameObject downMagnet;
    GameObject upMagnetWaves;
    GameObject downMagnetWaves;

    GameObject placeHolderArrowUp;
    GameObject placeHolderArrowDown;

    bool racketState = false;
    float v;
    float prevV = 0;
    float accFactor = 0;

    Scene CurrentScene; 

    void Start()
    {
        CurrentScene = SceneManager.GetActiveScene();
        highlight = transform.GetChild(2).gameObject;

        //Racket Movement Effects
        racketUpEffect = transform.GetChild(5).gameObject;
        racketDownEffect = transform.GetChild(6).gameObject;
        racketUpEffect.SetActive(false);
        racketDownEffect.SetActive(false);

        //Error Effect on Simultaneous Press
        errorEffect = transform.GetChild(7).gameObject;
        errorEffect.SetActive(false);


        ////////////////////////////////////////////////////
        //REQUIRED FOR RACKET PONG
        //Arrows for RacketPong
        upArrow = transform.GetChild(3).gameObject;
        downArrow = transform.GetChild(4).gameObject;
        downArrow.SetActive(false);
        upArrow.SetActive(false);

        placeHolderArrowUp = transform.GetChild(0).gameObject;
        placeHolderArrowDown = transform.GetChild(1).gameObject;
        placeHolderArrowUp.SetActive(false);
        placeHolderArrowDown.SetActive(false);

        ////////////////////////////////////////////////////
        //REQUIRED FOR MAGNET PONG

        //Magnet Waves for MagnetPong
        upMagnetWaves = upMagnet.transform.GetChild(0).gameObject;
        downMagnetWaves = downMagnet.transform.GetChild(0).gameObject;
        upMagnetWaves.SetActive(false);
        downMagnetWaves.SetActive(false);

        //Magnets
        if (CurrentScene.name == "red")
        {
            upMagnet.SetActive(true);
            downMagnet.SetActive(true);

        } else if (CurrentScene.name == "blue")
        {
            upMagnet.SetActive(false);
            downMagnet.SetActive(false);
        }
        

    }

    void FixedUpdate()
    {
        CurrentScene = SceneManager.GetActiveScene();
        v = Input.GetAxisRaw(axis);

        //SET FEEDBACK ARROWS 
        if ((v < 0) && (racketState))
        {
            racketUpEffect.SetActive(false);
            racketDownEffect.SetActive(true);


            if (CurrentScene.name == "red")
            {
                ////Magnet Pong
                downMagnetWaves.SetActive(true);
                upMagnetWaves.SetActive(false);

            }
            else if (CurrentScene.name == "blue")
            {
                //Racket Pong
                downArrow.SetActive(true);
                upArrow.SetActive(false);
                placeHolderArrowUp.SetActive(true);
                placeHolderArrowDown.SetActive(true);
            }

        }
        else if ((v > 0) && (racketState))
        {
            racketUpEffect.SetActive(true);
            racketDownEffect.SetActive(false);

            if (CurrentScene.name == "red")
            {
                //Magnet Pong
                downMagnetWaves.SetActive(false);
                upMagnetWaves.SetActive(true);

            }
            else if (CurrentScene.name == "blue")
            {
                //Racket Pong
                downArrow.SetActive(false);
                upArrow.SetActive(true);
                placeHolderArrowUp.SetActive(true);
                placeHolderArrowDown.SetActive(true);
            }

        }
        else
        {
            racketUpEffect.SetActive(false);
            racketDownEffect.SetActive(false);

            if (CurrentScene.name == "red")
            {
                //Magnet Pong
                downMagnetWaves.SetActive(false);
                upMagnetWaves.SetActive(false);

            }
            else if (CurrentScene.name == "blue")
            {
                //Racket Pong
                downArrow.SetActive(false);
                upArrow.SetActive(false);
                placeHolderArrowUp.SetActive(true);
                placeHolderArrowDown.SetActive(true);
            }

        }


        //SET RACKET SPEED
        if (racketState)
        {
            if (Input.GetKey("w") && Input.GetKey("down"))
            {
                errorEffect.SetActive(true);
                highlight.SetActive(false);
            }
            else
            {
                errorEffect.SetActive(false);
                highlight.SetActive(true);
            }

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
                accFactor+=2;
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
        errorEffect.SetActive(false);
        racketState = false;

    }

    void StartRacket()
    {
        highlight.SetActive(true);
        racketState = true;
    }
}
