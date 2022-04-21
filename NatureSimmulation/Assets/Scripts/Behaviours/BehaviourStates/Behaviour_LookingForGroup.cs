using System;
using System.Collections;
using System.Collections.Generic;

using TreeEditor;

using UnityEngine;

public class Behaviour_LookingForGroup : BehaviourState
{

	public Group groupToJoin;
	
	//Find groups that are nearby, otherwise create my own
	public override void Enter() {
		var groupsInScene = FindObjectsOfType<Group>();
		Group closestGroup = null;
		foreach (var group in groupsInScene) {
			if (closestGroup != null) {
				var distToNew = Vector3.Distance(transform.position, group.transform.position);
				var distToOld = Vector3.Distance(transform.position, closestGroup.transform.position);
				if (distToNew < distToOld) {
					closestGroup = group;
				} 
                
			} else {
				closestGroup = group;
			}
		}
		if (closestGroup != null) {
			if (Vector3.Distance(transform.position, closestGroup.transform.position) <= animal.groupFindRange) {
				groupToJoin = closestGroup;
			} 
		}
		else {
			CreateNewGroup();
		}
		animal.move = true;
		animal.ToggleDecay(true);
		
	}

	private void CreateNewGroup() {
		var group = gameObject.AddComponent<Group>();
		group.groupLeader = animal;
		group.Enter(animal);
		animal.StateMachine(GetComponent<Behaviour_Wander>());
	}
	
	//If we have found a group we then head for that group
	public override void Think() {
		if (groupToJoin != null) {
			animal.currentTarget = groupToJoin.transform;

			if (Vector3.Distance(transform.position, groupToJoin.transform.position) < 10) {
				groupToJoin.Enter(animal);
				Debug.Log(groupToJoin.groupLeader);
				GetComponent<OffsetPursue>().Init(groupToJoin.groupLeader);	
				animal.StateMachine(GetComponent<Behaviour_FollowLeader>());
				
			}
		}
	}
	
	
	public override void Exit() {
		groupToJoin = null;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		if (animal != null) {
			Gizmos.DrawWireSphere(transform.position, animal.groupFindRange);
		}
		
	}
}
