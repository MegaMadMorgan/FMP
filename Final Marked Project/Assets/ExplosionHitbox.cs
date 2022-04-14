using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHitbox : MonoBehaviour
{
    float timer = 0.3f;
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
