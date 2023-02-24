using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    [SerializeField] private Animator anicontroller;
    public Animation rackethit;
    private float finished;
    // Start is called before the first frame update
    void Start()
    {
        Animation rackethit = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ResetAni()
    {
      
   
            anicontroller.ResetTrigger("L_HitPlay");
        Debug.Log("BOUNCED!");


    }
    void OnCollisionEnter2D(Collision2D col)
    {
        anicontroller.SetTrigger("L_HitPlay");
      

    }
}
