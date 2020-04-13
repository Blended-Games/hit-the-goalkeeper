using System.Collections;
using Accessables;
using DG.Tweening;
using GUI;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

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

    private Vector3[] path = new Vector3[3];
    private PathType pathType = PathType.CatmullRom;
    private bool _close;
    private Vector3 _gameManagerPos;
    private Camera _camera;
    [FormerlySerializedAs("_updateStop")] public bool updateStop;

    private static readonly int LegHit = Animator.StringToHash("LegHit");
    private static readonly int MidHit = Animator.StringToHash("MidHit");
    private static readonly int HeadHit = Animator.StringToHash("HeadHit");
    private static readonly int Laugh = Animator.StringToHash("Laugh");
    private static readonly int Shoot = Animator.StringToHash("Shoot");

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
            GameManager.main
                .transformPositionToShoot; //The position for the ball to reach, it was taken via players input.
        BallParabollaMove(_gameManagerPos,
            randomPos: new Vector3(GameManager.main.ballCurveValue, Random.Range(.35f, 1.26f), -5));
   GameManager.main.shootParticleObj.SetActive(true);
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
        var p1 = GameManager.main.p1sCameraPosition.transform;
        var p2 = GameManager.main.p2sCameraPosition.transform;


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
                transform.position = GameManager.main.p2BallsTransform.localPosition;
                GameManager.main.p1.transform.position = GameManager.main.p1Pos.position;
                GameManager.main.p1.transform.rotation = GameManager.main.p1Pos.rotation;
                BallGetsFixedSize();
                break;
            case PlayerState.GoalKeeperTurn:
                transform.position = GameManager.main.p1BallsTransform.localPosition;
                GameManager.main.p2.transform.position = GameManager.main.p2Pos.position;
                GameManager.main.p2.transform.rotation = GameManager.main.p2Pos.rotation;
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
                        GameManager.main.goalKeeperAnim.SetBool(HeadHit, true);
                        if (Unit.main.maxHP >= 75 && Unit.main.maxHP < 100)
                            GameManager.main.goalKeeperAnim.SetLayerWeight(1, 1);
                        else if (Unit.main.maxHP >= 40 && Unit.main.maxHP < 75)
                            GameManager.main.goalKeeperAnim.SetLayerWeight(2, 1);
                        else if(Unit.main.maxHP < 40)
                            GameManager.main.goalKeeperAnim.SetLayerWeight(3, 1);
                        BallGetsSmaller();
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
                }
            }
                break;
            case PlayerState.GoalKeeperTurn:
            {
                switch (GameManager.main.ballsHitRoad)
                {
                    case TransformPosition.Head:
                        GameManager.main.playerAnim.SetBool(HeadHit, true);
                        if (Unit.main.maxHP >= 75 && Unit.main.maxHP < 100)
                            GameManager.main.playerAnim.SetLayerWeight(1, 1);
                        else if (Unit.main.maxHP >= 40 && Unit.main.maxHP < 75)
                            GameManager.main.playerAnim.SetLayerWeight(2, 1);
                        else if(Unit.main.maxHP < 40)
                            GameManager.main.playerAnim.SetLayerWeight(3, 1);
                        BallGetsSmaller();
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
                }
            }
                break;
        }


        ChangeState();
        updateStop = true;
    }

    #endregion

    public void ChangeKeeper()
    {
        var random = Random.Range(0, .99f);
        var calculationID = 0;
        PowerBarIndicator.main.CalculateShotValue(random, calculationID);
        calculationID++;
        PowerBarIndicator.main.CalculateShotValue(random, calculationID);
        GameManager.main.goalKeeperAnim.SetBool(Shoot, true);
        GameManager.main.ActivateCam();
    }

    private void CameraFollowStop()
    {
        
        transform.DOKill();
        _camera.GetComponent<CameraControls>().enabled = false;
        GetComponent<Rigidbody>().AddForce(Vector3.forward * (10000 * Time.fixedDeltaTime), ForceMode.Force);
    }

    private void BallGetsSmaller()
    {
        transform.localScale = new Vector3(.08f, .08f, .08f);
    }

    private void BallGetsFixedSize()
    {
        transform.localScale = new Vector3(.15f, .15f, .15f);
    }
}