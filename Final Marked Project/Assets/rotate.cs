using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.RotateAround(this.transform.position, Vector3.down, 10 * Time.deltaTime);
    }
}