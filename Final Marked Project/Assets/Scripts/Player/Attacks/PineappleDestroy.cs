using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleDestroy : MonoBehaviour
{
    // initialising variables
    public float existtimer;
    void Update()
    {
        // the usual exist timer set up
        existtimer -= Time.deltaTime;
        if (existtimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
