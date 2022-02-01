using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float Stun;

    void Update()
    {
        if (Stun > 0)
        {
            Stun -= Time.deltaTime;
        }

        if (health <= 0) { Destroy(gameObject); }
    }

    void OnTriggerEnter(Collider collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        //yeet = new Vector3(0f, 0f, 0f).normalized;

        if (collision.tag == "PlayerAttack")
        {
            //rb.rotation = GetComponent<PlayerAttackAngle>().AttackMesh.rotation;

            Vector3 direction = collision.transform.position - transform.position;
            direction = -direction.normalized;
            //direction.y = -5;
            

            rb.AddForce(direction * 6, ForceMode.Impulse);
            rb.AddForce(0, 5, 0, ForceMode.Impulse);
            health = 5;
        }
    }

    }
