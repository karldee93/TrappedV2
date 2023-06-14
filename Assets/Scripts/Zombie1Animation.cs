using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1Animation : MonoBehaviour
{
    public Animator animator;
    public bool run;
    public bool attack;
    public bool dead;
    public void AnimateZombie()
    {
        if (run)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
        if (attack)
        {
            animator.SetBool("InRange", true);
        }
        else
        {
            animator.SetBool("InRange", false);
        }
        if (dead)
        {
            animator.SetBool("Dead", true);
        }
    }
}
