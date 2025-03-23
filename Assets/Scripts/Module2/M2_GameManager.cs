using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_GameManager : MonoBehaviour
{
	[SerializeField] private int speedUp = 1;
	[SerializeField] private Vector3 vec;

	public int creatureCount;

	// Variables for calculating Average
	public float avgCreatureCount;
	private int windowSize = 20;
	private Queue<int> samples = new Queue<int>();
	private int runningSum = 0;

	private void Start()
	{
		StartCoroutine(AverageSamplingRoutine());
	}

	private void Update()
	{
		if (Time.timeScale != speedUp)
			Time.timeScale = speedUp;
	}

	private void OnEnable()
	{
		SpontaneousChanceBasedSpawner.OnObjectSpawned += HandleObjectSpawn;
		SpontaneousChanceBasedDestroyer.OnObjectDestroyed += HandleObjectDestroyed;
	}
	private void OnDisable()
	{
		SpontaneousChanceBasedSpawner.OnObjectSpawned -= HandleObjectSpawn;
		SpontaneousChanceBasedDestroyer.OnObjectDestroyed -= HandleObjectDestroyed;
	}

	private void HandleObjectSpawn(GameObject obj)
	{
		creatureCount++;
	}

	private void HandleObjectDestroyed(GameObject obj)
	{
		creatureCount--;
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

	public int GetTimeSpeedUp() { return speedUp; }
	public void SetTimeSpeedUp(int speedUp) { this.speedUp = speedUp; }
}
