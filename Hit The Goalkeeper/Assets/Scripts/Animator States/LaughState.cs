using UnityEngine;

namespace Animator_States
{
    public class LaughState : StateMachineBehaviour
    {
        private static readonly int Off = Animator.StringToHash("Off");

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(Off, false);
        }
    }
}