using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Idle : BehaviourState
{
	public override void Enter() {
		animal.currentTarget = null;
		animal.move = false;
		animal.ToggleDecay(false);
	}
	public override void Think() {
		
	}
	
	public override void Exit() {
		if (animal != null) {
			animal.move = true;
		}
		animal.ToggleDecay(true);
		
	}
}
