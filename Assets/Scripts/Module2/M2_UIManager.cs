using TMPro;
using UnityEngine;

public class M2_UIManager : MonoBehaviour
{
	[SerializeField] private M2_SimulationManager simulationManager;

	[Header("Stats Panel")]
    [SerializeField] private TMP_InputField birthChanceIF;
	[SerializeField] private TMP_InputField replicationChanceIF;
	[SerializeField] private TMP_InputField deathChanceIF;
	[SerializeField] private TMP_Text totalCountText;
	[SerializeField] private TMP_Text avgCountText;

	[Header("Time Panel")]
	[SerializeField] private TMP_InputField timescaleIF;

	private void Start()
	{
		timescaleIF.text = 1.ToString();
		birthChanceIF.text = simulationManager.birthChance.ToString();
		replicationChanceIF.text = simulationManager.replicationChance.ToString();
		deathChanceIF.text = simulationManager.deathChance.ToString();
	}

	private void Update()
	{
		int timeScale = ValidateIntInput(timescaleIF.text, 0, 100);
		Time.timeScale = timeScale;
		timescaleIF.text = timeScale.ToString();

		totalCountText.text = simulationManager.creatureCount.ToString();
		avgCountText.text = simulationManager.avgCreatureCount.ToString();

		int birthChance = ValidateIntInput(birthChanceIF.text, 0, 100);
		simulationManager.birthChance = birthChance;
		birthChanceIF.text = birthChance.ToString();

		int deathChance = ValidateIntInput(deathChanceIF.text, 0, 100);
		simulationManager.deathChance = deathChance;
		deathChanceIF.text = deathChance.ToString();

		int replicationChance = ValidateIntInput(replicationChanceIF.text, 0, 100);
		simulationManager.replicationChance = replicationChance;
		replicationChanceIF.text = replicationChance.ToString();
	}

	private int ValidateIntInput(string input, int minValue, int maxValue)
	{
		int number = 0;
		if (int.TryParse(input, out number))
			number = Mathf.Clamp(number, minValue, maxValue);
		return number;
	}
}
