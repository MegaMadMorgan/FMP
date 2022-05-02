using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHitbox : MonoBehaviour
{
    //initialising Variable
    float timer = 0.3f;

    private void Awake()
    {
        //play boom sound
        FindObjectOfType<SoundManager>().PlaySound("Boom");
    }
    void Update()
    {
        //time to exist for until disappearing
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
