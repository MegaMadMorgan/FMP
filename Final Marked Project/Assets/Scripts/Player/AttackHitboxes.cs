using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxes : MonoBehaviour
{
    public Animator PlayerAnimator;

    public GameObject BBAttack11;
    public GameObject BBAttack12;
    public GameObject BBAttack13;
    public GameObject BBAttack14;
    public GameObject BBAttack21;
    public GameObject BBAttack31;
    public GameObject SBBAttack11;
    public GameObject SBBAttack12;
    public GameObject SBBAttack13;
    public GameObject SBBAttack14;
    public GameObject SBBAttack21;
    public GameObject SBBAttack31;
    public GameObject SSAttack11;
    public GameObject SSAttack12;
    public GameObject SSAttack13;
    public GameObject SSAttack14;
    public GameObject SSAttack21;
    public GameObject SSAttack31;
    public GameObject SSPVAttack11;
    public GameObject SSPVAttack12;
    public GameObject SSPVAttack13;
    public GameObject SSPVAttack21;
    public GameObject SSPVAttack31;
    public GameObject bullet;
    public GameObject BAttack11;
    public GameObject BAttack12;
    public GameObject BAttack13;
    public GameObject BAttack21;
    public GameObject BAttack22;
    public GameObject BAttack3;
    public GameObject FFAttack11;
    public GameObject FFAttack12;
    public GameObject FFAttack2;
    public GameObject FFAttack3;
    public GameObject Headbutt;
    public GameObject PIZZA;

    #region Baseball Bat
    public void BBA1()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BBAttack11, transform.position, playerRotation);
        }
    }

    public void BBA2()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BBAttack12, transform.position, playerRotation);
        }
    }

    public void BBA3()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BBAttack13, transform.position, playerRotation);
        }
    }

    public void BBA4()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BBAttack14, transform.position, playerRotation);
        }
    }

    public void BBA21()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(BBAttack21, transform.position, playerRotation);
    }

    public void BBA31()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(BBAttack31, transform.position, playerRotation);
    }

    #endregion

    #region Spiked Baseball bat

    public void SBBA1()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(SBBAttack11, transform.position, playerRotation);
        }
    }

    public void SBBA2()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(SBBAttack12, transform.position, playerRotation);
        }
    }

    public void SBBA3()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(SBBAttack13, transform.position, playerRotation);
        }
    }

    public void SBBA4()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(SBBAttack14, transform.position, playerRotation);
        }
    }

    public void SBBA21()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SBBAttack21, transform.position, playerRotation);
    }

    public void SBBA31()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SBBAttack31, transform.position, playerRotation);
    }

    #endregion

    #region Stop Sign
    public void SSA11()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack11, transform.position, playerRotation);
    }

    public void SSA12()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack12, transform.position, playerRotation);
    }

    public void SSA13()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack13, transform.position, playerRotation);
    }

    public void SSA14()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack14, transform.position, playerRotation);
    }

    public void SSA2()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack21, transform.position, playerRotation);
    }

    public void SSA3()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSAttack31, transform.position, playerRotation);
    }
    #endregion

    #region Stop Sign Pizza Varient
    public void SSPVA11()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSPVAttack11, transform.position, playerRotation);
    }

    public void SSPVA12()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSPVAttack12, transform.position, playerRotation);
    }

    public void SSPVA13()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSPVAttack13, transform.position, playerRotation);
    }

    public void SSPVA2()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSPVAttack21, transform.position, playerRotation);
    }

    public void SSPVA3()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(SSPVAttack31, transform.position, playerRotation);
    }
    #endregion

    #region gun
    public void gun()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(bullet, (transform.TransformPoint(Vector3.forward * 2)) + (transform.up * 1.5f), playerRotation);
    }
    #endregion

    #region bottle
    public void BA11()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BAttack11, transform.position, playerRotation);
        }
    }
    public void BA12()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BAttack12, transform.position, playerRotation);
        }
    }
    public void BA13()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BAttack13, transform.position, playerRotation);
        }
    }
    public void BA21()
    {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BAttack21, transform.TransformPoint(Vector3.forward * 1) + (transform.up * 1f), playerRotation);
    }
    public void BA22()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BAttack22, transform.position, playerRotation);
        }
    }
    public void BA3()
    {
        if (transform.GetComponentInParent<PlayerMovement>().AttackTime > 0.01)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(BAttack3, transform.position, playerRotation);
        }
    }

    public void BA3Bounce()
    {
        Rigidbody rb = transform.parent.GetComponent<Rigidbody>();
        rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
    }
    #endregion

    public void FFA1Bounce()
    {
        if (PlayerAnimator.GetInteger("Anim") == 27)
        {
            Rigidbody rb = transform.parent.GetComponent<Rigidbody>();
            rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
        }
    }

    public void FFA11()
    {
        if (PlayerAnimator.GetInteger("Anim") == 27)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(FFAttack11, transform.position, playerRotation);
        }
    }

    public void FFA12()
    {
        if (PlayerAnimator.GetInteger("Anim") == 27)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(FFAttack12, transform.position, playerRotation);
        }
    }

    public void FFA2()
    {
        if (PlayerAnimator.GetInteger("Anim") == 28)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(FFAttack2, transform.position, playerRotation);
        }
    }

    public void FFA3()
    {
        if (PlayerAnimator.GetInteger("Anim") == 28)
        {
            Quaternion playerRotation = this.transform.rotation;
            Instantiate(FFAttack3, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z) , playerRotation);
        }
    }

    #region Supers
    public void HeadButt()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(Headbutt, (transform.TransformPoint(Vector3.forward * 1)) + (transform.up * 1.5f), playerRotation);
    }

    public void S4Bounce()
    {
        Rigidbody rb = transform.parent.GetComponent<Rigidbody>();
        rb.AddForce(0, 20f, 0, ForceMode.Impulse);
    }

    public void PizzaTime()
    {
        Quaternion playerRotation = this.transform.rotation;
        Instantiate(PIZZA, transform.position, playerRotation);
    }
    #endregion
}
