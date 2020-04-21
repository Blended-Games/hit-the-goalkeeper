using Managers;
using UnityEngine;
using UnityEngine.Animations;

namespace Animator_States
{
    public class ShootBeforeDance : StateMachineBehaviour
    {
        
    //private static readonly int FightIdle = Animator.StringToHash("FightIdle");
   // private static readonly int Shoot = Animator.StringToHash("Shoot");
    //private static readonly int Taunt = Animator.StringToHash("Taunt");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
            animator.SetBool("Taunt", false);
            animator.SetBool("FightIdle", false); 
            animator.SetBool("Plotting", false);
             animator.SetBool("Sweep", false);
        }

    }
}