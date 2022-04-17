using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

public class Water : MonoBehaviour
{
    public float feedRadius = 5f;
    [Range(0f, 0.1f)] public float feedRate = 0.01f;

    private List<Animal> animalsInArea = new List<Animal>();
    private List<Animal> animalsToRemove = new List<Animal>();

    private void Start() {
        //Create water mesh
        GameObject waterMesh = GameObject.CreatePrimitive(PrimitiveType.Plane);
        waterMesh.transform.parent = this.gameObject.transform;
        waterMesh.transform.position = this.transform.position;
        Material waterMat = new Material(Shader.Find("Specular"));
        waterMat.color = Color.blue;
        waterMesh.GetComponent<MeshRenderer>().material = waterMat;
    }

    private void Update() {
        if (animalsInArea.Count > 0) {
            foreach (var animal in animalsInArea) {
                if (Vector3.Distance(animal.transform.position, transform.position) > feedRadius + 0.5f) {
                    animalsToRemove.Add(animal);
                    continue;
                } 
                animal.Drinking(feedRate);
                // Debug.Log("DRINKING");
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
            // Debug.Log("ANIMAL ENTER");
        }
    }

    public void Exiting(Animal aniamlExiting) {
        animalsInArea.Remove(aniamlExiting);
        // Debug.Log("ANIMAL LEAVE");
    }


    private void OnDrawGizmos() { 
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, feedRadius);
    }
}
