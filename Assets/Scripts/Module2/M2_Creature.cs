using System.Collections;
using UnityEngine;

public class M2_Creature : MonoBehaviour
{
	public static float replicationChance = 10f;
	public static float deathChance = 10.0f;

	public static float cycleDuration = 0.2f;

	[SerializeField] private M2_SimulationManager simulationManager;

	private void Start()
	{
		StartCoroutine(CreatureRoutine());
	}

	private IEnumerator CreatureRoutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(cycleDuration);

			TryToDie();
			TryToReplicate();
		}
	}

	private void TryToDie()
	{
		if (Random.Range(1f, 100f) <= deathChance)
		{
			simulationManager.CreatureDied();
			Destroy(gameObject);
		}
	}

	private void TryToReplicate()
	{
		if (Random.Range(1f, 100f) <= replicationChance)
		{
			simulationManager.CreatureBorn();
			Replicate();
		}
	}

	private void Replicate()
	{
		// Instantiate a new creature near the current creature
		Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f)); // Adjust spawn area as needed
		simulationManager.SpawnCreature(spawnPos);
	}
}
