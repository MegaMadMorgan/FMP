using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleDestroy : MonoBehaviour
{
    public float existtimer;
    void Update()
    {
        existtimer -= Time.deltaTime;
        if (existtimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
