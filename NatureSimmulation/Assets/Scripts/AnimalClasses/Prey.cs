using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class Prey : Animal
{
    
    public StateBehaviour currentState;
    public StateBehaviour previousState;
    
    public void ChangeState(StateBehaviour newState) {
        if (currentState != null) {
            currentState.Exit();
        }
        previousState = currentState;
        currentState = newState;
        newState.Enter();
    }

    public void Update() {
        Move();
        if (currentState != null) {
            currentState.Think();
        }
        
    }

}
