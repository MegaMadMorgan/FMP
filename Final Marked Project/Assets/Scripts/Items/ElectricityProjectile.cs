using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityProjectile : MonoBehaviour
{
    //initialising Variable
    float EXIST = 0.25f;

    void Awake()
    {
        //set rotation
        if (this.name != "GunShot") // do this if you're not a gunshot item (e.g. only for magic and electricity shots)
        {
            Quaternion HBRotation = GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerMesh.transform.rotation;
            this.transform.rotation = HBRotation;
        }
    }

    void Update()
    {
        //time to exist for
        EXIST -= Time.deltaTime;
        if (EXIST <= 0) { Destroy(gameObject); }

        //set rotation
        if (this.name != "GunShot") // do this if you're not a gunshot item (e.g. only for magic and electricity shots)
        {
            Quaternion HBRotation = GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerMesh.transform.rotation;
            this.transform.rotation = HBRotation;
        }
    }
}
