using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyNavagation : MonoBehaviour
{
    public Transform player;
    //public Rigidbody rigidbody;

    public LayerMask WhatIsPlayer;

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

    public GameObject GroundedVer;
    bool noted;
    bool ded = false;

    // lucky death
    public int success;
    int rotatex;
    int rotatey;
    bool luckydeath = false;
    public GameObject Explosion;
    float rotateTimer = 0.75f;
    float existTimer = 3;

    bool activate = false;

    private void Awake()
    {
        player = GameObject.Find("Third-Person Player").transform;
        rotatex = Random.Range(1, 4);
        rotatey = Random.Range(1, 4);
        success = Random.Range(1, 3);
        if (luckydeath == false)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    private void Update()
    {
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);

        if (transform.rotation == Quaternion.identity)
        {
            activate = true;
        }

        if (ded == true)
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
                Instantiate(GroundedVer, transform.position, transform.rotation);

            }
            else
            {
                Destroy(gameObject);
                Instantiate(GroundedVer, transform.position, transform.rotation);
            }
        }

        if (luckydeath == true)
        {
            rotateTimer -= Time.fixedDeltaTime;
            existTimer -= Time.fixedDeltaTime;
            if (existTimer <= 0)
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

        //this is for showing the health bar
        if (transform.Find("TargetingConePivot"))
        {
            healthbarimage.SetActive(true);
        }
        else
        {
            healthbarimage.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (luckydeath == false && activate == true)
        {
            if (PlayerInSightRange) { AttackPlayer(); Debug.Log("InRange"); }
            if (!PlayerInSightRange) { Patroling(); }

            if (WalkPointIdleTimer > 0) { WalkPointIdleTimer -= Time.deltaTime; } else { WalkPointIdleTimer = 0; }
            if (WalktoPointTimer > 0) { WalktoPointTimer -= Time.deltaTime; } else { WalktoPointTimer = 0; }
        }

        if (luckydeath == true)
        {
            transform.position += this.transform.up/2;

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

        if (luckydeath == true && this.transform.position.y <= 1)
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

        if (transform.position.y > 5 && luckydeath == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
            if (luckydeath == false)
            {
                transform.rotation = Quaternion.identity;
            }

        }

        if (transform.position.y < 5 && luckydeath == false)
        {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        }
    }

    private void Patroling()
    {
        if (luckydeath == false)
            {
                if (WalkPointSet == false || WalkPointIdleTimer <= 0) { SearchWalkPoint(); }
                Vector3 WalkToPoint = (transform.position - WalkPoint).normalized / 25;

                if (WalktoPointTimer > 0 && transform.position.y == 5)
                {
                    this.transform.rotation = Quaternion.LookRotation(WalkToPoint);
                    transform.position += WalkToPoint;
                    EnemyAnimator.SetInteger("EAnim", 1);
                }
                else
                {
                    EnemyAnimator.SetInteger("EAnim", 0);
                }
            }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float RandomZ = Random.Range(-WalkPointRange - 3, WalkPointRange + 3);
        float RandomX = Random.Range(-WalkPointRange - 3, WalkPointRange + 3);

        WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);
        WalktoPointTimer = Random.Range(0.5f, 3.5f);
        WalkPointIdleTimer = WalktoPointTimer + Random.Range(2.5f, 5.5f);
        if (luckydeath == false)
        {
            transform.rotation = Quaternion.identity;
        }
        WalkPointSet = true;
        TimeUntilAttack = 0;
    }

    private void AttackPlayer()
    {
        EnemyAnimator.SetInteger("EAnim", 0); 
        WalkPointSet = false;

        if (TimeUntilAttack <= 0 && player.GetComponent<PlayerMovement>().Health >= 0.0001)
        {
            Instantiate(FireBall, new Vector3(transform.position.x, 5, transform.position.z), transform.rotation);
            TimeUntilAttack = Random.Range(2.5f, 7.5f);
        }

        if (TimeUntilAttack > 0) { TimeUntilAttack -= Time.deltaTime; } else { TimeUntilAttack = 0; }

        Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;
        this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "PlayerAttack" && EnemyAnimator.GetInteger("EAnim") == 0)
        {
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
