using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    public enum FoodType {
        Grass,
        Tree,
        Animal
    };
    
    public FoodType foodType;
    public float feedRadius = 2.5f;
    [Range(0f, 0.1f)] public float feedRate = 0.01f;

    private List<Animal> animalsInArea = new List<Animal>();
    private List<Animal> animalsToRemove = new List<Animal>();

    public GameObject[] models;

    private void Start() {

        if (models != null) {
            Instantiate(models[Random.Range(0, models.Length)], transform);
        }
    }
    
    

    private void Update() {
        if (animalsInArea.Count > 0) {
            foreach (var animal in animalsInArea) {
                if (Vector3.Distance(animal.transform.position, transform.position) > feedRadius + 0.5f) {
                    animalsToRemove.Add(animal);
                    continue;
                } 
                animal.Eating(feedRate);
                Debug.Log("FEEDING");
            }
            foreach (var animal in animalsToRemove) {
                Exiting(animal);
            }
            animalsToRemove.Clear();
        }
    }

    public void Entering(Animal animalEntering) {
        if (!animalsInArea.Contains(animalEntering)) {
            animalsInArea.Add(animalEntering);
            Debug.Log("ANIMAL ENTER");
        }
    }

    public void Exiting(Animal aniamlExiting) {
        animalsInArea.Remove(aniamlExiting);
        Debug.Log("ANIMAL LEAVE");
    }

    private void OnDrawGizmos() {
        if (foodType == FoodType.Animal) {
            Gizmos.color = Color.red;
        }else if (foodType == FoodType.Grass) {
            Gizmos.color = Color.green;
        }else if (foodType == FoodType.Tree) {
            Gizmos.color = new Color(0.58f, 0.29f, 0f, 1f);
        }

        if (animalsInArea.Count > 0) {
            Gizmos.color = Color.blue;
        }

        Gizmos.DrawWireSphere(transform.position, feedRadius);
    }
}
