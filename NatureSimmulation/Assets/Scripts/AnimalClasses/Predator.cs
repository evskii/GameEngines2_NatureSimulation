using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Predator : Animal
{
	private Animal prey;

	public abstract void Attack();

}
