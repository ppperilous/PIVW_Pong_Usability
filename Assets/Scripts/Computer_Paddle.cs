using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer_Paddle : MonoBehaviour
{
    public float speed = 30;
    public string axis = "Vertical";

    public Rigidbody2D ball;
    public new Rigidbody2D rigidbody;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        //old code
        //float v = //Input.GetAxisRaw(axis);
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;


        // Check if the ball is moving towards the paddle (positive x velocity)
        // or away from the paddle (negative x velocity)


        if (this.ball.velocity.x > 0f)
        {
            // Move the paddle in the direction of the ball to track it
            if (this.ball.position.y > this.rigidbody.position.y)
            {
                this.rigidbody.AddForce(Vector2.up * this.speed);
            }
            else if (this.ball.position.y < this.rigidbody.position.y)
            {
                this.rigidbody.AddForce(Vector2.down * this.speed);
            }
        }
        else
        {
            // Move towards the center of the field and idle there until the
            // ball starts coming towards the paddle again
            if (this.rigidbody.position.y > 0f)
            {
                this.rigidbody.AddForce(Vector2.down * this.speed);
            }
            else if (this.rigidbody.position.y < 0f)
            {
                this.rigidbody.AddForce(Vector2.up * this.speed);
            }
        }
    }
}