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
            rb.velocity = (transform.up + transform.right) * 6;
            health = 5;
        }
    }

    }
