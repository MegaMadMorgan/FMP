using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

public class FreelookAxisControl : MonoBehaviour
{
    public void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    public float GetAxisCustom(string axisName)
    {
        if (axisName == "X Axis")
        {
            return BarelyInputManager.SubHorizontal();
        }
        else if (axisName == "Y Axis")
        {
            return BarelyInputManager.SubVertical();
        }
        return 0;
    }
}
