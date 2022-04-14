using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float maxhealth;
    public float Stun;
    public int stunframe = 0;
    //bool isGrounded;
    public NavMeshAgent agent;
    public Rigidbody rb;
    //bool isColliding;
    public float recollision;
    public bool notstunned = false;
    bool noted;

    public Image Healthbar;
    public GameObject healthbarimage;

    public float bigcollisonrange = 3.05f;

    public int itemnum;

    public float lungetimer;
    public Transform EnemyMesh;

    public Animator EnemyAnimator;

    public GameObject StickyDynamite;

    public GameObject AR;
    public GameObject BB;
    public GameObject B;
    public GameObject C;
    public GameObject FF;
    public GameObject FP;
    public GameObject GC;
    public GameObject KK;
    public GameObject LDLS;
    public GameObject S;
    public GameObject SBB;
    public GameObject SH;
    public GameObject SS;
    public GameObject SSPV;
    public GameObject UB;
    public GameObject WAMH;
    public GameObject D;
    public GameObject SB;

    private void Awake()
    {
        itemnum = Random.Range(1, 7);
        Stun = 0.5f;
        maxhealth = health;
    }

    void Update()
    {
        Healthbar.fillAmount = health / maxhealth;
        recollision -= Time.deltaTime;
        if (Stun > 0 || !GroundCheck())
        {
            Stun -= Time.deltaTime;
            notstunned = false;


            if (this.name != "BouncerV1" && this.name != "BouncerV1(Clone)")
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                gameObject.GetComponent<EnemyNav>().enabled = false;
            }
        }

        if (Stun <= 0 && GroundCheck())
        {
            Stun = 0;
            stunframe = 0;
            if (this.name != "BouncerV1" && this.name != "BouncerV1(Clone)" && lungetimer <= 0)
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                gameObject.GetComponent<EnemyNav>().enabled = true;
            }
            else
            {
                if (lungetimer <= 0)
                {
                    EnemyAnimator.SetInteger("EAnim", 0);
                }
            }
            notstunned = true;
        }

        //if (name == "Chaser" || name == "Chaser(Clone)" || name == "Defender" || name == "Defender(Clone)")
        if ((EnemyAnimator.GetInteger("EAnim") == 1 || EnemyAnimator.GetInteger("EAnim") == 2) && notstunned == true)
        {
            EnemyAnimator.SetInteger("EAnim", 5);
        }
        //this is for showing the health bar
        if (transform.Find("TargetingConePivot"))
        {
            healthbarimage.SetActive(true);
        }
        else
        {
            healthbarimage.SetActive(false);
        }

            if (health <= 0) 
        {
            if (transform.Find("TargetingConePivot"))
            {
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = false;
                noted = true;
            }
            else if (noted == true)
            {
                spawnitem();
                Destroy(gameObject);
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = true;
            }
            else
            {
                spawnitem();
                Destroy(gameObject);
            }
        }

        if (name == "Chaser" || name == "Chaser(Clone)" || name == "Defender" || name == "Defender(Clone)")
        {
            if (lungetimer > 0) { lungetimer -= Time.deltaTime; rb.velocity = new Vector3(EnemyMesh.forward.x * 5, rb.velocity.y, EnemyMesh.forward.z * 5); gameObject.GetComponent<NavMeshAgent>().enabled = false;
                gameObject.GetComponent<EnemyNav>().enabled = false;
            }
            if (lungetimer < 0 && notstunned == true)
            { lungetimer = 0; gameObject.GetComponent<NavMeshAgent>().enabled = true;
                gameObject.GetComponent<EnemyNav>().enabled = true;
            }
        }

        if (recollision <= 0 && EnemyAnimator.GetInteger("EAnim") == 7)
        {
            EnemyAnimator.SetInteger("EAnim", 5);
        }
    }

    //power growth dynamic
    //chip damage = 0.1
    // knock away = 0.2
    // fully charged = 0.3

    void OnTriggerEnter(Collider collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        //transform.LookAt(GameObject.Find("Third-Person Player").transform, new Vector3(0,0,0));



        if (collision.tag == "PlayerAttack")
        {
                #region kick
                if (collision.name == "KickHB(Clone)" && !(this.name == "Defender(Clone)" && (EnemyAnimator.GetInteger("EAnim") == 5 || EnemyAnimator.GetInteger("EAnim") == 7)))
                {
                health -= 1;

                Vector3 knockback = collision.transform.forward;

                Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                direction = -direction.normalized;

                StunFrameSwitch();

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 16, ForceMode.Impulse); // was direction
                rb.AddForce(0, 7, 0, ForceMode.Impulse);
                recollision = 0.1f;
                Stun = 0.6f;
                GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
            }

            if (collision.name == "KickHB(Clone)" && this.name == "Defender(Clone)" && (EnemyAnimator.GetInteger("EAnim") == 5 || EnemyAnimator.GetInteger("EAnim") == 7))
            {
                health -= 0.25f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                StunFrameSwitch();

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 1.5f;
                GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
            }
            #endregion

            //blocking check
            if (!((EnemyAnimator.GetInteger("EAnim") == 5 || EnemyAnimator.GetInteger("EAnim") == 7) && (name == "Defender" || name == "Defender(Clone)")))
            {
                #region Baseball Bat

                if (collision.name == "BBA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "BBA12(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "BBA13(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 16, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "BBA14(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 8, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 6, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "BBA2(Clone)")
                {
                    health -= 1;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 5.2f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 24, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "BBA3(Clone)")
                {
                    health -= 2;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion

                #region Spiked Baseball Bat

                if (collision.name == "SBBA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SBBA12(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SBBA13(Clone)")
                {
                    health -= 1;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1.0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 10, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SBBA14(Clone)")
                {
                    health -= 1;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 8, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 6, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SBBA2(Clone)")
                {
                    health -= 2;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 6, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "SBBA3(Clone)")
                {
                    health -= 3;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 18, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion

                #region Stop Sign
                if (collision.name == "SSA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 2f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SSA12(Clone)")
                {
                    health -= 1.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, -24, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SSA13(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SSA14(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 9, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 9, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SSA2(Clone)")
                {
                    health -= 1;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }

                if (collision.name == "SSA3(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 4, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 6, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                #endregion

                #region Stop Sign Pizza Varient
                if (collision.name == "SSPVA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SSPVA12(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SSPVA13(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SSPVA2(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 20, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "SSPVA3(Clone)")
                {
                    health -= 1;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                #endregion

                #region Bottle

                if (collision.name == "BA11(Clone)")
                {
                    if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                    {
                        health -= 0.5f;

                        Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                        Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                        direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                        direction = -direction.normalized;


                        StunFrameSwitch();

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 4, 0, ForceMode.Impulse);
                        recollision = 0.2f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                    else
                    {
                        health -= 1f;

                        Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                        Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                        direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                        direction = -direction.normalized;


                        StunFrameSwitch();

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 4, 0, ForceMode.Impulse);
                        recollision = 0.2f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                }

                if (collision.name == "BA12(Clone)")
                {
                    if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                    {
                        health -= 0.5f;

                        Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                        Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                        direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                        direction = -direction.normalized;


                        StunFrameSwitch();

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 4, 0, ForceMode.Impulse);
                        recollision = 0.2f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                    else
                    {
                        health -= 1f;

                        Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                        Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                        direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                        direction = -direction.normalized;


                        StunFrameSwitch();

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 4, 0, ForceMode.Impulse);
                        recollision = 0.2f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                }

                if (collision.name == "BA13(Clone)")
                {
                    if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().B.activeSelf == true)
                    {
                        if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                        {
                            health -= 1f;

                            Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                            Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                            direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                            direction = -direction.normalized;


                            StunFrameSwitch();

                            rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                            rb.AddForce(knockback * 2f, ForceMode.Impulse); // was direction
                            rb.AddForce(0, 18.5f, 0, ForceMode.Impulse);
                            recollision = 0.4f;
                            Stun = 0.6f;
                            GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                            GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                        }
                        else
                        {
                            health -= 2f;

                            Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                            Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                            direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                            direction = -direction.normalized;


                            StunFrameSwitch();

                            rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                            rb.AddForce(knockback * 2f, ForceMode.Impulse); // was direction
                            rb.AddForce(0, 18.5f, 0, ForceMode.Impulse);
                            recollision = 0.4f;
                            Stun = 0.6f;
                            GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                        }
                    }
                    if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().C.activeSelf == true)
                    {
                        health -= 1f;

                        Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                        Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                        direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                        direction = -direction.normalized;


                        StunFrameSwitch();

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 8.5f, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
                        recollision = 0.4f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                }

                    if (collision.name == "BA21(Clone)")
                {
                    if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                    {
                        health -= 0.5f;

                        Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                        Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                        direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                        direction = -direction.normalized;


                        StunFrameSwitch();

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 0, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
                        recollision = 0.4f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                    else
                    {
                        health -= 1f;

                        Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                        Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                        direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                        direction = -direction.normalized;


                        StunFrameSwitch();

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 0, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
                        recollision = 0.4f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                }

                if (collision.name == "BA22(Clone)")
                {
                    if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                    {
                        health -= 0.5f;

                        Vector3 knockback = collision.transform.forward;

                        Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                        direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                        direction = -direction.normalized;


                        StunFrameSwitch();

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 5.2f, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
                        recollision = 0.4f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                    else
                    {
                        health -= 1f;

                        Vector3 knockback = collision.transform.forward;

                        Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                        direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                        direction = -direction.normalized;


                        StunFrameSwitch();

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 5.2f, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
                        recollision = 0.4f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                }

                if (collision.name == "BA3(Clone)")
                {
                    if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP >= 1)
                    {
                        health -= 1;

                        Vector3 knockback = collision.transform.forward;

                        Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                        direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                        direction = -direction.normalized;


                        StunFrameSwitch();

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 12, 0, ForceMode.Impulse);
                        recollision = 0.4f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().bottleShatterHP -= 1;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                    else
                    {
                        health -= 2;

                        Vector3 knockback = collision.transform.forward;

                        Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                        direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                        direction = -direction.normalized;


                        StunFrameSwitch();

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 12, 0, ForceMode.Impulse);
                        recollision = 0.4f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                }
                #endregion

                #region Foam Finger

                if (collision.name == "FFA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 7, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "FFA12(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 7f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 7, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "FFA2(Clone)")
                {
                    health -= 1;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * -1f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 15, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "FFA3(Clone)")
                {
                    health -= 1.5f;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 8, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 15, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion

                #region Dynamite

                if (collision.name == "DA1(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;

                    if (GameObject.Find("StickyDynamite(Clone)") == null)
                    {
                        var Stick = Instantiate(StickyDynamite, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        Stick.transform.parent = this.transform;
                    }
                }

                #endregion

                if (collision.name == "HeadButtHB(Clone)")
                {
                    health -= 500;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 18, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                }

                if (collision.name == "Pineapple(Clone)") // (Clone)
                {
                    health -= 20;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 18, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                }

                if (collision.name == "PepperoniPizza(Clone)" && GameObject.Find("PepperoniPizza(Clone)").GetComponent<Pizza>().existtimer >= 4) // (Clone)
                {
                    health -= 100;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 18, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                }
                if (collision.name == "PepperoniPizza")
                {
                    health -= 1;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;


                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 16, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.1f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }
            }
            else
            {
                Debug.Log("cant hit me lol");
                this.GetComponent<EnemyNav>().TimeUntilAttack += 0.5f;
                EnemyAnimator.SetInteger("EAnim", 7);
                recollision = 0.2f;
            }

            //if attack hits with and without shield up
            if (collision.name == "ExplosionHitBox")
            {
                health -= 1.5f;

                Vector3 knockback = collision.transform.forward;

                Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                direction = -direction.normalized;

                StunFrameSwitch();

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(0, 15, 0, ForceMode.Impulse);
                rb.AddForce(collision.transform.position - this.transform.position);
                recollision = 0.4f;
                Stun = 0.6f;
            }
        }
    }


    public bool GroundCheck()
    {

            return Physics.Raycast(transform.position, Vector3.down, bigcollisonrange);

    }

    void StunFrameSwitch()
    {
        stunframe += 1;
        if (stunframe % 2 == 0)
        {
            EnemyAnimator.SetInteger("EAnim", 1);
        }
        else
        {
            EnemyAnimator.SetInteger("EAnim", 2);
        }

        Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;
        this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void spawnitem()
    {
        if (itemnum == 1)
        {
            Instantiate(AR, transform.position, transform.rotation);
        }

        if (itemnum == 2)
        {
            Instantiate(BB, transform.position, transform.rotation);
        }

        if (itemnum == 3)
        {
            Instantiate(B, transform.position, transform.rotation);
        }

        if (itemnum == 4)
        {
            Instantiate(SS, transform.position, transform.rotation);
        }

        if (itemnum == 5)
        {
            Instantiate(SBB, transform.position, transform.rotation);
        }

        if (itemnum == 6)
        {
            Instantiate(SSPV, transform.position, transform.rotation);
        }
    }
}


