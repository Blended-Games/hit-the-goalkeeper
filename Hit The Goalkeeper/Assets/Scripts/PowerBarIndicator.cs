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
        private static readonly int Shoot = Animator.StringToHash("Shoot"); //This is temporary its just reaching the value inside animator.
        

        #region RectAnim

        private void Start()
        {
            //Moving the indicator from corner to another corner.
            var transform1 = transform;
            DoTweenController.DoLocalMove3D(transform1,
                new Vector3(1,0, 0), 2,
                Ease.Linear, -1,
                LoopType.Yoyo);
        }

        #endregion

        #region BallShootToGoalKeeper

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || GameManager.main.firstTouch) return; //Detecting for the input.
            GameManager.main.firstTouch = false;
            var shootValue = (float) Mathf.Abs(transform.localPosition.x); //Setting indicators current x value to a variable.
            CalculateShotValue(shootValue, GameManager.main.calculationID);
            transform.DORestart(); //Restarting anim for the second time because of power value assignment
        }

        
        private void CalculateShotValue(float shootValue, int id)
        {
            //Calculating value of the power 
            //Id is representing the current state(transform bar, power bar).
            //when we finalize the result we kill the animation.
            switch (id)
            {
                case 0 when shootValue >= .8f && shootValue <= 1:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(0,2)); //Text that will display on the screen.
                    GameManager.main.calculationID = 1; //Moving to next step which is shoot power.
                    GameManager.main.transformPositionToShoot =  GameManager.main.goalKeeperShootPositions[0];
                    GameManager.main.camStopFollow = true;
                    break;
                case 0 when shootValue >= .6f && shootValue < .8f:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(0,2)); //Text that will display on the screen.
                    GameManager.main.calculationID = 1; //Moving to next step which is shoot power.
                    GameManager.main.transformPositionToShoot =  GameManager.main.goalKeeperShootPositions[1];
                    break;
                case 0 when shootValue >= .4f && shootValue < .7f:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(2,4));
                    GameManager.main.calculationID = 1;
                    GameManager.main.transformPositionToShoot = GameManager.main.goalKeeperShootPositions[1];
                    break;
                case 0 when shootValue < .4f:        
                    DisplayMessage.main.ShowPowerBarText(Random.Range(4,6));
                    GameManager.main.calculationID = 1;
                    GameManager.main.transformPositionToShoot = GameManager.main.goalKeeperShootPositions[2];
                    break;
                case 0:
                    transform.DORestart();
                    break;
                case 1:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(0,6));                    
                    GameManager.main.ballAnimStartTrigger.SetBool(Shoot,true);
                    GameManager.main.ballShootPowerValue =  (1 / shootValue) * 2;
                    if (GameManager.main.ballShootPowerValue <= 10) GameManager.main.ballShootPowerValue = 10f;
                    transform.DOKill();
                    break;
            }

        }

        #endregion
    }
}