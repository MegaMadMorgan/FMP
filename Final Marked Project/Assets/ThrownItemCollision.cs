using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItemCollision : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject ActualItem;
    public bool hit = false;
    void OnTriggerEnter(Collider Enemy)
    {
        rb.useGravity = true;
        if (Enemy.tag != "Player" && GetComponentInParent<ThrowItem>().ThrowTime >= 0.2 && hit == false)
        {
            hit = true;
            GameObject item = Instantiate(ActualItem, this.transform.position, this.transform.rotation) as GameObject;
            item.GetComponent<ItemCollision>().bounce = false;
            Destroy(transform.root.gameObject);
        }
    }
}
