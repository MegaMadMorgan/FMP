using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameObject.Find("Third-Person Player").transform.position;

        Vector3 lockedenemy = GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
        lockedenemy.y = 0;
        transform.rotation = Quaternion.LookRotation(lockedenemy);
    }
}
