using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToRacker : MonoBehaviour
{
    private float speed = 5.0f; // move speed
    GameObject RacketRight;

    void Start()
    {
        speed = speed * Time.fixedDeltaTime;
        RacketRight = GameObject.Find("RacketRight");
        
    }

    void Update()
    {
        // float step = speed * Time.deltaTime;
        //   point = RacketRight.transform.position;
        //  target = point;
        // move sprite towards the target location
        Vector2 relativePos = GameObject.Find("RacketRight").transform.position - gameObject.transform.position;
        GetComponent<Rigidbody>().AddForce(100 * relativePos);
        Invoke("ApplyFlightDirection", 1.0f);

    }

    void OnGUI()
    {
       // Event currentEvent = Event.current;
  

        // compute where the mouse is in world space
       // rRacketPos.x = currentEvent.mousePosition.x;
      //  rRacketPos.y = cam.pixelHeight - currentEvent.mousePosition.y;
       
       

       
           
            // set the target to the mouse click location
           
            MoveTowards();


    }
    void MoveTowards()
    {
        
       
    }
}
