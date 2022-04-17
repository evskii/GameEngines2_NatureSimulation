using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Behaviour_LookForFood : BehaviourState
{
    private List<Food> allFood = new List<Food>();
    private Food foodFinding;
    
    public override void Enter() {
        // Debug.Log("Enter LookForFood");
        //Find the closest type of food and set target
        allFood = FindObjectsOfType<Food>().ToList();
        Food closestFood = null;
        foreach (var food in allFood) {
            if (food.foodType != animal.favouredFood) {
                continue;
            }
            if (closestFood != null) {
                if (food.foodType == animal.favouredFood) {
                       var distToNew = Vector3.Distance(transform.position, food.transform.position);
                       var distToOld = Vector3.Distance(transform.position, closestFood.transform.position);
                       if (distToNew < distToOld) {
                           closestFood = food;
                       } 
                }
                
            } else {
                closestFood = food;
            }
        }
        

        if (closestFood == null) {
            Debug.Log("I CANNAE FIND SCRAN");
            animal.StateMachine(GetComponent<Behaviour_Wander>());
        } else {
            foodFinding = closestFood;
            animal.currentTarget = closestFood.transform;
            animal.move = true;
        }
    }

    public override void Think() {
        //Check if we have reacheed the food and transition to eating
        if (Vector3.Distance(transform.position, animal.currentTarget.position) <= foodFinding.feedRadius) {
            foodFinding.Entering(animal);
            animal.StateMachine(GetComponent<Behaviour_Consuming>());
        }
    }
    public override void Exit() {
        //IDK
        if (foodFinding != null) {
            foodFinding.Entering(animal);
        }
        allFood.Clear();
    }

    
}
