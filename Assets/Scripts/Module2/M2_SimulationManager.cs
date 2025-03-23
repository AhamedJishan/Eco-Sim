using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_SimulationManager : MonoBehaviour
{
	public float cycleDuration = 0.2f;
	public float birthChance = 20.0f;
	public float replicationChance = 10.0f;
	public float deathChance = 10.0f;

	[SerializeField] private GameObject creature;
	[SerializeField] private M2_ChanceBasedSpawner spawner;

	[SerializeField]private int maxCount = 100;
	public int creatureCount;

	// Variables for calculating Average
	public float avgCreatureCount;
	private int windowSize = 20;
	private Queue<int> samples = new Queue<int>();
	private int runningSum = 0;


	private void Start()
	{
		creatureCount = 0;
		avgCreatureCount = 0;
		StartCoroutine(SimulationCycle());
		StartCoroutine(AverageSamplingRoutine());
	}

	private IEnumerator SimulationCycle()
	{
		while (true)
		{
			yield return new WaitForSeconds(cycleDuration);

			if (creatureCount <= maxCount)
			{
				M2_Creature.replicationChance = replicationChance;
				M2_Creature.deathChance = deathChance;
				M2_Creature.cycleDuration = cycleDuration;

				spawner.Spawn(creature);
				CreatureBorn();
			}
			else
			{
				M2_Creature.replicationChance = 0;
				M2_Creature.deathChance = deathChance;
				M2_Creature.cycleDuration = cycleDuration;
			}
		}
	}

	public void SpawnCreature(Vector3 spawnPos)
	{
		spawner.Spawn(creature, spawnPos);
	}

	private IEnumerator AverageSamplingRoutine()
	{
		while (true)
		{
			CalculateAverageCount();
			yield return new WaitForSeconds(1);
		}
	}

	private void CalculateAverageCount()
	{
		samples.Enqueue(creatureCount);
		runningSum += creatureCount;

		if (samples.Count > windowSize)
			runningSum -= samples.Dequeue();

		if (samples.Count > 0)
			avgCreatureCount = runningSum / (float)samples.Count;
	}

	public void CreatureBorn()
	{
		creatureCount++;
	}

	public void CreatureDied()
	{
		creatureCount--;
	}
}
