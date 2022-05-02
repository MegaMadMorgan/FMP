using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

public class FreelookAxisControl : MonoBehaviour
{
    public void Start()
    {
        // set input axis to the scroll wheel for who next to target in terms of enemies
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    public float GetAxisCustom(string axisName)
    {
        // changes in diagonals for next or previous enemy
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
