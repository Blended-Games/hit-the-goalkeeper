using Accessables;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

namespace GUI
{
    public class PowerBarIndicator : MonoBehaviour
    {
        #region Variables

        private static readonly int
            Shoot = Animator.StringToHash("Shoot"); //This is temporary its just reaching the value inside animator.
        #endregion

        #region RectAnim

        private void Start()
        {
            //Moving the indicator from corner to another corner.
            var transform1 = transform;
            DoTweenController.DoLocalMove3D(transform1,
                new Vector3(1, 0, 0), 2,
                Ease.Linear, -1,
                LoopType.Yoyo);
        }

        #endregion

        #region BallShootToGoalKeeper

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || !GameManager.main.firstTouch) return; //Detecting for the input.
            //GameManager.main.firstTouch = false;

//kordinat duzlemndeki degeri
            var shootValue =
                Mathf.Abs(transform.localPosition.x); //Setting indicators current x value to a variable.
            CalculateShotValue(shootValue, GameManager.main.calculationID);
            transform.DORestart(); //Restarting anim for the second time because of power value assignment
        }


        public void CalculateShotValue(float shootValue, int id)
        {
            //Calculating value of the power
            //Id is representing the current state(transform bar, power bar).
            //when we finalize the result we kill the animation.
            switch (id)
            {
                case 0 when shootValue >= .71f && shootValue <= 1:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2)); //Text that will display on the screen.
                    GameManager.main.calculationID = 1; //Moving to next step which is shoot power.
                    GameManager.main.goalkeepersPositionArrayValue = 0;
                    GameManager.main.transformPositionToShoot = new Vector3(GameManager.main.goalKeeperShootPositions[3].
                        transform.position.x + Random.Range(1,3), 
                        GameManager.main.goalKeeperShootPositions[3].transform.position.y + Random.Range(1,3),
                        GameManager.main.goalKeeperShootPositions[3].transform.position.z + Random.Range(1,3));
                    GameManager.main.camStopFollow = true;

                    ShootSystem.instance.goalKeeperHUD.SetHp((int)GameManager.main.ballShootPowerValue);
                     ShootSystem.instance.PlayerAttack();
           
                    break;
                case 0 when shootValue >= .45f && shootValue < .7f:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2)); //Text that will display on the screen.
                  
                    GameManager.main.calculationID = 1; //Moving to next step which is shoot power.
                    GameManager.main.goalkeepersPositionArrayValue = 1;
                    GameManager.main.transformPositionToShoot = GameManager.main.goalKeeperShootPositions[1].transform.position;
                    break;
                case 0 when shootValue >= .135f && shootValue < .45f:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(2, 4));
                    GameManager.main.calculationID = 1;
                    GameManager.main.goalkeepersPositionArrayValue = 2;
                    GameManager.main.transformPositionToShoot = GameManager.main.goalKeeperShootPositions[2].transform.position;
                    break;
                case 0 when shootValue < .135f:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(4, 6));
                  
                    GameManager.main.calculationID = 1;
                    GameManager.main.transformPositionToShoot = GameManager.main.goalKeeperShootPositions[3].transform.position;
                    GameManager.main.ballGoesToHead = true;
                    break;
                case 0:
                    transform.DORestart();
                    break;
                case 1:

                    #region Display Message Conditions
                    
                    if(shootValue < .135f) DisplayMessage.main.ShowPowerBarText(Random.Range(4, 6));
                    if(shootValue >= .135f && shootValue <.45f) DisplayMessage.main.ShowPowerBarText(Random.Range(2, 4));
                    if(shootValue >=.45f && shootValue <.7f) DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2));
                    if(shootValue >= .71f && shootValue <=1) DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2));
                    #endregion

                    GameManager.main.ballAnimStartTrigger.SetBool(Shoot, true);
                    GameManager.main.ballShootPowerValue = (1 / shootValue) * 1.5f; //Setting the balls shooting value.
                    GameManager.main.ballAttackValue = GameManager.main.ballShootPowerValue % 20; //Setting the balls attack value to a normalized range.
                    if (shootValue < 1.5f) GameManager.main.ballShootPowerValue = 55f;
                    if (GameManager.main.ballShootPowerValue <= 20)
                        GameManager.main.ballShootPowerValue = 15f; //Setting the balls min shooting value.
                    if (GameManager.main.ballShootPowerValue >= 45)
                        GameManager.main.ballShootPowerValue = 55f; //Setting the balls max shooting value.
                    transform.DOKill(); //Killing the power bar indicator animation.
                    GameManager.main.firstTouch = false;
                    if (shootValue >= .8f) break;
                    GameManager.main.cineMachines[0].gameObject.SetActive(false);

                    break;
                      }
           }

        #endregion
           }
}