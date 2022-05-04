using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAngle : MonoBehaviour
{
    // initialising variables
    public Transform AttackMesh;
    public float ExistTimer;
    public float AttackAngle;

    void Start()
    {
        // set rotation to player rotation!
        AttackMesh.rotation = GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerMesh.rotation;
    }

    void Update()
    {
        // get player position and rotation for hitbox
        Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
        Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
        Quaternion HBRotation = GameObject.Find("Third-Person Player").transform.rotation;
        
        // set attack hitbox position based upon name
        if (this.gameObject.name == "BBA3(Clone)" || this.gameObject.name == "SBBA14(Clone)" || this.gameObject.name == "LDLSA11(Clone)" || this.gameObject.name == "LDLSA12(Clone)" || this.gameObject.name == "LDLSA13(Clone)" || this.gameObject.name == "LDLSA2(Clone)" || this.gameObject.name == "LDLSA3(Clone)")
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
        else if (this.gameObject.name == "SSPVA11(Clone)" || this.gameObject.name == "SSPVA12(Clone)" || this.gameObject.name == "SSPVA13(Clone)" || this.gameObject.name == "SSPVA3(Clone)" || this.gameObject.name == "SA2(Clone)" || this.gameObject.name == "SHA2(Clone)")
        {
            Vector3 HBPos = playerPos + (playerDirection * 1.5f);
            transform.position = HBPos;
        }
        else if (this.gameObject.name == "FFA3(Clone)")
        {
            Vector3 HBPos = playerPos + (playerDirection * 0.6f);
            HBPos.y += 5;
            transform.position = HBPos;
        }
        else if (this.gameObject.name == "VA11(Clone)" || this.gameObject.name == "VA12(Clone)")
        {
            Vector3 playerRight = GameObject.Find("Third-Person Player").transform.right;
            Vector3 HBPos = playerPos + (playerDirection * 0.8f) + (playerRight * 0.4f);
            transform.position = HBPos;
        }
        else
        {
            Vector3 HBPos = playerPos + (playerDirection * 0.8f);
            transform.position = HBPos;
        }

        // set y rotation
        AttackAngle = AttackMesh.rotation.y;

        // animation end
        if (this.gameObject.name == "KickHB(Clone)" && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") !=  2)
        {
            Destroy(gameObject);
        }

        // destroying gameobject when animation isnt the same

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

        // Bottle
        if ((this.gameObject.name == "BA11(Clone)" || this.gameObject.name == "BA12(Clone)" || this.gameObject.name == "BA13(Clone)") && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 19)
        {
            Destroy(gameObject);
        }

        if ((this.gameObject.name == "BA21(Clone)" || this.gameObject.name == "BA22(Clone)") && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 20)
        {
            Destroy(gameObject);
        }

        if (this.gameObject.name == "BA3(Clone)" && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 21)
        {
            Destroy(gameObject);
        }

        // Foam Finger
        if ((this.gameObject.name == "FF11(Clone)" || this.gameObject.name == "FF12(Clone)") && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 27)
        {
            Destroy(gameObject);
        }

        if ((this.gameObject.name == "FFA2(Clone)" || this.gameObject.name == "FFA3(Clone)") && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 28)
        {
            Destroy(gameObject);
        }

        // SledgeHammer
        if ((this.gameObject.name == "SA11(Clone)" || this.gameObject.name == "SA12(Clone)") && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 40)
        {
            Destroy(gameObject);
        }

        if ((this.gameObject.name == "SA2(Clone)") && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 41)
        {
            Destroy(gameObject);
        }

        if ((this.gameObject.name == "SA3(Clone)") && GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerAnimator.GetInteger("Anim") != 42)
        {
            Destroy(gameObject);
        }

        // timer end
        if (ExistTimer > 0)
        {
            ExistTimer -= Time.fixedDeltaTime;
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