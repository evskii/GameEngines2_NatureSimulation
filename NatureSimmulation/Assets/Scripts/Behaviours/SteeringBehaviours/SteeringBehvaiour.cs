using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animal))]
public abstract class SteeringBehaviour : MonoBehaviour
{
    public float weight = 1.0f;
    public Vector3 force;

    [HideInInspector]
    public Animal animal;

    public void Awake()
    {
        animal = GetComponent<Animal>();
    }

    public abstract Vector3 Calculate();
}
