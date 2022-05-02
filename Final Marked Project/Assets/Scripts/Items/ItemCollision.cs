using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    //initialising Variables
    private GameObject ItemParent;
    private PlayerMovement ItemParentScript;
    public bool triggerrange = false;
    public int weaponnum;
    public float existtimer = 12;
    public Rigidbody rb;
    public bool bounce = true;

    //items to spawn
    public GameObject AR;
    public GameObject BB;
    public GameObject B;
    public GameObject C;
    public GameObject FF;
    public GameObject FP;
    public GameObject GC;
    public GameObject KK;
    public GameObject LDLS;
    public GameObject S;
    public GameObject SBB;
    public GameObject SH;
    public GameObject SS;
    public GameObject SSPV;
    public GameObject UB;
    public GameObject WAMH;
    public GameObject D;
    public GameObject M;
    public GameObject V;
    public GameObject Sc;
    public GameObject F;
    public GameObject SC;

    private void Awake()
    {
        //randomise this object's rotation
        transform.rotation = Random.rotation;
    }

    private void Start()
    {
        //make object bounce unless set otherwise in spawn
        if (bounce == true) // for fixing collision issues with player upon enemy's death
        {
            rb.AddForce(0, 16, 0, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        //time to exist for countdown
        existtimer -= Time.deltaTime;

        // this object's parent is the player
        ItemParent = GameObject.Find("Third-Person Player");

        //set the weapon number for the player object
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

        //destroy if exist timer is finished
        if (existtimer <= Time.deltaTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //if colliding with player, you're in trigger range, make sure everything is ok with player picking up the object!
        if (collision.tag == "Player")
        {
            triggerrange = true;
            GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().CollidingWithItem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if player left the trigger range, make sure everything is not ok with player picking up the object!
        triggerrange = false; 
        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().CollidingWithItem = false;
    }

    public void pickup()
    {
        PlayerMovement PlayerMovementScript = ItemParent.transform.GetComponent<PlayerMovement>();
        if (triggerrange == true && PlayerMovementScript.AttackTime <= 0)
        {
            //code for spawning the current item the player is using behind him/her to swap to the item this script is on! (this is lengthy)
            if (PlayerMovementScript.WeaponActiveNum == 0)
            {
                PlayerMovementScript.WeaponActiveNum = weaponnum;
            }
            else if (PlayerMovementScript.WeaponActiveNum == 1)
            {
                //get player position
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                //get player direction
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                //set the spawning position
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                //spawn in appropiate position and rotation
                Instantiate(AR, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 2)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(BB, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 3)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(B, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 4)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(C, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 5)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(FF, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 6)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(FP, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 7)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(GC, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 8)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(KK, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 9)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(LDLS, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 10)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(S, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 11)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(SBB, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 12)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(SH, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 13)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(SS, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 14)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(SSPV, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 15)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(UB, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 16)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(WAMH, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 17)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(D, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 18)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(M, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 19)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(V, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 20)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(Sc, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 21)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(F, SpawnPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 22)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 SpawnPos = playerPos + (playerDirection * -0.8f);
                Instantiate(SC, SpawnPos, Random.rotation);
            }
            // set player's weapon to the number this weapon is
            PlayerMovementScript.WeaponActiveNum = weaponnum;
            // player isnt colliding with an item anymore
            PlayerMovementScript.CollidingWithItem = false;
            // attack time / attack repeat timer / attack full string set to give the player a pause before continuing
            PlayerMovementScript.AttackTime = 0.4f;
            PlayerMovementScript.AttackRepeatTimer = 0.4f;
            PlayerMovementScript.attackfullstring = 0.4f;
            //destroy this game object
            Destroy(transform.root.gameObject);
        }
    }
}
