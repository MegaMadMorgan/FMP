using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //initialising Variables

    //the enemies to spawn
    public GameObject Enemy;
    public GameObject Bigger;
    public GameObject Bouncer;
    public GameObject Defender;
    public GameObject Healer;
    public GameObject Teleporter;

    //enemy randomisation
    int selectedenemy;

    //spawning enemy
    public void Spawn()
    {
        // if boss, randomely choose from range of enemies 
        if (transform.parent.name == "DropPodBoss")
        {
            selectedenemy = Random.Range(1, 7);
            if (selectedenemy == 1) { Instantiate(Enemy, transform.position, Quaternion.identity); }
            if (selectedenemy == 2) { Instantiate(Bigger, transform.position, Quaternion.identity); }
            if (selectedenemy == 3) { Instantiate(Bouncer, transform.position, Quaternion.identity); }
            if (selectedenemy == 4) { Instantiate(Defender, transform.position, Quaternion.identity); }
            if (selectedenemy == 5) { Instantiate(Healer, transform.position, Quaternion.identity); }
            if (selectedenemy == 6) { Instantiate(Teleporter, transform.position, Quaternion.identity); }
        }
        else // else simply spawn basic one!
        {
            Instantiate(Enemy, transform.position, Quaternion.identity);
        }
    }
}
