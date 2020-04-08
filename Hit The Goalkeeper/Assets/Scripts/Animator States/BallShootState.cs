using Managers;
using UnityEngine;

namespace Animator_States
{
    public class BallShootState : StateMachineBehaviour
    {
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            BallMove.main.Movement();
            GameManager.main.playerAnim
                .SetBool(Shoot, false); //This is the animation trigger for ball shooting mechanic
            
            CameraControls.main.StartFieldOfViewChangeMainCam(); //This is the trigger for camera following mechanic.
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //
        //}
    }
}