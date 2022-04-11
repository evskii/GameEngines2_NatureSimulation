using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Wander : BehaviourState
{
    public override void Enter() {
        //Set wander to go
        animal.move = true;
        animal.currentTarget.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
    }
    public override void Think() {
        //Update what to do while wandering
    }
    public override void Exit() {
        //Leave wander
    }
}
