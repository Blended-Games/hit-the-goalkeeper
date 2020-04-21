using Accessables;
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
        [SerializeField] private DOTweenAnimation circular, ballMove;
        [SerializeField] private GameObject circularBar;

        #endregion

        #region RectAnim

        private void OnEnable()
        {
            transform.DORestart();
            transform.DOPlay();
            ballMove.DORestart();
            ballMove.DOPlay();
            circular.DOPause();
            circularBar.SetActive(false);
            ColorChange.main.DoColorChange();
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
            switch (GameManager.main.calculationID)
            {
                //Detecting for the input.
                case 1:
                {
                    var transform1 = transform;
                    var localPosition = transform1.localPosition;
                    _shootValue =
                        (localPosition.x);
                    GameManager.main.ballCurveValue = _shootValue;
                    _shootValue =
                        Mathf.Abs(localPosition.x);
                    circular.DOPause();
                    break;
                }
                case 0:
                {
                    _shootValue =
                        Mathf.Abs(transform.localPosition.x); //Setting indicators current x value to a variable.
                    foreach (var button in GameManager.main.upgradeButtons)
                    {
                        button.SetActive(false);
                    }
                    transform.DORestart(); //Restarting anim for the second time because of power value assignment
                    circularBar.SetActive(true);
                    circular.DORestart();
                    circular.DOPlay();
                    ColorChange.main.DoColorChangeStop();
                    break;
                }
            }

            Vibrations.VibrationSoft();
            CalculateShotValue(_shootValue, GameManager.main.calculationID);
        }


        public void CalculateShotValue(float shootVal, int id)
        {
            //Calculating value of the power
            //Id is representing the current state(transform bar, power bar).
            //when we finalize the result we kill the animation.
            switch (id)
            {
                case 0 when _shootValue >= .8f:
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

                case 0 when _shootValue >= .5f && _shootValue < .8f:
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

                    _shootValue = Random.Range(.55f, .35f);

                    GameManager.main.ballsHitRoad = TransformPosition.Leg;

                    break;

                case 0 when _shootValue >= .135f && _shootValue < .5f:
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

                    _shootValue = Random.Range(.35f, .25f);
                    GameManager.main.ballsHitRoad = TransformPosition.Spine;

                    break;
                case 0 when _shootValue < .135f:
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

                    if (_shootValue < .135f && ShootSystem.instance.state == PlayerState.PlayerTurn)
                        DisplayMessage.main.ShowPowerBarText(Random.Range(4, 6));
                    if (_shootValue >= .135f && _shootValue < .45f &&
                        ShootSystem.instance.state == PlayerState.PlayerTurn)
                        DisplayMessage.main.ShowPowerBarText(Random.Range(2, 4));
                    if (_shootValue >= .45f && _shootValue < .7f && ShootSystem.instance.state == PlayerState.PlayerTurn)
                        DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2));
                    if (_shootValue >= .71f && _shootValue <= 1 && ShootSystem.instance.state == PlayerState.PlayerTurn)
                        DisplayMessage.main.ShowPowerBarText(Random.Range(0, 2));

                    #endregion


                    if (ShootSystem.instance.state == PlayerState.PlayerTurn)
                    {
                        ChangeFirstDanceAnimation();
                    }

                    if (ShootSystem.instance.state == PlayerState.PlayerTurn)
                    {
                        GameManager.main.ballAttackValue =
                            ((1 - _shootValue) * 40f) +
                            ((40 * 5) / 100 *
                             ShootSystem.instance.unitPlayer.damageUpgrade
                            ); //Setting the balls shooting value with a normalized range.
                        Unit.SetDamageAfterTurn();
                    }
                    else if (ShootSystem.instance.state == PlayerState.GoalKeeperTurn)
                    {
                        GameManager.main.ballAttackValue =
                            ((1 - _shootValue) * 40f) +
                            ((40 * 8) / 100 * ShootSystem.instance.unitGoalKeeper.damageUpgrade);
                        Unit.SetMaxDamage();
                    }
                    GameManager.main.firstTouch = false;
                    GameManager.main.powerBarIndicatorParent.SetActive(false);
                    transform.DOPause();
                    LevelSetter.main.ActivateCam();
                    GameManager.main.calculationID = 0;

                    break;
            }
        }

        #endregion

        int luck;
    //private static readonly int Taunt = Animator.StringToHash("Taunt");
   // private static readonly int FightIdle = Animator.StringToHash("FightIdle");

        private void ChangeFirstDanceAnimation()
        {
            if (GameManager.main.firstTouch)
                luck = Random.Range(0,20);
               if(luck<5)
                  LevelSetter.main.playerAnim.SetBool("Taunt", true);
               else if(luck>=5&&luck<10)
                  LevelSetter.main.playerAnim.SetBool("FightIdle", true);
                else if(luck>=10&&luck<15)
                  LevelSetter.main.playerAnim.SetBool("Sweep", true);
                else if(luck>=15)
                  LevelSetter.main.playerAnim.SetBool("Plotting", true);

         LevelSetter.main.playerAnim.SetBool("Shoot", true);
         }
    }
}