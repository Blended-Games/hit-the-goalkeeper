using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightScript : MonoBehaviour
{
    [SerializeField] private Transform directionalsNextPosAndRot;


    private void Start()
    {
        var transform1 = transform;
        var position = transform1.position;
        var rotation = transform1.rotation;

        position = directionalsNextPosAndRot.position;
        rotation = directionalsNextPosAndRot.rotation;

    }
}