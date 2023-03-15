using ServerAdministratorSimulator.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using static ServerAdministratorSimulator.Player.PlayerController;

namespace ServerAdministratorSimulator.Interactable
{
	/// <summary>
	/// �������, ������� ����� ����������������� � ����
	/// </summary>
	public class InteractOnHand : GrabbableObject
	{
		[Header("InteractInHand")]
		[SerializeField] public KeyCode interactKey = KeyCode.Mouse1;
		[SerializeField] public UnityEvent OnInteractInHand = new UnityEvent();
		[SerializeField] public string descriptionInHand;
		public override void Interact()
		{
			base.Interact();
			
			PlayerGUI.Instance.UpdateObjectInHandHelperText(descriptionInHand + "\n[Q] ��������� �� ����");
		}
	}
}