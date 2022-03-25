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
        if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter <= 3.95f)
        {
            CurrentPower.text = GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().PowerMeter.ToString("0.0");
        }
        else
        {
            CurrentPower.text = "MAX";
        }
    }
}

