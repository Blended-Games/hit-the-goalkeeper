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

        // private static readonly int
        //     Shoot = Animator.StringToHash("Shoot"); //This is temporary its just reaching the value inside animator.

        private float _shootValue;
        [SerializeField] private DOTweenAnimation circular;
        [SerializeField] private GameObject circularBar;

        #endregion

        #region RectAnim

        private void OnEnable()
        {
            transform.DORestart();
            circular.DORestart();
            transform.DOPlay();
            circular.DOPlay();
        }

        private void Start()
        {
            //Moving the indicator from corner to another corner.
            DoTweenController.DoLocalMove3DWithLoop(transform,
                new Vector3(1, 0, 0), 2,
                Ease.Linear, -1,
                LoopType.Yoyo);
        }

        #endregion

        #region BallShootToGoalKeeper

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || !GameManager.main.firstTouch ||
                EventSystem.current.IsPointerOverGameObject() ||
                ShootSystem.instance.state != PlayerState.PlayerTurn) return;

            _shootValue =
                Mathf.Abs(transform.localPosition.x); //Setting indicators current x value to a variable.
            foreach (var button in GameManager.main.upgradeButtons)
            {
                button.SetActive(false);
            }

            transform.DOPause(); //Pausing anim for the second time because of power value assignment
            circular.DOPause();

            Vibrations.VibrationLight();
            CalculateShotValue(_shootValue);
        }


      public void CalculateShotValue(float shootVal)
        {
            //Calculating value of the power
            //Id is representing the current state(transform bar, power bar).
            //when we finalize the result we kill the animation.
            transform.DOPause();
            circular.DOPause();
            if (shootVal >= .8f)
            {
                if (ShootSystem.instance.state == PlayerState.PlayerTurn)
                {
                    DisplayMessage.main.ShowPowerBarText(Random.Range(0,
                        2)); //Text that will display on the screen.
                }

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
            }

            else if (shootVal >= .5f && shootVal < .8f)
            {
                if (ShootSystem.instance.state == PlayerState.PlayerTurn)
                {
                    DisplayMessage.main.ShowPowerBarText(Random.Range(0,
                        2)); //Text that will display on the screen.
                }

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

                //shootVal = Random.Range(.55f, .35f);

                GameManager.main.ballsHitRoad = TransformPosition.Leg;
            }

            else if (shootVal >= .135f && shootVal < .5f)
            {
                if (ShootSystem.instance.state == PlayerState.PlayerTurn)
                {
                    DisplayMessage.main.ShowPowerBarText(Random.Range(2, 4));
                }

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

                //shootVal = Random.Range(.35f, .25f);
                GameManager.main.ballsHitRoad = TransformPosition.Spine;
            }

            else if (shootVal < .135f)
            {
                if (ShootSystem.instance.state == PlayerState.PlayerTurn)
                {
                    DisplayMessage.main.ShowPowerBarText(Random.Range(4, 6));
                }

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
            }

            if (ShootSystem.instance.state == PlayerState.PlayerTurn)
            {
                ChangeFirstDanceAnimation();
            }

            if (ShootSystem.instance.state == PlayerState.PlayerTurn)
            {
                GameManager.main.ballAttackValue =
                    ((1 - shootVal) * 40f) +
                    ((40 * 5) / 100 *
                     ShootSystem.instance.unitPlayer.damageUpgrade
                    ); //Setting the balls shooting value with a normalized range.
                GameManager.main.ballCurveValue = transform.localPosition.x;
                Unit.SetDamageAfterTurn();
            }
            else if (ShootSystem.instance.state == PlayerState.GoalKeeperTurn)
            {
                GameManager.main.ballAttackValue =
                    ((1 - shootVal) * 40f) +
                    ((40 * 8) / 100 * ShootSystem.instance.unitGoalKeeper.damageUpgrade);
                Unit.SetMaxDamage();
            }

            GameManager.main.firstTouch = false;
           // GameManager.main.powerBarIndicatorParent.SetActive(false);
            
            LevelSetter.main.ActivateCam();
            shootVal=0;
        }
        #endregion

        int luck=0;
        //private static readonly int Taunt = Animator.StringToHash("Taunt");
        // private static readonly int FightIdle = Animator.StringToHash("FightIdle");

        private void ChangeFirstDanceAnimation()
        {
               luck +=1;

            if (luck==1)  
                LevelSetter.main.playerAnim.SetTrigger("Taunt");
            else if (luck==2)
                LevelSetter.main.playerAnim.SetTrigger("FightIdle");
            else if (luck ==3)
                LevelSetter.main.playerAnim.SetTrigger("Sweep");
            else if (luck>=4)
             {  LevelSetter.main.playerAnim.SetTrigger("Plotting");
                luck=0;
             }

         
            LevelSetter.main.playerAnim.SetTrigger("Shoot");
        }


    }
}