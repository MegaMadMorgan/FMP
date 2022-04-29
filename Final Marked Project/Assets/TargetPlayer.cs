using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 playerPosition;

    float existtimer = 15f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        playerPosition = GameObject.Find("Third-Person Player").transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += (playerPosition - startPosition).normalized / 25;
        if (existtimer <= 0)
        {
            Destroy(transform.root.gameObject);
        }

        existtimer -= Time.deltaTime;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Destroy(transform.root.gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Destroy(transform.root.gameObject);
        }
    }
    }
