using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpin : MonoBehaviour
{
    public float spinSpeed;

    private void Update() {
        transform.Rotate(new Vector3(0, spinSpeed * Time.deltaTime, 0));
    }
}
