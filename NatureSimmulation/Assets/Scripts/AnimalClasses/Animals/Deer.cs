using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Deer : Prey
{
	/// <summary>
	/// To-DO
	/// - Set up stats that are specific to the deer
	/// - Have a system that looks out for predators and changes states to avoid
	/// </summary>
	
	
	private void OnEnable() {
		ChangeState(GetComponent<PreyStateGoToPos>());
	}

}
