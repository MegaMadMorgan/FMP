using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    //public Rigidbody rigidbody;

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

    Quaternion rotato;

    //States
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Third-Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.angularSpeed = 999;
    }

    private void Update()
    {

        if ((this.name == "Flyer" || this.name == "Flyer(Clone)" || this.name == "FlyerV2" || this.name == "FlyerV2(Clone)" || this.name == "FlyerV3" || this.name == "FlyerV3(Clone)") && flying == true)
        {
            Vector3 pos = transform.position;
            pos.y = 5;
            transform.position = pos;
        }

        //Check for sight and attack range
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

        if (!(GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 1 || GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 2 || GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 6))
        {
            if (!(GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 5))
            {
                if (PlayerInSightRange && !PlayerInAttackRange) { ChasePlayer(); }
            }
            if (!PlayerInSightRange && !PlayerInAttackRange) { Patroling(); }
            if (PlayerInSightRange && PlayerInAttackRange) { AttackPlayer(); }
        }

        if (WalkPointIdleTimer > 0) { WalkPointIdleTimer -= Time.deltaTime; } else { WalkPointIdleTimer = 0; }
        if (TimeUntilAttack > 0) { TimeUntilAttack -= Time.deltaTime; } else { TimeUntilAttack = 0; }
        if (AttackResetTimer > 0) { AttackResetTimer -= Time.deltaTime; } else { AttackResetTimer = 0; }

        if (AttackResetTimer <= 0 && GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") == 6) { GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0); }

        if (TimeUntilAttack <= 0 && ReadyingAttack == true && player.GetComponent<PlayerMovement>().Health >= 0.0001)
        {
            GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 6);
            ReadyingAttack = false;
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
            if (name == "Flyer" || name == "Flyer(Clone)" || name == "FlyerV2" || name == "FlyerV2(Clone)" || name == "FlyerV3" || name == "FlyerV3(Clone)")
            {
                AttackResetTimer = 1.25f;
            }
        }

        if ((this.name == "Flyer" || this.name == "Flyer(Clone)" || this.name == "FlyerV2" || this.name == "FlyerV2(Clone)" || this.name == "FlyerV3" || this.name == "FlyerV3(Clone)") && flying == true)
        {
            if (transform.position.y > 5)
            {
                Vector3 pos = transform.position;
                pos.y -= 1;
                transform.position = pos;
            }
            else
            {
                Vector3 pos = transform.position;
                pos.y = 5;
                transform.position = pos;
            }
        }
    }

    private void Patroling()
    {
        if (this.name == "Bigger" || this.name == "Bigger(Clone)" || this.name == "BiggerV2" || this.name == "BiggerV2(Clone)" || this.name == "BiggerV3" || this.name == "BiggerV3(Clone)")
        {
            agent.speed = 1;
        }
        else
        {
            agent.speed = 2;
        }
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
                    ReadyingAttack = false;
                    AttackResetTimer = 0f;
                    TimeUntilAttack = 0f;
                    GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0);
                }
            }

            if (WalkPointIdleTimer <= 0) 
            {
                if ((this.name != "Flyer" && this.name != "Flyer(Clone)" && this.name != "FlyerV2" && this.name != "FlyerV2(Clone)" && this.name != "FlyerV3" && this.name != "FlyerV3(Clone)") || flying == false)
                {
                    GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 4);
                }
                else
                {
                    GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 7);
                }
            } 
            else 
            {
                if ((this.name != "Flyer" && this.name != "Flyer(Clone)" && this.name != "FlyerV2" && this.name != "FlyerV2(Clone)" && this.name != "FlyerV3" && this.name != "FlyerV3(Clone)") || flying == false)
                {
                    GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 0);
                }
                else
                {
                    GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 5);
                }
            }

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

    private void ChasePlayer()
    {
        agent.speed = 5;
        EnemyStats ES = GetComponent<EnemyStats>();
        if (ES.notstunned == true)
        {
            if ((this.name != "Flyer" && this.name != "Flyer(Clone)" && this.name != "FlyerV2" && this.name != "FlyerV2(Clone)" && this.name != "FlyerV3" && this.name != "FlyerV3(Clone)") || flying == false)
            {
                GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 3);
            }
            else
            {
                GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 7);
            }
            agent.SetDestination(player.position);
            State = "Chasing";
           // gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    private void AttackPlayer()
    {
        EnemyStats ES = GetComponent<EnemyStats>();
        if (ES.notstunned == true)
        {

            // stop enemy from moving

            // gameObject.GetComponent<NavMeshAgent>().enabled = false;

            //if (this.transform.position.y <= GameObject.Find("Third-Person Player").transform.position.y + 0.1f && this.transform.position.y >= GameObject.Find("Third-Person Player").transform.position.y - 0.1f)
            //{
            

                if (ReadyingAttack == false && AttackResetTimer <= 0) { TimeUntilAttack = Random.Range(1, 2); agent.SetDestination(transform.position); }

                if (TimeUntilAttack >= 0.0001 && GetComponentInParent<EnemyStats>().recollision <= 0 && (this.name != "Flyer" && this.name != "Flyer(Clone)" && this.name != "FlyerV2" && this.name != "FlyerV2(Clone)" && this.name != "FlyerV3" && this.name != "FlyerV3(Clone)"))
                {

                    if (GetComponentInParent<EnemyStats>().EnemyAnimator.GetInteger("EAnim") != 7)
                    {
                        ReadyingAttack = true;

                        GetComponentInParent<EnemyStats>().EnemyAnimator.SetInteger("EAnim", 5);

                        if ((this.name == "Flyer" || this.name == "Flyer(Clone)" || this.name == "FlyerV2" || this.name == "FlyerV2(Clone)" || this.name == "FlyerV3" || this.name == "FlyerV3(Clone)") && flying == true)
                        {
                            Vector3 pos = transform.position;
                            pos.y = 5;
                            transform.position = pos;
                        }
                        
                        Vector3 dir = GameObject.Find("Third-Person Player").transform.position - transform.position;
                        Quaternion lookRotation = Quaternion.LookRotation(dir);
                        Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 99).eulerAngles;
                        this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                    }
                }


            }

            if (!AlreadyAttacked)
            {
                //insert attack code here

                AlreadyAttacked = true;
                Invoke(nameof(ResetAttack), TimeBetweenAttacks);
            }

            State = "Attacking";
        //}
    }

    private void ResetAttack()
    {
        AlreadyAttacked = false;
    }
}

// https://www.codegrepper.com/code-examples/csharp/follow+path+with+rigidbody+in+unity //
//this should help make rigidbody movement
