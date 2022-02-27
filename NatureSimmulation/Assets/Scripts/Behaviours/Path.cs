using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
	public List<Vector3> waypoints = new List<Vector3>();
	public int current = 0;
	public bool isLooped = true;

	public void Awake() {
		PopulatePath();
	}
	
	private void PopulatePath() {
		waypoints.Clear();
		foreach(Transform child in transform.GetComponentsInChildren<Transform>()) {
			if (child != transform) {
				waypoints.Add(child.position);
			}
		}
	}

	public Vector3 Next() {
		return waypoints[current];
	}

	public bool IsLast() {
		return (current == waypoints.Count - 1);
	}
 
	public void AdvanceToNext() {
		if (! isLooped) {
			if (! IsLast()) {
				current ++; 
			}
		}
		else {
			current = (current + 1) % waypoints.Count;
		}
	}
	
	
	public void OnDrawGizmos() {
		PopulatePath();
		Gizmos.color = Color.cyan;
		for(int i = 1 ; i < waypoints.Count ; i ++) {
			Gizmos.DrawLine(waypoints[i - 1], waypoints[i]);
			Gizmos.DrawSphere(waypoints[i-1], 0.25f);
			Gizmos.DrawSphere(waypoints[i], 0.25f);
		}

		if (isLooped) {
			Gizmos.DrawLine(waypoints[waypoints.Count - 1], waypoints[0]);

		}
	}
}
