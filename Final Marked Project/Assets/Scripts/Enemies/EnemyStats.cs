using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float Stun;
    public NavMeshAgent agent;

    void Update()
    {
        if (Stun > 0)
        {
            Stun -= Time.deltaTime;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
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

            //collision.GetComponent<PlayerAttackAngle>().AttackAngle
            health -= 1;

            Vector3 knockback = collision.transform.forward;

            Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
            direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
            direction = -direction.normalized;

            rb.AddForce(knockback * 6, ForceMode.Impulse); // was direction
            rb.AddForce(0, 5, 0, ForceMode.Impulse);
        }
    }

    }
