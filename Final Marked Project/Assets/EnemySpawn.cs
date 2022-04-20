using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    public void Spawn()
    {
        Instantiate(Enemy, transform.position, Quaternion.identity);
    }
}
