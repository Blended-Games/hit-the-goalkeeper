using Managers;
using UnityEngine;

namespace Animator_States
{
    public class SetTheGameState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GameManager.main.firstTouch = true;
        }
    }
}