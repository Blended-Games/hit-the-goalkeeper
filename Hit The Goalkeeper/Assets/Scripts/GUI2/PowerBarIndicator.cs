﻿using Accessables;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace GUI2
{
    public class PowerBarIndicator : MonoBehaviour
    {
        #region Singleton

        public static PowerBarIndicator main;

        private void Awake()
        {
            if (main != null && main != this)
            {
                Destroy(gameObject);
                return;
            }

            main = this;
        }

        #endregion

        #region Variables

        private static readonly int
            Shoot = Animator.StringToHash("Shoot"); //This is temporary its just reaching the value inside animator.

        private float shootValue;

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
                !EventSystem.current.IsPointerOverGameObject() &&
                ShootSystem.instance.state == PlayerState.PlayerTurn)
            {
                //Detecting for the input.
                if (GameManager.main.calculationID == 1)
                {
                    shootValue =
                        (transform.localPosition.x);
                    GameManager.main.ballCurveValue = shootValue;
                    shootValue =
                        Mathf.Abs(transform.localPosition.x);
                }
                else if (GameManager.main.calculationID == 0)
                {
                    shootValue =
                        Mathf.Abs(transform.localPosition.x); //Setting indicators current x value to a variable.
                }

                CalculateShotValue(shootValue, GameManager.main.calculationID);
                transform.DORestart(); //Restarting anim for the second time because of power value assignment
            }
        }


        public void CalculateShotValue(float shootValue, int id)
        {
            //Calculating value of the power
            //Id is representing the current state(transform bar, power bar).
            //when we finalize the result we kill the animation.
            switch (id)
            {
                case 0 when shootValue >= .8f:
                    if (ShootSystem.instance.state == PlayerState.PlayerTurn)
                    {
                        DisplayMessage.main.ShowPowerBarText(Random.Range(0,
                            2)); //Text that will display on the screen.
                    }

                    GameManager.main.calculationID = 1; //Moving to next step which is shoot power.
                    switch (ShootSystem.instance.state)
                    {
                        case PlayerState.PlayerTurn:
                            LevelSetter.main.transformPositionToShoot = new Vector3(Random.Range(-1, 1),
                                Random.Range(.35f, 1.26f),
                                LevelSetter.main.goalKeeperShootPositions[2].transform.position.z + 1);
                            break;
                        case PlayerState.GoalKeeperTurn:
                            LevelSetter.main.transformPositionToShoot = new Vector3(Random.Range(-1, 1),
                                Random.Range(.35f, 1.26f),
                                LevelSetter.main.playerShootPositions[2].transform.position.z - 1);
                            break;
                    }

                    GameManager.main.ballsHitRoad = TransformPosition.Off;

                    GameManager.main.camStopFollow = true;

                    break;

                case 0 when shootValue >= .5f && shootValue < .8f:
                    if (ShootSystem.instance.state == PlayerState.PlayerTurn)
                    {
                        DisplayMessage.main.ShowPowerBarText(Random.Range(0,
                            2)); //Text that will display on the screen.
                    }

                    GameManager.main.calculationID = 1; //Moving to next step which is shoot power.
                    switch (ShootSystem.instance.state)
                    {
                        case PlayerState.PlayerTurn:
                            LevelSetter.main.transformPositionToShoot =
                                LevelSetter.main.goalKeeperShootPositions[0].transform.position;
                            break;
                        case PlayerState.GoalKeeperTurn:
                            LevelSetter.main.transformPositionToShoot =
                                LevelSetter.main.playerShootPositions[0].transform.position;
                            break;
                    }

                    GameManager.main.ballsHitRoad = TransformPosition.Leg;

                    break;

                case 0 when shootValue >= .135f && shootValue < .5f:
                    if (ShootSystem.instance.state == PlayerState.PlayerTurn)
                    {
                        DisplayMessage.main.ShowPowerBarText(Random.Range(2, 4));
                    }

                    GameManager.main.calculationID = 1;
                    switch (ShootSystem.instance.state)
                    {
                        case PlayerState.PlayerTurn:
                            LevelSetter.main.transformPositionToShoot =
                                LevelSetter.main.goalKeeperShootPositions[1].transform.position;
                            break;
                        case PlayerState.GoalKeeperTurn:
                            LevelSetter.main.transformPositionToShoot =
                                LevelSetter.main.playerShootPositions[1].transform.position;
                            break;
                    }

                    GameManager.main.ballsHitRoad = TransformPosition.Spine;

                    break;
                case 0 when shootValue < .135f:
                    if (ShootSystem.instance.state == PlayerState.PlayerTurn)
                    {
                        DisplayMessage.main.ShowPowerBarText(Random.Range(4, 6));
                    }

                    GameManager.main.calculationID = 1;
                    switch (ShootSystem.instance.state)
                    {
                        case PlayerState.PlayerTurn:
                            LevelSetter.main.transformPositionToShoot =
                                LevelSetter.main.goalKeeperShootPositions[2].transform.position;
                            break;
                        case PlayerState.GoalKeeperTurn:
                            LevelSetter.main.transformPositionToShoot =
                                LevelSetter.main.playerShootPositions[2].transform.position;
                            break;
                    }

                    GameManager.main.ballsHitRoad = TransformPosition.Head;
                    break;
                case 0:
                    transform.DORestart();
                    break;
                case 1:

                    #region Display Message Conditions

                    if (shootValue < .135f && ShootSystem.instance.state == PlayerState.PlayerTurn) 
                        DisplayMessage.main.ShowPowerBarText(Random.Range(4, 6));
                    if (shootValue >= .135f && shootValue < .45f && ShootSystem.instance.state == PlayerState.PlayerTurn)
                        DisplayMessage.main.ShowPowerBarText(Random.Range(2, 4));
                    if (shootValue >= .45f && shootValue < .7f && ShootSystem.instance.state == PlayerState.PlayerTurn)
                        DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2));
                    if (shootValue >= .71f && shootValue <= 1 && ShootSystem.instance.state == PlayerState.PlayerTurn) 
                        DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2));

                    #endregion

                    if (ShootSystem.instance.state == PlayerState.PlayerTurn)
                    {
                        LevelSetter.main.playerAnim.SetBool(Shoot, true);
                    }
                    if ((PlayerPrefs.GetFloat("highlevel") == 0 || PlayerPrefs.GetFloat("highlevel") == 1) && ShootSystem.instance.state == PlayerState.PlayerTurn)
                    {
                        GameManager.main.ballAttackValue = 55;
                    }
                    else
                    {
                        GameManager.main.ballAttackValue =
                            ((1 - shootValue) * 40f); //Setting the balls shooting value with a normalized range.
                    }
                    
                 //  GameManager.main.ballAttackValue =
                 //      ((1 - shootValue) * 35f); //Setting the balls shooting value with a normalized range.
                    GameManager.main.powerBarIndicatorParent.SetActive(false);
                    GameManager.main.firstTouch = false;
                    LevelSetter.main.ActivateCam();
                    GameManager.main.calculationID = 0;
                    break;
            }
        }

        #endregion
    }
}