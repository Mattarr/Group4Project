using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PunchScript : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        //lets player punch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
        }
    }

    //punch animation plays when triggered
    void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
