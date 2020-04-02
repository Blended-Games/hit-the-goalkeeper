using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // public float comerValue;
    // public float a;
    public float rise;
    public GameObject goalkeeper;
    public Rigidbody rb;
    public bool direction = false;
    private bool speed = false, final = false;
    [SerializeField] private int shootPower; //Lerp için time değişkeni inputtan gelen değer.

    [SerializeField] public Animator cameraAnim;


    public float healt;
    public float startHealt = 100f;
    Vector3 position;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //a = Random.Range(0, 3);
    }

    // Update is called once per frame
    private void Update()
    {
        //if (!GameManager.main.ballMoveStart) return;

        Movement();
        //If ball is kicked by the player, then we will the ball to corresponding position.
        shootPower= (int) GameManager.main.ballShootPowerValue;
    }

    private void Movement()
    {
        position = GameManager.main.transformPositionToShoot.position;

        transform.position =
            Vector3.Lerp(transform.position, goalkeeper.transform.position, shootPower * Time.deltaTime);
        if (transform.position.z < goalkeeper.transform.localPosition.z / 2)
        {
            transform.position -= new Vector3(transform.position.x, -3, position.z) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("goalkeeper")) return;
        else if (other.CompareTag("goalkeeper"))
        {
            cameraAnim.SetBool("goalKeeperTouch", true);
            Bar.instance.heart -= 2;
            Bar.instance.heartBar.fillAmount =
                Bar.instance.heart /
                Bar.instance.startHealt;
            final = true;
            direction = false;
            speed = false;
        }
    }
}