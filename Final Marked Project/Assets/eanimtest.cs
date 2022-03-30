using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eanimtest : MonoBehaviour
{
    public Animator EnemyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAnimator.SetInteger("EAnim", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
