using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerScr : MonoBehaviour
{
    //initialising Variables
    public Rigidbody rb;
    public GameObject AHB;
    public bool follow = false;

    //bounce function
    public void bounce()
    {
        //reverse the velocity essentially setting it to zero
        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
        
        //bounce (set force on y axis to 18)
        rb.AddForce(0, 18, 0, ForceMode.Impulse);

        //follow if specific type of bouncer
        if (transform.parent.parent.name == "BouncerBoss" || this.transform.parent.parent.name == "BouncerBoss(Clone)" || this.transform.parent.parent.name == "BouncerV3" || this.transform.parent.parent.name == "BouncerV3(Clone)")
        {
            follow = true;
        }
    }

    public void Update()
    {
        //if falling downwards and in jumping animation, set attack hitbox to active
        if (rb.velocity.y < -0.3f && GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 0)
        {
            AHB.SetActive(true);
        }
        else { AHB.SetActive(false); } // otherwise, dont

        //if following
        if (follow == true)
        {

            //if above player, stop following to squish the little bug!
            if (transform.position.x >= GameObject.Find("Third-Person Player").transform.position.x-1 && transform.position.x <= GameObject.Find("Third-Person Player").transform.position.x + 1 && transform.position.z >= GameObject.Find("Third-Person Player").transform.position.z - 1 && transform.position.z <= GameObject.Find("Third-Person Player").transform.position.z + 1)
            {
                follow = false;
            }
            
            //move forwards
            transform.parent.parent.position += transform.forward / 8;
        }

        //if not following, stare in to your enemies eyes!
        if (follow == false)
        {
            Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;
            this.transform.parent.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    //a function to stop following
    public void StopFollow()
    {
        if (transform.parent.parent.name == "BouncerBoss" || this.transform.parent.parent.name == "BouncerBoss(Clone)" || this.transform.parent.parent.name == "BouncerV3" || this.transform.parent.parent.name == "BouncerV3(Clone)")
        {
            follow = false;
        }
    }
}
