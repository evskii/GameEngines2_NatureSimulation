using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[RequireComponent(typeof(Behaviour_Drinking))]
public class Behaviour_LookForWater : BehaviourState
{
    private List<Water> allWater = new List<Water>();
    public Water waterFinding;
    
    public override void Enter() {
        Debug.Log("Enter LookForFood");
        //Find the closest type of food and set target
        allWater = FindObjectsOfType<Water>().ToList();
        Water closestWater = null;
        foreach (var water in allWater) {
            if (closestWater != null) {
                   var distToNew = Vector3.Distance(transform.position, water.transform.position);
                   var distToOld = Vector3.Distance(transform.position, closestWater.transform.position);
                   if (distToNew < distToOld) {
                       closestWater = water;
                   } 
                
            } else {
                closestWater = water;
            }
        }
        waterFinding = closestWater;
        animal.currentTarget = closestWater.transform;
        animal.move = true;
    }

    public override void Think() {
        //Check if we have reacheed the food and transition to eating
        if (Vector3.Distance(transform.position, animal.currentTarget.position) <= waterFinding.feedRadius) {
            waterFinding.Entering(animal);
            animal.StateMachine(GetComponent<Behaviour_Drinking>());
        }
    }
    public override void Exit() {
        //IDK
        waterFinding.Entering(animal);
        allWater.Clear();
    }
    
}
