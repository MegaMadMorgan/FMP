using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteExplosionTimer : MonoBehaviour
{
    public float Timer = 2;
    public GameObject explosion;
    public Rigidbody rb;

    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0) 
        {
            Explode();
        }
    }

    void OnTriggerEnter(Collider Enemy)
    {
        rb.useGravity = true;
        if (this.name == "Sphere")
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
        Instantiate(explosion, transform.position, Quaternion.identity);
        if (this.name == "Sphere")
        {
            Destroy(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
