using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eanimtest : MonoBehaviour
{
    //initialising Variables
    public Animator EnemyAnimator;

    void Start()
    {
        //setting the enemy's animation
        EnemyAnimator.SetInteger("EAnim", 5);
    }
}
