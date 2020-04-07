using System.IO;
using DG.Tweening;
using Managers;
using UnityEngine;
using Path = DG.Tweening.Plugins.Core.PathCore.Path;
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

    #endregion

    #region Movement

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
            GameManager.main.transformPositionToShoot; //The position for the ball to reach, it was taken via players input.

        var position = transform.position; //This is for performance clearity.


        if (!GameManager.main.ballMoveStop
        ) //If this trigger is not set by game manager, ball gets a force to reach the position.
        {
            // rb.AddForce((gameManagerPos - position).normalized *
            //             (GameManager.main.ballShootPowerValue * Time.fixedDeltaTime * 50)
            //     , ForceMode.Impulse); //We set rigidbody force, because without physics we have lag
            //BallShoot.main.Launch(gameManagerPos, Random.Range(5,10),-18);
            
            BallParabollaMove(gameManagerPos, randomPos: new Vector3(Random.Range(-1f,1f),Random.Range(.35f,1.26f),-5));
            CameraFollow.main.StartFieldOfViewChange(); //Triggering camera zoom function.
        }

        if ((transform.position - gameManagerPos).sqrMagnitude < 3 && GameManager.main.ballGoesToHead
            ) //This is for slow motion situations.
            //If player makes perfect hit. Ball will slow down and hit to the head.
        {
            GameManager.main.ballMoveStop = true;
            transform.localScale = new Vector3(.25f, .25f, .25f);
            AttackCompleted();
        }

        if ((transform.position - gameManagerPos).sqrMagnitude < .1f
        ) //This is for camera follow stop and slow motion stop.
        {
            //Bu kısımda can scriptini tetikleyebilirsin
            CameraFollow.main.CinemacHineClose();
            GameManager.main.ballMoveStop = true; //This is the trigger for balls force stop.
            AttackCompleted();
        }

        if (!((transform.position - gameManagerPos).sqrMagnitude > 5f) || !GameManager.main.camStopFollow)
        {
            AttackCompleted();
        }
    }

    private void BallParabollaMove(Vector3 endValue, Vector3 randomPos)
    {
        //var seq =  DOTween.Sequence();
        //seq.Append(transform.DOLocalMove(endValue, 2, false)).Join(transform.DOLocalMove(randomPos, 1, false));
        path[0] = transform.position;
        path[1] = randomPos;
        path[2] = endValue;
        transform.DOPath(path, .75f, pathType).SetEase(Ease.Linear);
    }

    #endregion

    #region Attack
    
    private void AttackCompleted()
    {
        ShootSystem.instance.unitPlayer.currentHP = (int) GameManager.main.ballAttackValue;
        StartCoroutine(ShootSystem.instance.PlayerAttack());
        GameManager.main.shootTheBall = false;
    }
    #endregion
}