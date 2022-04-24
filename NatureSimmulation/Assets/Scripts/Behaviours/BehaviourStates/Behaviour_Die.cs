using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using UnityEngine.PlayerLoop;

public class Behaviour_Die : BehaviourState
{
    private Food food;
    
    public override void Enter() {
        //Animal bits (redundant)
        animal.ToggleDecay(false);
        animal.move = false;
        
        //Create food from myself
        food = gameObject.AddComponent<Food>();
        food.foodType = Food.FoodType.Animal;
        food.feedRadius = 2.5f;
        food.feedRate = 0.09f;

        //Handle mesh when die
        var mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        mesh.material.color = Color.red;

        //Remove components
        animal.enabled = false;
        var movementScripts = GetComponents<SteeringBehaviour>();
        foreach (var script in movementScripts) {
            Destroy(script);
        }
        
        //Disable UI over animal
        GetComponentInChildren<StatUIHandler>().HideStats();
        
        //Removing from food pools
        if (animal.previousState == GetComponent<Behaviour_Consuming>()) {
            var foodSource = GetComponent<Behaviour_LookForFood>().foodFinding;
            foodSource.Exiting(animal);
        }
        if (animal.previousState == GetComponent<Behaviour_Drinking>()) {
            var waterSource = GetComponent<Behaviour_LookForWater>().waterFinding;
            waterSource.Exiting(animal);
        }
    }
    public override void Think() {
        
    }
    public override void Exit() {
        
    }
}
