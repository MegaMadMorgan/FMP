using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealerNav : MonoBehaviour
{
    //note that this script i believe was scrapped due to lackluster code, so the healer had a rework on its healing concept

    //initialising Variables
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer, WhatIsEnemy;
    public string State;

    //Patroling
    public Vector3 WalkPoint;
    bool WalkPointSet;
    public float WalkPointRange;
    public float WalktoPointTimer = 3.5f;
    public float WalkPointIdleTimer;

    //Attacking
    public float TimeBetweenHeals;
    public bool AlreadyHealed;

    public bool ReadyingHeal = false;
    public float HealResetTimer;
    public Rigidbody rb;

    //rotation
    Quaternion rotato;

    //States
    public float SightRange, HealRange;
    public bool PlayerInSightRange, EnemyInSightRange, EnemyInHealRange;

    private void Awake()
    {
        // set variables
        player = GameObject.Find("Third-Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.angularSpeed = 999;
    }

    private void Update()
    {
        // set variable again, just in case!
        player = GameObject.Find("Third-Person Player").transform;
        
        //Check for sight and Heal range
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        EnemyInSightRange = (GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>().health < GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>().maxhealth) && !this && Vector3.Distance(GameObject.FindGameObjectWithTag("Enemy").transform.position, transform.position) >= 3;
        EnemyInHealRange = (GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>().health < GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>().maxhealth) && !this && Vector3.Distance(GameObject.FindGameObjectWithTag("Enemy").transform.position, transform.position) <= 3;

        // make ai do actions dependant on distance is seen from!
        if (!(GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 1 || GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 2 || GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 6))
        {
            if (!PlayerInSightRange && !EnemyInSightRange && !EnemyInHealRange) { Patroling(); }
            if (PlayerInSightRange && !EnemyInSightRange && !EnemyInHealRange) { FleeFromPlayer();}
            if (EnemyInSightRange && !EnemyInHealRange) { ChaseWeakEnemy(); }
            if (EnemyInHealRange) { HealEnemy(); }
        }
        if (PlayerInSightRange) { FleeFromPlayer(); Debug.Log("ahh, player"); }

        // if you cannot get to walk point and the timer runs out, change walk point and reset timer
        if (WalkPointIdleTimer > 0) { WalkPointIdleTimer -= Time.deltaTime; } else { WalkPointIdleTimer = 0; }

        // timer to reset ability to heal
        if (HealResetTimer > 0) { HealResetTimer -= Time.deltaTime; } else { HealResetTimer = 0; }

        // stop healing and go back to idle
        if (HealResetTimer <= 0 && GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 6) { GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0); }

        // heal enemy!
        if (ReadyingHeal == true && player.GetComponent<PlayerMovement>().Health >= 0.0001)
        {
            GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 6);
            ReadyingHeal = false;
            HealResetTimer = 0.8f;
        }
    }

    private void Patroling()
    {
        // set movement speed to 2
        agent.speed = 2;

        // set ES Variable to enemy stats script
        EnemyStats ES = GetComponent<EnemyStats>();

        // if enemy isnt stunned
        if (ES.notstunned == true)
        {
            // find position to walk to
            if (!WalkPointSet && WalkPointIdleTimer <= 0) { SearchWalkPoint(); }

            // if there is a walk point, walk to it
            if (WalkPointSet)
            {
                // walk point timer is lowering
                WalktoPointTimer -= Time.deltaTime;

                // go to walk point
                agent.SetDestination(WalkPoint);

                // if patrolling whilst in animation which is about to heal
                if (GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 5)
                {
                    ReadyingHeal = false;
                    HealResetTimer = 0f;
                    GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0);
                }
            }

            // stop walking else you can walk (this is botched old code)
            if (WalkPointIdleTimer <= 0) { GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 4); } else { GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0); }

            // get the distance to walk point
            Vector3 DistanceToWalkPoint = transform.position - WalkPoint;

            //WalkPoint reached
            if (DistanceToWalkPoint.magnitude < 1f || WalktoPointTimer <= 0)
            {
                if (WalkPointIdleTimer <= 0) { WalkPointIdleTimer = Random.Range(1, 4); }
                WalkPointSet = false;
                WalktoPointTimer = 3.5f;
            }

            // set state
            State = "Patroling";
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float RandomZ = Random.Range(-WalkPointRange-3, WalkPointRange+3);
        float RandomX = Random.Range(-WalkPointRange-3, WalkPointRange+3);

        //walkpoint is both random points in x and z to make a place to walk to!
        WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

        // if on floor set the check to see if walk point set to be true
        if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround))
        {
            WalkPointSet = true;
        }
    }

    // chase enemy needing health
    private void ChaseWeakEnemy()
    {
        // set new move speed
        agent.speed = 5;
        // set target enemy
        GameObject enemy = GameObject.FindWithTag("Enemy");
        // get enemy health
        float enemyhealth = enemy.GetComponent<EnemyStats>().health;
        // get enemy max health
        float maxenemyhealth = enemy.GetComponent<EnemyStats>().maxhealth;

        // chase weak enemy
        if (this.GetComponent<EnemyStats>().notstunned == true && enemyhealth < maxenemyhealth)
        {
            GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 3);
            agent.SetDestination(enemy.transform.position);
        }

        State = "Chasing";
    }

    private void FleeFromPlayer()
    {
        // set enemy speed
        agent.speed = 5;

        // set variable to get enemystats component
        EnemyStats ES = GetComponent<EnemyStats>();

        // run away!
        if (ES.notstunned == true)
        {
            GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 3);
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            agent.SetDestination(newPos);
            State = "Fleeing";
        }
    }

    private void HealEnemy()
    {
        // set variable to get enemystats component
        EnemyStats ES = GetComponent<EnemyStats>();

        // if enemy isnt stunned
        if (ES.notstunned == true)
        {
            // if not healing, movement is idle
            if (ReadyingHeal == false && HealResetTimer <= 0) {agent.SetDestination(transform.position); }

            if (GetComponentInParent<EnemyStats>().recollision <= 0)
            {
                if (GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") != 7) //healing, never was finished
                {
                    ReadyingHeal = true;

                    GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 5);
                    
                    Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(dir);
                    Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;
                    this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                }
            }
        }

        //reseting the heal process post healing
        if (!AlreadyHealed)
        {
            AlreadyHealed = true;
            Invoke(nameof(ResetHeal), TimeBetweenHeals);
        }

        //clarifying the state for testing in the inspector
        State = "Healing";
    }

    //reset heal variable
    private void ResetHeal()
    {
        AlreadyHealed = false;
    }
}