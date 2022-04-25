using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyNavagation : MonoBehaviour
{
    public Transform player;
    //public Rigidbody rigidbody;

    public LayerMask WhatIsPlayer;

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


    private void Awake()
    {
        player = GameObject.Find("Third-Person Player").transform;
    }

    private void Update()
    {
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
    }

    private void FixedUpdate()
    {

        if (PlayerInSightRange) { AttackPlayer(); Debug.Log("InRange"); }
        if (!PlayerInSightRange) { Patroling(); }

        if (WalkPointIdleTimer > 0) { WalkPointIdleTimer -= Time.deltaTime; } else { WalkPointIdleTimer = 0; }
        if (WalktoPointTimer > 0) { WalktoPointTimer -= Time.deltaTime; } else { WalktoPointTimer = 0; }
    }

    private void Patroling()
    {
        if (WalkPointSet == false || WalkPointIdleTimer <= 0) { SearchWalkPoint(); }
        Vector3 WalkToPoint = (transform.position - WalkPoint).normalized / 25;

        if (WalktoPointTimer > 0)
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

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float RandomZ = Random.Range(-WalkPointRange - 3, WalkPointRange + 3);
        float RandomX = Random.Range(-WalkPointRange - 3, WalkPointRange + 3);

        WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);
        WalktoPointTimer = Random.Range(0.5f, 3.5f);
        WalkPointIdleTimer = WalktoPointTimer + Random.Range(2.5f, 5.5f);

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
            Destroy(gameObject);
            Instantiate(GroundedVer, transform.position, transform.rotation);
        }

        if (collision.tag == "PlayerAttack" && EnemyAnimator.GetInteger("EAnim") == 1)
        {
            //flying death barrel
        }
    }
}
