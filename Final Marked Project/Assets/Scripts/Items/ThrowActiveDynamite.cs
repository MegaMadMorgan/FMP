using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowActiveDynamite : MonoBehaviour
{
    //initialising Variables
    public float ThrowTime;
    private float ThrowHeight;
    private Vector3 StartPos;
    private Vector3 TargetPos;
    private Rigidbody rb;

    void Awake()
    {
        //set the start location upon awaking 
        StartPos = this.transform.position;

        //set the rotation to be player's rotation
        transform.rotation = GameObject.Find("Third-Person Player").transform.rotation;

        //if player is locked on, set the target to be the locked location
        if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().LockedOn == true && Vector3.Distance(GameObject.Find("TargetingConePivot").transform.position, transform.position) <= 10)
        {
            TargetPos = GameObject.Find("TargetingConePivot").transform.position;
        }
        else //otherwise just throw it forwards
        {
            TargetPos = transform.position + transform.forward * 10;
        }

        //if target is high in the air set the throw height appropiately, otherwise just set it to two
        if (TargetPos.y > 2)
        {
            ThrowHeight = TargetPos.y;
        }
        else //if target isn't high in the air, just set it to two
        {
            ThrowHeight = 2;
        }

        //set rotation to random rotation
        transform.rotation = Random.rotation;
        
        //make rigidbody use gravity
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        //set throw time 
        ThrowTime += Time.deltaTime;

        //make throw animation loop
        ThrowTime = ThrowTime % 5f;

        //if throw time more then one, use gravity and disable this script
        if (ThrowTime >= 1) { rb.useGravity = true; this.GetComponent<ThrowActiveDynamite>().enabled = false; }

        //transform position akin to a parabola (this stuff is complicated so it's in its own script)
        transform.position = MathParabola.Parabola(StartPos, TargetPos, ThrowHeight, ThrowTime * 1.2f);
    }
}
