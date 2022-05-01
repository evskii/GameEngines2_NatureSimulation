using System;
using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;

using Random = UnityEngine.Random;

public class Behaviour_Wander : BehaviourState
{

    public float changeDirectionDistance = 1f;

    private GameObject wanderTarget;
    
    public override void Enter() {
        animal.move = true;
        animal.ToggleDecay(true);
        
        wanderTarget = new GameObject();
        wanderTarget.name = "WanderTarget";
        wanderTarget.transform.position = GetRandomPosition(-40, 40);
        animal.currentTarget = wanderTarget.transform;

        GetComponent<Arrive>().enabled = false; //disable arrive so we have a continuous smooth movement
    }
    public override void Think() {
        //When we get to our target, find a new one
        if (Vector3.Distance(transform.position, animal.currentTarget.position) < changeDirectionDistance) {
            var finalPos = GetRandomPosition(-40, 40);
            var temp = FindObjectOfType<AnimalSpawner>();
            wanderTarget.transform.position = finalPos;
        }
        
    }
    public override void Exit() {
        GetComponent<Arrive>().enabled = true;
        Destroy(wanderTarget);
    }
    
    private Vector3 GetRandomPosition(float minDistance, float maxDistance) {
        AnimalSpawner spawner = FindObjectOfType<AnimalSpawner>();
        var x = Random.Range(transform.position.x + minDistance, transform.position.x + maxDistance);
        x = Mathf.Clamp(x, -spawner.mapWidth / 2, spawner.mapWidth / 2);
        var z = Random.Range(transform.position.z + minDistance, transform.position.z + maxDistance);
        z = Mathf.Clamp(z, -spawner.mapHeight / 2, spawner.mapHeight / 2);
        var rawPos = new Vector3(x, transform.position.y, z);
        var finalPos = Vector3.zero;
        //Raycast to make sure its not under ground
        var raycastPos = new Vector3(rawPos.x, rawPos.y + 100, rawPos.z);
        RaycastHit hit;
        if (Physics.Raycast(raycastPos, Vector3.down, out hit, Mathf.Infinity)) {
            finalPos = hit.point;
        } else {
            finalPos = rawPos;
        }

        return finalPos;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        if (wanderTarget != null) {
            Gizmos.DrawWireSphere(wanderTarget.transform.position, 0.5f);
        }
    }
}
