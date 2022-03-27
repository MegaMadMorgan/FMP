using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserAttack : MonoBehaviour
{
    public GameObject Attack;
    void lunge()
    {
        GetComponentInParent<EnemyStats>().lungetimer = 0.5f;
    }

    void attack()
    {
        //Quaternion enemyRotation = this.transform.rotation;
        //Instantiate(Attack, transform.position, enemyRotation);
    }
}
