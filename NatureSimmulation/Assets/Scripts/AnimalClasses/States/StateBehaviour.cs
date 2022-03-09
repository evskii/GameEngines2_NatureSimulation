using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBehaviour : MonoBehaviour
{
    public abstract void Enter();
    public abstract void Think();
    public abstract void Exit();
}
