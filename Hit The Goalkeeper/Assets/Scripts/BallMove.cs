using System;
using DG.Tweening;
using Managers;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using System.Collections;
using Accessables;

public class BallMove : MonoBehaviour
{
    #region Singleton

    public static BallMove main;

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

    private Rigidbody rb;
    private Vector3[] path = new Vector3[3];
    private PathType pathType = PathType.CatmullRom;
    private bool ballMoveAnimClose;

    private static readonly int LegHit = Animator.StringToHash("LegHit");
    private static readonly int MidHit = Animator.StringToHash("MidHit");
    private static readonly int HeadHit = Animator.StringToHash("HeadHit");
    private static readonly int Laugh = Animator.StringToHash("Laugh");
    private Camera _camera;

    #endregion

    #region Movement

    private void Start()
    {
        _camera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % 3 == 0)
        {
            if (!GameManager.main.shootTheBall)
                return; //If player enters the inputs, global manager will set the trigger for movement.
            Movement();
        }
    }

    //This is the script for ball movement, Ball will move to goalkeepers selected positions.
    private void Movement()
    {
        var gameManagerPos =
            GameManager.main
                .transformPositionToShoot; //The position for the ball to reach, it was taken via players input.

        var position = transform.position; //This is for performance clearity.


        if (!ballMoveAnimClose
        ) //If this trigger is not set by game manager, ball gets a force to reach the position.
        {
            CameraControls.main.StartFieldOfViewChangeMainCam();

            BallParabollaMove(gameManagerPos,
                randomPos: new Vector3(Random.Range(-.45f, .45f), Random.Range(.35f, 1.26f), -5));
            ballMoveAnimClose = true;
        }

        if (Vector3.Distance(transform.position, gameManagerPos) <= 1f)
        {
            Debug.Log("Veya Bu");
            switch (ShootSystem.instance.state)
            {
                case PlayerState.PlayerTurn:
                {
                    switch (GameManager.main.ballsHitRoad)
                    {
                        case TransformPosition.Head:
                            GameManager.main.goalKeeperAnim.SetBool(HeadHit, true);
                            break;
                        case TransformPosition.Spine:
                            GameManager.main.goalKeeperAnim.SetBool(MidHit, true);
                            break;
                        case TransformPosition.Leg:
                            GameManager.main.goalKeeperAnim.SetBool(LegHit, true);
                            break;
                        case TransformPosition.Off:
                            GameManager.main.goalKeeperAnim.SetBool(Laugh, true);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                    break;
                case PlayerState.GoalKeeperTurn:
                {
                    switch (GameManager.main.ballsHitRoad)
                    {
                        case TransformPosition.Head:
                            GameManager.main.playerAnim.SetBool(HeadHit, true);
                            break;
                        case TransformPosition.Spine:
                            GameManager.main.playerAnim.SetBool(MidHit, true);
                            break;
                        case TransformPosition.Leg:
                            GameManager.main.playerAnim.SetBool(LegHit, true);
                            break;
                        case TransformPosition.Off:
                            GameManager.main.playerAnim.SetBool(Laugh, true);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                    break;
                case PlayerState.Won:
                    break;
                case PlayerState.Lost:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }

            ChangeState();
            AttackCompleted();
        }
        
    }

    private void BallParabollaMove(Vector3 endValue, Vector3 randomPos)
    {
        path[0] = transform.position;
        path[1] = randomPos;
        path[2] = endValue;
        transform.DOLocalPath(path, .75f, pathType, PathMode.Full3D).SetEase(Ease.Linear).OnComplete(ForceStop);
    }

    private void ForceStop()
    {
        transform.DOKill();
        _camera.GetComponent<CameraControls>().enabled = false;
        GameManager.main.shootTheBall = false;
    }

    private void ChangeState()
    {
        var p1 = GameManager.main.p1sCameraPosition.transform;
        var p2 = GameManager.main.p2sCameraPosition.transform;
        switch (ShootSystem.instance.state)
        {
            case PlayerState.PlayerTurn:
                var seq = DOTween.Sequence();
                seq.Append(_camera.transform.DOLocalMove(p2.position, 1))
                    .Join(_camera.transform.DORotate(p2.eulerAngles, 1)).OnComplete(TurnChange);
                transform.position = GameManager.main.p2BallsTransform.localPosition;

                break;
            case PlayerState.GoalKeeperTurn:
                var seq2 = DOTween.Sequence();
                seq2.Append(_camera.transform.DOLocalMove(p1.position, 1))
                    .Join(_camera.transform.DORotate(p1.eulerAngles, 1)).OnComplete(TurnChange);
                transform.position = GameManager.main.p1BallsTransform.localPosition;
                break;
            case PlayerState.Won:
                break;
            case PlayerState.Lost:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void TurnChange()
    {
        switch (ShootSystem.instance.state)
        {
            case PlayerState.PlayerTurn:
                ShootSystem.instance.state = PlayerState.GoalKeeperTurn;
                break;
            case PlayerState.GoalKeeperTurn:
                ShootSystem.instance.state = PlayerState.PlayerTurn;
                break;
            case PlayerState.Won:
                break;
            case PlayerState.Lost:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion

    #region Attack

    private void AttackCompleted()
    {
        Debug.Log("eeeee");
        if (ShootSystem.instance.state == PlayerState.PlayerTurn)
        {
            if (GameManager.main.ballsHitRoad != TransformPosition.Off)
            {
                ShootSystem.instance.unitPlayer.currentHP = (int) GameManager.main.ballAttackValue;
            }

            //StartCoroutine(ShootSystem.instance.PlayerAttack());
            ShootSystem.instance.PlayerAttack();
        }
        else if (ShootSystem.instance.state == PlayerState.GoalKeeperTurn)
        {
            {
                if (GameManager.main.ballsHitRoad != TransformPosition.Off)
                {
                    ShootSystem.instance.unitGoalKeeper.currentHP = (int) GameManager.main.ballAttackValue;
                }

                //StartCoroutine(ShootSystem.instance.GoalKeeperAttack());
                ShootSystem.instance.GoalKeeperAttack();
            }
        }
    }

    #endregion

    public void ChangeKeeper()
    {
        GameManager.main.ballAttackValue = Random.Range(5, 20);
        GameManager.main.transformPositionToShoot = GameManager.main.playerShootPositions[Random.Range(0, 3)].position;
        Debug.Log(GameManager.main.ballAttackValue);
        //_camera.GetComponent<CameraControls>().enabled = true;
        Movement();
    }
    
}