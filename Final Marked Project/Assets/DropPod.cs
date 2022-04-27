using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPod : MonoBehaviour
{
    public float SpawnTimerMax;
    public float SpawnTimer;
    public Animator door;
    public GameObject HB;
    void Awake()
    {
        SpawnTimer = SpawnTimerMax / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (door.GetFloat("DoorTimer") <= -0.01)
        {
            SpawnTimer = SpawnTimerMax;
        }

        if (GetComponentInParent<EnemyStats>().Stun <= 0)
        {
            door.SetFloat("DoorTimer", SpawnTimer);
            SpawnTimer -= Time.deltaTime;
        }

        if (this.door.GetCurrentAnimatorStateInfo(0).IsName("Door"))
        {
            HB.SetActive(true);
        }
        else
        {
            HB.SetActive(false);
            if (GetComponentInParent<EnemyStats>().Stun <= 0 && (transform.name == "DropPodBoss" || transform.name == "DropPodBoss(Clone)" || transform.name == "DropPodV3" || transform.name == "DropPodV3(Clone)"))
            {
                transform.Rotate(0, 5, 0, Space.Self);
            }
        }
    }
}
