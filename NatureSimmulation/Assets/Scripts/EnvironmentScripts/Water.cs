using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

using Random = UnityEngine.Random;

public class Water : MonoBehaviour
{
    public float feedRadius = 5f;
    [Range(0f, 0.1f)] public float feedRate = 0.01f;

    private List<Animal> animalsInArea = new List<Animal>();
    private List<Animal> animalsToRemove = new List<Animal>();

    private void Start() {
        DrawWater();
    }

    public int points;
    public float min, max;
    
    [ContextMenu("TEST GENERATE")]
    public void DrawWater() {
        //Generate points on circle
        Vector3[] circlePoints = new Vector3[points];
        float theta = Mathf.Deg2Rad * (360 / points );
        for (int i = 0; i < circlePoints.Length; i++) {
            float randomDist = Random.Range(min, max);
            float x = randomDist * Mathf.Cos(theta * i);
            float z = randomDist * Mathf.Sin(theta * i);
            circlePoints[i] = new Vector3(x, transform.position.y, z);

            // GameObject newObj = new GameObject();
            // newObj.transform.parent = transform;
            // newObj.transform.localPosition = new Vector3(x, transform.position.y, z);
            // newObj.name = i.ToString();
        }
        
        //Draw mesh
        MeshFilter filter = GetComponent<MeshFilter>(); 
        Mesh mesh = new Mesh();
        mesh.name = "Water"; //Name our mesh
        filter.mesh = mesh; //Assign the mesh to the filter
        
        mesh.vertices = circlePoints; //Set the meshes vertices to be your new array
        int[] tris = new int[circlePoints.Length * 3]; //New array that holds your tris (3 times the length of your vert array)
        int baseNum = 0; //Used to set the index of each vert for the tris
        for(int i = 0; i < tris.Length; i += 3) { //This loop adds the index of each vert for the tris
            //Triangle is (x, x+1, 0) so it is two of the verts at the exterior and then the center of the shape you make
            tris[i] = baseNum;
            baseNum++; 
            if (baseNum > circlePoints.Length-1) {
                baseNum = 1;
            }
            tris[i + 1] = baseNum;
            tris[i + 2] = 0;
        }
        mesh.triangles = tris;//Set the meshes tris to the array you justn filled
        

        //Create a material
        Material material = new Material(Shader.Find("Particles/Standard Unlit"));
        material.name = "WaterMaterial";
        material.color = Color.blue;
        GetComponent<MeshRenderer>().material = material; //Set mesh renderers material to the one created
        transform.Translate(0, -0.05f, 0); //Solves a Z indexing issue
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
