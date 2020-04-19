using UnityEngine;

namespace Animator_States
{
    public class WaitForMove : StateMachineBehaviour
    {
        private const float MinTime = 0;
        private const float MaxTime = 3;
        float _timer = 0;

        private readonly string[] _playerTrigger = {"Laugh", "Looking", "Taunt"};

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_timer <= 0)
            {
                RandomPlayerMove(animator);
                _timer = Random.Range(MinTime, MaxTime);
            }
            else
                _timer -= Time.deltaTime;
        }

        private void RandomPlayerMove(Animator animator)
        {
            System.Random rand = new System.Random();
            var movePos = rand.Next(_playerTrigger.Length);
            var player = _playerTrigger[movePos];
            animator.SetTrigger(player);
        }
    }
}