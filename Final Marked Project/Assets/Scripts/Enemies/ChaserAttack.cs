using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserAttack : MonoBehaviour
{
    //initialising Variables
    public GameObject Attack;
    
    // lunge function
    void lunge()
    {
        //set lunge timer to 0.5
        GetComponentInParent<EnemyStats>().lungetimer = 0.5f;

        //if teleporter, teleport forwards
        if (transform.parent.name == "Teleporter" || transform.parent.name == "Teleporter(Clone)")
        {
            GetComponentInParent<EnemyStats>().teleportAttack();
        }
    }

    //set the attack hitbox to active
    void attack()
    {
        Attack.SetActive(true);
    }

    //set the attack hitbox to inactive
    void stopattack()
    {
        Attack.SetActive(false);
    }

    //when not in attack animation, set attack hitbox to inactive
    private void Update()
    {
        if (GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") != 6)
        {
            Attack.SetActive(false);
        }
    }
}
