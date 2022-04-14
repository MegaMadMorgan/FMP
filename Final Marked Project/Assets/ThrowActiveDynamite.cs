using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowActiveDynamite : MonoBehaviour
{
    public float ThrowTime;
    private float ThrowHeight;
    private Vector3 StartPos;
    private Vector3 TargetPos;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        StartPos = this.transform.position;

        transform.rotation = GameObject.Find("Third-Person Player").transform.rotation;


        if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().LockedOn == true && Vector3.Distance(GameObject.Find("TargetingConePivot").transform.position, transform.position) <= 10)
        {
            TargetPos = GameObject.Find("TargetingConePivot").transform.position;
        }
        else
        {
            TargetPos = transform.position + transform.forward * 10;
        }

        if (TargetPos.y > 2)
        {
            ThrowHeight = TargetPos.y;
        }
        else
        {
            ThrowHeight = 2;
        }

        transform.rotation = Random.rotation;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        ThrowTime += Time.deltaTime;

        ThrowTime = ThrowTime % 5f;

        if (ThrowTime >= 1) { rb.useGravity = true; this.GetComponent<ThrowActiveDynamite>().enabled = false; }

        transform.position = MathParabola.Parabola(StartPos, TargetPos, ThrowHeight, ThrowTime * 1.2f);
    }
}
