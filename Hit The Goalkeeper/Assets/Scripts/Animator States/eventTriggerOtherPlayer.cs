using UnityEngine;

namespace Animator_States
{
    public class EventTriggerOtherPlayer : MonoBehaviour
    {
        private static readonly int Shoot = Animator.StringToHash("Shoot");


        public void BallShotOnState()
        {
            //if (ShootSystem.instance.state == PlayerState.GoalKeeperTurn) return;
            BallMove.main.updateStop = false;
            BallMove.main.Movement();
            GetComponent<Animator>().SetBool(Shoot, false);

        
        }
    }
}