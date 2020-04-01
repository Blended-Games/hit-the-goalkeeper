using System;
using System.Collections;
using DG.Tweening;
using NonObjectScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GUI
{
    public class PowerBarIndicator : MonoBehaviour
    {
        private static readonly int KickTheBall = Animator.StringToHash("KickTheBall"); //This is temporary its just reaching the value inside animator.
        

        #region RectAnim

        private void Start()
        {
            //Moving the indicator from corner to another corner.
            DoTweenController.AnchoredPosMove(GetComponent<RectTransform>(),
                new Vector2(180, GetComponent<RectTransform>().anchoredPosition.y), 1,
                Ease.Linear, -1,
                LoopType.Yoyo);
        }

        #endregion

        #region BallShootToGoalKeeper

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || GameManager.main.firstTouch) return; //Detecting for the input.
            GameManager.main.firstTouch = false;
            var shootValue = (int) Mathf.Abs(transform.localPosition.x); //Setting indicators current x value to a variable.
            CalculateShotValue(shootValue, GameManager.main.calculationID);
            transform.DORestart(); //Restarting anim for the second time because of power value assignment
        }

        
        private Transform CalculateShotValue(int shootValue, int id)
        {
            //Calculating value of the power 
            //Id is representing the current state(transform bar, power bar).
            //when we finalize the result we kill the animation.
            switch (id)
            {
                case 0 when shootValue >= 110 && shootValue <= 180:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(0,3));
                    GameManager.main.calculationID = 1;
                    return GameManager.main.goalKeeperShootPositions[0];
                case 0 when shootValue >= 40 && shootValue < 110:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(3,6));
                    GameManager.main.calculationID = 1;
                    return GameManager.main.goalKeeperShootPositions[1];
                case 0 when shootValue < 40:        
                    DisplayMessage.main.ShowPowerBarText(Random.Range(6,9));
                    GameManager.main.calculationID = 1;
                    return GameManager.main.goalKeeperShootPositions[2];
                case 0:
                    transform.DORestart();
                    break;
                case 1:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(5,8));
                    GameManager.main.ballAnimStartTrigger.SetTrigger(KickTheBall);
                    GameManager.main.ballShootPowerValue = shootValue * 2;
                    transform.DOKill();
                    break;
            }

            return null;
        }

        #endregion
    }
}