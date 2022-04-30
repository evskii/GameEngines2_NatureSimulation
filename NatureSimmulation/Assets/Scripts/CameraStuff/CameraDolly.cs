using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDolly : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 direction;

    private void Update() {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
