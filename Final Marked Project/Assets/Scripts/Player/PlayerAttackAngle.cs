using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAngle : MonoBehaviour
{
    public Transform AttackMesh;
    public float ExistTimer;
    public float AttackAngle;

    // Start is called before the first frame update
    void Start()
    {
        AttackMesh.rotation = GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerMesh.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
        Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
        Quaternion HBRotation = GameObject.Find("Third-Person Player").transform.rotation;
        Vector3 HBPos = playerPos + playerDirection * 0.6f;

        transform.position = HBPos;

        AttackAngle = AttackMesh.rotation.y;

        if (name != "BBHB1")
        if (ExistTimer > 0)
        {
            ExistTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
