using UnityEngine;
using UnityEngine.Events;

namespace ServerAdministratorSimulator.Base
{
	/// <summary>
	/// Класс переключателя
	/// </summary>
	public class Switcher : AbstractSwitcher
	{

		public UnityEvent OnEnable = new UnityEvent();
		public UnityEvent OnDisable = new UnityEvent();

		public override void ToEnable()
		{
			base.ToEnable();
			OnEnable.Invoke();
		}

		public override void ToDisable()
		{
			base.ToDisable();
			OnDisable.Invoke();
		}
	}
}