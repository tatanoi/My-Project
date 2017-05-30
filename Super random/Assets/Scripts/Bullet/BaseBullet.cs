using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Might be inherites by bullets from player and enemy
public class BaseBullet : MonoBehaviour {

	public float bulletSpeed = 2;           // Bullet will fly with this speed
	public float bulletDamage = 50;			// Damage of bullet, reduce hp of oponent (enemy or player)
	

	// Use fixed update to make bullet movement more accurate (independence from fps)
	protected virtual void FixedUpdate()
	{
		// Bullet fly up with bulletSpeed
		transform.position += Vector3.up * bulletSpeed * Time.fixedDeltaTime; 
	} 

	// Using boxCollider2D to detect collision
	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		// If bullet collide with border
		if (col.CompareTag("Border"))
		{
			// Then disable bullet
			gameObject.SetActive(false);
		}
	}
}
