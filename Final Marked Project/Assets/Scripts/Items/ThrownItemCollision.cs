using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItemCollision : MonoBehaviour
{
    //initialising Variables
    public Rigidbody rb;
    public GameObject ActualItem;
    public bool hit = false;

    void OnTriggerEnter(Collider Enemy)
    {
        //set gravity to be active
        rb.useGravity = true;

        //if colliding with anything (and throwtime less then equal to 0.2 & hit is false)
        if (Enemy.tag != "Player" && (Enemy.tag != "Enemy" || (Enemy.tag == "Enemy" && (Enemy.name == "Fly(Clone)" || Enemy.name == "FlyV2(Clone)" || Enemy.name == "FlyV3(Clone)"))) && GetComponentInParent<ThrowItem>().ThrowTime >= 0.2 && hit == false)
        {
            // we've hit something
            hit = true;

            // make the colectable version of this game object
            GameObject item = Instantiate(ActualItem, this.transform.position, this.transform.rotation) as GameObject;

            // dont make the created object bounce
            item.GetComponent<ItemCollision>().bounce = false;

            //destroy this object
            Destroy(transform.root.gameObject);
        }

        if (Enemy.tag == "Enemy" && GetComponentInParent<ThrowItem>().ThrowTime >= 0.2 && hit == false)
        {
            // we've hit something
            hit = true;

            // make the colectable version of this game object
            GameObject item = Instantiate(ActualItem, this.transform.position, this.transform.rotation) as GameObject;

            // dont make the created object bounce
            item.GetComponent<ItemCollision>().bounce = true;

            //disable collision
            item.GetComponent<ItemCollision>().collisiontimer = 0.5f;

            //destroy this object
            Destroy(transform.root.gameObject);
        }
    }
}
