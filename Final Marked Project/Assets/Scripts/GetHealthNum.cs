using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetHealthNum : MonoBehaviour
{
    public TextMeshProUGUI CurrentHealth;
    public string Health;

    void Update()
    {
        if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().Health <= 99.5f)
        {
            CurrentHealth.text = GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().Health.ToString("0");
        }
        else
        {
            CurrentHealth.text = "MAX";
        }
    }
}
