using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPod : MonoBehaviour
{
    //initialising Variables
    public float SpawnTimerMax;
    public float SpawnTimer;
    public Animator door;
    public GameObject HB;
    
    // set spawn timer to half the max
    void Awake()
    {
        SpawnTimer = SpawnTimerMax / 2;
    }

    void FixedUpdate()
    {
        // if door animation done, reset spawn timer
        if (door.GetFloat("DoorTimer") <= -0.01)
        {
            SpawnTimer = SpawnTimerMax;
        }

        // lower the spawn timer unless stunned
        if (GetComponentInParent<EnemyStats>().Stun <= 0)
        {
            door.SetFloat("DoorTimer", SpawnTimer);
            SpawnTimer -= Time.deltaTime;
        }

        // if doing door open animation, set hitbox to active
        if (this.door.GetCurrentAnimatorStateInfo(0).IsName("Door"))
        {
            HB.SetActive(true);
        }
        else //if not doing door open animation, set hitbox to inactive
        {
            HB.SetActive(false);

            // if unique version of enemy, spin to win!
            if (GetComponentInParent<EnemyStats>().Stun <= 0 && (transform.name == "DropPodBoss" || transform.name == "DropPodBoss(Clone)" || transform.name == "DropPodV3" || transform.name == "DropPodV3(Clone)"))
            {
                transform.Rotate(0, 5, 0, Space.Self);
            }
        }
    }
}
