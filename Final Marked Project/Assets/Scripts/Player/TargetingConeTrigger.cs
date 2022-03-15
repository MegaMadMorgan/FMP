using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingConeTrigger : MonoBehaviour
{
    public GameObject selectedEnemy;
    public float coneScaleX;
    public float coneScaleY;
    public float coneScaleZ;

    private void Awake()
    {
        coneScaleX = transform.localScale.x;
        coneScaleY = transform.localScale.y;
        coneScaleZ = transform.localScale.x;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Targetable>())
        {
            selectedEnemy = other.gameObject;
        }
    }
}
