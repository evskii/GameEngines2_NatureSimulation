using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Packages.Rider.Editor.UnitTesting;

using UnityEngine;

using Random = UnityEngine.Random;


public class Deer : Animal
{
	public void Start() {
		hunger = Random.Range(75, 100);
		thirst = Random.Range(75, 100);
		decayDelay = Random.Range(2f, 3f);
		decayMulti = Random.Range(0.9f, 1.1f);
			
		Init();
	}
	
}
