using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetWaveNum : MonoBehaviour
{
    public TextMeshProUGUI CurrentWave;
    public string Wave;

    void Update()
    {
        CurrentWave.text = GameObject.Find("GameManager").GetComponent<WaveManager>().WaveNum.ToString();
    }
}
