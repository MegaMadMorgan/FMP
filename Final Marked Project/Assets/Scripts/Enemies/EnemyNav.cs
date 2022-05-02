using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    //initialising Variables
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;
    public string State;
    public bool flying = true;

    //Patroling
    public Vector3 WalkPoint;
    bool WalkPointSet;
    public float WalkPointRange;
    public float WalktoPointTimer = 3.5f;
    public float WalkPointIdleTimer;

    //Attacking
    public float TimeBetweenAttacks;
    public bool AlreadyAttacked;

    public bool ReadyingAttack = false;
    public float TimeUntilAttack;
    public float AttackResetTimer;
    public Rigidbody rb;

    //rotation
    Quaternion rotato;

    //States
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    private void Awake()
    {
        // set variables
        player = GameObject.Find("Third-Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.angularSpeed = 999;
    }

    private void Update()
    {
        //Check for sight and attack range
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

        // make ai do actions dependant on distance is seen from!
        if (!(GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 1 || GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 2 || GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 6))
        {
            if (!(GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 5))
            {
                if (PlayerInSightRange && !PlayerInAttackRange) { ChasePlayer(); }
            }
            if (!PlayerInSightRange && !PlayerInAttackRange) { Patroling(); }
            if (PlayerInSightRange && PlayerInAttackRange) { AttackPlayer(); }
        }

        // if you cannot get to walk point and the timer runs out, change walk point and reset timer
        if (WalkPointIdleTimer > 0) { WalkPointIdleTimer -= Time.deltaTime; } else { WalkPointIdleTimer = 0; }

        // timer to reset ability to attack
        if (TimeUntilAttack > 0) { TimeUntilAttack -= Time.deltaTime; } else { TimeUntilAttack = 0; }

        // countdown attack timer when in animation
        if (AttackResetTimer > 0) { AttackResetTimer -= Time.deltaTime; } else { AttackResetTimer = 0; }

        // stop attacking and go back to idle
        if (AttackResetTimer <= 0 && GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 6) { GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0); }

        // attack times (set when enemy starts attacking (e.g, ReadyingAttack))
        if (TimeUntilAttack <= 0 && ReadyingAttack == true && player.GetComponent<PlayerMovement>().Health >= 0.0001)
        {
            // play attack animation
            GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 6);

            // not readying attack animation anymore
            ReadyingAttack = false;

            //set times
            if (name == "Chaser" || name == "Chaser(Clone)" || name == "ChaserV2" || name == "ChaserV2(Clone)" || name == "ChaserV3" || name == "ChaserV3(Clone)")
            {
                AttackResetTimer = 1.25f;
            }
            if (name == "Defender" || name == "Defender(Clone)" || name == "DefenderV2" || name == "DefenderV2(Clone)" || name == "DefenderV3" || name == "DefenderV3(Clone)")
            {
                AttackResetTimer = 0.8f;
            }
            if (name == "Bigger" || name == "Bigger(Clone)" || name == "BiggerV2" || name == "BiggerV2(Clone)" || name == "BiggerV3" || name == "BiggerV3(Clone)")
            {
                AttackResetTimer = 2.75f;
            }
            if (name == "Heavy" || name == "Heavy(Clone)" || name == "HeavyV2" || name == "HeavyV2(Clone)" || name == "HeavyV3" || name == "HeavyV3(Clone)")
            {
                AttackResetTimer = 1.25f;
            }
            if (name == "GroundedFly" || name == "GroundedFly(Clone)" || name == "GroundedFlyV2" || name == "GroundedFlyV2(Clone)" || name == "GroundedFlyV3" || name == "GroundedFlyV3(Clone)")
            {
                AttackResetTimer = 1.25f;
            }
        }
    }

    private void Patroling()
    {
        //set patrol movement speed
        if (this.name == "Bigger" || this.name == "Bigger(Clone)" || this.name == "BiggerV2" || this.name == "BiggerV2(Clone)" || this.name == "BiggerV3" || this.name == "BiggerV3(Clone)")
        {
            agent.speed = 1;
        }
        else
        {
            agent.speed = 2;
        }

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

                // if patrolling whilst in animation which is about to attack
                if (GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 5)
                {
                    // set all attacking variables to be essentially reset
                    ReadyingAttack = false;
                    AttackResetTimer = 0f;
                    TimeUntilAttack = 0f;
                    GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0);
                }
            }

            // if you're at the walk point
            if (WalkPointIdleTimer <= 0) 
            {
                // walk to point animation
                GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 4);
            }
            else
            {
                // stand around animation
                GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0);
            }

            // set the distance between the target location and the current location
            Vector3 DistanceToWalkPoint = transform.position - WalkPoint;

            // WalkPoint reached
            if (DistanceToWalkPoint.magnitude < 1f || WalktoPointTimer <= 0)
            {
                if (WalkPointIdleTimer <= 0) { WalkPointIdleTimer = Random.Range(1, 4); }
                WalkPointSet = false;
                WalktoPointTimer = 3.5f;
            }

            // clarify the state in the inspector
            State = "Patroling";
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float RandomZ = Random.Range(-WalkPointRange-3, WalkPointRange+3);
        float RandomX = Random.Range(-WalkPointRange-3, WalkPointRange+3);

        // walkpoint is both random points in x and z to make a place to walk to!
        WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

        // if on floor set the check to see if walk point set to be true
        if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround))
        {
            WalkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        // set new move speed
        agent.speed = 5;

        // set variable to get enemystats component
        EnemyStats ES = GetComponent<EnemyStats>();

        // if enemy isnt stunned
        if (ES.notstunned == true)
        {
            //any anim, i apologise, i like doing the funnies
            GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 3);

            // set target to move towards to be player
            agent.SetDestination(player.position);

            // clarify the state in the inspector
            State = "Chasing";
        }
    }

    private void AttackPlayer()
    {
        // set variable to get enemystats component
        EnemyStats ES = GetComponent<EnemyStats>();

        if (ES.notstunned == true)
        {
            //randomise attack time in preparing animation & stop moving
            if (ReadyingAttack == false && AttackResetTimer <= 0) { TimeUntilAttack = Random.Range(1, 2); agent.SetDestination(transform.position); }

            // if readying attack
            if (TimeUntilAttack >= 0.0001 && GetComponentInParent<EnemyStats>().recollision <= 0)
            {
                // if animation not 7
                if (GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") != 7)
                {
                    // readying attack bool is true
                    ReadyingAttack = true;

                    // set animation to preparing attack animation
                    GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 5);
                    
                    // rotate to face player
                    Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(dir);
                    Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;
                    this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                }
            }
        }

        //  fixing attack if not active
        if (!AlreadyAttacked)
        {
            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);
        }

        //clarifying the state for testing in the inspector
        State = "Attacking";
    }

    //reset already attack variable
    private void ResetAttack()
    {
        AlreadyAttacked = false;
    }
}
