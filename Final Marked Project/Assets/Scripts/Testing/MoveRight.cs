using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    void Update()
    {
        // get rigidbody and move right
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right;
    }
}
