﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start() => GetComponent<Canvas>().worldCamera = Camera.main;
}
