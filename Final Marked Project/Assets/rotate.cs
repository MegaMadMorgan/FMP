using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    void Update()
    {
        transform.RotateAround(this.transform.position, Vector3.down, 20 * Time.deltaTime);
    }
}
