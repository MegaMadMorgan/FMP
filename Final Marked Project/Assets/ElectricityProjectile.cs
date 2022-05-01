using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityProjectile : MonoBehaviour
{
    float EXIST = 0.25f;
    // Start is called before the first frame update
    void Awake()
    {
        if (this.name != "GunShot")
        {
            Quaternion HBRotation = GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerMesh.transform.rotation;
            this.transform.rotation = HBRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        EXIST -= Time.deltaTime;
        if (EXIST <= 0) { Destroy(gameObject); }
        if (this.name != "GunShot")
        {
            Quaternion HBRotation = GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PlayerMesh.transform.rotation;
            this.transform.rotation = HBRotation;
        }
    }
}
