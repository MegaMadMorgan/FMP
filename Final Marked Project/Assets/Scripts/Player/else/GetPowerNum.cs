using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetPowerNum : MonoBehaviour
{
    public TextMeshProUGUI CurrentPower;
    public string Power;

    void Update()
    {
        if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter < 4)
        {
            // get power and convert it to text otherwise...
            CurrentPower.text = GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter.ToString("0.00");
        }
        else
        {
            // set it to MAX
            CurrentPower.text = "MAX";
        }
    }
}

