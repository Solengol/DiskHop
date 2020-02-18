using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskProperties : MonoBehaviour
{
	// Configuration Parameters
	[Header("Horizontal Movement")]
	[SerializeField] private bool isMovingHorizontally;
	[SerializeField] private float horizontalMovementSpeed;
	[SerializeField] private float horizontalMovementDistance;
	[Header("Scale Up & Down")]
	[SerializeField] private bool isScaling;
	[SerializeField] private float scaleFactor = 0.3f;
	[SerializeField] private float scaleSpeed = 1f;

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

	void Update()
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
		newPosition.x = startPosition.x + (horizontalMovementDistance * Mathf.Sin(Time.time * horizontalMovementSpeed));
		transform.position = newPosition;
	}

	void Scaling()
	{
		newScale = Mathf.PingPong(Time.time * scaleSpeed, scaleFactor - (startScale * scaleFactor)) + startScale;
		transform.localScale = new Vector3(newScale, newScale, transform.localScale.z);
	}
}