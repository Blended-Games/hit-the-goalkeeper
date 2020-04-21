using UnityEngine;

namespace Animator_States
{
    public class LaughState : StateMachineBehaviour
    {
       // private static readonly int Off = Animator.StringToHash("Off");
      //  private static readonly int Laugh = Animator.StringToHash("Laugh");

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            switch (ShootSystem.instance.state)
            {
                case PlayerState.GoalKeeperTurn:
                    animator.SetBool("Laugh", false);
                    break;
                case PlayerState.PlayerTurn: animator. SetBool("Off", false);
                    break;
            }
        }
    }
}