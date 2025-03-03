using System;
using UnityEngine;

using Random = UnityEngine.Random;

public class SpontaneousChanceBasedDestroyer : MonoBehaviour
{
	[SerializeField, Range(0, 100)] private int destructionRate = 10;

	private float timeElapsed;

	public static event Action<GameObject> OnObjectDestroyed;

	private void Update()
	{
		timeElapsed += Time.deltaTime;
		if (timeElapsed > 0.2f)
		{
			timeElapsed = 0f;

			if (Random.Range(0, 100) < destructionRate)
			{
				OnObjectDestroyed?.Invoke(this.gameObject);
				Destroy(this.gameObject);
			}
		}
	}

	public int GetDestructionRate() { return destructionRate; }
	public void SetDestructionRate(int rate) { destructionRate = rate; }
}
