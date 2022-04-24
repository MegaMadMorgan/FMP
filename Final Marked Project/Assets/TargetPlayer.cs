using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        playerPosition = GameObject.Find("Third-Person Player").transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += (playerPosition - startPosition).normalized / 25;
    }
}
