using ServerAdministratorSimulator.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using static ServerAdministratorSimulator.Player.PlayerController;

namespace ServerAdministratorSimulator.Interactable
{
	/// <summary>
	/// Объекты, который могут взаимодействовать в руке
	/// </summary>
	public class InteractOnHand : GrabbableObject
	{
		[Header("InteractInHand")]
		[SerializeField] public KeyCode interactKey = KeyCode.Mouse1;
		[SerializeField] public UnityEvent OnUseInHand = new UnityEvent();
		[SerializeField] public string descriptionInHand;
		public override void Interact()
		{
			base.Interact();
			PlayerGUI.Instance.UpdateObjectInHandHelperText(descriptionInHand + "\n[Q] Выбросить из руки");
		}

		public void Use() 
		{
			OnUseInHand.Invoke();

		}
	}
}