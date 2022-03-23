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
    public float recollision;
    bool noted;

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
                noted = true;
            }
            else if (noted == true)
            {
                Destroy(gameObject);
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = true;
            }
            else
            {
                Destroy(gameObject);
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
            #region kick
            if (collision.name == "KickHB(Clone)")
            {
                health -= 1;

                Vector3 knockback = collision.transform.forward;

                Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 16, ForceMode.Impulse); // was direction
                rb.AddForce(0, 12, 0, ForceMode.Impulse);
                recollision = 0.1f;
                Stun = 0.6f;
            }
            #endregion

            #region Baseball Bat

            if (collision.name == "BBA11(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 0.6f;
            }

            if (collision.name == "BBA12(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 0.6f;
            }

            if (collision.name == "BBA13(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 16, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            if (collision.name == "BBA14(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 8, ForceMode.Impulse); // was direction
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

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 5.2f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 24, 0, ForceMode.Impulse);
            }

            if (collision.name == "BBA3(Clone)")
            {
                health -= 2;

                Vector3 knockback = collision.transform.forward;

                Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                rb.AddForce(0, 12, 0, ForceMode.Impulse);
            }
            #endregion

            #region Spiked Baseball Bat

            if (collision.name == "SBBA11(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 0.6f;
            }

            if (collision.name == "SBBA12(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 0.6f;
            }

            if (collision.name == "SBBA13(Clone)")
            {
                health -= 1;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 1.0f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 10, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            if (collision.name == "SBBA14(Clone)")
            {
                health -= 1;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 8, ForceMode.Impulse); // was direction
                rb.AddForce(0, 6, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            if (collision.name == "SBBA2(Clone)")
            {
                health -= 2;

                Vector3 knockback = collision.transform.forward;

                Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 6, ForceMode.Impulse); // was direction
                rb.AddForce(0, 12, 0, ForceMode.Impulse);
            }

            if (collision.name == "SBBA3(Clone)")
            {
                health -= 3;

                Vector3 knockback = collision.transform.forward;

                Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                rb.AddForce(0, 18, 0, ForceMode.Impulse);
            }
            #endregion

            #region Stop Sign
            if (collision.name == "SSA11(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 2f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 12, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            if (collision.name == "SSA12(Clone)")
            {
                health -= 1.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                rb.AddForce(0, -24, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 0.6f;
            }

            if (collision.name == "SSA13(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 0.6f;
            }

            if (collision.name == "SSA14(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 9, ForceMode.Impulse); // was direction
                rb.AddForce(0, 9, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            if (collision.name == "SSA2(Clone)")
            {
                health -= 1;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                rb.AddForce(0, 12, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            if (collision.name == "SSA3(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 4, ForceMode.Impulse); // was direction
                rb.AddForce(0, 6, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            #endregion

            #region Stop Sign Pizza Varient
            if (collision.name == "SSPVA11(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 0.6f;
            }

            if (collision.name == "SSPVA12(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 0.6f;
            }

            if (collision.name == "SSPVA13(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                rb.AddForce(0, 12, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            if (collision.name == "SSPVA2(Clone)")
            {
                health -= 0.5f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 1f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 20, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            if (collision.name == "SSPVA3(Clone)")
            {
                health -= 1;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                rb.AddForce(0, 12, 0, ForceMode.Impulse);
                recollision = 0.4f;
                Stun = 0.6f;
            }

            #endregion

            #region Bottle

            if (collision.name == "BA11(Clone)")
            {
                if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                }
                else
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                }
            }

            if (collision.name == "BA12(Clone)")
            {
                if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                }
                else
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                }
            }

            if (collision.name == "BA13(Clone)")
            {
                if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 2f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 16, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                }
                else
                {
                    health -= 2f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 2f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 16, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                }
            }

            if (collision.name == "BA21(Clone)")
            {
                if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                }
                else
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                }
            }

            if (collision.name == "BA22(Clone)")
            {
                if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                {
                    health -= 0.5f;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 5.2f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                }
                else
                {
                    health -= 1f;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 5.2f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
                }
            }

            if (collision.name == "BA3(Clone)")
            {
                if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                {
                    health -= 1;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                }
                else
                {
                    health -= 2;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                }
            }
            #endregion
        }
    }

    }
