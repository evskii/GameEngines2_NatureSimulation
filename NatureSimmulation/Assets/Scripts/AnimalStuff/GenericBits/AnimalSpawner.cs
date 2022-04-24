using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class AnimalSpawner : MonoBehaviour
{
	public GameObject[] animalsToSpawn;
	public int[] quantitiesToSpawn;

	public int mapWidth;
	public int mapHeight;
	
	private void Start() {
		SpawnAnimals();
	}

	[ContextMenu("Spawn Animals")]
	public void SpawnAnimals() {
		for (int i = 0; i < animalsToSpawn.Length; i++) {
			for (int spawned = 0; spawned < quantitiesToSpawn[i]; spawned++) {
				var spawnPos = new Vector3(Random.Range(-mapWidth/2, mapWidth/2), transform.position.y, Random.Range(-mapHeight/2, mapHeight/2));
				Instantiate(animalsToSpawn[i], spawnPos, Quaternion.identity, transform);
			}
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(transform.position, new Vector3(mapWidth, 5, mapHeight));
	}
}
