using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    // note that this is an old script, kept here incase i needed it for extra use later spawning slow moving objects
    
    //initialising Variables
    public Transform AttackMesh;
    public float AttackAngle;
    public float existtimer = 3;

    void Awake()
    {
        //set this rotation to player rotation
        transform.rotation = GameObject.Find("Third-Person Player").transform.rotation;
        //set this script to enabled (it was a bug that forced me to do this)
        this.GetComponent<projectile>().enabled = true;
    }

    void Update()
    {
        // move forwards
        transform.position += transform.forward / 2;
        //set this script to enabled (it was a bug that forced me to do this)
        this.GetComponent<projectile>().enabled = true;

        //set exist timer if the projectile is "ARProjectile" and destroy once up!
        if (name == "ARProjectile(Clone)")
        {
            existtimer -= Time.deltaTime;
            if (existtimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //upon collision apply appropiate damage if hitting enemy, and destroy self
        if (this.name == "ARProjectile(Clone)")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;
                Rigidbody enemyrb = collision.gameObject.GetComponent<EnemyStats>().GetComponent<Rigidbody>();
                float enemyh = collision.gameObject.GetComponent<EnemyStats>().health;
                float enemyrc = collision.gameObject.GetComponent<EnemyStats>().recollision;
                float enemys = collision.gameObject.GetComponent<EnemyStats>().Stun;

                enemyrb.AddForce(-enemyrb.velocity, ForceMode.VelocityChange);
                enemyrb.AddForce(knockback * 1.5f, ForceMode.Impulse);
                enemyrb.AddForce(0, 6, 0, ForceMode.Impulse);
                collision.gameObject.GetComponent<EnemyStats>().health -= 1;
                enemyrc = 0.2f;
                enemys = 0.6f; ;

                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
