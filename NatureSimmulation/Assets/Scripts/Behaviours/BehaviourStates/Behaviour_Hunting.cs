using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Random = UnityEngine.Random;


public class Behaviour_Hunting : BehaviourState
{
	//Todo
	// - Persue an animal if you want to eat them
	// - Up the max speed
	// - Lower stamina if running at max
	// - Return to standard speed if stamina is low
	// - When in range -> BITE animal
	// - Keep chasing until dead or out of stamina


	public float currentStamina = 100;
	public float maxStamina = 100; // 0 -> 100
	public float depletionRate = 0.05f;
	public bool depleted = false;
	
	public float preservedMaxSpeed;
	public float newMaxSpeed;

	public Animal currentTarget;

	public float attackRange = 1.5f;

	public float attackRegen = 1f;
	private float lastAttack = 0f;
	
	public override void Enter() {
		Debug.Log("LET THE HUNT BEGIN");
		//Stamina shit
		newMaxSpeed = animal.maxSpeed * 1.75f;
		// Debug.Log(newMaxSpeed);
		currentStamina = maxStamina;
		preservedMaxSpeed = animal.maxSpeed;
		animal.maxSpeed = newMaxSpeed;
		
		//Assigns our target to be the closest animal to us
		var allAnimals = FindObjectsOfType<Animal>().Where(animal1 => animal1.animalSize < animal.animalSize).ToList();
		Animal closestAnimal = null;
		foreach (var animal in allAnimals) {
			if (animal == GetComponent<Animal>()) {
				continue;
			}
			if (closestAnimal != null) {
				var distToNew = Vector3.Distance(transform.position, animal.transform.position);
				var distToOld = Vector3.Distance(transform.position, closestAnimal.transform.position);
				if (distToNew < distToOld) {
					closestAnimal = animal;
				} 
                
			} else {
				closestAnimal = animal;
			}
		}
		currentTarget = closestAnimal;
		animal.currentTarget = currentTarget.transform;
	}
	
	public override void Think() {
		if (currentStamina <= 0) { //If we are out of stamina make it small speed
			animal.maxSpeed = preservedMaxSpeed * 0.5f;
			depleted = true;
		} else {
			animal.maxSpeed = newMaxSpeed;
		}

		if (depleted) { //Regen stamina while walking
			currentStamina += depletionRate * Time.deltaTime;
			currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
			if(currentStamina >= Random.Range(0.9f, 1f) * maxStamina) {
				depleted = false;
			}
		} else { //Deplete
			currentStamina -= depletionRate * Time.deltaTime;
			currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
		}
		
		//Check distance and attack
		if (Vector3.Distance(transform.position, currentTarget.transform.position) <= attackRange && Time.time > lastAttack + attackRegen) {
			Debug.Log("ATTACK");
			currentTarget.TakeDamage(animal.attackStrength);
			animal.velocity = Vector3.zero;
			lastAttack = Time.time;
		}
		
		if (currentTarget.TryGetComponent(out Food possibleFood)) {
			possibleFood.Entering(animal);
			animal.StateMachine(GetComponent<Behaviour_Consuming>());
		}
	}
	
	public override void Exit() {
		animal.maxSpeed = preservedMaxSpeed;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		if (currentTarget != null) {
			Gizmos.DrawLine(transform.position, currentTarget.transform.position);
		}
	}
}
