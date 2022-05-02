using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteExplosionTimer : MonoBehaviour
{
    //initialising Variables
    public float Timer = 2;
    public GameObject explosion;
    public Rigidbody rb;

    void Update()
    {
        //count down by time and explode if so
        Timer -= Time.deltaTime;
        if (Timer <= 0) 
        {
            Explode();
        }
    }

    void OnTriggerEnter(Collider Enemy)
    {
        // when colliding with anything, activate gravity
        rb.useGravity = true;
        if (this.name == "Sphere") // for if this is not dynamite
        {
            this.GetComponentInParent<ThrowActiveDynamite>().enabled = false;
            if (Enemy.tag == "Enemy")
            {
                Explode();
            }
        }
    }

    public void Explode()
    {
        // make explosion
        Instantiate(explosion, transform.position, Quaternion.identity);
        if (this.name == "Sphere") // for if this is not dynamite
        {
            Destroy(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
