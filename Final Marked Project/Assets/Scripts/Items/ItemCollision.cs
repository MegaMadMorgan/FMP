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
    public Rigidbody rb;

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
        transform.rotation = Random.rotation;
    }

    private void Start()
    {
        rb.AddForce(0, 16, 0, ForceMode.Impulse);
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
            GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().CollidingWithItem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggerrange = false; 
        //PlayerMovement PlayerMovementScript = ItemParent.transform.GetComponent<PlayerMovement>(); // PlayerMovement PlayerMovementScript = ItemParent.transform.GetComponent<PlayerMovement>(); 
        GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().CollidingWithItem = false;
    }

    public void pickup()
    {
        PlayerMovement PlayerMovementScript = ItemParent.transform.GetComponent<PlayerMovement>();
        if (triggerrange == true && PlayerMovementScript.AttackTime <= 0)
        {
            if (PlayerMovementScript.WeaponActiveNum == 0)
            {
                PlayerMovementScript.WeaponActiveNum = weaponnum;
            }
            else if (PlayerMovementScript.WeaponActiveNum == 1)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(AR, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 2)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(BB, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 3)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(B, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 4)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(C, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 5)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(FF, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 6)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(FP, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 7)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(GC, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 8)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(KK, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 9)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(LDLS, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 10)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(S, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 11)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(SBB, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 12)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(SH, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 13)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(SS, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 14)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(SSPV, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 15)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(UB, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 16)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(WAMH, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 17)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(D, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 18)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(M, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 19)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(V, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 20)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(Sc, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 21)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(F, HBPos, Random.rotation);
            }
            else if (PlayerMovementScript.WeaponActiveNum == 22)
            {
                Vector3 playerPos = GameObject.Find("Third-Person Player").transform.position;
                Vector3 playerDirection = GameObject.Find("Third-Person Player").transform.forward;
                Vector3 HBPos = playerPos + (playerDirection * -0.8f);
                Instantiate(SC, HBPos, Random.rotation);
            }
            PlayerMovementScript.WeaponActiveNum = weaponnum;
            PlayerMovementScript.CollidingWithItem = false;
            PlayerMovementScript.AttackTime = 0.4f;
            PlayerMovementScript.AttackRepeatTimer = 0.4f;
            PlayerMovementScript.attackfullstring = 0.4f;
            Destroy(transform.root.gameObject);
        }
    }
}
