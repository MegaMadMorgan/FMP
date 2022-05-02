using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightSensor : MonoBehaviour
{
    //initialising Variables
    public bool LightsActive = false;
    public Light Light;
    void Update()
    {
        //check time of day to see if its dark, if it is dark, activate the lights
        if (GameObject.Find("GameManager").GetComponent<LightingManager>().TimeOfDay >= 6.5f && GameObject.Find("GameManager").GetComponent<LightingManager>().TimeOfDay <= 17.5f)
        {
            LightsActive = true;
        }
        else
        {
            LightsActive = false;
        }

        //set light intensity (used to show if the lights are meant to be on or off in game!)
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
