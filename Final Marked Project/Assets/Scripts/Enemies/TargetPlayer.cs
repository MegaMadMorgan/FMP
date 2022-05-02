using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    //initialising Variables
    Vector3 startPosition;
    Vector3 playerPosition;
    float existtimer = 15f;
    
    void Start()
    {
        // set start position and player position
        startPosition = this.transform.position;
        playerPosition = GameObject.Find("Third-Person Player").transform.position;
    }

    void FixedUpdate()
    {
        // move towards targeted position
        transform.position += (playerPosition - startPosition).normalized / 25;

        // if exist timer is up, destroy self
        if (existtimer <= 0)
        {
            Destroy(transform.root.gameObject);
        }

        // countdown exist timer
        existtimer -= Time.deltaTime;
    }

}
