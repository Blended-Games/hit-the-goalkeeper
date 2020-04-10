using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class SetTheGameState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.main.firstTouch = true;
    }
}