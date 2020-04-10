using Managers;
using UnityEngine;

namespace Animator_States
{
    public class BallShootState : StateMachineBehaviour
    {
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(Shoot,false);
        }
        
    }
}