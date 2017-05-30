using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
	public Transform instanceTransform;
	private Vector3 offset;

	void OnMouseDown()
	{

		offset = instanceTransform.position - Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y));
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

		Vector3 newPoint = Camera.main.WorldToViewportPoint(curPosition);
		if (newPoint.x < 0 || newPoint.x > 1)
		{
			curPosition.x = transform.position.x;
		}
		if (newPoint.y < 0 || newPoint.y > 1)
		{
			curPosition.y = transform.position.y;
		}

		instanceTransform.position = curPosition;
	}
}