using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_FollowLeader : BehaviourState
{

	public override void Enter() {
		
	}
	public override void Think() {
		
	}
	public override void Exit() {
		GetComponent<Behaviour_LookingForGroup>().groupToJoin.Leave(animal);
		GetComponent<OffsetPursue>().enabled = false;
	}
}
