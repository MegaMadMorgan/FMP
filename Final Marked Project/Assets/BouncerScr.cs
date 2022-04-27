using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerScr : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject AHB;
    public bool follow = false;

    public void bounce()
    {
        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
        rb.AddForce(0, 18, 0, ForceMode.Impulse);
        if (transform.parent.parent.name == "BouncerBoss" || this.transform.parent.parent.name == "BouncerBoss(Clone)" || this.transform.parent.parent.name == "BouncerV3" || this.transform.parent.parent.name == "BouncerV3(Clone)")
        {
            follow = true;
        }
    }

    public void Update()
    {
        if (rb.velocity.y < -0.3f && GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 0)
        {
            AHB.SetActive(true);
        }
        else { AHB.SetActive(false); }

        if (follow == true)
        {
            if (transform.position.x >= GameObject.Find("Third-Person Player").transform.position.x-1 && transform.position.x <= GameObject.Find("Third-Person Player").transform.position.x + 1 && transform.position.z >= GameObject.Find("Third-Person Player").transform.position.z - 1 && transform.position.z <= GameObject.Find("Third-Person Player").transform.position.z + 1)
            {
                follow = false;
            }
            transform.parent.parent.position += transform.forward / 8;
        }

        if (follow == false)
        {
            Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;
            this.transform.parent.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    public void StopFollow()
    {
        if (transform.parent.parent.name == "BouncerBoss" || this.transform.parent.parent.name == "BouncerBoss(Clone)" || this.transform.parent.parent.name == "BouncerV3" || this.transform.parent.parent.name == "BouncerV3(Clone)")
        {
            follow = false;
        }
    }
}
