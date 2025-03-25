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

	[Header("Analysis Panel")]
	[SerializeField] private GameObject analysisPanel;
	[SerializeField] private TMP_Text analysisEquationText;
	[SerializeField] private TMP_Text analysisResultText;

	private void Start()
	{
		timescaleIF.text = 1.ToString();
		birthChanceIF.text = simulationManager.birthChance.ToString();
		replicationChanceIF.text = simulationManager.replicationChance.ToString();
		deathChanceIF.text = simulationManager.deathChance.ToString();
		AnalysisPanel((int)simulationManager.birthChance,
			(int)simulationManager.deathChance, (int)simulationManager.replicationChance);
	}

	private void Update()
	{
		// TIME
		int timeScale = ValidateIntInput(timescaleIF.text, 0, 100);
		Time.timeScale = timeScale;
		timescaleIF.text = timeScale.ToString();

		totalCountText.text = simulationManager.creatureCount.ToString();
		avgCountText.text = simulationManager.avgCreatureCount.ToString();

		// STATS
		int birthChance = ValidateIntInput(birthChanceIF.text, 0, 100);
		simulationManager.birthChance = birthChance;
		birthChanceIF.text = birthChance.ToString();

		int deathChance = ValidateIntInput(deathChanceIF.text, 0, 100);
		simulationManager.deathChance = deathChance;
		deathChanceIF.text = deathChance.ToString();

		int replicationChance = ValidateIntInput(replicationChanceIF.text, 0, 100);
		simulationManager.replicationChance = replicationChance;
		replicationChanceIF.text = replicationChance.ToString();

		// ANALYSIS
		AnalysisPanel(birthChance, deathChance, replicationChance);
	}

	private void AnalysisPanel(int birthChance, int deathChance, int replicationChance)
	{
		analysisEquationText.text = "   =" + birthChance.ToString()
			+ "/(" + deathChance.ToString() + "-" + replicationChance.ToString() + ")";

		float analysedResultDenominator = deathChance - replicationChance;
		if (analysedResultDenominator == 0)
			analysisResultText.text = "Inf.";
		else
			analysisResultText.text = "=" + (birthChance / analysedResultDenominator).ToString();
	}

	private int ValidateIntInput(string input, int minValue, int maxValue)
	{
		int number = 0;
		if (int.TryParse(input, out number))
			number = Mathf.Clamp(number, minValue, maxValue);
		return number;
	}

	public void OnAnalysisButtonPressed()
	{
		analysisPanel.active = !analysisPanel.active;
	}
}
