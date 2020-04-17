using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBeforeDance : StateMachineBehaviour
{
     private static readonly int FightIdle = Animator.StringToHash("FightIdle");
     private static readonly int Capoeria = Animator.StringToHash("Capoeria");
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         animator.SetBool(Capoeria,false);
         animator.SetBool(FightIdle,false);
    }

}
