using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingConeDecoupler : MonoBehaviour
{
    public void Detach()
    {
        // set lockon object's parent to be null
        transform.parent = null;
    }
}
