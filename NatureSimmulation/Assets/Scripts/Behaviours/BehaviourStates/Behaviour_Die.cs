using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;

public class Behaviour_Die : BehaviourState
{
    private Food food;
    
    public override void Enter() {
        animal.ToggleDecay(false);
        animal.move = false;
        
        food = gameObject.AddComponent<Food>();
        food.foodType = Food.FoodType.Animal;
        food.feedRadius = 2.5f;
        food.feedRadius = 0.75f;

        var mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        mesh.material.color = Color.red;

    }
    public override void Think() {
        
    }
    public override void Exit() {
        
    }
}
