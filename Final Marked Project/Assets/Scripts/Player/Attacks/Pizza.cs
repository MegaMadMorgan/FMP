using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    // initialising variables
    public float existtimer = 4;

    void Update()
    {
        // if existing
        if (existtimer >= 0.0001)
        {
            // if in position
            if (this.transform.position.y <= 1)
            {
                // and if less then size
                if (this.transform.localScale.x <= 149)
                {
                    // expand size
                    this.transform.localScale = new Vector3(this.transform.localScale.x + 2f, this.transform.localScale.y + 2f, this.transform.localScale.z + 2f);
                }
                else
                {
                    // count down exist timer
                    existtimer -= Time.deltaTime;
                }
            }
            else
            {
                // set y to appropiate position
                this.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            }
        }

        if (existtimer <= 0)
        {
            // shrink when exist timer is equal to zero
            this.transform.localScale = new Vector3(this.transform.localScale.x - 2f, this.transform.localScale.y - 2f, this.transform.localScale.z - 2f);
        }

        // if size is less then 0
        if (this.transform.localScale.x <= 0)
        {
            // destroy self
            Destroy(gameObject);
        }
        else if (this.transform.position.y <= 1)
        {
            // otherwise rotate
            this.transform.Rotate(0, 0.05f, 0);
        }
    }
}
