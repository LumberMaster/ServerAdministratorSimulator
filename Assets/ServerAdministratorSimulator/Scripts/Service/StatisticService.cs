using ServerAdministratorSimulator.Base;
using TMPro;
using UnityEngine;

namespace ServerAdministratorSimulator.Service
{
	/// <summary>
	/// Класс отвечающий за вывод статистики
	/// </summary>
	public class StatisticService : MonoBehaviour
	{
		[SerializeField] private Timer timer;
		[SerializeField] private TMP_Text outTime;

		[SerializeField] private TMP_Text outErrors;

		public void CollectStatistic()
		{
			outTime.text = timer.GetTimeByString();
			outErrors.text = ErrorChecker.CountErrors.ToString();

		}
	}
}
