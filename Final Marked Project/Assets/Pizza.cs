using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public float existtimer = 4;
    void Update()
    {
        if (existtimer >= 0.0001)
        {
            if (this.transform.position.y <= 1)
            {
                if (this.transform.localScale.x <= 149)
                {
                    this.transform.localScale = new Vector3(this.transform.localScale.x + 2f, this.transform.localScale.y + 2f, this.transform.localScale.z + 2f);
                }
                else
                {
                    existtimer -= Time.deltaTime;
                }
            }
            else
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            }
        }

        if (existtimer <= 0)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x - 2f, this.transform.localScale.y - 2f, this.transform.localScale.z - 2f);
        }
        if (this.transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
        else if (this.transform.position.y <= 1)
        {
            this.transform.Rotate(0, 0.05f, 0);
        }
    }
}
