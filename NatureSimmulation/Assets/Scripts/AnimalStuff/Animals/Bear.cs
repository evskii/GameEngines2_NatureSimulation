using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal
{
	public void Start() {
		hunger = 30;
		thirst = Random.Range(75, 100);
		decayDelay = Random.Range(2f, 3f);
		
		hungerDecayMulti = Random.Range(0.9f, 1.1f);
		thirstDecayMulti = Random.Range(0.9f, 1.1f);

		maxSpeed = 9;

		attackStrength = 35f;
		
		Init();
	}
}
