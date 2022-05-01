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
    public bool PostureBreak = false;
    bool noted;

    public float projectiletimermax = Random.Range(6, 10);
    public float projectiletimer = Random.Range(6, 10);

    public float TeleportTimerMax;
    public float TeleportTimer;
    public int TeleportDirection;

    public Image Healthbar;
    public GameObject healthbarimage;

    public float bigcollisonrange = 3.05f;

    public int itemnum;

    public float healtimer;

    public float lungetimer;
    public Transform EnemyMesh;

    public Animator EnemyAnimator;

    public GameObject StickyDynamite;
    public GameObject Explosion;

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
    public GameObject M;
    public GameObject V;
    public GameObject Sc;
    public GameObject F;
    public GameObject SC;
    public GameObject SB;

    public GameObject FireBall;

    int rotatex;
    int rotatey;
    public bool explode = false;

    private void Awake()
    {
        itemnum = Random.Range(1, 23);
        Stun = 0.5f;
        maxhealth = health;
        if (name == "DropPod" || name == "DropPod(Clone)" || name == "DropPodV2" || name == "DropPodV2(Clone)" || name == "DropPodV3" || name == "DropPodV3(Clone)" || name == "DropPodBoss" || name == "DropPodBoss(Clone)")
        {
            this.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 180f);
        }
        rotatex = Random.Range(1, 4);
        rotatey = Random.Range(1, 4);

        projectiletimermax = Random.Range(6, 10);
        projectiletimer = Random.Range(6, 10);
    }

    void Update()
    {
        Healthbar.fillAmount = health / maxhealth;
        recollision -= Time.deltaTime;

        if (Stun > 0 || !GroundCheck())
        {
            Stun -= Time.deltaTime;
            if (this.name != "Heavy" && this.name != "Heavy(Clone)" && this.name != "HeavyV2" && this.name != "HeavyV2(Clone)" && this.name != "HeavyV3" && this.name != "HeavyV3(Clone)" && this.name != "HeavyBoss" && this.name != "HeavyBoss(Clone)")
            {
                notstunned = false;
                if (this.name != "BouncerV1" && this.name != "BouncerV1(Clone)" && this.name != "BouncerV2" && this.name != "BouncerV2(Clone)" && this.name != "BouncerV3" && this.name != "BouncerV3(Clone)" && this.name != "BouncerBoss" && this.name != "BouncerBoss(Clone)" && this.name != "DropPod" && this.name != "DropPod(Clone)" && this.name != "DropPodV2" && this.name != "DropPodV2(Clone)" && this.name != "DropPodV3" && this.name != "DropPodV3(Clone)" && this.name != "DropPodBoss" && this.name != "DropPodBoss(Clone)")
                {
                    gameObject.GetComponent<NavMeshAgent>().enabled = false;

                    if (gameObject.GetComponent<EnemyNav>() != null)
                    {
                        gameObject.GetComponent<EnemyNav>().enabled = false;
                    }
                    if (gameObject.GetComponent<EnemyHealerNav>() != null)
                    {
                        gameObject.GetComponent<EnemyHealerNav>().enabled = false;
                    }
                }
            }
            else if (PostureBreak == true)
            {
                notstunned = false;
                if (this.name != "BouncerV1" && this.name != "BouncerV1(Clone)" && this.name != "BouncerV2" && this.name != "BouncerV2(Clone)" && this.name != "BouncerV3" && this.name != "BouncerV3(Clone)" && this.name != "BouncerBoss" && this.name != "BouncerBoss(Clone)" && this.name != "DropPod" && this.name != "DropPod(Clone)" && this.name != "DropPodV2" && this.name != "DropPodV2(Clone)" && this.name != "DropPodV3" && this.name != "DropPodV3(Clone)" && this.name != "DropPodBoss" && this.name != "DropPodBoss(Clone)")
                {
                    gameObject.GetComponent<NavMeshAgent>().enabled = false;

                    if (gameObject.GetComponent<EnemyNav>() != null)
                    {
                        gameObject.GetComponent<EnemyNav>().enabled = false;
                    }
                    if (gameObject.GetComponent<EnemyHealerNav>() != null)
                    {
                        gameObject.GetComponent<EnemyHealerNav>().enabled = false;
                    }
                }
            }
        }

        if (Stun <= 0 && GroundCheck())
        {
            Stun = 0;
            stunframe = 0;
            if (this.name != "BouncerV1" && this.name != "BouncerV1(Clone)" && this.name != "BouncerV2" && this.name != "BouncerV2(Clone)" && this.name != "BouncerV3" && this.name != "BouncerV3(Clone)" && this.name != "BouncerBoss" && this.name != "BouncerBoss(Clone)" && this.name != "DropPod" && this.name != "DropPod(Clone)" && this.name != "DropPodV2" && this.name != "DropPodV2(Clone)" && this.name != "DropPodV3" && this.name != "DropPodV3(Clone)" && this.name != "DropPodBoss" && this.name != "DropPodBoss(Clone)" && lungetimer <= 0)
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                if (gameObject.GetComponent<EnemyNav>() != null)
                {
                    gameObject.GetComponent<EnemyNav>().enabled = true;
                }
                if (gameObject.GetComponent<EnemyHealerNav>() != null)
                {
                    gameObject.GetComponent<EnemyHealerNav>().enabled = true;
                }
            }
            else
            {
                if (lungetimer <= 0)
                {
                    EnemyAnimator.SetInteger("EAnim", 0);
                }
            }

            if ((this.name == "Healer" || this.name == "Healer(Clone)" || this.name == "HealerV2" || this.name == "HealerV2(Clone)" || this.name == "HealerV3" || this.name == "HealerV3(Clone)") && health <= maxhealth)
            {
                healtimer -= Time.deltaTime;

                if (healtimer <= 0)
                {
                    healtimer = 0.75f;
                    health += 0.75f;
                }
            }
            notstunned = true;
            PostureBreak = false;
        }
        else
        {
            healtimer = 0.75f;
        }

        if (health > maxhealth)
        {
            health = maxhealth;
        }

        //if (name == "Chaser" || name == "Chaser(Clone)" || name == "Defender" || name == "Defender(Clone)")
        if ((EnemyAnimator.GetInteger("EAnim") == 1 || EnemyAnimator.GetInteger("EAnim") == 2) && notstunned == true)
        {
            EnemyAnimator.SetInteger("EAnim", 5);

            if (name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)")
            {
                Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;
                this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
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

        if (name == "Chaser" || name == "Chaser(Clone)" || name == "ChaserV2" || name == "ChaserV2(Clone)" || name == "ChaserV3" || name == "ChaserV3(Clone)" || name == "Defender" || name == "Defender(Clone)" || name == "DefenderV2" || name == "DefenderV2(Clone)" || name == "DefenderV3" || name == "DefenderV3(Clone)" || name == "DefenderBoss" || name == "DefenderBoss(Clone)" || name == "Bigger" || name == "Bigger(Clone)" || name == "BiggerV2" || name == "BiggerV2(Clone)" || name == "BiggerV3" || name == "BiggerV3(Clone)" || name == "Heavy" || name == "Heavy(Clone)" || name == "HeavyV2" || name == "HeavyV2(Clone)" || name == "HeavyV3" || name == "HeavyV3(Clone)" || name == "HeavyBoss" || name == "HeavyBoss(Clone)" || name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)" || name == "Healer" || name == "Healer(Clone)" || name == "HealerV2" || name == "HealerV2(Clone)" || name == "HealerV3" || name == "HealerV3(Clone)" || name == "GroundedFly" || name == "GroundedFly(Clone)" || name == "GroundedFlyV2" || name == "GroundedFlyV2(Clone)" || name == "GroundedFlyV3" || name == "GroundedFlyV3(Clone)")
        {
            if (lungetimer > 0) { lungetimer -= Time.deltaTime; rb.velocity = new Vector3(EnemyMesh.forward.x * 5, rb.velocity.y, EnemyMesh.forward.z * 5); gameObject.GetComponent<NavMeshAgent>().enabled = false;
                if (gameObject.GetComponent<EnemyNav>() != null)
                {
                    gameObject.GetComponent<EnemyNav>().enabled = false;
                }
                if (gameObject.GetComponent<EnemyHealerNav>() != null)
                {
                    gameObject.GetComponent<EnemyHealerNav>().enabled = false;
                }
            }
            if (lungetimer < 0 && notstunned == true)
            { lungetimer = 0; gameObject.GetComponent<NavMeshAgent>().enabled = true;
                if (gameObject.GetComponent<EnemyNav>() != null)
                {
                    gameObject.GetComponent<EnemyNav>().enabled = true;
                }
                if (gameObject.GetComponent<EnemyHealerNav>() != null)
                {
                    gameObject.GetComponent<EnemyHealerNav>().enabled = true;
                }
            }
        }

        if (recollision <= 0 && EnemyAnimator.GetInteger("EAnim") == 7 && !(name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)"))
        {
            EnemyAnimator.SetInteger("EAnim", 5);
        }

        if (EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fighting Idle") && (name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)"))
        {
            TeleportTimer -= Time.deltaTime;
        }
        else
        {
            TeleportTimer = TeleportTimerMax;
        }

        if (TeleportTimer <= 0 && EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fighting Idle") && (name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)"))
        {
            TeleportTimer = TeleportTimerMax;
            TeleportDirection = Random.Range(1, 3);
        }

        if (TeleportDirection != 0 && EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fighting Idle") && (name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)"))
        {
            if (TeleportDirection == 1)
            {
                transform.position += (EnemyMesh.transform.right * 2.5f);
            }
            else if (TeleportDirection == 2)
            {
                transform.position += (EnemyMesh.transform.right *-2.5f);
            }
            TeleportDirection = 0;
        }


        if (explode == true)
        {
            transform.position += this.transform.up;
            if (rotatex == 1)
            {
                transform.Rotate(new Vector3(5, 0, 0));
            }
            if (rotatex == 2)
            {
                transform.Rotate(new Vector3(-5, 0, 0));
            }
            if (rotatex == 3)
            {
                transform.Rotate(new Vector3(0, 0, 0));
            }
            if (rotatey == 1)
            {
                transform.Rotate(new Vector3(0, 0, 5));
            }
            if (rotatey == 2)
            {
                transform.Rotate(new Vector3(0, 0, -5));
            }
            if (rotatey == 3)
            {
                transform.Rotate(new Vector3(0, 0, 0));
            }
        }

        if (transform.position.y <= 0)
        {
            health = -1;
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
                if (collision.name == "KickHB(Clone)" && !((this.name == "Defender(Clone)" || this.name == "DefenderV2(Clone)" || this.name == "DefenderV3(Clone)" || this.name == "DefenderBoss(Clone)" || this.name == "DefenderBoss") && (EnemyAnimator.GetInteger("EAnim") == 5 || EnemyAnimator.GetInteger("EAnim") == 7)))
                {
                health -= 1;

                Vector3 knockback = collision.transform.forward;

                Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                direction = -direction.normalized;

                StunFrameSwitch();

                FindObjectOfType<SoundManager>().PlaySound("Punch");

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 16, ForceMode.Impulse); // was direction
                rb.AddForce(0, 7, 0, ForceMode.Impulse);
                recollision = 0.1f;
                Stun = 0.6f;
                GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
            }

            

            if (collision.name == "KickHB(Clone)" && (this.name == "Defender(Clone)" || this.name == "DefenderV2(Clone)" || this.name == "DefenderV3(Clone)") && (EnemyAnimator.GetInteger("EAnim") == 5 || EnemyAnimator.GetInteger("EAnim") == 7))
            {
                health -= 0.25f;

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                StunFrameSwitch();

                FindObjectOfType<SoundManager>().PlaySound("Clash");

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 1.5f;
                GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
            }

            if (collision.name == "KickHB(Clone)" && (this.name == "DefenderBoss(Clone)" || this.name == "DefenderBoss") && (EnemyAnimator.GetInteger("EAnim") == 5 || EnemyAnimator.GetInteger("EAnim") == 7))
            {

                Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                direction = -direction.normalized;

                StunFrameSwitch();

                FindObjectOfType<SoundManager>().PlaySound("Clash");

                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                rb.AddForce(0, 4, 0, ForceMode.Impulse);
                recollision = 0.2f;
                Stun = 0.79f;
                GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
            }
            #endregion

            #region Items
            if (collision.name == "ThrownItemHB")
            {
                health -= 0.5f;

                FindObjectOfType<SoundManager>().PlaySound("Punch");

                if (name != "Flyer" && name != "Flyer(Clone)")
                {
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

                if ((this.name == "Flyer" || this.name == "Flyer(Clone)"))
                {
                    health = 0;
                    Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                }
            }
            #endregion

            //blocking check
            if (!((EnemyAnimator.GetInteger("EAnim") == 5 || EnemyAnimator.GetInteger("EAnim") == 7) && (name == "Defender" || name == "Defender(Clone)" || name == "DefenderV2" || name == "DefenderV2(Clone)" || name == "DefenderV3" || name == "DefenderV3(Clone)" || name == "DefenderBoss" || name == "DefenderBoss(Clone)")))
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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("DeepPunch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion
                // sounds
                #region Spiked Baseball Bat

                if (collision.name == "SBBA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("DeepPunch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 18, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion
                // sounds
                #region Stop Sign
                if (collision.name == "SSA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 4, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 6, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                #endregion
                // sounds
                #region Stop Sign Pizza Varient
                if (collision.name == "SSPVA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("DeepPunch");

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

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    FindObjectOfType<SoundManager>().PlaySound("DeepPunch");

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                #endregion
                // sounds
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

                        FindObjectOfType<SoundManager>().PlaySound("Punch");

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
                        FindObjectOfType<SoundManager>().PlaySound("Stab");

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

                        FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                        FindObjectOfType<SoundManager>().PlaySound("Punch");

                        StunFrameSwitch();
                        FindObjectOfType<SoundManager>().PlaySound("Stab");

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

                            FindObjectOfType<SoundManager>().PlaySound("Punch");

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
                            FindObjectOfType<SoundManager>().PlaySound("Stab");

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

                        FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                        if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                        {
                            PostureBreak = true;
                        }

                        StunFrameSwitch();

                        FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                        if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                        {
                            PostureBreak = true;
                        }

                        StunFrameSwitch();
                        FindObjectOfType<SoundManager>().PlaySound("Stab");

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

                        if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                        {
                            PostureBreak = true;
                        }

                        StunFrameSwitch();

                        FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                        if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                        {
                            PostureBreak = true;
                        }

                        StunFrameSwitch();

                        FindObjectOfType<SoundManager>().PlaySound("Stab");

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

                        if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                        {
                            PostureBreak = true;
                        }

                        StunFrameSwitch();

                        FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                        if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                        {
                            PostureBreak = true;
                        }

                        StunFrameSwitch();

                        FindObjectOfType<SoundManager>().PlaySound("Stab");

                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                        rb.AddForce(knockback * 12, ForceMode.Impulse); // was direction
                        rb.AddForce(0, 12, 0, ForceMode.Impulse);
                        recollision = 0.4f;
                        Stun = 0.6f;
                        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                    }
                }
                #endregion
                // sounds
                #region Foam Finger

                if (collision.name == "FFA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Boom");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 8, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 15, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion
                // sounds
                #region Dynamite

                if (collision.name == "DA1(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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
                // sounds
                #region Mirror
                if (collision.name == "MA11(Clone)")
                {
                    health -= 0.25f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Slice");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 5, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "MA12(Clone)")
                {
                    health -= 0.25f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Slice");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 7, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "MA13(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Slice");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 7f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "MA2(Clone)")
                {
                    health -= 2f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 16f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, -12, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "MA3(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Slice");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 15, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }
                #endregion
                // sounds
                #region Volt
                if (collision.name == "VA11(Clone)")
                {
                    health -= 0.25f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Slice");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * -1f, ForceMode.Impulse);
                    if (GameObject.Find("Third-Person Player").transform.position.y >= 2.051)
                    {
                        rb.AddForce(0, 11, 0, ForceMode.Impulse);
                    }
                    else
                    {
                        rb.AddForce(0, 5, 0, ForceMode.Impulse);
                    }
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "VA12(Clone)")
                {
                    health -= 0.25f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Slice");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * -1f, ForceMode.Impulse);
                    if (GameObject.Find("Third-Person Player").transform.position.y >= 2.051)
                    {
                        rb.AddForce(0, 11, 0, ForceMode.Impulse);
                    }
                    else
                    {
                        rb.AddForce(0, 5, 0, ForceMode.Impulse);
                    }
                    recollision = 0.2f;
                    Stun = 1f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "VA13(Clone)")
                {
                    health -= 0.25f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * -1f, ForceMode.Impulse);
                    if (GameObject.Find("Third-Person Player").transform.position.y >= 2.051)
                    {
                        rb.AddForce(0, 11, 0, ForceMode.Impulse);
                    }
                    else
                    {
                        rb.AddForce(0, 5, 0, ForceMode.Impulse);
                    }
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "VA14(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 7f, ForceMode.Impulse); 
                    rb.AddForce(0, 15, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 1f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "VA2(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Slice");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 2f, ForceMode.Impulse);
                    rb.AddForce(0, 15, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 1f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "ElectricityProjectileHB")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Shock");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 7f, ForceMode.Impulse);
                    rb.AddForce(0, 7, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }
                #endregion
                // sounds
                #region Whack-A-Mole Hammer
                if (collision.name == "WAMHA1(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    FindObjectOfType<SoundManager>().PlaySound("Squeak");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 2f, ForceMode.Impulse);
                    rb.AddForce(0, -7, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "WAMHA2(Clone)")
                {
                    health -= 4f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Squeak");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 2f, ForceMode.Impulse);
                    rb.AddForce(0, -7, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.05f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }
                #endregion
                // sounds
                #region Sledge Hammer
                if (collision.name == "SA11(Clone)")
                {
                    health -= 2f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 2f, ForceMode.Impulse);
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 1f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "SA12(Clone)")
                {
                    health -= 2f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 7f, ForceMode.Impulse);
                    rb.AddForce(0, 8, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 1f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "SA2(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 10f, ForceMode.Impulse);
                    rb.AddForce(0, 8, 0, ForceMode.Impulse);
                    recollision = 0.35f;
                    Stun = 0.7f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "SA3(Clone)")
                {
                    health -= 3f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("DeepPunch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 6f, ForceMode.Impulse);
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 1f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }
                #endregion
                // sounds
                #region Squeaky Hammer
                if (collision.name == "SHA11(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Squeak");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 2f, ForceMode.Impulse);
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 1f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "SHA12(Clone)")
                {
                    health -= 2f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Squeak");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 2f, ForceMode.Impulse);
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 1f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "SHA2(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Squeak");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 5f, ForceMode.Impulse);
                    rb.AddForce(0, 8, 0, ForceMode.Impulse);
                    recollision = 0.35f;
                    Stun = 0.7f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "SHA3(Clone)")
                {
                    health -= 2f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    FindObjectOfType<SoundManager>().PlaySound("Squeak");

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 2f, ForceMode.Impulse);
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 2f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }
                #endregion
                // sounds
                #region Legally Different Laser Sword
                if (collision.name == "LDLSA11(Clone)")
                {
                    health -= 0.25f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("LaserSlash");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 5, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "LDLSA12(Clone)")
                {
                    health -= 0.25f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("LaserSlash");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 7, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "LDLSA13(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("LaserSlash");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 7f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "LDLSA2(Clone)")
                {
                    health -= 2f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("LaserSlash");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 16f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 1f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }

                if (collision.name == "LDLSA3(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("LaserSlash");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 15, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }
                #endregion
                // sounds
                #region Uber-Blase
                if (collision.name == "UBA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("LaserSlash");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 5, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.25f;
                }

                if (collision.name == "UBA12(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("LaserSlash");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 7, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.25f;
                }

                if (collision.name == "UBA13(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("LaserSlash");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 7f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.25f;
                }

                if (collision.name == "UBA2(Clone)")
                {
                    health -= 5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("LaserSlash");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 16f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 1f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 3f;
                }

                if (collision.name == "UBA3(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("LaserSlash");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 4f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 18, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion
                // sounds
                #region Kitchen Knife
                if (collision.name == "KKA1(Clone)")
                {
                    health -= 3f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("DeepStab");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 1, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.25f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }

                if (collision.name == "KKA21(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Stab");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }

                if (collision.name == "KKA22(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Stab");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 4f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }

                if (collision.name == "KKA31(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Stab");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }

                if (collision.name == "KKA32(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Stab");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 4f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 8, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion
                // sounds
                #region Golf Club

                if (collision.name == "GCA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.8f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "GCA12(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.8f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "GCA13(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 0f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 16, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "GCA2(Clone)")
                {
                    health -= 1;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 5.2f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 24, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.7f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "GCA3(Clone)")
                {
                    health -= 2;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("DeepPunch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion
                // sounds
                #region Fish

                if (collision.name == "FA11(Clone)")
                {
                    health -= 2f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.8f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "FA12(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "FA2(Clone)")
                {
                    health -= 2;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 5.2f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 24, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.7f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "FA3(Clone)")
                {
                    health -= 3;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("DeepPunch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion
                // sounds
                #region Scepter

                if (collision.name == "ScA11(Clone)")
                {
                    health -= 0.5f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 7, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "ScA12(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 7f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 7, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "ScA2(Clone)")
                {
                    health -= 2;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 6f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.7f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "ScA3(Clone)")
                {
                    health -= 1;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Shock");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 4, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 2, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion
                // sounds
                #region SawCleaver

                if (collision.name == "SCEPTA11(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Slice");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 1.5f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.8f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SCEPTA12(Clone)")
                {
                    health -= 1f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Slice");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 4f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 4, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.1f;
                }

                if (collision.name == "SCEPTA2(Clone)")
                {
                    health -= 1.5f;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("Slice");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 6f, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.7f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }

                if (collision.name == "SCEPTA3(Clone)")
                {
                    health -= 4;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    if (this.name == "Heavy" || this.name == "Heavy(Clone)" || this.name == "HeavyV2" || this.name == "HeavyV2(Clone)" || this.name == "HeavyV3" || this.name == "HeavyV3(Clone)" || this.name == "HeavyBoss" || this.name == "HeavyBoss(Clone)")
                    {
                        PostureBreak = true;
                    }

                    StunFrameSwitch();

                    FindObjectOfType<SoundManager>().PlaySound("DeepSlice");

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.3f;
                }
                #endregion
                // sounds
                #region Supers
                if (collision.name == "HeadButtHB(Clone)")
                {
                    health -= 15;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

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

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 18, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                }

                if (collision.name == "PepperoniPizza(Clone)" && GameObject.Find("PepperoniPizza(Clone)").GetComponent<Pizza>().existtimer >= 4) // (Clone)
                {
                    health -= 20;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 18, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 18, 0, ForceMode.Impulse);
                    recollision = 0.4f;
                    Stun = 0.6f;
                }
                if (collision.name == "PepperoniPizza")
                {
                    health -= 20;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    FindObjectOfType<SoundManager>().PlaySound("Punch");

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 16, ForceMode.Impulse); // was direction
                    rb.AddForce(0, 12, 0, ForceMode.Impulse);
                    recollision = 0.1f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
                }
                #endregion

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
        if (this.name != "Heavy" && this.name != "Heavy(Clone)" && this.name != "HeavyV2" && this.name != "HeavyV2(Clone)" && this.name != "HeavyV3" && this.name != "HeavyV3(Clone)" && this.name != "HeavyBoss" && this.name != "HeavyBoss(Clone)")
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
        }
        else if (PostureBreak == true)
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
        }

        Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;
        if (this.name == "DropPod" || this.name == "DropPod(Clone)" || this.name == "DropPodV2" || this.name == "DropPodV2(Clone)" || this.name == "DropPodV3" || this.name == "DropPodV3(Clone)" || this.name == "DropPodBoss" || this.name == "DropPodBoss(Clone)")
        {
            this.transform.rotation = Quaternion.Euler(0f, rotation.y-90, 180f);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    public void teleportAttack()
    {
        transform.position += (this.transform.forward * 2.5f);
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
            Instantiate(C, transform.position, transform.rotation);
        }

        if (itemnum == 5)
        {
            Instantiate(FF, transform.position, transform.rotation);
        }

        if (itemnum == 6)
        {
            Instantiate(FP, transform.position, transform.rotation);
        }

        if (itemnum == 7)
        {
            Instantiate(GC, transform.position, transform.rotation);
        }

        if (itemnum == 8)
        {
            Instantiate(KK, transform.position, transform.rotation);
        }

        if (itemnum == 9)
        {
            Instantiate(LDLS, transform.position, transform.rotation);
        }

        if (itemnum == 10)
        {
            Instantiate(S, transform.position, transform.rotation);
        }

        if (itemnum == 11)
        {
            Instantiate(SBB, transform.position, transform.rotation);
        }

        if (itemnum == 12)
        {
            Instantiate(SH, transform.position, transform.rotation);
        }

        if (itemnum == 13)
        {
            Instantiate(SS, transform.position, transform.rotation);
        }

        if (itemnum == 14)
        {
            Instantiate(SSPV, transform.position, transform.rotation);
        }

        if (itemnum == 15)
        {
            Instantiate(UB, transform.position, transform.rotation);
        }

        if (itemnum == 16)
        {
            Instantiate(WAMH, transform.position, transform.rotation);
        }

        if (itemnum == 17)
        {
            Instantiate(D, transform.position, transform.rotation);
        }

        if (itemnum == 18)
        {
            Instantiate(M, transform.position, transform.rotation);
        }

        if (itemnum == 19)
        {
            Instantiate(V, transform.position, transform.rotation);
        }

        if (itemnum == 20)
        {
            Instantiate(Sc, transform.position, transform.rotation);
        }

        if (itemnum == 21)
        {
            Instantiate(F, transform.position, transform.rotation);
        }

        if (itemnum == 22)
        {
            Instantiate(SC, transform.position, transform.rotation);
        }
    }
}


