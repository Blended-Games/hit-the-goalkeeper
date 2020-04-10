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
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            BallMove.main._updateStop = false;
            BallMove.main.Movement();
            //CameraControls.main.StartFieldOfViewChangeMainCam(); //This is the trigger for camera following mechanic.
        }
    }
}