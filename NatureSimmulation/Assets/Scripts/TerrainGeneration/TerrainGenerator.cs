using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;
    public float heightMulitplier = 2f;

    public float xOffset = 0.3f;
    public float zOffset = 0.3f;

    public Color borderColor;
    
    void Start() {
        InitTerrain();
    }

    [ContextMenu("Generate New Terrain")]
    public void InitTerrain() {
        mesh = new Mesh();
        mesh.name = "Generated Terrain";
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    public int borderTaper = 2;

    void CreateShape() {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z =0; z<= zSize; z++) {
            for (int x = 0; x<=xSize; x++) {
                float y = Mathf.PerlinNoise(x * xOffset, z * zOffset) * heightMulitplier;
                
                // if (x == xSize || x == 0 ||z == zSize || z == 0) { //On border set height to 0 so we have a joining point
                //     y = 0;
                // }
                // else if (x == xSize - 1 || x == 1 ||z == zSize - 1 || z == 1) { //On border set height to 0 so we have a joining point
                //     y = Mathf.Lerp(y, 0, 0.5f);
                // } 
                
                
                if (x < borderTaper  || x > xSize - borderTaper) {
                    y = x < borderTaper ? Mathf.Lerp(0, y, (float) x/borderTaper) : Mathf.Lerp(0, y, (float) (xSize - x) / borderTaper) ;
                }
                
                if (z < borderTaper || z > zSize - borderTaper) {
                    y = z < borderTaper ? Mathf.Lerp(0, y, (float) z/borderTaper) : Mathf.Lerp(0, y, (float) (zSize- z) / borderTaper) ;
                }
                

                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++) {
            for (int x = 0; x < xSize; x++) {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

    }

    void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = borderColor;
        var origin = transform.position;
        var corner1 = new Vector3(origin.x, origin.y, origin.z + zSize);
        var corner2 = new Vector3(origin.x + xSize, origin.y, origin.z + zSize);
        var corner3 = new Vector3(origin.x + xSize, origin.y, origin.z);
        Gizmos.DrawLine(origin, corner1);
        Gizmos.DrawLine(corner1, corner2);
        Gizmos.DrawLine(corner2, corner3);
        Gizmos.DrawLine(corner3, origin);

        
    }

}