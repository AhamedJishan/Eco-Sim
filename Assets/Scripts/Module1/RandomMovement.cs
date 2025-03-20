using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [Header("Movement Settings")]
	[SerializeField] private float speed = 10f;
	[SerializeField] private float angularSpeed = 2f;

    [SerializeField] private float directionChangeInterval = 1f;

	[Tooltip("Optional: Half-extents of the plane along X and Z axis to constain movement.")]
    [SerializeField] private Vector2 planeBounds = new Vector2(50f, 50f);
	[SerializeField] private bool debugMode = false;

    private Vector3 moveDirection;

	private void Start()
	{
		PickRandomDirection();
		StartCoroutine(ChangeDirectionRoutine());
	}

	private IEnumerator ChangeDirectionRoutine()
	{
		while (true)
		{
			PickRandomDirection();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}

	// Generate a random Normalized direction on XZ plane
	private void PickRandomDirection()
	{
		float randomAngle = Random.Range(0, 360f);
		float theta = randomAngle * Mathf.Deg2Rad;
		moveDirection = new Vector3(Mathf.Cos(theta), 0f, Mathf.Sin(theta)).normalized;
	}

	private void Update()
	{
		// Move towards moveDirection
		transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
		// Face toward moveDirection
		Quaternion targetRotation = Quaternion.LookRotation(moveDirection, transform.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);

		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, -planeBounds.x, planeBounds.x);
		pos.z = Mathf.Clamp(pos.z, -planeBounds.y, planeBounds.y);
		transform.position = pos;
	}

	private void OnDrawGizmos()
	{
		if (debugMode)
		{
			Gizmos.color = Color.green;
			Vector3 size = new Vector3(2 * planeBounds.x, 0f, 2 * planeBounds.y);
			Vector3 center = new Vector3(0, 0);
			Gizmos.DrawWireCube(center, size);
		}
	}
}
