using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Transform AttackMesh;
    public float AttackAngle;
    public float existtimer = 3;

    // Start is called before the first frame update
    void Awake()
    {
        //AttackMesh.rotation = GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerMesh.rotation;
        transform.rotation = GameObject.Find("Third-Person Player").transform.rotation;
        this.GetComponent<projectile>().enabled = true;
    }

    void Update()
    {
        transform.position += transform.forward / 2;
        this.GetComponent<projectile>().enabled = true;


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
