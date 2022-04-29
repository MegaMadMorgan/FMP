using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightSensor : MonoBehaviour
{
    public bool LightsActive = false;
    public Light Light;
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<LightingManager>().TimeOfDay >= 6.5f && GameObject.Find("GameManager").GetComponent<LightingManager>().TimeOfDay <= 17.5f)
        {
            LightsActive = true;
        }
        else
        {
            LightsActive = false;
        }

        if (LightsActive == true)
        {
            Light.intensity = 5;
        }
        else
        {
            Light.intensity = 0;
        }
    }
}
