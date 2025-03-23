using UnityEngine;

public class M2_ChanceBasedSpawner : MonoBehaviour
{
	[Tooltip("Optional: Half-extents of the plane along X and Z axis to constain Spawn Area.")]
	[SerializeField] private Vector2 spawnAreaBounds = new Vector2(50f, 50f);
	[SerializeField] private bool debugMode = false;

	public void Spawn(GameObject spawnObject)
	{
		Vector3 spawnPos = GetRandomSpawnPosition();
		GameObject spawnedObject = Instantiate(spawnObject);
		spawnedObject.transform.position = spawnPos;
		spawnedObject.SetActive(true);
	}
	public void Spawn(GameObject spawnObject, Vector3 spawnPos)
	{
		GameObject spawnedObject = Instantiate(spawnObject);
		spawnedObject.transform.position = spawnPos;
		spawnedObject.SetActive(true);
	}
	private Vector3 GetRandomSpawnPosition()
	{
		float xPos = Random.Range(-spawnAreaBounds.x, spawnAreaBounds.x);
		float zPos = Random.Range(-spawnAreaBounds.y, spawnAreaBounds.y);
		return new Vector3(xPos, 0f, zPos);
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
}
