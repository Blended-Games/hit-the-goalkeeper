using System.Collections;
using Accessables;
using DG.Tweening;
using GUI2;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class BallMove : MonoBehaviour
{
    #region Singleton

    public static BallMove main;
    
      //  private static readonly int
           // Shoot = Animator.StringToHash("Shoot");

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

    private Vector3[] path = new Vector3[3];
    private PathType pathType = PathType.CatmullRom;
    private bool _close;
    private Vector3 _gameManagerPos;
    public Camera _camera; //shootSystemAndValue için .
    [FormerlySerializedAs("_updateStop")] public bool updateStop;
    #endregion

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if ((transform.position - _gameManagerPos).sqrMagnitude < 5 && !updateStop)
            CameraControls.main.CameraGetCloser();

        if ((transform.position - _gameManagerPos).sqrMagnitude < 1 && !updateStop)
        {
            AnimStateChanger();
        }
    }

    #region Movement

    //This is the script for ball movement, Ball will move to goalkeepers selected positions.
    public void Movement()
    {
        _gameManagerPos =
            LevelSetter.main
                .transformPositionToShoot; //The position for the ball to reach, it was taken via players input.
        BallParabollaMove(_gameManagerPos,
            randomPos: new Vector3(GameManager.main.ballCurveValue, Random.Range(.35f, 1.26f), -5));
        GameManager.main.shootParticleObj.SetActive(true);
        DisplayMessage.main.powerBarText.enabled = false;
        GameManager.main.powerBarIndicatorParent.SetActive(false);
    }

    private void BallParabollaMove(Vector3 endValue, Vector3 randomPos)
    {
        path[0] = transform.position;
        path[1] = randomPos;
        path[2] = endValue;
        transform.DOLocalPath(path, .65f, pathType).SetEase(Ease.Flash).OnComplete(CameraFollowStop);
    }


    private void ChangeState()
    {
        var p1 = LevelSetter.main.p1sCameraPosition.transform;
        var p2 = LevelSetter.main.p2sCameraPosition.transform;


        StartCoroutine(ChangeStateDelayCoroutine());
        switch (ShootSystem.instance.state)
        {
            case PlayerState.PlayerTurn:

                DoTweenController.SeqMoveRotateCallBack(_camera.transform, p2.position, p2.eulerAngles, 2,
                    ChangeStateDelay, Ease.Flash);
                GameManager.main.faceParticleObj.SetActive(true);

                break;
            case PlayerState.GoalKeeperTurn:

                DoTweenController.SeqMoveRotateCallBack(_camera.transform, p1.position, p1.eulerAngles, 2,
                    ChangeStateDelay, Ease.Flash);

                GameManager.main.faceParticleObj.SetActive(true);
                break;
        }
    }

    private void ChangeStateDelay()
    {
        switch (ShootSystem.instance.state)
        {
            case PlayerState.PlayerTurn:
                var transform1 = transform;
                transform1.position = LevelSetter.main.p2BallsTransform.localPosition;
                transform1.rotation = LevelSetter.main.p2BallsTransform.localRotation;

                LevelSetter.main.p1.transform.position = LevelSetter.main.p1Pos.position;
                LevelSetter.main.p1.transform.rotation = LevelSetter.main.p1Pos.rotation;
                BallGetsFixedSize();
                break;
            case PlayerState.GoalKeeperTurn:
                var transform2 = transform;
                transform2.position = LevelSetter.main.p1BallsTransform.localPosition;
                transform2.rotation = LevelSetter.main.p1BallsTransform.localRotation;
                LevelSetter.main.p2.transform.position = LevelSetter.main.p2Pos.position;
                LevelSetter.main.p2.transform.rotation = LevelSetter.main.p2Pos.rotation;
                BallGetsFixedSize();
                break;
        }
    }

    private IEnumerator ChangeStateDelayCoroutine()
    {
        if (ShootSystem.instance.state == PlayerState.PlayerTurn)
        {
            if (GameManager.main.ballsHitRoad != TransformPosition.Off)
            {
                ShootSystem.instance.unitPlayer.damage = (int) GameManager.main.ballAttackValue;
                ShootSystem.instance.PanelHealthDisplayPlayer();
            }
        }
        else if (ShootSystem.instance.state == PlayerState.GoalKeeperTurn)
        {
            {
                if (GameManager.main.ballsHitRoad != TransformPosition.Off)
                {
                    ShootSystem.instance.unitGoalKeeper.damage = (int) GameManager.main.ballAttackValue;
                    ShootSystem.instance.PanelHealthDisplayGoalkeeper();
                }
            }
        }

        IsDeadControl();
        yield return new WaitForSeconds(5);
        switch (ShootSystem.instance.state)
        {
            case PlayerState.PlayerTurn:
                AttackCompleted();
                break;
            case PlayerState.GoalKeeperTurn:
                AttackCompleted();
                break;
        }
    }

    #endregion

    #region Attack

    private void AttackCompleted()
    {
        if (ShootSystem.instance.state == PlayerState.PlayerTurn)
        {
            ShootSystem.instance.PlayerAttack();
        }
        else if (ShootSystem.instance.state == PlayerState.GoalKeeperTurn)
        {
            ShootSystem.instance.GoalKeeperAttack();
        }
    }

    #endregion

    #region AnimStateChanger

    private void AnimStateChanger()
    {
        switch (ShootSystem.instance.state)
        {
            case PlayerState.PlayerTurn:
            {
                switch (GameManager.main.ballsHitRoad)
                {
                    case TransformPosition.Head:
                        LevelSetter.main.goalKeeperAnim.SetTrigger("HeadHit");
                        Vibrations.VibrationHeavy();
                        BallGetsSmaller();
                        break;
                    case TransformPosition.Spine:
                        Vibrations.VibrationLight();
                        LevelSetter.main.goalKeeperAnim.SetTrigger("MidHit");
                        break;
                    case TransformPosition.Leg:
                        Vibrations.VibrationLight();
                        LevelSetter.main.goalKeeperAnim.SetTrigger("LegHit");
                        break;
                    case TransformPosition.Off:
                        LevelSetter.main.goalKeeperAnim.SetTrigger("Off");
                        break;
                }
                if (ShootSystem.instance.unitGoalKeeper.maxHP - GameManager.main.ballAttackValue <
                    ((ShootSystem.instance.unitGoalKeeper.firstHP * 70) / 100))

                {
                    LevelSetter.main.goalKeeperAnim.SetLayerWeight(1, 1);
                    LevelSetter.main.renderTextureMaterials[1].mainTexture = LevelSetter.main.p2Textures[1];
                }

                if (ShootSystem.instance.unitGoalKeeper.maxHP - GameManager.main.ballAttackValue <
                    ((ShootSystem.instance.unitGoalKeeper.firstHP * 50) / 100))
                {
                    LevelSetter.main.goalKeeperAnim.SetLayerWeight(2, 1);
                    LevelSetter.main.renderTextureMaterials[1].mainTexture = LevelSetter.main.p2Textures[2];
                }

                if (ShootSystem.instance.unitGoalKeeper.maxHP - GameManager.main.ballAttackValue <
                    ((ShootSystem.instance.unitGoalKeeper.firstHP * 30) / 100))
                {
                    LevelSetter.main.goalKeeperAnim.SetLayerWeight(3, 1);
                    LevelSetter.main.renderTextureMaterials[1].mainTexture = LevelSetter.main.p2Textures[3];
                }
            }
                break;
            case PlayerState.GoalKeeperTurn:
            {
                switch (GameManager.main.ballsHitRoad)
                {
                    case TransformPosition.Head:
                        LevelSetter.main.playerAnim.SetTrigger("HeadHit");
                        Vibrations.VibrationHeavy();
                        BallGetsSmaller();
                        break;
                    case TransformPosition.Spine:
                        Vibrations.VibrationLight();
                        LevelSetter.main.playerAnim.SetTrigger("MidHit");
                        break;
                    case TransformPosition.Leg:
                        Vibrations.VibrationLight();
                        LevelSetter.main.playerAnim.SetTrigger("LegHit");
                        break;
                    case TransformPosition.Off:
                        LevelSetter.main.playerAnim.SetTrigger("Laugh");
                        break;
                }
                if (ShootSystem.instance.unitPlayer.maxHP - GameManager.main.ballAttackValue <
                    ((ShootSystem.instance.unitPlayer.firstHP * 70) / 100))
                {
                    LevelSetter.main.playerAnim.SetLayerWeight(1, 1);
                    LevelSetter.main.renderTextureMaterials[0].mainTexture = LevelSetter.main.p1Textures[1];
                }

                if (ShootSystem.instance.unitPlayer.maxHP - GameManager.main.ballAttackValue <
                    ((ShootSystem.instance.unitPlayer.firstHP* 50) / 100))
                {
                    LevelSetter.main.playerAnim.SetLayerWeight(2, 1);
                    LevelSetter.main.renderTextureMaterials[0].mainTexture = LevelSetter.main.p1Textures[2];
                }

                if (ShootSystem.instance.unitPlayer.maxHP - GameManager.main.ballAttackValue <
                    ((ShootSystem.instance.unitPlayer.firstHP * 30) / 100))
                {
                    LevelSetter.main.playerAnim.SetLayerWeight(3, 1);
                    LevelSetter.main.renderTextureMaterials[0].mainTexture = LevelSetter.main.p1Textures[3];
                }
            }
                break;
        }


        ChangeState();
        updateStop = true;
    }

    #endregion

    public static void ChangeKeeper()
    {
        var random = Random.Range(0, .99f);
       
        PowerBarIndicator.main.CalculateShotValue(random);
        LevelSetter.main.goalKeeperAnim.SetTrigger("Shoot");
        LevelSetter.main.ActivateCam();
    }

    private void CameraFollowStop()
    {
        transform.DOKill();
        _camera.GetComponent<CameraControls>().enabled = false;
        GetComponent<Rigidbody>().AddForce(Vector3.forward * (10000 * Time.fixedDeltaTime), ForceMode.Force);
    }

    private static void PlayerAttackElse()
    {
        var transform1 = LevelSetter.main.p2Pos.transform;
        var transform2 = LevelSetter.main.p2.transform;
        transform2.position = transform1.position;
        transform2.rotation = transform1.rotation;
        GameManager.main.ballAttackValue = 0;
        GameManager.main.ballCurveValue = 0;
        GameManager.main.firstTouch = false;
    }

    private static void GoalKeeperAttackElse()
    {
        var transform1 = LevelSetter.main.p1Pos.transform;
        LevelSetter.main.p1.transform.position = transform1.position;
        LevelSetter.main.p1.transform.rotation = transform1.rotation;
        GameManager.main.firstTouch = true;
    }

    private void IsDeadControl()
    {
        switch (ShootSystem.instance.state)
        {
            case PlayerState.PlayerTurn:
            {
                var isDead2 = ShootSystem.instance.unitGoalKeeper.TakeDamage(ShootSystem.instance.unitPlayer.damage);
                ShootSystem.instance.unitPlayer.damage = 0;
                if (isDead2)
                {
                    ShootSystem.instance.state = PlayerState.Won;
                    ShootSystem.instance.EndShoot();
                }
                else
                    PlayerAttackElse();

                break;
            }
            case PlayerState.GoalKeeperTurn:
            {
                var isDead = ShootSystem.instance.unitPlayer.TakeDamage(ShootSystem.instance.unitGoalKeeper.damage);
                ShootSystem.instance.unitGoalKeeper.damage = 0;
                if (isDead)
                {
                    ShootSystem.instance.state = PlayerState.Lost;
                    ShootSystem.instance.EndShoot();
                }
                else
                    GoalKeeperAttackElse();

                break;
            }
        }
    }

    private void BallGetsSmaller()
    {
        transform.localScale = new Vector3(.5f, .5f, .5f);
    }

    private void BallGetsFixedSize()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    
}