using System;
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

    public bool isNotFollow; //This will be the trigger for following;
    
    [SerializeField] private Transform target;

    [SerializeField] private float smoothSpeed = .125f;
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        if (isNotFollow) return;
        var desiredPosition = target.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        transform.LookAt(target);
    }
}
