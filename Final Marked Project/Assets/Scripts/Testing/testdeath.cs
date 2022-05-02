using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testdeath : MonoBehaviour
{
    // initialise variables
    int rotatex;
    int rotatey;
    void Start()
    {
        // set random range for rotation
        rotatex = Random.Range(1, 4);
        rotatey = Random.Range(1, 4);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move up and rotate appropiately
        transform.position += this.transform.up;
        if (rotatex == 1)
        {
            transform.Rotate(new Vector3(5, 0, 0));
        }
        if (rotatex == 2)
        {
            transform.Rotate(new Vector3(-5, 0, 0));
        }
        if (rotatex == 3)
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }
        if (rotatey == 1)
        {
            transform.Rotate(new Vector3(0, 0, 5));
        }
        if (rotatey == 2)
        {
            transform.Rotate(new Vector3(0, 0, -5));
        }
        if (rotatey == 3)
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }
    }
}
