using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlane : MonoBehaviour {

	public float maxHP;				// Max health point of plane
	protected float currentHP;		// Current health point


	// Called one time only
	public virtual void Start()
	{
		// Setup all stats of plane
		Setup();
	}


	// Setup when start or enable plane
	public virtual void Setup()
	{
		// Set full HP
		currentHP = maxHP;
	}


	// When plane is hit by bullets, meteor or something else
	public virtual void OnHit(float damage)
	{
		// Create on hit effect
		// FUNCTION HERE <----------------------

		// Current HP is reduced by damage 
		currentHP -= damage;

		// If run out of HP
		if (currentHP <= 0)
		{
			// Then plane dies
			OnDie();
		}
	}


	// When HP is recovered
	public virtual void OnAddHP(float addedHP)
	{
		// Current HP is added by addedHP
		currentHP += addedHP;

		// If currentHP is over maxHP
		if (currentHP > maxHP)
		{
			// Then set it to max HP
			currentHP = maxHP;
		}
	}


	// When plane die
	public virtual void OnDie()
	{
		// Create die effect
		// FUNCTION HERE <----------------------------

		// Disable or Destroy gameObject. In this case, destroy plane
		Destroy(gameObject);
	}
}
