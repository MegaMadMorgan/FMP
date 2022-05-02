using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    //initialising Variables
    
    //health
    public float health;
    public float maxhealth;
    //stuns
    public float Stun;
    public int stunframe = 0;

    //extras
    public NavMeshAgent agent;
    public Rigidbody rb;
    public float recollision;
    public bool notstunned = false;
    public bool PostureBreak = false;
    bool noted;

    //teleporting
    public float TeleportTimerMax;
    public float TeleportTimer;
    public int TeleportDirection;

    //health bar
    public Image Healthbar;
    public GameObject healthbarimage;

    //floor collision
    public float bigcollisonrange = 3.05f;

    //item spawn
    public int itemnum;

    //healer timer
    public float healtimer;

    //attack lunge
    public float lungetimer;
    public Transform EnemyMesh;

    //enemy animations
    public Animator EnemyAnimator;

    //dynamite effects
    public GameObject StickyDynamite;
    public GameObject Explosion;

    //teleport effect
    public GameObject Poof;

    //death effect
    public GameObject PoofDeath;

    //items to spawn upon death
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

    private void Awake()
    {
        // randomly choose the number that represents the item you're gonna spawn
        itemnum = Random.Range(1, 23);

        // set stun for when falling to not magnet yourself to terrain with the navagation systems
        Stun = 0.5f;

        // set health max at start
        maxhealth = health;

        // randomise rotation if you're a droppod enemy
        if (name == "DropPod" || name == "DropPod(Clone)" || name == "DropPodV2" || name == "DropPodV2(Clone)" || name == "DropPodV3" || name == "DropPodV3(Clone)" || name == "DropPodBoss" || name == "DropPodBoss(Clone)")
        {
            this.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 180f);
        }
    }

    void Update()
    {
        //update healthbar
        Healthbar.fillAmount = health / maxhealth;

        //countdown for check for when the enemy can get attacked again
        recollision -= Time.deltaTime;

        //if stunned and/or not touching ground
        if (Stun > 0 || !GroundCheck())
        {
            //countdown the stun time
            Stun -= Time.deltaTime;

            //if not a heavy enemy
            if (this.name != "Heavy" && this.name != "Heavy(Clone)" && this.name != "HeavyV2" && this.name != "HeavyV2(Clone)" && this.name != "HeavyV3" && this.name != "HeavyV3(Clone)" && this.name != "HeavyBoss" && this.name != "HeavyBoss(Clone)")
            {
                // you're stunned
                notstunned = false;

                // if you're not an enemy that doesnt have a navmesh
                if (this.name != "BouncerV1" && this.name != "BouncerV1(Clone)" && this.name != "BouncerV2" && this.name != "BouncerV2(Clone)" && this.name != "BouncerV3" && this.name != "BouncerV3(Clone)" && this.name != "BouncerBoss" && this.name != "BouncerBoss(Clone)" && this.name != "DropPod" && this.name != "DropPod(Clone)" && this.name != "DropPodV2" && this.name != "DropPodV2(Clone)" && this.name != "DropPodV3" && this.name != "DropPodV3(Clone)" && this.name != "DropPodBoss" && this.name != "DropPodBoss(Clone)")
                {
                    //disable the nav stuff!
                    gameObject.GetComponent<NavMeshAgent>().enabled = false;

                    if (gameObject.GetComponent<EnemyNav>() != null)
                    {
                        gameObject.GetComponent<EnemyNav>().enabled = false;
                    }
                }
            } // relating back to the not a heavy bit, if you're a heavy and your posture is broken (by a heavy/secondary attack)
            else if (PostureBreak == true)
            {
                // you're stunned
                notstunned = false;

                // if you're not an enemy that doesnt have a navmesh
                if (this.name != "BouncerV1" && this.name != "BouncerV1(Clone)" && this.name != "BouncerV2" && this.name != "BouncerV2(Clone)" && this.name != "BouncerV3" && this.name != "BouncerV3(Clone)" && this.name != "BouncerBoss" && this.name != "BouncerBoss(Clone)" && this.name != "DropPod" && this.name != "DropPod(Clone)" && this.name != "DropPodV2" && this.name != "DropPodV2(Clone)" && this.name != "DropPodV3" && this.name != "DropPodV3(Clone)" && this.name != "DropPodBoss" && this.name != "DropPodBoss(Clone)")
                {
                    //disable the nav stuff!
                    gameObject.GetComponent<NavMeshAgent>().enabled = false;

                    if (gameObject.GetComponent<EnemyNav>() != null)
                    {
                        gameObject.GetComponent<EnemyNav>().enabled = false;
                    }
                }
            }
        }

        // if you arent stunned and on the ground
        if (Stun <= 0 && GroundCheck())
        {
            // stun is at zero
            Stun = 0;

            // stunframe is at zero
            stunframe = 0;

            // if you're not an enemy that doesnt have a navmesh
            if (this.name != "BouncerV1" && this.name != "BouncerV1(Clone)" && this.name != "BouncerV2" && this.name != "BouncerV2(Clone)" && this.name != "BouncerV3" && this.name != "BouncerV3(Clone)" && this.name != "BouncerBoss" && this.name != "BouncerBoss(Clone)" && this.name != "DropPod" && this.name != "DropPod(Clone)" && this.name != "DropPodV2" && this.name != "DropPodV2(Clone)" && this.name != "DropPodV3" && this.name != "DropPodV3(Clone)" && this.name != "DropPodBoss" && this.name != "DropPodBoss(Clone)" && lungetimer <= 0)
            {
                //enable the nav stuff!
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                if (gameObject.GetComponent<EnemyNav>() != null)
                {
                    gameObject.GetComponent<EnemyNav>().enabled = true;
                }
            }
            else
            {
                // otherwise if you arent lunging, be in the idle animation
                if (lungetimer <= 0)
                {
                    EnemyAnimator.SetInteger("EAnim", 0);
                }
            }

            // if you're a healer and lower on health then usual
            if ((this.name == "Healer" || this.name == "Healer(Clone)" || this.name == "HealerV2" || this.name == "HealerV2(Clone)" || this.name == "HealerV3" || this.name == "HealerV3(Clone)") && health <= maxhealth)
            {
                // heal timer is counting down
                healtimer -= Time.deltaTime;

                // if heal timer is up
                if (healtimer <= 0)
                {
                    // heal timer is reset
                    healtimer = 0.75f;

                    // heal enemy by 0.75 (same amount as damage for ranged attacks for a reason)
                    health += 0.75f;
                }
            }

            // you arent stunned
            notstunned = true;

            // your posture isnt broken
            PostureBreak = false;
        }
        else
        {
            // heal timer is reset
            healtimer = 0.75f;
        }

        // if health is more then max
        if (health > maxhealth)
        {
            // set it to max again
            health = maxhealth;
        }

        // if in stun animations and stun isnt true
        if ((EnemyAnimator.GetInteger("EAnim") == 1 || EnemyAnimator.GetInteger("EAnim") == 2) && notstunned == true)
        {
            // go to fighting animation
            EnemyAnimator.SetInteger("EAnim", 5);

            // if you're a teleporter, face the player
            if (name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)")
            {
                Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;
                this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
        }

        // if locked on
        if (transform.Find("TargetingConePivot"))
        {
            // turn on health bar
            healthbarimage.SetActive(true);
        }
        else
        {
            // otherwise turn off the health bar
            healthbarimage.SetActive(false);
        }

        // if out of health
        if (health <= 0) 
        {
            // if lock on object is still in object as child
            if (transform.Find("TargetingConePivot"))
            {
                // set lock on to false
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = false;
                // set a bool to check if this process has been activated
                noted = true;
            }
            else if (noted == true) // if process has been activated
            {
                // spawn weapons
                spawnitem();
                // destroy enemy
                Destroy(gameObject);

                // play a random death sound
                int deathsound = Random.Range(1, 4);
                if (deathsound == 1) { FindObjectOfType<SoundManager>().PlaySound("Death1"); }
                if (deathsound == 2) { FindObjectOfType<SoundManager>().PlaySound("Death2"); }
                if (deathsound == 3) { FindObjectOfType<SoundManager>().PlaySound("Death3"); }

                // set lock on again to the next closest enemy that exists
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = true;

                // spawn a effect that communicate's the enemy's dead
                Instantiate(PoofDeath, transform.position, transform.rotation);
            }
            else
            {
                // spawn weapons
                spawnitem();

                // destroy enemy
                int deathsound = Random.Range(1, 4);

                // play a random death sound
                if (deathsound == 1) { FindObjectOfType<SoundManager>().PlaySound("Death1"); }
                if (deathsound == 2) { FindObjectOfType<SoundManager>().PlaySound("Death2"); }
                if (deathsound == 3) { FindObjectOfType<SoundManager>().PlaySound("Death3"); }

                // destroy this object
                Destroy(gameObject);

                // spawn a effect that communicate's the enemy's dead
                Instantiate(PoofDeath, transform.position, transform.rotation);
            }
        }

        // if you're an enemy
        if (name == "Chaser" || name == "Chaser(Clone)" || name == "ChaserV2" || name == "ChaserV2(Clone)" || name == "ChaserV3" || name == "ChaserV3(Clone)" || name == "Defender" || name == "Defender(Clone)" || name == "DefenderV2" || name == "DefenderV2(Clone)" || name == "DefenderV3" || name == "DefenderV3(Clone)" || name == "DefenderBoss" || name == "DefenderBoss(Clone)" || name == "Bigger" || name == "Bigger(Clone)" || name == "BiggerV2" || name == "BiggerV2(Clone)" || name == "BiggerV3" || name == "BiggerV3(Clone)" || name == "Heavy" || name == "Heavy(Clone)" || name == "HeavyV2" || name == "HeavyV2(Clone)" || name == "HeavyV3" || name == "HeavyV3(Clone)" || name == "HeavyBoss" || name == "HeavyBoss(Clone)" || name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)" || name == "Healer" || name == "Healer(Clone)" || name == "HealerV2" || name == "HealerV2(Clone)" || name == "HealerV3" || name == "HealerV3(Clone)" || name == "GroundedFly" || name == "GroundedFly(Clone)" || name == "GroundedFlyV2" || name == "GroundedFlyV2(Clone)" || name == "GroundedFlyV3" || name == "GroundedFlyV3(Clone)")
        {
            // if lunging at player (lunge time is how long the lunge lasts for)
            if (lungetimer > 0) 
            { 
                // countdown lunge timer
                lungetimer -= Time.deltaTime; 

                // lunge forwards
                rb.velocity = new Vector3(EnemyMesh.forward.x * 5, rb.velocity.y, EnemyMesh.forward.z * 5);

                // enemy navagation stuff is disabled
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                if (gameObject.GetComponent<EnemyNav>() != null)
                {
                    gameObject.GetComponent<EnemyNav>().enabled = false;
                }
            }

            // if not lunging at player and not stunned
            if (lungetimer < 0 && notstunned == true)
            { 
                // set lunge timer to zero
                lungetimer = 0;

                // enemy navagation stuff is enabled
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                if (gameObject.GetComponent<EnemyNav>() != null)
                {
                    gameObject.GetComponent<EnemyNav>().enabled = true;
                }
            }
        }

        // if you're a teleporter, you're not hit and you're in animation 7
        if (recollision <= 0 && EnemyAnimator.GetInteger("EAnim") == 7 && !(name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)"))
        {
            // play prepare for attack animation
            EnemyAnimator.SetInteger("EAnim", 5);
        }

        // while preparing to attack
        if (EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fighting Idle") && (name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)"))
        {
            //count down teleport timer
            TeleportTimer -= Time.deltaTime;
        }
        else
        {
            // otherwise reset it
            TeleportTimer = TeleportTimerMax;
        }

        // while preparing to attack and teleport timer equal to zero
        if (TeleportTimer <= 0 && EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fighting Idle") && (name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)"))
        {
            // reset teleport timer
            TeleportTimer = TeleportTimerMax;

            //randomise teleport direction
            TeleportDirection = Random.Range(1, 3);

            // spawn a teleport effect
            Instantiate(Poof, transform.position, transform.rotation);
        }

        // if teleport direction isnt zero and in appropate animation and are appropiate enemy
        if (TeleportDirection != 0 && EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fighting Idle") && (name == "Teleporter" || name == "Teleporter(Clone)" || name == "TeleporterV2" || name == "TeleporterV2(Clone)" || name == "TeleporterV3" || name == "TeleporterV3(Clone)" || name == "TeleporterBoss" || name == "TeleporterBoss(Clone)"))
        {
            // either teleport right
            if (TeleportDirection == 1)
            {
                transform.position += (EnemyMesh.transform.right * 2.5f);
            }
            else if (TeleportDirection == 2) // or teleport left
            {
                transform.position += (EnemyMesh.transform.right *-2.5f);
            }

            // and reset direction afterwards
            TeleportDirection = 0;
        }

        // if below map... die
        if (transform.position.y <= 0)
        {
            health = -1;
        }
    }

    // on collision with a hitbox
    void OnTriggerEnter(Collider collision)
    {
        // i want to note that here there will be a lot of repeated code so i will annotate the first few but the rest since it's repitition will not be annotated
        
        // set rb to be this object's rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();

        // if colliding with an object that has the tag "PlayerAttack"
        if (collision.tag == "PlayerAttack")
        {
            #region kick
            // if colliding with kick hitbox and is not a defender whose using shield
            if (collision.name == "KickHB(Clone)" && !((this.name == "Defender(Clone)" || this.name == "DefenderV2(Clone)" || this.name == "DefenderV3(Clone)" || this.name == "DefenderBoss(Clone)" || this.name == "DefenderBoss") && (EnemyAnimator.GetInteger("EAnim") == 5 || EnemyAnimator.GetInteger("EAnim") == 7)))
            {
                // take away one health
                health -= 1;

                // set knockback to be collision's angle of forward
                Vector3 knockback = collision.transform.forward;

                // optional use, direction
                Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                direction = -direction.normalized;

                //switch stun frame (for if already stunned)
                StunFrameSwitch();

                // play a punch sound effect
                FindObjectOfType<SoundManager>().PlaySound("Punch");

                // knock back direction
                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(knockback * 16, ForceMode.Impulse); // was direction
                rb.AddForce(0, 7, 0, ForceMode.Impulse);

                // time until able to be hit again set
                recollision = 0.1f;

                // time to be stunned for set
                Stun = 0.6f;

                // boost player's power meter!
                GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.2f;
            }

            // below you'll notice it's pretty much the same as above with some variables changed, and here you'll get what i mean!

            // if colliding with kick hitbox and is a defender whose using shield
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

            // if colliding with kick hitbox and is a defender Boss whose using shield
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
                #region AK
                if (collision.name == "ARProjectile")
                {
                    health -= 0.75f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

                    rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    rb.AddForce(knockback * 7f, ForceMode.Impulse);
                    rb.AddForce(0, 7, 0, ForceMode.Impulse);
                    recollision = 0.2f;
                    Stun = 0.6f;
                    GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter += 0.05f;
                }
                #endregion
                //sounds
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


                    // if hitting a heavy with this attack, break the posture and allow for stun as long as the combo is continued
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

                    // if hitting a heavy with this attack, break the posture and allow for stun as long as the combo is continued
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

                    // if there's no sticky dynamite on the enemy
                    if (GameObject.Find("StickyDynamite(Clone)") == null)
                    {
                        // create the sticky dynamite as a child of this enemy
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
                    health -= 0.75f;

                    Vector3 knockback = GameObject.Find("Third-Person Player").transform.forward;

                    Vector3 direction = GameObject.Find("Third-Person Player").transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = GameObject.Find("Third-Person Player").transform.rotation.y;
                    direction = -direction.normalized;

                    StunFrameSwitch();

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
                    health -= 0.75f;

                    Vector3 knockback = collision.transform.forward;

                    Vector3 direction = collision.transform.position - transform.position; // checks the position between the enemy and the hitbox for the direction to be launched
                    direction.y = collision.GetComponent<PlayerAttackAngle>().AttackAngle;
                    direction = -direction.normalized;

                    StunFrameSwitch();

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
                // if hit but shield is up

                // delay ability to attack by 0.5f
                this.GetComponent<EnemyNav>().TimeUntilAttack += 0.5f;

                // make sure animation stays
                EnemyAnimator.SetInteger("EAnim", 7);

                // time until able to be attacked again is set
                recollision = 0.2f;
            }

            //if attack hits regardless of anything
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

        // if inside object you shouldnt be, set health to -1... e.g. die
        if (collision.gameObject.tag == "IGNOREMECAMERA" && Stun <= 0)
        {
            health = -1;
        }
    }


    public bool GroundCheck()
    {
        //return if touching ground
        return Physics.Raycast(transform.position, Vector3.down, bigcollisonrange);
    }

    void StunFrameSwitch()
    {
        // if not a heavy enemy
        if (this.name != "Heavy" && this.name != "Heavy(Clone)" && this.name != "HeavyV2" && this.name != "HeavyV2(Clone)" && this.name != "HeavyV3" && this.name != "HeavyV3(Clone)" && this.name != "HeavyBoss" && this.name != "HeavyBoss(Clone)")
        {
            // add one to stunframe
            stunframe += 1;

            // change stun animation if stunframe is changed
            if (stunframe % 2 == 0)
            {
                EnemyAnimator.SetInteger("EAnim", 1);
            }
            else
            {
                EnemyAnimator.SetInteger("EAnim", 2);
            }
        }
        else if (PostureBreak == true) // if posture is broken for the heavies
        {
            // add one to stunframe
            stunframe += 1;

            // change stun animation if stunframe is changed
            if (stunframe % 2 == 0)
            {
                EnemyAnimator.SetInteger("EAnim", 1);
            }
            else
            {
                EnemyAnimator.SetInteger("EAnim", 2);
            }
        }

        // face player
        Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;

        // make sure drop pod isnt upside-down in stun frame!
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
        // teleport forwards when called
        transform.position += (this.transform.forward * 2.5f);
    }

    // check what item number was set to and spawn the appropiate item
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


