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
    public float existtimer = 12;

    private void Awake()
    {
        transform.rotation = Random.rotation;
    }

    private void Update()
    {
        existtimer -= Time.deltaTime;
        ItemParent = GameObject.Find("Third-Person Player");
        if (this.name == "Item Assault Rifle(Clone)" || this.name == "Item Assault Rifle")
        {
            weaponnum = 1;
        }
        if (this.name == "Item Baseball Bat(Clone)" || this.name == "Item Baseball Bat")
        {
            weaponnum = 2;
        }
        if (this.name == "Item Bottle(Clone)" || this.name == "Item Bottle")
        {
            weaponnum = 3;
            ItemParent.GetComponent<PlayerMovement>().bottleShatterHP = 5;
        }
        if (this.name == "Item Cleaver(Clone)" || this.name == "Item Cleaver")
        {
            weaponnum = 4;
        }
        if (this.name == "Item Foam Finger(Clone)" || this.name == "Item Foam Finger")
        {
            weaponnum = 5;
        }
        if (this.name == "Item Frying Pan(Clone)" || this.name == "Item Frying Pan")
        {
            weaponnum = 6;
        }
        if (this.name == "Item Golf Club(Clone)" || this.name == "Item Golf Club")
        {
            weaponnum = 7;
        }
        if (this.name == "Item Kitchen Knife(Clone)" || this.name == "Item Kitchen Knife")
        {
            weaponnum = 8;
        }
        if (this.name == "Item Legally Different Laser Sword(Clone)" || this.name == "Item Legally Different Laser Sword")
        {
            weaponnum = 9;
        }
        if (this.name == "Item Sledgehammer(Clone)" || this.name == "Item Sledgehammer")
        {
            weaponnum = 10;
        }
        if (this.name == "Item Spiked Baseball Bat(Clone)" || this.name == "Item Spiked Baseball Bat")
        {
            weaponnum = 11;
        }
        if (this.name == "Item Squeaky Hammer(Clone)" || this.name == "Item Squeaky Hammer")
        {
            weaponnum = 12;
        }
        if (this.name == "Item Stop Sign(Clone)" || this.name == "Item Stop Sign")
        {
            weaponnum = 13;
        }
        if (this.name == "Item Stop Sign Pizza Variant(Clone)" || this.name == "Item Stop Sign Pizza Variant")
        {
            weaponnum = 14;
        }
        if (this.name == "Item Uber Blade(Clone)" || this.name == "Item Uber Blade")
        {
            weaponnum = 15;
        }
        if (this.name == "Item Whack-A-Mole Hammer(Clone)" || this.name == "Item Whack-A-Mole Hammer")
        {
            weaponnum = 16;
        }
        if (this.name == "Item Dynamite(Clone)" || this.name == "Item Dynamite")
        {
            weaponnum = 17;
        }
        if (this.name == "Item Mirror(Clone)" || this.name == "Item Mirror")
        {
            weaponnum = 18;
        }
        if (this.name == "Item Volt(Clone)" || this.name == "Item Volt")
        {
            weaponnum = 19;
        }
        if (this.name == "Item Scepter(Clone)" || this.name == "Item Scepter")
        {
            weaponnum = 20;
        }
        if (this.name == "Item Fish(Clone)" || this.name == "Item Fish")
        {
            weaponnum = 21;
        }
        if (this.name == "Item Saw Cleaver(Clone)" || this.name == "Item Saw Cleaver")
        {
            weaponnum = 22;
        }


        if (existtimer <= Time.deltaTime)
        {
            Destroy(gameObject);
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
