using System;
using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Singleton

    public static CameraFollow main;

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
    
    public bool isNotFollow; //This will be the trigger for following;

    public Transform target; //This is the target which we follow
    [SerializeField] private float smoothSpeed = .125f; //The speed for smooth following

    [TextArea] public string text = "You can change the offset for more clear following situations. " +
                                    "I suggest you to change it while playing, then save the values to hierarchy later.";

    public Vector3 offset; //This is the ofset while following. 

    #endregion

    #region CamFollow
    
    private void FixedUpdate()
    {
        if (isNotFollow) return;
        var desiredPosition = target.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
    #endregion

}