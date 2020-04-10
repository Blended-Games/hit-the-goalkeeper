using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTriggerOtherPlayer : MonoBehaviour
{
    private static readonly int Shoot = Animator.StringToHash("Shoot");

    public void BallShotOnState()
    {
        //if (ShootSystem.instance.state == PlayerState.GoalKeeperTurn) return;
        BallMove.main.updateStop = false;
        BallMove.main.Movement();
        this.GetComponent<Animator>().SetBool(Shoot, false);
    }
}