using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Gun {

	// Min and max moveable offset of gun 
	[System.Serializable]
	public class Offset
	{
		public float min; // Min offset
		public float max; // Max offset
	}

	public Transform player;					// Player's transform
	public BulletMode bulletMode;               // Mode of bullets, might be NORMAL or CHAOS
	public int bulletLevel;                     // Level of bullet
	public float gunSpeed;						// Speed of moving gun
	//public Offset verticalOffset;				// Vertical moving offset
	public Offset horizontalOffset;             // Horizontal moving offset
	public Vector3 startOffset;                 // Offset from start

	protected int verticalOperator = 1;         // In ApplyMovement, gun move vertically to [1 : positive] or [-1 : negative] 
	protected int horizontalOperator = 1;       // In ApplyMovement, gun move horizontally to [1 : positive] or [-1 : negative] 


	// Call once when create
	protected virtual void Start()
	{
		// Start with bullet at offset position
		transform.position += startOffset; 
	}


	// Independence from fps
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		ApplyMovement(Time.fixedDeltaTime);
	}


	// Apply movement of gun in fixedUpdate
	protected virtual void ApplyMovement(float time)
	{
		//// If gun's position.y is out of restrict 
		//if (transform.position.y > startOffset.y + verticalOffset.positive ||
		//	transform.position.y < startOffset.y + verticalOffset.negative)
		//{
		//	// Then change verticalOperator
		//	verticalOperator = -verticalOperator;
		//}

		//// Apply moving vertical
		//transform.position += verticalOperator * Vector3.up * gunSpeed * time;


		if (horizontalOffset.min < horizontalOffset.max)
		{
			// If gun's position.x is out of restrict 
			if (transform.position.x > HorizontalPositionMax().x)
			{
				// Then change horizontalOperator
				horizontalOperator = -horizontalOperator;

				// Set position to max horizontal, avoid over-value
				transform.position = HorizontalPositionMax();
			}
			else if (transform.position.x < HorizontalPositionMin().x)
			{
				// Then change horizontalOperator
				horizontalOperator = -horizontalOperator;

				// Set position to min horizontal, avoid over-value
				transform.position = HorizontalPositionMin();
			}

			// Apply moving horizontal
			transform.position += horizontalOperator * Vector3.right * gunSpeed * time;
		}
	}


	// Return gun position after apply startOffset
	protected virtual Vector3 OffsetPosition()
	{
		return player.position + startOffset;
	}


	// Return horizotal max position
	protected virtual Vector3 HorizontalPositionMax()
	{
		return new Vector3(OffsetPosition().x + horizontalOffset.max, OffsetPosition().y); 
	}


	// Return horizotal min position
	protected virtual Vector3 HorizontalPositionMin()
	{
		return new Vector3(OffsetPosition().x + horizontalOffset.min, OffsetPosition().y);
	}


	// Setup and activate bullet in Bullet pool
	protected override void EnableBullet(GameObject bullet)
	{
		// Inherite from Gun class
		base.EnableBullet(bullet);

		// Get PlayerBullet component from instance
		PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();

		// Pass bulletMode to bullet, NORMAL or CHAOS
		bulletScript.bulletMode = bulletMode;

		// Pass bulletSpeed to bullet
		bulletScript.bulletSpeed = bulletSpeed;

		// Pass bulletLevel to bullet, affect sprite and damage
		bulletScript.bulletLevel = bulletLevel;

		// Have bullet's position return to gun's position
		bulletScript.transform.position = transform.position;

		// Generate bullet
		bulletScript.Generate(sortingNumber++);

		//	If sorting number too high then reset sorting number 
		if (sortingNumber + 10 > Mathf.Infinity)
		{
			sortingNumber = 0;
		}

		// Enable bullet
		bulletScript.gameObject.SetActive(true);
	}
}
