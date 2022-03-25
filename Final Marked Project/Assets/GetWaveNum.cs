using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetWaveNum : MonoBehaviour
{
    public TextMeshProUGUI CurrentWave;
    public string Wave;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Wave = GetComponent<WaveManager>().WaveNum;
        CurrentWave.text = GameObject.Find("GameManager").GetComponent<WaveManager>().WaveNum.ToString();
    }
}
