using ServerAdministratorSimulator.Player;
using UnityEngine;

namespace ServerAdministratorSimulator.Items.Server
{
	/// <summary>
	/// ����� �������� ��� ��������� ������ 
	/// </summary>
	public class Server : MonoBehaviour
	{
		public void SetFixedLookPlayer(bool value) => PlayerController.Instance.SetFixedLook(value);
		public void SetMouseVisablePlayer(bool value) => PlayerController.Instance.SetMouseVisable(value);
	}

}