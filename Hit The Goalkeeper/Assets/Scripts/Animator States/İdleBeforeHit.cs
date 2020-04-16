using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class İdleBeforeHit : StateMachineBehaviour
{
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
