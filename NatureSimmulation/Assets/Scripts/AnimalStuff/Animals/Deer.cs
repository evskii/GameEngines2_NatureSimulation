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
		animalSize = 20;
		lonelinessDecayMulti = Random.Range(1f, 1.75f);
		currentLoneliness = Random.Range(100, 200);
		hungerDecayMulti = Random.Range(0.9f, 1.1f);
		thirstDecayMulti = Random.Range(0.9f, 1.1f);
			
		Init();
	}
	
}
