using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Drinking : BehaviourState
{
	/*		This state is used if the animal is consuming food
	 *		or drinking water. It pauses all stat analysis and
	 *		decay.
	 */
	
	//TO DO:
	// Set up system to say if drinking or eating
	// Set it up to transition out of state when reached full stat for whatever thing you are doing
	
	public override void Enter() {
		animal.ToggleDecay(false);
		animal.move = false;
	}
	
	public override void Think() {
		//Check hunger to see if we hit our max and then decide to move
		if (animal.thirst >= Random.Range(90, 100)) {
			animal.StateMachine(GetComponent<Behaviour_Wander>());
		}
	}
	
	public override void Exit() {
		animal.ToggleDecay(false);
		animal.move = true;
	}
}
