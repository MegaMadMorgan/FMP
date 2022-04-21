using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserAttack : MonoBehaviour
{
    public GameObject Attack;
    void lunge()
    {
        GetComponentInParent<EnemyStats>().lungetimer = 0.5f;
        if (transform.parent.name == "Teleporter" || transform.parent.name == "Teleporter(Clone)")
        {
            GetComponentInParent<EnemyStats>().teleportAttack();
        }
    }

    void attack()
    {
        Attack.SetActive(true);
    }

    void stopattack()
    {
        Attack.SetActive(false);
    }

    private void Update()
    {
        if (GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") != 6)
        {
            Attack.SetActive(false);
        }
    }
}
