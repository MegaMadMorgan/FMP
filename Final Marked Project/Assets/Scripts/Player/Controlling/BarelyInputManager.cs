using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BarelyInputManager // static
{
    // scroll wheel enemy switching for if you're using mouse and keyboard
    public static float SubHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("Mouse ScrollWheel");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float SubVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("Mouse ScrollWheel");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
}
