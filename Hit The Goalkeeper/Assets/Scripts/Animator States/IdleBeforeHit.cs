using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class İdleBeforeHit : StateMachineBehaviour
{
    
    //     private const float MinTime = 0;
    //     private const float MaxTime = 3;
    //     float _timer = 0;

    // string[] playerTrigger ={"FightTrigger","TauntTrigger"};

    // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {   
    //     RandomPlayerMove(animator);

    // }
    // void RandomPlayerMove(Animator animator){
    //     System.Random rand= new System.Random();
    //     int movePos=rand.Next(playerTrigger.Length);
    //     string player=playerTrigger[movePos];
    //     animator.SetTrigger(player);
    // }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ShootSystem.instance.state == PlayerState.PlayerTurn)
        {
            
           LevelSetter.main.p1.transform.position = LevelSetter.main.p1Pos.position;
        }
        else if (ShootSystem.instance.state == PlayerState.GoalKeeperTurn)
        {
            LevelSetter.main.p2.transform.position = LevelSetter.main.p2Pos.position;
        }
    }
}
