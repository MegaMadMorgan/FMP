using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptbug : MonoBehaviour
{
    void Start()
    {
        // set projectile to active
        this.GetComponent<projectile>().enabled = true;
    }
}
