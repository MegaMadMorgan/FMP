using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BarelyInputManager
{
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
    public static bool CameraButton()
    {
        return Input.GetButtonDown("Lock-On");
    }

    public static bool CameraButtonHeld()
    {
        return Input.GetButton("Lock-On");
    }
}
