using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BallMove : MonoBehaviour
{
    public static BallMove main;
    private Rigidbody rb;

    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(gameObject);
            return;
        }

        main = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.main.shootTheBall) return;
        Movement();
    }

    private void Movement()
    {
        var gameManagerPos = GameManager.main.transformPositionToShoot.position;

        var position = transform.position;
        //rb.velocity = (gameManagerPos - position).normalized * GameManager.main.ballShootPowerValue;

        if (!GameManager.main.ballMoveStop)
        {
            rb.AddForce((gameManagerPos - position).normalized
                        * GameManager.main.ballShootPowerValue, ForceMode.Impulse);
        }
    
    if ((transform.position - gameManagerPos).sqrMagnitude< .06f)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        GameManager.main.ballMoveStop = true;
    }

    if (!((transform.position - gameManagerPos).sqrMagnitude > 5f) || !GameManager.main.camStopFollow) return;
    CameraFollow.main.isNotFollow = true;
    rb.AddForce(Vector3.forward);
}

}