using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Rigidbody rigidbody;

    public LayerMask WhatIsGround, WhatIsPlayer;

    public string State;

    //Patroling
    public Vector3 WalkPoint;
    bool WalkPointSet;
    public float WalkPointRange;

    //Attacking
    public float TimeBetweenAttacks;
    bool AlreadyAttacked;

    //States
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Third-Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

        if (!PlayerInSightRange && !PlayerInAttackRange) { Patroling(); }
        if (PlayerInSightRange && !PlayerInAttackRange) { ChasePlayer(); }
        if (PlayerInSightRange && PlayerInAttackRange) { AttackPlayer(); }
    }

    private void Patroling()
    {
        EnemyStats ES = GetComponent<EnemyStats>();
        if (ES.Stun <= 0)
        {
            if (!WalkPointSet) { SearchWalkPoint(); }

            if (WalkPointSet)
            {
                agent.SetDestination(WalkPoint);
            }

            Vector3 DistanceToWalkPoint = transform.position - WalkPoint;

            //WalkPoint reached
            if (DistanceToWalkPoint.magnitude < 1f)
            {
                WalkPointSet = false;
            }

            State = "Patroling";
            //gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float RandomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float RandomX = Random.Range(-WalkPointRange, WalkPointRange);

        WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

        if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround))
        {
            WalkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        EnemyStats ES = GetComponent<EnemyStats>();
        if (ES.Stun <= 0)
        {
            agent.SetDestination(player.position);
            State = "Chasing";
           // gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    private void AttackPlayer()
    {
        EnemyStats ES = GetComponent<EnemyStats>();
        if (ES.Stun <= 0)
        {

            // stop enemy from moving
            agent.SetDestination(transform.position);
           // gameObject.GetComponent<NavMeshAgent>().enabled = false;

            transform.LookAt(player);

            if (!AlreadyAttacked)
            {
                //insert attack code here

                AlreadyAttacked = true;
                Invoke(nameof(ResetAttack), TimeBetweenAttacks);
            }

            State = "Attacking";
        }
    }

    private void ResetAttack()
    {
        AlreadyAttacked = false;
    }
}

// https://www.codegrepper.com/code-examples/csharp/follow+path+with+rigidbody+in+unity //
//this should help make rigidbody movement
