using DG.Tweening;
using Managers;
using UnityEngine;
using Debug = UnityEngine.Debug;
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
    private Sequence _seq;
    private Vector3 _gameManagerPos;
    private Camera _camera;
    private bool _updateStop;

    private static readonly int LegHit = Animator.StringToHash("LegHit");
    private static readonly int MidHit = Animator.StringToHash("MidHit");
    private static readonly int HeadHit = Animator.StringToHash("HeadHit");
    private static readonly int Laugh = Animator.StringToHash("Laugh");

    #endregion

    private void Start()
    { 
        _camera = Camera.main;
        _seq = DOTween.Sequence();
    }

    private void Update()
    {
        
        if ((transform.position - _gameManagerPos).sqrMagnitude < 5 && !_updateStop)
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

        
            CameraControls.main.StartFieldOfViewChangeMainCam();

            BallParabollaMove(_gameManagerPos,
                randomPos: new Vector3(Random.Range(-.45f, .45f), Random.Range(.35f, 1.26f), -5));
        
    }

    private void BallParabollaMove(Vector3 endValue, Vector3 randomPos)
    {
        path[0] = transform.position;
        path[1] = randomPos;
        path[2] = endValue;
        transform.DOLocalPath(path, .75f, pathType).SetEase(Ease.Linear).OnComplete(ForceStop);
    }

    private void ForceStop()
    {
        Debug.Log("ForceStop çalıştı");
        transform.DOKill();
        _camera.GetComponent<CameraControls>().enabled = false;
    }

    private void ChangeState()
    {
        var p1 = GameManager.main.p1sCameraPosition.transform;
        var p2 = GameManager.main.p2sCameraPosition.transform;
        switch (ShootSystem.instance.state)
        {
            case PlayerState.PlayerTurn:
                //seq.Append(_camera.transform.DOLocalMove(p2.position, 1))
                //    .Join(_camera.transform.DORotate(p2.eulerAngles, 1)); 
                _camera.transform.DOLocalMove(p2.position, 5).OnComplete(ChangeStateDelay);
                transform.position = GameManager.main.p2BallsTransform.localPosition;
                //ChangeStateDelay();
                //Debug.Log("Buraya Gelmeyi başardım");
                break;
            case PlayerState.GoalKeeperTurn:
                _seq.Append(_camera.transform.DOLocalMove(p1.position, 1))
                    .Join(_camera.transform.DORotate(p1.eulerAngles, 1)).OnComplete(ChangeStateDelay);
                transform.position = GameManager.main.p1BallsTransform.localPosition;
                break;
        }
    }

    private void ChangeStateDelay()
    {
        Debug.Log("ChangeStateDelay Girdim");
        switch (ShootSystem.instance.state)
        {
            case PlayerState.PlayerTurn:
                ShootSystem.instance.state = PlayerState.GoalKeeperTurn;
                _updateStop = true;
                Debug.Log("PlayerTurndeki Updatei kapattım");
                break;
            case PlayerState.GoalKeeperTurn:
                ShootSystem.instance.state = PlayerState.PlayerTurn;
                Debug.Log("Goalkeeperturndeki Updatei kapattım");
                _updateStop = true;
                break;
        }
    }

    #endregion

    #region Attack

    private void AttackCompleted()
    {
        if (ShootSystem.instance.state == PlayerState.PlayerTurn)
        {
            if (GameManager.main.ballsHitRoad != TransformPosition.Off)
            {
                ShootSystem.instance.unitPlayer.damage = (int) GameManager.main.ballAttackValue;
            }

            ShootSystem.instance.PlayerAttack();
        }
        else if (ShootSystem.instance.state == PlayerState.GoalKeeperTurn)
        {
            {
                if (GameManager.main.ballsHitRoad != TransformPosition.Off)
                {
                    ShootSystem.instance.unitGoalKeeper.damage = (int) GameManager.main.ballAttackValue;
                }

                ShootSystem.instance.GoalKeeperAttack();
            }
        }
    }

    #endregion

    

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

         // if (GameManager.main.ballsHitRoad != TransformPosition.Off)
         // {
         //     AttackCompleted();
         // }
         
    }
    
    public void ChangeKeeper()
    {
        GameManager.main.ballAttackValue = Random.Range(5, 20);
        GameManager.main.transformPositionToShoot = GameManager.main.playerShootPositions[Random.Range(0, 3)].position;
        Debug.Log(GameManager.main.ballAttackValue);
        Movement();
    }
}