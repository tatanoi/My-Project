using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control shooting bullets
public class Gun : MonoBehaviour {

	public GameObject bulletPrefab;             // Type of bullet will be shooted
	public float bulletSpeed;                   // Speed of bullet
	public float spawnBulletTime;				// Time between spawning-bullet
	public int bulletPoolAmount;                // Number of bullets will be created in pool
	protected List<GameObject> bulletPool;      // Contains all bullets might be used
	protected float passedSpawnTime = 0f;       // Time passed since last spawn bullet
	protected int sortingNumber = 0;			// Sorting number in layer

	// Call once, create bulletPool
	protected virtual void Awake()
	{
		// Create bullet pool when awake, call once
		CreateBulletPool();
	}

	// FixedUpdate make process independence from fps
	protected virtual void FixedUpdate()
	{
		Shoot(Time.fixedDeltaTime);
	}


	// Shoot bullet
	protected virtual void Shoot(float time)
	{
		passedSpawnTime += time;
		if (passedSpawnTime >= spawnBulletTime)
		{
			SpawnBullet();
			passedSpawnTime = 0f;
		}
	}


	// Shoot bullet, actually make bullet active
	protected virtual void SpawnBullet()
	{
		// Run along bulletPool list
		for (int i = 0; i < bulletPool.Count; i++)
		{
			// If there is a bullet inactive
			if (!bulletPool[i].activeInHierarchy)
			{
				// Make that bullet active
				EnableBullet(bulletPool[i]);

				// Finish shooting, break loop
				break;
			}
		}
	}


	// Enable bullet, override by PlayerGun and EnemyGun
	protected virtual void EnableBullet(GameObject bullet)
	{
	}


	// Create a pool to reuse bullet, also improve fps 
	protected virtual void CreateBulletPool()
	{
		// Init bulletPool
		bulletPool = new List<GameObject>();

		// Find Bullet Pool in Hierarchy
		GameObject poolObject = GameObject.Find("Bullet Pool");

		// Create and add bulletPoolAmount bullets to bulletPool
		for (int i = 0; i < bulletPoolAmount; i++)
		{
			// Instantiate bulletPrefab with this-gun's transform 
			GameObject bullet = Instantiate(bulletPrefab, poolObject.transform);

			// Set inactive
			bullet.gameObject.SetActive(false);

			// Add bullet to bulletPool
			bulletPool.Add(bullet);
		}
	}
}
