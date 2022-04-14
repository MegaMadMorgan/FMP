using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    public float ThrowTime;
    private Vector3 StartPos;
    private Vector3 TargetPos;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        StartPos = this.transform.position;
        //TargetPos;

        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        ThrowTime += Time.deltaTime;

        if (ThrowTime >= 1) { rb.useGravity = true; this.GetComponent<Parabola>().enabled = false; }

        transform.position = MathParabola.Parabola(StartPos, Vector3.forward * 10f, 3f, ThrowTime);
    }
}
