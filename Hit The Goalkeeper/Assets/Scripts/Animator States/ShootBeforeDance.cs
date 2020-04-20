using Managers;
using UnityEngine;
using UnityEngine.Animations;

namespace Animator_States
{
    public class ShootBeforeDance : StateMachineBehaviour
    {
        private static readonly int FightIdle = Animator.StringToHash("FightIdle");
        private static readonly int Capoeria = Animator.StringToHash("Capoeria");


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(Capoeria, false);
            animator.SetBool(FightIdle, false);
        }

    }
}