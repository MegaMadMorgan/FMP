using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerScr : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject AHB;

    public void bounce()
    {
        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
        rb.AddForce(0, 18, 0, ForceMode.Impulse);
    }

    public void Update()
    {
        if (rb.velocity.y < -0.3f && GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 0)
        {
            AHB.SetActive(true);
        }
        else { AHB.SetActive(false); }
    }
}
