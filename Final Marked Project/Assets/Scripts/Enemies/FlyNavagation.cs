using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyNavagation : MonoBehaviour
{
    // this is the true fly navagation when actually flying
    
    //initialising Variables

    //player transform
    public Transform player;

    //player layer
    public LayerMask WhatIsPlayer;

    //health bar image (to not confuse for lock-on)
    public GameObject healthbarimage;

    //Patroling
    public Vector3 WalkPoint;
    public bool WalkPointSet = false;
    public float WalkPointRange;
    public float WalktoPointTimer = 3.5f;
    public float WalkPointIdleTimer;

    //attacking
    public float TimeUntilAttack;
    public GameObject FireBall;

    public Animator EnemyAnimator;

    //States
    public float SightRange;
    public bool PlayerInSightRange;

    // extras
    public GameObject GroundedVer;
    bool noted;
    bool ded = false;
    bool yforced = false;

    // lucky death
    public int success;
    int rotatex;
    int rotatey;
    bool luckydeath = false;
    public GameObject Explosion;
    float rotateTimer = 0.75f;
    float existTimer = 3;

    // start fixing rotation
    bool activate = false;

    private void Awake()
    {
        // set variables
        player = GameObject.Find("Third-Person Player").transform;
        rotatex = Random.Range(1, 4);
        rotatey = Random.Range(1, 4);
        success = Random.Range(1, 3);

        // set rotations to be equal to zero
        if (luckydeath == false)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    private void Update()
    {
        //set player sight range
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        // if rotation is equal to zero, set activate to true (activate allows general rotation)
        if (transform.rotation == Quaternion.identity)
        {
            activate = true;
        }

        // if this enemy is hit
        if (ded == true)
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
                // destroy enemy
                Destroy(gameObject);

                // set lock on again to the next closest enemy that exists
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = true;

                // create grounded version of enemy
                Instantiate(GroundedVer, transform.position, transform.rotation);
            }
            else
            {
                // destroy this object
                Destroy(gameObject);

                // create grounded version of enemy
                Instantiate(GroundedVer, transform.position, transform.rotation);
            }
        }

        // if you're lucky with hitting this enemy
        if (luckydeath == true)
        {
            // make rotate and exist timer countdown
            rotateTimer -= Time.fixedDeltaTime;
            existTimer -= Time.fixedDeltaTime;
            // if timer is less than zero
            if (existTimer <= 0)
            {
                if (transform.Find("TargetingConePivot"))
                {
                    // set lock on to false
                    GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = false;
                    // set a bool to check if this process has been activated
                    noted = true;
                }
                else if (noted == true) // if process has been activated
                {
                    // destroy enemy
                    Destroy(gameObject);

                    // set lock on again to the next closest enemy that exists
                    GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = true;

                    // create grounded version of enemy
                    Instantiate(Explosion, transform.position, transform.rotation);

                }
                else
                {
                    // destroy this object
                    Destroy(gameObject);

                    // create grounded version of enemy
                    Instantiate(Explosion, transform.position, transform.rotation);
                }
            }
        }

        // if there is lockon object in this object as a child
        if (transform.Find("TargetingConePivot"))
        {
            // turn on health bar
            healthbarimage.SetActive(true);
        }
        else
        { 
            // otherwise turn off health bar
            healthbarimage.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // if all good and functioning
        if (luckydeath == false && activate == true)
        {
            // sensing to make actions happen
            if (PlayerInSightRange) { AttackPlayer();}
            if (!PlayerInSightRange) { Patroling(); }

            // movement timers
            if (WalkPointIdleTimer > 0) { WalkPointIdleTimer -= Time.deltaTime; } else { WalkPointIdleTimer = 0; }
            if (WalktoPointTimer > 0) { WalktoPointTimer -= Time.deltaTime; } else { WalktoPointTimer = 0; }
        }

        // if there is entirely no way the player can take down the enemy, make it self destruct (though it would be a tedious wait)
        if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().WeaponActiveNum == 0 && GameObject.FindGameObjectsWithTag("ItemHitBox").Length <= 0.9 && GameObject.FindGameObjectsWithTag("PlayerAttack").Length <= 0.9)
        {
            luckydeath = true;
        }

        // if your lucky and hit
        if (luckydeath == true)
        {
            // move what is this object's position of up
            transform.position += this.transform.up/2;

            // rotate all over the place
            if (rotateTimer >= 0)
            {
                if (rotatex == 3 && rotatey == 3) { rotatex = 1; }
                if (rotatex == 1)
                {
                    transform.Rotate(new Vector3(5f, 0, 0));
                }
                if (rotatex == 2)
                {
                    transform.Rotate(new Vector3(-5f, 0, 0));
                }
                if (rotatex == 3)
                {
                    transform.Rotate(new Vector3(0, 0, 0));
                }
                if (rotatey == 1)
                {
                    transform.Rotate(new Vector3(0, 0, 5f));
                }
                if (rotatey == 2)
                {
                    transform.Rotate(new Vector3(0, 0, -5f));
                }
                if (rotatey == 3)
                {
                    transform.Rotate(new Vector3(0, 0, 0));
                }
            }
        }

        // if lucky death is true and hitting/below floor
        if (luckydeath == true && this.transform.position.y <= 1)
        {
            // this is the third death in here so i wont annotate it as it's exactly the same
            if (transform.Find("TargetingConePivot"))
            {
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = false;
                noted = true;
            }
            else if (noted == true)
            {
                Destroy(gameObject);
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = true;
                Instantiate(Explosion, transform.position, transform.rotation);

            }
            else
            {
                Destroy(gameObject);
                Instantiate(Explosion, transform.position, transform.rotation);
            }
        }

        //if y position is more then 5
        if (transform.position.y > 5 && luckydeath == false)
        {
            // slowly lower self
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);

            // set rotation to equal zero in luckydeath is false
            if (luckydeath == false)
            {
                transform.rotation = Quaternion.identity;
            }

        }

        // if below wanted y height
        if (transform.position.y <= 5 && luckydeath == false)
        {
            // yforced is equal to true
            yforced = true;
        }

        // yforced means force the y to be perminantly at the wanted height
        if (yforced == true && luckydeath == false)
        {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        }
    }

    private void Patroling()
    {
        if (luckydeath == false)
            {
                // if no walkpoint and timer is less then 0, find a new walk point
                if (WalkPointSet == false || WalkPointIdleTimer <= 0) { SearchWalkPoint(); }

                // set direction to walk to point
                Vector3 WalkToPoint = (transform.position - WalkPoint).normalized / 25;

                //if walkpoint is morethen zero and y position is five
                if (WalktoPointTimer > 0 && transform.position.y == 5)
                {
                    // rotate and move to point with appropiate animation
                    this.transform.rotation = Quaternion.LookRotation(WalkToPoint);
                    transform.position += WalkToPoint;
                    EnemyAnimator.SetInteger("EAnim", 1);
                }
                else // otherwise
                {
                    // idle animation
                    EnemyAnimator.SetInteger("EAnim", 0);
                }
            }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float RandomZ = Random.Range(-WalkPointRange - 3, WalkPointRange + 3);
        float RandomX = Random.Range(-WalkPointRange - 3, WalkPointRange + 3);

        // set random point by the calculations above
        WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

        // set timer to walk to point
        WalktoPointTimer = Random.Range(0.5f, 3.5f);

        // set timer to be idle for
        WalkPointIdleTimer = WalktoPointTimer + Random.Range(2.5f, 5.5f);

        // if not lucky death, rotation is zero
        if (luckydeath == false)
        {
            transform.rotation = Quaternion.identity;
        }

        // set walk point to true and time until attack to zero
        WalkPointSet = true;
        TimeUntilAttack = 0;
    }

    private void AttackPlayer()
    {
        // set animation to idle
        EnemyAnimator.SetInteger("EAnim", 0); 
        // set walk point set to false
        WalkPointSet = false;

        // if time til attack is less then zero and player isnt out of health
        if (TimeUntilAttack <= 0 && player.GetComponent<PlayerMovement>().Health >= 0.0001)
        {
            // shoot fireball
            Instantiate(FireBall, new Vector3(transform.position.x, 5, transform.position.z), transform.rotation);

            // set time until next attack
            TimeUntilAttack = Random.Range(2.5f, 7.5f);
        }

        // if time til next attack isnt less then zero, count down the time, otherwise set it to zero!
        if (TimeUntilAttack > 0) { TimeUntilAttack -= Time.deltaTime; } else { TimeUntilAttack = 0; }

        // look at player
        Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;
        this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void OnTriggerEnter(Collider collision)
    {
        // if hit by player's attack
        if (collision.tag == "PlayerAttack")
        {
            // play hit sound effect
            FindObjectOfType<SoundManager>().PlaySound("Punch");
            // decide what happens next by the random check at creation
            if (success == 1)
            {
                ded = true;
            }
            else
            {
                luckydeath = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if collding with floor and lucky death is active, die like any other enemy but with an explosion!
        if (luckydeath == true && collision.gameObject.layer == 7)
        {
            if (transform.Find("TargetingConePivot"))
            {
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = false;
                noted = true;
            }
            else if (noted == true)
            {
                Destroy(gameObject);
                GameObject.Find("Third-Person Player").GetComponent<EnemyLockOn>().temp = true;
                Instantiate(Explosion, transform.position, transform.rotation);

            }
            else
            {
                Destroy(gameObject);
                Instantiate(Explosion, transform.position, transform.rotation);
            }
        }
    }
}
