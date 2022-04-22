using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealerNav : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    //public Rigidbody rigidbody;

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

    Quaternion rotato;

    //States
    public float SightRange, HealRange;
    public bool PlayerInSightRange, EnemyInSightRange, EnemyInHealRange;

    private void Awake()
    {
        player = GameObject.Find("Third-Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.angularSpeed = 999;
    }

    private void Update()
    {
        player = GameObject.Find("Third-Person Player").transform;
        //Check for sight and Heal range
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        EnemyInSightRange = (GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>().health < GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>().maxhealth) && !this && Vector3.Distance(GameObject.FindGameObjectWithTag("Enemy").transform.position, transform.position) >= 3;
        EnemyInHealRange = (GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>().health < GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>().maxhealth) && !this && Vector3.Distance(GameObject.FindGameObjectWithTag("Enemy").transform.position, transform.position) <= 3;

        if (!(GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 1 || GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 2 || GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 6))
        {
            if (!PlayerInSightRange && !EnemyInSightRange && !EnemyInHealRange) { Patroling(); }
            if (PlayerInSightRange && !EnemyInSightRange && !EnemyInHealRange) { FleeFromPlayer();}
            if (EnemyInSightRange && !EnemyInHealRange) { ChaseWeakEnemy(); }
            if (EnemyInHealRange) { HealEnemy(); }
        }
        if (PlayerInSightRange) { FleeFromPlayer(); Debug.Log("ahh, player"); }

        if (WalkPointIdleTimer > 0) { WalkPointIdleTimer -= Time.deltaTime; } else { WalkPointIdleTimer = 0; }
        if (HealResetTimer > 0) { HealResetTimer -= Time.deltaTime; } else { HealResetTimer = 0; }

        if (HealResetTimer <= 0 && GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 6) { GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0); }

        if (ReadyingHeal == true && player.GetComponent<PlayerMovement>().Health >= 0.0001)
        {
            GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 6);
            ReadyingHeal = false;
            HealResetTimer = 0.8f;
        }
    }

    private void Patroling()
    {
        agent.speed = 2;
        EnemyStats ES = GetComponent<EnemyStats>();
        if (ES.notstunned == true)
        {
            if (!WalkPointSet && WalkPointIdleTimer <= 0) { SearchWalkPoint(); }

            if (WalkPointSet)
            {
                WalktoPointTimer -= Time.deltaTime;
                agent.SetDestination(WalkPoint);
                if (GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 5)
                {
                    ReadyingHeal = false;
                    HealResetTimer = 0f;
                    GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0);
                }
            }

            if (WalkPointIdleTimer <= 0) { GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 4); } else { GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0); }

            Vector3 DistanceToWalkPoint = transform.position - WalkPoint;

            //WalkPoint reached
            if (DistanceToWalkPoint.magnitude < 1f || WalktoPointTimer <= 0)
            {
                if (WalkPointIdleTimer <= 0) { WalkPointIdleTimer = Random.Range(1, 4); }
                WalkPointSet = false;
                WalktoPointTimer = 3.5f;
            }

            State = "Patroling";
            //gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float RandomZ = Random.Range(-WalkPointRange-3, WalkPointRange+3);
        float RandomX = Random.Range(-WalkPointRange-3, WalkPointRange+3);

        WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

        if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround))
        {
            WalkPointSet = true;
        }
    }

    private void ChaseWeakEnemy()
    {
        agent.speed = 5;
        GameObject enemy = GameObject.FindWithTag("Enemy");
        float enemyhealth = enemy.GetComponent<EnemyStats>().health;
        float maxenemyhealth = enemy.GetComponent<EnemyStats>().maxhealth;
        if (this.GetComponent<EnemyStats>().notstunned == true && enemyhealth < maxenemyhealth)
        {
            GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 3);
            agent.SetDestination(enemy.transform.position);
            State = "Chasing";
        }
    }

    private void FleeFromPlayer() // done
    {
        agent.speed = 5;
        EnemyStats ES = GetComponent<EnemyStats>();
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
        EnemyStats ES = GetComponent<EnemyStats>();
        if (ES.notstunned == true)
        {
                if (ReadyingHeal == false && HealResetTimer <= 0) {agent.SetDestination(transform.position); }

                if (GetComponentInParent<EnemyStats>().recollision <= 0)
                {

                    if (GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") != 7)
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

            if (!AlreadyHealed)
            {
                //insert attack code here

                AlreadyHealed = true;
                Invoke(nameof(ResetHeal), TimeBetweenHeals);
            }

            State = "Healing";
    }

    private void ResetHeal()
    {
        AlreadyHealed = false;
    }
}

// https://www.codegrepper.com/code-examples/csharp/follow+path+with+rigidbody+in+unity //
//this should help make rigidbody movement
