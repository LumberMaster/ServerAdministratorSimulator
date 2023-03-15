using System.Collections.Generic;
using UnityEngine;
using ServerAdministratorSimulator.Base;

namespace ServerAdministratorSimulator.Service
{
	/// <summary>
	///  ласс управл€ющий всеми лампами
	/// </summary>
	public class BulbManager : MonoBehaviour
	{
		[SerializeField] private List<Bulb> bulbs;

		private void Awake()
		{
			bulbs = new List<Bulb>(FindObjectsOfType<Bulb>());
		}
		public void ToDisableAllBulb()
		{
			foreach (Bulb bulb in bulbs)
			{
				bulb.ToDisable();
			}
		}
		public void ToEnableAllBulb()
		{
			foreach (Bulb bulb in bulbs)
			{
				bulb.ToEnable();
			}
		}
		public void ToSwitchAllBulb()
		{
			foreach (Bulb bulb in bulbs)
			{
				bulb.ToSwitch();
			}
		}

	}
}