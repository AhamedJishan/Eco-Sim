using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[Header("UI References")]
	[SerializeField] private TMP_InputField timeMultiplierIF;
	[SerializeField] private TMP_InputField statBirthChanceIF;
	[SerializeField] private TMP_InputField statDeathChanceIF;
	[SerializeField] private TextMeshProUGUI statTotalText;
	[SerializeField] private TextMeshProUGUI statAverageText;

	[Header("Other References")]
	[SerializeField] private SpontaneousChanceBasedSpawner spawner;
	[SerializeField] private SpontaneousChanceBasedDestroyer destroyer;
	[SerializeField] private GameManager gameManager;

	[Header("Analysis References")]
	[SerializeField] private GameObject analysisPanel;
	[SerializeField] private TextMeshProUGUI analysisOutputText;

	private void Start()
	{
		timeMultiplierIF.text = gameManager.GetTimeSpeedUp().ToString();
		statBirthChanceIF.text = spawner.GetSpawnRate().ToString();
		statDeathChanceIF.text = destroyer.GetDestructionRate().ToString();
	}

	private void Update()
	{
		statTotalText.text = ": " + gameManager.creatureCount.ToString();
		statAverageText.text = ": " + gameManager.avgCreatureCount.ToString();

		int spawnRate = ValidateIntInput(statBirthChanceIF.text, 0, 100);
		statBirthChanceIF.text = spawnRate.ToString();
		spawner.SetSpawnRate(spawnRate);

		int deathRate = ValidateIntInput(statDeathChanceIF.text, 0, 100);
		statDeathChanceIF.text = deathRate.ToString();
		destroyer.SetDestructionRate(deathRate);

		int timeMultiplier = ValidateIntInput(timeMultiplierIF.text, 0, 100);
		timeMultiplierIF.text = timeMultiplier.ToString();
		gameManager.SetTimeSpeedUp(timeMultiplier);

		if (analysisPanel.active)
		{
			analysisOutputText.text = "= " + spawner.GetSpawnRate()/destroyer.GetDestructionRate();
		}
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
		analysisPanel.SetActive(!analysisPanel.active);
	}
}
