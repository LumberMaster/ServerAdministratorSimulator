using ServerAdministratorSimulator.Service;
using UnityEngine;

namespace ServerAdministratorSimulator.Items.Server
{
	/// <summary>
	/// ����� �������� ��� ���������� ����� 
	/// </summary>
	public class ServerBlock : MonoBehaviour
	{
		public void TakeMistake()
		{
			StagesManager.Instance.PlayTips(0);
			ErrorChecker.Add();
		}
	}
}