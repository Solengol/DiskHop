using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskProperties : MonoBehaviour
{
	// Configuration Parameters
	[Header("Horizontal Movement")]
	[SerializeField] private bool isMovingHorizontally = default;
	[SerializeField] private float horizontalMovementSpeed = default;
	[SerializeField] private float horizontalMovementDistance = default;
	[SerializeField] private bool startLeft = default;
	[Header("Scale Up & Down")]
	[SerializeField] private bool isScaling = default;
	[SerializeField] private float scaleFactor = default;
	[SerializeField] private float scaleSpeed = default;

	// State Variables
	private Vector2 startPosition;
	private Vector2 newPosition;
	private float startScale;
	private float newScale;

	void Start()
	{
		startPosition = transform.position;
		newPosition = transform.position;
		startScale = transform.localScale.x;
		newScale = transform.localScale.x;
	}

	void FixedUpdate()
	{
		if (isMovingHorizontally)
		{
			MoveHorizontally();
		}
		if (isScaling)
		{
			Scaling();
		}
	}

	void MoveHorizontally()
	{
		if (startLeft)
		{
			newPosition.x = startPosition.x - (horizontalMovementDistance * Mathf.Sin(Time.time * horizontalMovementSpeed));
		}
		else
		{
			newPosition.x = startPosition.x + (horizontalMovementDistance * Mathf.Sin(Time.time * horizontalMovementSpeed));
		}
		transform.position = newPosition;
	}

	void Scaling()
	{
		newScale = Mathf.PingPong(Time.time * scaleSpeed, scaleFactor - (startScale * scaleFactor)) + startScale;
		transform.localScale = new Vector3(newScale, newScale, transform.localScale.z);
	}
}