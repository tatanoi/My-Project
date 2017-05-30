using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/********************************************    OTHER SETUP   **************************************************/

// Player's bullets have two modes: [NORMAL] and [CHAOS]
// When in chaos mode, sprite change, damage increase
public enum BulletMode
{
	NORMAL,
	CHAOS
}


// Class include normal and chaos sprite of bullet, serializable to make this appear in inspector
[System.Serializable]
public class BulletSprite
{
	public Sprite normalSprite;      // Normal-mode's sprites of bullet
	public Sprite chaosSprite;       // Chaos-mode's sprites of bullet
}


public class PlayerBullet : BaseBullet {

	
	/********************************************      VARIABLE     **************************************************/

	public BulletMode bulletMode;               // Current mode of bullet
	public int bulletLevel;						// Level of bullet
	public List<BulletSprite> bulletSprites;    // Sprites of bullet
	protected SpriteRenderer spriteRenderer;    // Component of bullet, used to change sprite


	/********************************************      FUNCTION     **************************************************/

	// Using OnEnable because of pool
	protected virtual void OnEnable()
	{

	}

	// First time init
	protected virtual void Start()
	{
		// Init sprite
		Initialize();
	}


	// Call when this bullet is started, one time only
	protected virtual void Initialize()
	{
		// Generate with sorting order
		Generate(0);
	}


	// Generate bullet with available stat
	public virtual void Generate(int sortingOrder)
	{
		// Generate sprite
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = GetSprite(bulletLevel, bulletMode);
		spriteRenderer.sortingOrder = sortingOrder; 
		// Generate damage
		// FUNCTION HERE <-------------------------------
	}


	// Get sprite from bulletSprites using index = [No. in bulletSprite] | bulletMode = [mode of bullet]
	protected virtual Sprite GetSprite(int index, BulletMode bulletMode)
	{
		// If bulletSprites does not have any sprites
		if (bulletSprites.Count < 1)
		{
			// Then return null, make bullet invisible
			return null;
		}

		// If index is valid
		if (index > -1 && index < bulletSprites.Count)
		{
			// If bulletMode is NORMAL
			if (bulletMode == BulletMode.NORMAL)
			{
				// Then get normal-sprite in bulletSprites at index-position
				return bulletSprites[index].normalSprite;
			}
			// If bulletMode is CHAOS
			else
			{
				// Then get chaos-sprite in bulletSprites at index-position
				return bulletSprites[index].chaosSprite;
			}
		}
		// If index is invalid
		else
		{
			return bulletSprites[0].normalSprite;
		}
	}


	// If collide with enemy 
	protected override void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision with border
		base.OnTriggerEnter2D(col);

		// Detect collision with enemy
		if (col.CompareTag("Enemy"))
		{
			// Reduce Enemy hp with bulletDamage
			col.GetComponent<EnemyPlane>().OnHit(bulletDamage);

			// Disable bullet
			gameObject.SetActive(false); 
		}
	}
}
