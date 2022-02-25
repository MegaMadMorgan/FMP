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
        AttackAngle = AttackMesh.rotation.y;
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
