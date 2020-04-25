using System.Collections;
using Managers;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class MainCameraAnimationState : StateMachineBehaviour
{
    private IEnumerator _firstTouchEnable;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LevelSetter.main.goalKeeperAnim.SetTrigger("Laugh");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Assert(Camera.main != null, "Camera.main != null");
        Camera.main.GetComponent<Animator>().SetBool("CamAnimStop", true);
        Camera.main.GetComponent<Animator>().enabled = false;
        GameManager.main.StartCoroutine(GameManager.main.FirstTouchEnable());
    }
   
}