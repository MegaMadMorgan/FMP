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
        if (this.gameObject.name == "BBA3(Clone)")
        {
            Vector3 HBPos = playerPos + playerDirection * 1f;
            transform.position = HBPos;
        }
        else if (this.gameObject.name == "BBA2(Clone)")
        {
            Vector3 HBPos = playerPos + (playerDirection * 1.5f);
            HBPos.y -= 0.5f;
            transform.position = HBPos;
        }
        else if (this.gameObject.name == "KickHB(Clone)")
        {
            Vector3 HBPos = playerPos + (playerDirection * 0.6f);
            HBPos.y -= 0.5f;
            transform.position = HBPos;
        }
        else
        {
            Vector3 HBPos = playerPos + (playerDirection * 0.6f);
            transform.position = HBPos;
        }

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
