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
            DoTweenController.DoLocalMove3DWithLoop(transform1,
                new Vector3(1, 0, 0), 2,
                Ease.Linear, -1,
                LoopType.Yoyo);
        }

        #endregion

        #region BallShootToGoalKeeper

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && GameManager.main.firstTouch &&
                ShootSystem.instance.state == PlayerState.PlayerTurn)
            {
                //Detecting for the input.
                var shootValue =
                    Mathf.Abs(transform.localPosition.x); //Setting indicators current x value to a variable.
                CalculateShotValue(shootValue, GameManager.main.calculationID);
                transform.DORestart(); //Restarting anim for the second time because of power value assignment
            }
        }


        private void CalculateShotValue(float shootValue, int id)
        {
            //Calculating value of the power
            //Id is representing the current state(transform bar, power bar).
            //when we finalize the result we kill the animation.
            switch (id)
            {
                case 0 when shootValue >= .8f:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2)); //Text that will display on the screen.
                    GameManager.main.calculationID = 1; //Moving to next step which is shoot power.
                    switch (ShootSystem.instance.state)
                    {
                        case PlayerState.PlayerTurn:
                            GameManager.main.transformPositionToShoot = new Vector3(Random.Range(-1, 1),
                                Random.Range(.35f, 1.26f),
                                GameManager.main.goalKeeperShootPositions[2].transform.position.z + 1);
                            break;
                        case PlayerState.GoalKeeperTurn:
                            GameManager.main.transformPositionToShoot = new Vector3(Random.Range(-1, 1),
                                Random.Range(.35f, 1.26f),
                                GameManager.main.playerShootPositions[2].transform.position.z + 1);
                            break;
                    }

                    GameManager.main.transformPositionToShoot = new Vector3(Random.Range(-1, 1),
                        Random.Range(.35f, 1.26f),
                        GameManager.main.goalKeeperShootPositions[2].transform.position.z + 1);
                    GameManager.main.ballsHitRoad = TransformPosition.Off;

                    GameManager.main.camStopFollow = true;

                    break;

                case 0 when shootValue >= .5f && shootValue < .8f:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2)); //Text that will display on the screen.

                    GameManager.main.calculationID = 1; //Moving to next step which is shoot power.
                    switch (ShootSystem.instance.state)
                    {
                        case PlayerState.PlayerTurn:
                            GameManager.main.transformPositionToShoot =
                                GameManager.main.goalKeeperShootPositions[0].transform.position;
                            break;
                        case PlayerState.GoalKeeperTurn:
                            GameManager.main.transformPositionToShoot =
                                GameManager.main.playerShootPositions[0].transform.position;
                            break;
                    }

                    GameManager.main.ballsHitRoad = TransformPosition.Leg;

                    break;

                case 0 when shootValue >= .135f && shootValue < .5f:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(2, 4));
                    GameManager.main.calculationID = 1;
                    switch (ShootSystem.instance.state)
                    {
                        case PlayerState.PlayerTurn:
                            GameManager.main.transformPositionToShoot =
                                GameManager.main.goalKeeperShootPositions[1].transform.position;
                            break;
                        case PlayerState.GoalKeeperTurn:
                            GameManager.main.transformPositionToShoot =
                                GameManager.main.playerShootPositions[1].transform.position;
                            break;
                    }

                    GameManager.main.ballsHitRoad = TransformPosition.Spine;

                    break;
                case 0 when shootValue < .135f:
                    DisplayMessage.main.ShowPowerBarText(Random.Range(4, 6));

                    GameManager.main.calculationID = 1;
                    switch (ShootSystem.instance.state)
                    {
                        case PlayerState.PlayerTurn:
                            GameManager.main.transformPositionToShoot =
                                GameManager.main.goalKeeperShootPositions[2].transform.position;
                            break;
                        case PlayerState.GoalKeeperTurn:
                            GameManager.main.transformPositionToShoot =
                                GameManager.main.playerShootPositions[2].transform.position;
                            break;
                    }

                    GameManager.main.ballsHitRoad = TransformPosition.Head;
                    break;
                case 0:
                    transform.DORestart();
                    break;
                case 1:

                    #region Display Message Conditions

                    if (shootValue < .135f) DisplayMessage.main.ShowPowerBarText(Random.Range(4, 6));
                    if (shootValue >= .135f && shootValue < .45f)
                        DisplayMessage.main.ShowPowerBarText(Random.Range(2, 4));
                    if (shootValue >= .45f && shootValue < .7f)
                        DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2));
                    if (shootValue >= .71f && shootValue <= 1) DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2));

                    #endregion

                    GameManager.main.playerAnim.SetBool(Shoot, true);
                    GameManager.main.ballAttackValue =
                        ((1 / shootValue) * 1.5f) % 20; //Setting the balls shooting value with a normalized range.
                    //transform.DOKill(); //Killing the power bar indicator animation.
                    GameManager.main.powerBarIndicatorParent.SetActive(false);
                    GameManager.main.firstTouch = false;
                    GameManager.main.ActivateCam();
                    GameManager.main.calculationID = 0;
                    break;
            }
        }

        #endregion
    }
}