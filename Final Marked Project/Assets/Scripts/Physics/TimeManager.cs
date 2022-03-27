using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;
    public float time = 1;

    void Update()
    {
        if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().Health <= 0.0001)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }

    public void DoSlowMotion()
    {
        if (GameObject.Find("Third-Person Player").GetComponent<PlayerMovement>().Health <= 0.0001)
        {
            if (Time.timeScale >= 0.1)
            {
                Time.timeScale -= slowdownFactor;
                Time.fixedDeltaTime = Time.timeScale; // time
                time -= 0.000000001f;
            }
            else
            {
                Time.timeScale = 0;
                Time.fixedDeltaTime = 0;
                time = 0;
                slowdownFactor = 0;
            }
        }



    }
}
