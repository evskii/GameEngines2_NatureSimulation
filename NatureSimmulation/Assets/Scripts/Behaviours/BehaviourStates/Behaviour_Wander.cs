using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Behaviour_Wander : BehaviourState
{

    public float changeDirectionDistance = 1f;

    private GameObject wanderTarget;
    
    public override void Enter() {
        //Set wander to go
        animal.move = true;
        animal.ToggleDecay(true);
        
        //SET  A NEW TRANSFORM TARGET INSTEAD OF REASSIGNING
        wanderTarget = new GameObject();
        wanderTarget.name = "WanderTarget";
        wanderTarget.transform.position = GetRandomPosition(-40, 40);
        animal.currentTarget = wanderTarget.transform;

        GetComponent<Arrive>().enabled = false;

    }
    public override void Think() {
        //Update what to do while wandering
        
        //When we get to our target, find a new one
        if (Vector3.Distance(transform.position, animal.currentTarget.position) < changeDirectionDistance) {
            wanderTarget.transform.position = GetRandomPosition(-40, 40);
        }
        
    }
    public override void Exit() {
        //Leave wander
        GetComponent<Arrive>().enabled = true;
        Destroy(wanderTarget);
    }
    
    //TODO - Make it pick a random position in a viewcone of the animal so they always move relatively forward

    private Transform GetRandomTransform(float minDistance, float maxDistance) {
        var x = Random.Range(transform.position.x + minDistance, transform.position.x + maxDistance);
        var z = Random.Range(transform.position.z + minDistance, transform.position.z + maxDistance);
        GameObject newObj = new GameObject();
        newObj.transform.position = new Vector3(x, transform.position.y, z);
        return newObj.transform;
    }

    private Vector3 GetRandomPosition(float minDistance, float maxDistance) {
        var x = Random.Range(transform.position.x + minDistance, transform.position.x + maxDistance);
        var z = Random.Range(transform.position.z + minDistance, transform.position.z + maxDistance);
        return new Vector3(x, transform.position.y, z);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        if (wanderTarget != null) {
            Gizmos.DrawWireSphere(wanderTarget.transform.position, 0.5f);
        }
    }
}
