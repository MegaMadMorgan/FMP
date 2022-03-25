using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("WhatIsGround"))
        //{
        //    GetComponent<EnemyStats>().Grounded = true;
        //}
        //else
        //{
        //    GetComponent<EnemyStats>().Grounded = false;
        //}
    }
}
