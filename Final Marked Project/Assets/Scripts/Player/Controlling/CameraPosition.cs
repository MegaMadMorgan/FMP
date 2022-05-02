using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class CameraPosition : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // set position to player position
        transform.position = GameObject.Find("Third-Person Player").transform.position;

        // set locked enemy
        Vector3 lockedenemy = GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
        
        // set y to zero
        lockedenemy.y = 0;

        // set look rotation
        if (Quaternion.LookRotation(lockedenemy) != Quaternion.identity)
        {
            transform.rotation = Quaternion.LookRotation(lockedenemy);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
