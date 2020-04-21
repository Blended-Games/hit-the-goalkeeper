using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidHitState : StateMachineBehaviour
{
    //private static readonly int MidHit = Animator.StringToHash("MidHit");

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("MidHit",false);
    }
}