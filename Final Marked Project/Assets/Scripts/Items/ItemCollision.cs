using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    private GameObject ItemParent;
    private PlayerMovement ItemParentScript;
    public bool triggerrange = false;
    public int weaponnum;

    private void Awake()
    {
        
    }

    private void Update()
    {
        ItemParent = GameObject.Find("Third-Person Player");
        if (this.name == "Item Assault Rifle")
        {
            weaponnum = 1;
        }
        if (this.name == "Item Baseball Bat")
        {
            weaponnum = 2;
        }
        if (this.name == "Item Bottle")
        {
            weaponnum = 3;
            ItemParent.GetComponent<PlayerMovement>().bottleShatterHP = 5;
        }
        if (this.name == "Item Cleaver")
        {
            weaponnum = 4;
        }
        if (this.name == "Item Foam Finger")
        {
            weaponnum = 5;
        }
        if (this.name == "Item Frying Pan")
        {
            weaponnum = 6;
        }
        if (this.name == "Item Golf Club")
        {
            weaponnum = 7;
        }
        if (this.name == "Item Kitchen Knife")
        {
            weaponnum = 8;
        }
        if (this.name == "Item Legally Different Laser Sword")
        {
            weaponnum = 9;
        }
        if (this.name == "Item Sledgehammer")
        {
            weaponnum = 10;
        }
        if (this.name == "Item Spiked Baseball Bat")
        {
            weaponnum = 11;
        }
        if (this.name == "Item Squeaky Hammer")
        {
            weaponnum = 12;
        }
        if (this.name == "Item Stop Sign")
        {
            weaponnum = 13;
        }
        if (this.name == "Item Stop Sign Pizza Variant")
        {
            weaponnum = 14;
        }
        if (this.name == "Item Uber Blade")
        {
            weaponnum = 15;
        }
        if (this.name == "Item Whack-A-Mole Hammer")
        {
            weaponnum = 16;
        }
        if (this.name == "Item Dynamite")
        {
            weaponnum = 17;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            triggerrange = true;
            PlayerMovement PlayerMovementScript = ItemParent.transform.GetComponent<PlayerMovement>();
            PlayerMovementScript.CollidingWithItem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggerrange = false; 
        PlayerMovement PlayerMovementScript = ItemParent.transform.GetComponent<PlayerMovement>(); 
        PlayerMovementScript.CollidingWithItem = false;
    }

    public void pickup()
    {
        if (triggerrange == true)
        {
            PlayerMovement PlayerMovementScript = ItemParent.transform.GetComponent<PlayerMovement>();
            PlayerMovementScript.WeaponActiveNum = weaponnum;
            Destroy(gameObject);
        }
    }
}
