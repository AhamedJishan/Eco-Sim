using System;
using UnityEngine;

using Random = UnityEngine.Random;

public class SpontaneousChanceBasedSpawner : MonoBehaviour
{
	[Tooltip("Gameobject to spawn")]
	[SerializeField] private GameObject spawnObject;

    [SerializeField, Range(0,100)] private float spawnRate = 10f;

	[Tooltip("Optional: Half-extents of the plane along X and Z axis to constain Spawn Area.")]
	[SerializeField] private Vector2 spawnAreaBounds = new Vector2(50f, 50f);
	[SerializeField] private bool debugMode = false;


	public static event Action<GameObject> OnObjectSpawned;

	private float timeElapsed;

	private void Update()
	{
		timeElapsed += Time.deltaTime;
        if (timeElapsed > 0.2f)
		{
			timeElapsed = 0f;

			if (Random.Range(0, 100) < spawnRate)
				Spawn();
		}
	}

	private void Spawn()
	{
		float xPos = Random.Range(-spawnAreaBounds.x, spawnAreaBounds.x);
		float zPos = Random.Range(-spawnAreaBounds.y, spawnAreaBounds.y);
		Vector3 spawnPos = new Vector3(xPos, 0f, zPos);
		GameObject spawnedObject = Instantiate(spawnObject);
		spawnedObject.transform.position = spawnPos;
		spawnedObject.SetActive(true);

		OnObjectSpawned?.Invoke(spawnedObject);
	}

	private void OnDrawGizmos()
	{
		if (debugMode)
		{
			Gizmos.color = Color.green;
			Vector3 size = new Vector3(2 * spawnAreaBounds.x, 0f, 2 * spawnAreaBounds.y);
			Vector3 center = new Vector3(0, 0);
			Gizmos.DrawWireCube(center, size);
		}
	}

	public float GetSpawnRate() { return spawnRate; }
	public void SetSpawnRate(int rate) { spawnRate =  rate; }
}
