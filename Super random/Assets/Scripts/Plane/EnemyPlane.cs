using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPlane : BasePlane
{
	// Setup with more specific stat
	public override void Setup()
	{
		// Inherite from BasePlane
		base.Setup();

		// Init tag of enemy plane
		gameObject.tag = "Enemy";
	}
}
