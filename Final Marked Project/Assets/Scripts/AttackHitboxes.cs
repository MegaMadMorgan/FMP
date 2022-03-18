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
}
