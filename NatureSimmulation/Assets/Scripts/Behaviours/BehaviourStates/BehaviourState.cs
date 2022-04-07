using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BehaviourState : MonoBehaviour
{
    
    [HideInInspector]
    public Animal animal;

    public void Awake()
    {
        animal = GetComponent<Animal>();
    }
    
    public abstract void Enter();

    public abstract void Think();

    public abstract void Exit();
}
