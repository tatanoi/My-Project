using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

	public Transform instanceTransform;
	private Vector3 offset;

	void OnMouseDown()
	{

		offset = instanceTransform.position - Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y));
	}

	void OnMouseDrag()
	{
		Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
		instanceTransform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
	}
}
