using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private void Update()
    {
        // make the enemy's healthbar face the camera!
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }
}
