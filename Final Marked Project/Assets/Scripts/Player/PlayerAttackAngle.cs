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
        if (this.gameObject.name == "BBA3(Clone)" || this.gameObject.name == "SBBA14(Clone)")
        {
            Vector3 HBPos = playerPos + playerDirection * 1f;
            transform.position = HBPos;
        }
        else if (this.gameObject.name == "BBA2(Clone)" || this.gameObject.name == "SBBA2(Clone)")
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
        else if (this.gameObject.name == "SSA12(Clone)")
        {
            Vector3 HBPos = playerPos + (playerDirection * 0.6f);
            HBPos.y += 1f;
            transform.position = HBPos;
        }
        else if (this.gameObject.name == "SSPVA11(Clone)" || this.gameObject.name == "SSPVA12(Clone)" || this.gameObject.name == "SSPVA13(Clone)" || this.gameObject.name == "SSPVA3(Clone)")
        {
            Vector3 HBPos = playerPos + (playerDirection * 1.5f);
            transform.position = HBPos;
        }
        else if (this.gameObject.name == "ARProjectile")
        {
            Vector3 HBPos = playerPos + (playerDirection * 0.6f);
            transform.position += Vector3.forward;
        }
        else
        {
            Vector3 HBPos = playerPos + (playerDirection * 0.6f);
            transform.position = HBPos;
        }

        AttackAngle = AttackMesh.rotation.y;

        // animation end
        if (this.gameObject.name == "KickHB(Clone)" && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") !=  2)
        {
            Destroy(gameObject);
        }

        // Baseball Bat
        if ((this.gameObject.name == "BB11HB(Clone)" || this.gameObject.name == "BB12HB(Clone)" || this.gameObject.name == "BB13HB(Clone)" || this.gameObject.name == "BB14HB(Clone)") && (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 4 && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 16))
        {
            Destroy(gameObject);
        }

        if (this.gameObject.name == "BBA2(Clone)" && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 6)
        {
            Destroy(gameObject);
        }

        if (this.gameObject.name == "BBA3(Clone)" && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 5)
        {
            Destroy(gameObject);
        }

        // Spiked Baseball Bat
        if ((this.gameObject.name == "SBB11HB(Clone)" || this.gameObject.name == "SBB12HB(Clone)" || this.gameObject.name == "SBB13HB(Clone)" || this.gameObject.name == "SBB14HB(Clone)") && (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 10 && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 16 && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 17))
        {
            Destroy(gameObject);
        }

        if (this.gameObject.name == "SBBA2(Clone)" && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 12)
        {
            Destroy(gameObject);
        }

        if (this.gameObject.name == "SBBA3(Clone)" && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 11)
        {
            Destroy(gameObject);
        }

        // Stop Sign
        if ((this.gameObject.name == "SS11HB(Clone)" || this.gameObject.name == "SS12HB(Clone)" || this.gameObject.name == "SS13HB(Clone)" || this.gameObject.name == "SS14HB(Clone)") && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 7)
        {
            Destroy(gameObject);
        }

        if (this.gameObject.name == "SSA2(Clone)" && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 9)
        {
            Destroy(gameObject);
        }

        if (this.gameObject.name == "SSA3(Clone)" && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 8)
        {
            Destroy(gameObject);
        }

        // Stop Sign Pizza Varient
        if ((this.gameObject.name == "SSPV11HB(Clone)" || this.gameObject.name == "SSPV12HB(Clone)" || this.gameObject.name == "SSPV13HB(Clone)" || this.gameObject.name == "SSPV14HB(Clone)") && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 13)
        {
            Destroy(gameObject);
        }

        if (this.gameObject.name == "SSPVA2(Clone)" && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 15)
        {
            Destroy(gameObject);
        }

        if (this.gameObject.name == "SSAPV3(Clone)" && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 14)
        {
            Destroy(gameObject);
        }

        // timer end
        if (ExistTimer > 0)
        {
            ExistTimer -= Time.deltaTime;
        }
        else
        {
             Destroy(gameObject);
        }
    }

    //collisions
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
