using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float Stun;
    public NavMeshAgent agent;
    bool isColliding;
    float recollision;

    void Update()
    {
        if (Stun > 0)
        {
            Stun -= Time.deltaTime;
            //gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }

        if (health <= 0) 
        {
            if (transform.Find("TargetingConePivot"))
            {
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = false;
            }
            else
            {
                Destroy(gameObject);
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = true;
            }
        }

        recollision -= Time.deltaTime;

        isColliding = false;
    }

    void OnTriggerEnter(Collider collision)
    {
        //if (isColliding) return;
        //isColliding = true;

        Rigidbody rb = GetComponent<Rigidbody>();

        //yeet = new Vector3(0f, 0f, 0f).normalized;

        if (collision.tag == "PlayerAttack")
        {
            //rb.rotation = GetComponent<PlayerAttackAngle>().AttackMesh.rotation;

            //collision.GetComponent<PlayerAttackAngle>().AttackAngle
            if (collision.name == "KickHB(Clone)")
            {
                health -= 1;

                Vector3 knockback = collision.transform.forward;

                Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                direction = -direction.normalized;

                rb.AddForce(knockback * 16, ForceMode.Impulse); // was direction
                rb.AddForce(0, 12, 0, ForceMode.Impulse);
                recollision = 0.1f;
                Stun = 0.6f;
            }

            if (collision.name == "BBA11(Clone)")
            {
                health -= 1;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 0.6f;
            }

            if (collision.name == "BBA12(Clone)")
            {
                health -= 1;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 0.6f;
            }

            if (collision.name == "BBA13(Clone)")
            {
                health -= 1;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(knockback * 0.0f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 10, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            if (collision.name == "BBA14(Clone)")
            {
                health -= 1;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(knockback * 6, ForceMode.Impulse); // was direction
                rb.AddForce(0, 6, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            if (collision.name == "BBA2(Clone)")
            {
                health -= 1;

                Vector3 knockback = collision.transform.forward;

                Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                direction = -direction.normalized;

                rb.AddForce(knockback * 6, ForceMode.Impulse); // was direction
                rb.AddForce(0, 12, 0, ForceMode.Impulse);
            }

            if (collision.name == "BBA3(Clone)")
            {
                health -= 1;

                Vector3 knockback = collision.transform.forward;

                Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                direction = -direction.normalized;

                rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                rb.AddForce(0, 12, 0, ForceMode.Impulse);
            }


        }
    }

    }
