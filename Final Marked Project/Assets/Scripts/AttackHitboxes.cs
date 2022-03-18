using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxes : MonoBehaviour
{
    public GameObject BBAttack11;
    public GameObject BBAttack12;
    public GameObject BBAttack13;
    public GameObject BBAttack14;
    public GameObject BBAttack21;
    public GameObject BBAttack31;
    public GameObject SSAttack11;
    public GameObject SSAttack12;
    public GameObject SSAttack13;
    public GameObject SSAttack14;
    public GameObject SSAttack21;
    public GameObject SSAttack31;


    public void BBA1()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackCooldown > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BBAttack11, transform.position, playerRotation);
        }
    }

    public void BBA2()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackCooldown > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BBAttack12, transform.position, playerRotation);
        }
    }

    public void BBA3()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackCooldown > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BBAttack13, transform.position, playerRotation);
        }
    }

    public void BBA4()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackCooldown > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BBAttack14, transform.position, playerRotation);
        }
    }

    public void BBA21()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(BBAttack21, transform.position, playerRotation);
    }

    public void BBA31()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(BBAttack31, transform.position, playerRotation);
    }

    public void SSA11()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack11, transform.position, playerRotation);
    }

    public void SSA12()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack12, transform.position, playerRotation);
    }

    public void SSA13()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack13, transform.position, playerRotation);
    }

    public void SSA14()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack14, transform.position, playerRotation);
    }

    public void SSA2()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack21, transform.position, playerRotation);
    }

    public void SSA3()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack31, transform.position, playerRotation);
    }
}
