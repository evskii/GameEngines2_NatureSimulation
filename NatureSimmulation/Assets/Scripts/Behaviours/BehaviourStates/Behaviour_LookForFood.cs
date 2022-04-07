using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Behaviour_LookForFood : BehaviourState
{
    private List<Food> allFood = new List<Food>();
    
    public override void Enter() {
        Debug.Log("Enter LookForFood");
        //Find the closest type of food and set target
        allFood = FindObjectsOfType<Food>().ToList();
        Food closestFood = null;
        foreach (var food in allFood) {
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
        animal.currentTarget = closestFood.transform;
        animal.move = true;
    }

    public float eatingDistance = 0.5f;
    public override void Think() {
        //Check if we have reacheed the food and transition to eating
        if (Vector3.Distance(transform.position, animal.currentTarget.position) <= eatingDistance) {
            animal.StateMachine(GetComponent<Behaviour_Idle>());
        }
    }
    public override void Exit() {
        //IDK
        allFood.Clear();
    }

    private void OnDrawGizmos() {
        //Display all food
        if (allFood.Count > 0) {
            foreach (var food in allFood) {
                if (food.foodType == Food.FoodType.Grass) {
                    Gizmos.color = Color.green;
                }else if (food.foodType == Food.FoodType.Tree) {
                    Gizmos.color = Color.grey;
                } else {
                    Gizmos.color = Color.magenta;
                }
                Gizmos.DrawSphere(food.transform.position, 1f);
            }
        }
        
    }
}
