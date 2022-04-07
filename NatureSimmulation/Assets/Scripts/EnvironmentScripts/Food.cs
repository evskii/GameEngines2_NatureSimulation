using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public enum FoodType {
        Grass,
        Tree,
        Animal
    };
    
    public FoodType foodType;
}
