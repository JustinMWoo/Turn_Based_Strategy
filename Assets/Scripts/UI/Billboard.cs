﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform cam;

    void LateUpdate()
    {
        cam = Camera.main.transform;
        transform.LookAt(transform.position + cam.forward);
    }
}
