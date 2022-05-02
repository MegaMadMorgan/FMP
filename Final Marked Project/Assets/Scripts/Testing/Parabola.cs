using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    // initialise variables
    public float ThrowTime;
    private Vector3 StartPos;
    private Vector3 TargetPos;
    private Rigidbody rb;

    void Awake()
    {
        //set start pos apon awakening
        StartPos = this.transform.position;
        
        // set rb up as rigidbody
        rb = gameObject.GetComponent<Rigidbody>();

        // stop using gravity
        rb.useGravity = false;
    }

    void Update()
    {
        // set throw time to grow appropiate to time
        ThrowTime += Time.deltaTime;

        // when throw time more then 1, use gravity and disable this script
        if (ThrowTime >= 1) { rb.useGravity = true; this.GetComponent<Parabola>().enabled = false; }

        // set this parabola move appropiately
        transform.position = MathParabola.Parabola(StartPos, Vector3.forward * 10f, 3f, ThrowTime);
    }
}
